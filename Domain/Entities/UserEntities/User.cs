using Domain.Entities.OrderEntities;
using Domain.Entities.ProductEntites;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserEntities
{
    [Table("Users", Schema = "Identity")]
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public required string UserName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }
        public virtual Address? Address { get; set; }

        public UserRole Role { get; set; } = UserRole.Buyer;
        public string? imgPathAvatar { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } =
            new List<RefreshToken>();
        
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
    public enum UserRole
    {
        Buyer,
        Admin
    }
}
