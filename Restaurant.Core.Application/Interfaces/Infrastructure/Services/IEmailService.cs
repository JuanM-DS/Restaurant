using Restaurant.Core.Application.DTOs.Email;

namespace Restaurant.Core.Application.Interfaces.Infrastructure.Services
{
    public interface IEmailService
    {
        public Task SendAsycn(EmailRequest request);
    }
}
