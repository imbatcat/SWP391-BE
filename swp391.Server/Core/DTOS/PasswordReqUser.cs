using System.ComponentModel.DataAnnotations;

namespace PetHealthcare.Server.Core.DTOS
{
    public class PasswordReqUser
    {
        [EmailAddress]
        public string Email { get; set; }
    }
}
