using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.ApiEndpoints;
using Minio.DataModel.Args;

namespace Infrastructure.Services
{
    public interface IMinioService
    {
        Task<string> UploadProductImageAsync(int ProductId, string ProductScheme, IFormFile File);
        Task<List<string>> UploadProductImagesAsync(int ProductId, string ProductScheme, List<IFormFile> Files);
    }
    public class MinioSettings
    {
        public string AccessKey { get; set; } = Environment.GetEnvironmentVariable("ACCESS_TOKEN_MINIO");
        public string SecretKey { get; set; } = Environment.GetEnvironmentVariable("SECRET_TOKEN_MINIO");
        public string Endpoint { get; set; } = Environment.GetEnvironmentVariable("ENDPOINT_MINIO");
        public string BucketName { get; set; } = Environment.GetEnvironmentVariable("BUCKET_NAME_MINIO");
        public bool UseSSL { get; set; } = false;
    }

    public class MinioService : IMinioService
    {
        private readonly IMinioClient _minioClient;
        private readonly MinioSettings _settings;
        private readonly ILogger<MinioSettings> _logger;

        public MinioService(MinioSettings settings, ILogger<MinioSettings> logger)
        {
            _settings = settings;
            _logger = logger;
            _minioClient = new MinioClient()
                .WithEndpoint(_settings.Endpoint)
                .WithCredentials(_settings.AccessKey, _settings.SecretKey)
                .WithSSL(_settings.UseSSL)
                .Build();

            InitializeBucketAsync().Wait();
        }
        #region Инициализация корзины
        private async Task InitializeBucketAsync()
        {
            try
            {
                var bucketName = _settings.BucketName.ToLowerInvariant();
                var found = await _minioClient.BucketExistsAsync(
                    new BucketExistsArgs().WithBucket(bucketName)
                    );

                if (!found) {
                    await _minioClient.MakeBucketAsync(
                        new MakeBucketArgs().WithBucket(bucketName)
                        );
                    _logger.LogInformation($"Created bucket: {bucketName}"); ;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to initialize bucket");
                throw; 
            }
        }
        #endregion

        #region Формирование ссылки
        private string GetStringLink(string arg1)
        {
            var protocol = _settings.UseSSL ? "https" : "http";
            string fileUrl = $"{protocol}://{_settings.Endpoint}/{_settings.BucketName}/{arg1}"; ;
            return fileUrl;
        }
        #endregion

        #region Приватный метод загрузки МНОЖЕСТА файлов по схеме
        private async Task<List<string>> UploadFileBySchemeList(string schema, int objectId, string? folderType, List<IFormFile> files)
        {
            var paths = new List<string>();

            foreach (var file in files)
            {
                string fileNames = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                string fullPaths = $"{schema}/{objectId}/{fileNames}";

                var link_one = GetStringLink(fullPaths);

                await using (var stream = file.OpenReadStream())
                {
                    await _minioClient.PutObjectAsync(
                        new PutObjectArgs()
                            .WithBucket(_settings.BucketName.ToLowerInvariant())
                            .WithObject(fullPaths)
                            .WithStreamData(stream)
                            .WithObjectSize(stream.Length)
                            .WithContentType(file.ContentType)
                    );
                }

                paths.Add(link_one);
            }
            return paths;
        }
        #endregion

        #region Приватный метод загрузки ОДНОГО файла по схеме
        private async Task<string> UploadFileBySchemeOne(string schema, int objectId, string? folderType, IFormFile file)
        {
            var paths = new List<string>();

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string fullPath = $"{schema}/{objectId}/{fileName}";

            var link_one = GetStringLink(fullPath);

            await using (var stream = file.OpenReadStream())
            {
                await _minioClient.PutObjectAsync(
                    new PutObjectArgs()
                    .WithBucket(_settings.BucketName.ToLowerInvariant())
                    .WithObject(fullPath)
                    .WithStreamData(stream)
                    .WithObjectSize(stream.Length)
                    .WithContentType(file.ContentType)
                );
            }
            return link_one;
        }
        #endregion

        private async Task DeleteFile(string objectPath)
        {
            await _minioClient.RemoveObjectAsync(
                new RemoveObjectArgs()
                    .WithBucket(_settings.BucketName)
                    .WithObject(objectPath)
            );

            _logger.LogInformation($"Файл удален: {objectPath}");
        }

        #region Публиныe метод загрузки
        public async Task<string> UploadProductImageAsync(int ProductId, string ProductScheme, IFormFile File)
        {
            var link = await UploadFileBySchemeOne(ProductScheme, ProductId, null, File);

            return link;
        }
        public async Task<List<string>> UploadProductImagesAsync(int ProductId, string ProductScheme, List<IFormFile> Files)
        {
            var links = await UploadFileBySchemeList(ProductScheme, ProductId, null, Files);
            return links;
        }
        #endregion
    }
}
