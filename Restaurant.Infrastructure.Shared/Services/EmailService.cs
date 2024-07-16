﻿using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Restaurant.Core.Application.DTOs.Shared.Email;
using Restaurant.Core.Application.Interfaces.Persistence.Services;
using Restaurant.Core.Domain.Settings;

namespace Restaurant.Infrastructure.Shared.Services
{
    public class EmailService(IOptions<EmailSettings> emailSettigns) : IEmailService
    {
        private readonly EmailSettings _emailSettings = emailSettigns.Value;

        public async Task SendAsycn(EmailRequest request)
        {
            var builder = new BodyBuilder() { HtmlBody = request.Body };
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_emailSettings.EmailFrom),
                Subject = request.Subject,
                Body = builder.ToMessageBody()
            };
            email.To.Add(MailboxAddress.Parse(request.To));

            try
            {
                using var client = new SmtpClient();
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync(_emailSettings.StmpHost, _emailSettings.StmpPort, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_emailSettings.StmpUser, _emailSettings.StmpPassword);
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
