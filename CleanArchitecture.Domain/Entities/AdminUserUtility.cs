using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Entities
{
    public class AdminUserUtility
    {
        [Key]
        public int AdminUserUtilityID { get; set; }
        public Guid UserId { get; set; }
        public int UtilityId { get; set; }
        public AdminUser AdminUser { get; set; }
    }
}
