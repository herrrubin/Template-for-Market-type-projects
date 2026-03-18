using Domain.Entities.OrderEntities;
using Domain.Entities.ProductEntites;
using Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        #region UserModel
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        #endregion

        #region ProductModels
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductAttribute> ProductAttributes { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        #endregion

        #region OrderModels
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplyTableSchemasFromAttributes(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        /// <summary>
        /// Автоматически применяет схемы из атрибутов [Table]
        /// </summary>
        private void ApplyTableSchemasFromAttributes(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableAttribute = entityType.ClrType
                    .GetCustomAttributes(typeof(TableAttribute), false)
                    .FirstOrDefault() as TableAttribute;

                if (tableAttribute != null && !string.IsNullOrEmpty(tableAttribute.Schema))
                {
                    entityType.SetSchema(tableAttribute.Schema);
                }
            }
        }
    }
}
