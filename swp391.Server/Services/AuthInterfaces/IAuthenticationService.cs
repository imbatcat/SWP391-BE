using PetHealthcare.Server.APIs.DTOS;
using PetHealthcare.Server.APIs.DTOS.Auth;
using PetHealthcare.Server.Models.ApplicationModels;

namespace PetHealthcare.Server.Services.AuthInterfaces
{
    public interface IAuthenticationService
    {
        Task SendConfirmationEmail(string userId, string userEmail);
        Task SendForgotPasswordEmail(ApplicationUser userId, string userEmail);
        Task<string> GenerateConfirmationToken(ApplicationUser user, string email, bool isChange = false);
        Task<string> GenerateForgotPasswordToken(ApplicationUser user, string email);
        Task<string?> GetUserRole(ApplicationUser user);

        Task<RegisterErrorDTO?> ValidateUniqueFields(AccountDTO accountDTO);
    }
}
