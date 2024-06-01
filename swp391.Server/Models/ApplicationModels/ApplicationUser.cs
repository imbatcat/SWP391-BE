using Microsoft.AspNetCore.Identity;
namespace PetHealthcare.Server.Models.ApplicationModels
{
    public class ApplicationUser : IdentityUser
    {
        public string AccountFullname { get; set; }
    }
}
