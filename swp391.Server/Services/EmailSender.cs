using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;
using System.Drawing.Text;
using NuGet.Protocol.Plugins;
using Microsoft.AspNetCore.Http.HttpResults;

namespace PetHealthcare.Server.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly string Sender = "noreply.peternary@gmail.com";
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SmtpClient client = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com", //or another email sender provider
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Sender, "imsvkedykqcszwzj")
            };

            var message = new MailMessage(Sender, email, subject, htmlMessage);
            message.IsBodyHtml = true;
            try
            {
                return client.SendMailAsync(message);

            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException("No such email exists");

            }
        }
    }
}
