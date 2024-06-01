using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.Server.APIs.DTOS.Auth
{
    public class RegisterModel
    {
        public string Username { get; set; }

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
