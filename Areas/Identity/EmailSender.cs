using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace ClimbingShoebox.Areas.Identity
{
    public class EmailSender : IEmailSender
    {
        private readonly string apiKey;
        private readonly string fromName;
        private readonly string fromEmail;

        public EmailSender(IConfiguration config)
        {
            apiKey = config["SendGrid:ApiKey"];
            fromName = config["SendGrid:FromName"];
            fromEmail = config["SendGrid:FromEmail"];
        }
        
        public async Task SendEmailAsync(string email, string subject, string message)
        {
           
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(fromEmail, fromName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };

            msg.AddTo(new EmailAddress(email));

            //nb this is for marketing campaigns
            msg.SetClickTracking(false, false);

            await client.SendEmailAsync(msg);

        }
    }
}
