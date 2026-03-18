using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.UserEntities
{
    [Table("RefreshTokens", Schema = "Identity")]
    public class RefreshToken : BaseEntity
    {
        public required string Token { get; set; }
        public DateTime ExpiredAt { get; set; }
        public bool IsValid { get; set; } = true;

        public int UserId { get; set; }
        public User User { get; set; }
    }
}