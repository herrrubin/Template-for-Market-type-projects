using Domain.Entities.ProductEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.ProductCfgs
{
    public class ProductCfg : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(p => p.Buyer)
            //    .WithMany(u => u.Products)
            //    .HasForeignKey(p => p.BuyerId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.StockQuantity)
                .HasDefaultValue(0);

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder.Property(p => p.AverageRating)
                .HasDefaultValue(0.0);

            builder.Property(p => p.ReviewCount)
                .HasDefaultValue(0);

            builder.HasIndex(p => p.CategoryId);
            //builder.HasIndex(p => p.BuyerId);
            builder.HasIndex(p => p.IsActive);
            builder.HasIndex(p => p.AverageRating);
        }
    }
}
