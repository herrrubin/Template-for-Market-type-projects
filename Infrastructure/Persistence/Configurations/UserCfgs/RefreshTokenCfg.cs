using Domain.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations.UserCfgs
{
    public class RefreshTokenCfg : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne(t => t.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(t => t.Token).IsUnique();

            builder.HasIndex(t => t.UserId);
            builder.HasIndex(t => t.ExpiredAt);
            builder.HasIndex(t => t.IsValid);

            builder.Property(t => t.IsValid)
                .HasDefaultValue(true);
        }
    }
}
