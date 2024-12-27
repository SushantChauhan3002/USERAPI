using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Entities
{
    public class AdminRole
    {
        [Key]
        public int RoleId { get; set; }
     
        public string RoleName { get; set; }

        public ICollection<AdminUserRole> AdminUserRoles { get; set; }
    }
}
