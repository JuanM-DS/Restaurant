using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Restaurant.Core.Application.DTOs.Email;
using Restaurant.Core.Application.Interfaces.Infrastructure.Services;
using Restaurant.Core.Domain.Settings;

namespace Restaurant.Infrastructure.Shared.Services
{
    public class EmailService(IOptions<EmailSettings> emailSettigns) : IEmailService
    {
        private readonly EmailSettings _emailSettings = emailSettigns.Value;

        public async Task SendAsycn(EmailRequest request)
        {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailSettings.EmailFrom);
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            var builder = new BodyBuilder() { HtmlBody = request.Body };
            email.Body = builder.ToMessageBody();

            try
            {
                using var client = new SmtpClient();
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync(_emailSettings.StmpHost, _emailSettings.StmpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailSettings.StmpUser, _emailSettings.StmpPassword);
                await client.SendAsync(email);
                await client.DisconnectAsync(false);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
