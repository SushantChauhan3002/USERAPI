
using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Entities
{
    public class AdminUser
    {
        [Key]
        public Guid UserId { get; set; }

        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public AdminUserUtility AdminUserUtilities { get; set; }
        public ICollection<AdminUserRole> AdminUserRoles { get; set; }
        public bool IsActive { get; set; }
    }
}
