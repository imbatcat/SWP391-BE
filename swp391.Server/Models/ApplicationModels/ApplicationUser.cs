using Microsoft.AspNetCore.Identity;
namespace PetHealthcare.Server.Models.ApplicationModels
{
    public class ApplicationUser : IdentityUser
    {
        public string AccountId { get; set; }
        public string AccountFullname { get; set; }
    }
}
