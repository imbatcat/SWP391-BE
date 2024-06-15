using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.Server.APIs.DTOS
{
    public class AccountDTO
    {
        public string FullName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public bool IsMale { get; set; }

        public int RoleId { get; set; }

        [Phone(ErrorMessage = "Invalid phone number")]
        public string PhoneNumber { get; set; }

        public DateOnly DateOfBirth { get; set; }

    }
}
