﻿using PetHealthcare.Server.Models.ApplicationModels;

namespace PetHealthcare.Server.Services.AuthInterfaces
{
    public interface IAuthenticationService
    {
        Task SendConfirmationEmail(string userId, string userEmail);
        Task<string> GenerateConfirmationToken(ApplicationUser user, string email, bool isChange = false);
    }
}