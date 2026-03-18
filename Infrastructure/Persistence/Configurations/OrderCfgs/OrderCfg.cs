using Domain.Entities.OrderEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.OrderCfgs
{
    public class OrderCfg : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(o => o.ShippingAddress)
                .WithMany()
                .HasForeignKey("ShippingAddressId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(o => o.Status)
                .HasConversion<string>()
                .HasDefaultValue(OrderStatus.Pending);

            builder.Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            builder.HasIndex(o => o.OrderNumber)
                .IsUnique();

            builder.Property(o => o.OrderDate)
                .HasDefaultValueSql("NOW()");

            builder.HasIndex(o => o.UserId);
            builder.HasIndex(o => o.Status);
            builder.HasIndex(o => o.OrderDate);

            builder.HasIndex(o => new { o.UserId, o.OrderDate });
        }
    }
}
