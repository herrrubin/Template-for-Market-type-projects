using Application.Interfaces.IProductInterface;
using Application.Interfaces.IUserInterface;
using Infrastructure.Config;
using Infrastructure.Persistence.Context;
using Infrastructure.Repositories.ProductRepository;
using Infrastructure.Repositories.UserRepository;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    public static class Extensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseConfig, DatabaseConfig>();
            services.AddSingleton(sp =>
            {
                var settings = sp.GetRequiredService<IOptions<MinioSettings>>().Value;
                return settings;
            });
            services.AddScoped<IMinioService, MinioService>();


            //services.AddScoped<ISecurityConfig, SecurityConfig>();
            //services.AddScoped<IJwtConfig, JwtConfig>();

            //services.AddAuthentication(options =>
            //    {
            //        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //    })
            //    .AddJwtBearer(options =>
            //    {
            //        options.RequireHttpsMetadata = false;
            //        options.SaveToken = true;
            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ValidateIssuerSigningKey = true,

            //            ValidIssuer = services
            //                .BuildServiceProvider()
            //                .GetRequiredService<IJwtConfig>()
            //                .Issuer,
            //            ValidAudience = services
            //                .BuildServiceProvider()
            //                .GetRequiredService<IJwtConfig>()
            //                .Audience,
            //            IssuerSigningKey = new SymmetricSecurityKey(
            //                Encoding.UTF8.GetBytes(
            //                    services
            //                        .BuildServiceProvider()
            //                        .GetRequiredService<IJwtConfig>()
            //                        .Secret
            //                )
            //            ),
            //            ClockSkew = TimeSpan.Zero,
            //        };
            //    });
            //services.AddAuthorization();

            services.AddDbContext<AppDbContext>(
                (serviceProvider, options) =>
                {
                    var dbConfig = serviceProvider.GetRequiredService<IDatabaseConfig>();
                    options.UseNpgsql(dbConfig.ConnectionString);
                }
            );


            #region UserService
            services.AddScoped<IUserRepository, UserRepository>();

            #endregion

            #region ProductService
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            #endregion

            return services;
        }
    }
}
