using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.Server.APIs.DTOS
{
    public class PasswordReqUser
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
