using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserEntities
{
    [Table("Addresses", Schema = "Identity")]
    public class Address : BaseEntity
    {
        [Required]
        public required string Street { get; set; }

        [Required]
        public required string City { get; set; }

        [Required]
        public required string Country { get; set; }

        public required string PostalCode { get; set; }
        public bool IsDefault { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
