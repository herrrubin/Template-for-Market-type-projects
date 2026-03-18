
//namespace Infrastructure.Config
//{
//    public interface ISecurityConfig
//    {
//        string ServerSalt { get; }
//        int AccessTokenExpiredMinutes { get; }
//        int RefreshTokenExpiredMinutes { get; }
//    }

//    public class SecurityConfig : ISecurityConfig
//    {
//        public string ServerSalt { get; }
//        public int AccessTokenExpiredMinutes { get; }
//        public int RefreshTokenExpiredMinutes { get; }

//        public SecurityConfig()
//        {
//            ServerSalt = GetRequired("SERVER_SALT");
//            AccessTokenExpiredMinutes = GetRequiredInt("ACCESS_TOKEN_EXPIRED");
//            RefreshTokenExpiredMinutes = GetRequiredInt("REFRESH_TOKEN_EXPIRED");
//        }

//        private static string GetRequired(string name)
//        {
//            var value = Environment.GetEnvironmentVariable(name);
//            if (string.IsNullOrWhiteSpace(value))
//                throw new InvalidOperationException($"Переменная окружения {name} не установлена");
//            return value;
//        }

//        private static int GetRequiredInt(string name)
//        {
//            var value = GetRequired(name);
//            if (!int.TryParse(value, out var result))
//                throw new InvalidOperationException(
//                    $"Переменная {name} должна быть целым числом, сейчас: '{value}'"
//                );
//            return result;
//        }
//    }
//}
