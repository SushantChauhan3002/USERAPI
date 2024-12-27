using System.ComponentModel.DataAnnotations;

namespace CleanArchitecture.Domain.Entities
{
    public class UserUtility
    {
        public readonly object AdminUserUtilities;

        [Key]
        public int UserUtilityID { get; set; }

        public string UtilityDetails { get; set; }
    }
}
