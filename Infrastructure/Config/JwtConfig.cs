//namespace Infrastructure.Config
//{
//    public interface IJwtConfig
//    {
//        string Secret { get; }
//        string Issuer { get; }
//        string Audience { get; }
//    }

//    public class JwtConfig : IJwtConfig
//    {
//        public string Secret { get; }
//        public string Issuer { get; }
//        public string Audience { get; }

//        public JwtConfig()
//        {
//            Secret = GetRequired("JWT_SECRET");
//            Issuer = GetRequired("JWT_ISSUER");
//            Audience = GetRequired("JWT_AUDIENCE");
//        }

//        private static string GetRequired(string name)
//        {
//            var value = Environment.GetEnvironmentVariable(name);
//            if (string.IsNullOrWhiteSpace(value))
//                throw new InvalidOperationException($"Переменная окружения {name} не установлена");
//            return value;
//        }
//    }
//}
