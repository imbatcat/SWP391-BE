using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.Server.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(10)]
        public string RoleName { get; set; }
    }
}
