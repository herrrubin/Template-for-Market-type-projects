using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Domain.Entities.ProductEntites
{
    [Table("ProductAttributes", Schema = "Catalog")]
    public class ProductAttribute : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public required string Key { get; set; }

        [Required]
        [MaxLength(500)]
        public required string Value { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
    }
}
