using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using PetHealthcare.Server.Models.ApplicationModels;
using PetHealthcare.Server.Services.AuthInterfaces;
using PetHealthcare.Server.Services.Interfaces;
using System.Net.Mail;
using System.Text;

namespace PetHealthcare.Server.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailService;

        public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailSender emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<string> GenerateConfirmationToken(ApplicationUser user, string email, bool isChange = false)
        {
            var code = isChange
                ? await _userManager.GenerateChangeEmailTokenAsync(user, email)
                : await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            return code;
        }
        public async Task SendConfirmationEmail(string userId, string userEmail)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var token = await GenerateConfirmationToken(user, userEmail);
            Console.WriteLine(token);
            var confirmationLink = $"https://localhost:5173/account-confirm?userId={userId}&token={token}";
            MailMessage message = new MailMessage();
            await _emailService.SendEmailAsync(
                userEmail,
                "Confirm Your Email Address",
                $"<p>Please confirm your email address by clicking <a href='{confirmationLink}'>here</a>. 100% reliable no scam.</p>");
        }
    }
}
