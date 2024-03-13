using Microsoft.AspNetCore.Identity.UI.Services;

namespace FILMEX.Models
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //email_logic
            return Task.CompletedTask;
        }
    }
}
