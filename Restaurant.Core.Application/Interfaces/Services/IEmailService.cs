using Restaurant.Core.Application.DTOs.Services.Email;

namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        public Task SendAsycn(EmailRequest request);
    }
}
