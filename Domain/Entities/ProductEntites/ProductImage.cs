using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.ProductEntites
{
    [Table("ProductImages", Schema = "Catalog")]
    public class ProductImage : BaseEntity
    {
        [Required]
        public required string ImageUrl { get; set; }

        public bool IsMain { get; set; }
        public int SortOrder { get; set; }

        [Required]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
    }
}
