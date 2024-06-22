using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.Server.APIs.DTOS
{
    public class AccountUpdateDTO
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }
        public bool IsMale { get; set; }
    }
}
