using DotNetEnv;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Config
{
    public interface IDatabaseConfig
    {
        string ConnectionString { get; }
        string ValidateParam(string param);
    }

    public class DatabaseConfig : IDatabaseConfig
    {
        private readonly ILogger<DatabaseConfig> _logger;
        private static readonly Dictionary<string, string> _defaultValues = new()
        {
            { "DB_HOST", "localhost" },
            { "DB_PORT", "5432" },
            { "DB_NAME", "postgres" },
            { "DB_USERNAME", "postgres" },
            { "DB_PASSWORD", "" },
        };

        public DatabaseConfig(ILogger<DatabaseConfig> logger)
        {
            _logger = logger;
            LoadEnvironmentVariables();
        }

        public string ConnectionString
        {
            get
            {
                var ENV_TYPE = Environment.GetEnvironmentVariable("ENV_TYPE");
                var host = ValidateParam("POSTGRES_HOST");
                if (ENV_TYPE == "dev")
                {
                    host = "postgres_dev";
                }
                var port = ValidateParam("POSTGRES_PORT");
                var database = ValidateParam("POSTGRES_DB");
                var username = ValidateParam("POSTGRES_USER");
                var password = ValidateParam("POSTGRES_PASSWORD");

                return $"Host={host};Port={port};Database={database};Username={username};Password={password};";
            }
        }

        public string ValidateParam(string param)
        {
            var value = Environment.GetEnvironmentVariable(param);
            if (string.IsNullOrEmpty(value))
            {
                if (_defaultValues.TryGetValue(param, out string defaultValue))
                {
                    _logger.LogWarning(
                        "Параметр '{Param}' не установлен, используется значение по умолчанию: {DefaultValue}",
                        param,
                        defaultValue
                    );
                    return defaultValue;
                }
                else
                {
                    _logger.LogError("Параметр '{Param}' не установлен в .env файле", param);
                    return "";
                }
            }
            _logger.LogDebug("Параметр '{Param}' успешно загружен", param);
            return value;
        }

        private void LoadEnvironmentVariables()
        {
            var currentDirectory = Directory.GetCurrentDirectory();
            var envFile = Path.Combine(currentDirectory, "..", "Infrastructure", ".env");
            var ENV_TYPE = Environment.GetEnvironmentVariable("ENV_TYPE");

            if (ENV_TYPE == "dev")
            {
                _logger.LogWarning(
                    "Выбран ENV_TYPE=dev. Используются переменные окружения из dev.env файла."
                );
            }
            else if (File.Exists(envFile))
            {
                Env.Load(envFile);
                _logger.LogInformation("Загружен .env файл из: {EnvFile}", envFile);
            }
            else
            {
                _logger.LogWarning(
                    ".env файл не найден. Используются переменные окружения и значения по умолчанию."
                );
            }
        }
    }
}

