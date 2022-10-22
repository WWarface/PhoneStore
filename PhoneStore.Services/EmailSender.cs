using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace PhoneStore.Services
{
    public class EmailSender : IEmailSender
    {

        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("PhoneStore", "2003harik2003@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", _config.GetSection("CompanyMail").Value));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("Plain")
            {
                Text = message
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 465);
                await client.AuthenticateAsync("2003harik2003@gmail.com", "zprklwaigmnmfbgd");
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
