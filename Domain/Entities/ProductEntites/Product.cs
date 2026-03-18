using Domain.Entities.OrderEntities;
using Domain.Entities.UserEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.ProductEntites
{
    [Table("Products", Schema = "Catalog")]
    public class Product : BaseEntity
    {
        [Required]
        [MaxLength(200)]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;

        //[Required]
        //public int BuyerId { get; set; }
        //public virtual User Buyer { get; set; } = null!;

        public int StockQuantity { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public double AverageRating { get; set; } = 0;
        public int ReviewCount { get; set; } = 0;

        public virtual ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();

        public virtual ICollection<ProductAttribute> Attributes { get; set; } = new List<ProductAttribute>();

        public virtual ICollection<Favorite> FavoritedBy { get; set; } = new List<Favorite>();

        public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
