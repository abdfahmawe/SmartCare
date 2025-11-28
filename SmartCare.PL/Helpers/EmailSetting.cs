using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net.Mail;
using System.Net;

namespace SmartCare.PL.Helpers
{
    public class EmailSetting : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("abdalrahman.hamdan.gpt@gmail.com", "rqog nvkf zdcm brua")
            };

            return client.SendMailAsync(
                new MailMessage(from: "abdalrahman.hamdan.gpt@gmail.com",
                                to: email,
                                subject,
                                htmlMessage
                                )
                { IsBodyHtml=true});
        
        }
    }
}
