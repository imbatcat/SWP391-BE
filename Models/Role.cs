using System.ComponentModel.DataAnnotations;

namespace PetHealthcareSystem.Models
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
