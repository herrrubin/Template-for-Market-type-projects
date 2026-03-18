using Microsoft.AspNetCore.Http;

namespace Application.DTOs.ProductDTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }

    #region Дто для записи в бд
    public class ProductImageDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
    }
    #endregion

    #region Дто для загрузки одного фото в минио
    public class UploadProductImageDto
    {
        public int ProductId { get; set; }
        public string ProductScheme { get; set; }
        public IFormFile File { get; set; } = null!;
    }
    #endregion
    #region Дто для загрузки множества фото в минио
    public class UploadMultipleProductImagesDto
    {
        public int ProductId { get; set; }
        public string ProductScheme { get; set; }
        public List<IFormFile> Files { get; set; } = new List<IFormFile>();
    }
    #endregion

    public class CategoryDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }

    }
}
