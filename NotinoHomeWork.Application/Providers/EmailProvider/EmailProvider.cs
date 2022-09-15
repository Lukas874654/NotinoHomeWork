using Microsoft.Extensions.Options;
using NotinoHomeWork.Application.Configurations;
using System.Net.Mail;

namespace NotinoHomeWork.Application.Providers.EmailProvider
{
    public class EmailProvider : IEmailProvider
    {
        private EmailConfiguration emailConfiguration;
        public EmailProvider(IOptions<EmailConfiguration> emailConfiguration)
        {
            this.emailConfiguration = emailConfiguration.Value;
        }

        public void SendFile(string toEmail, string filePath)
        {
            MailMessage message = new MailMessage();

            message.From = new MailAddress(emailConfiguration.FromEmail);
            message.To.Add(toEmail);
            message.Attachments.Add(new Attachment(filePath));

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(message);

        }
    }
}
