using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Entities
{
    public class AdminUserRole
    {
        [Key]
        public int AdminUserRoleID { get; set; }

        public Guid UserId { get; set; }

        public int RoleId { get; set; }

        public AdminUser AdminUser { get; set; }
        public AdminRole AdminRole { get; set; }

    }
}
