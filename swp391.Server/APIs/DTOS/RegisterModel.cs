using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.Server.APIs.DTOS
{
    public class RegisterModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string AccountFullname { get; set; }

        [Required]
        public string AccountId { get; set; }
    }
}
