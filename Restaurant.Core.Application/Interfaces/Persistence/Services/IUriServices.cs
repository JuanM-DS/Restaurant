namespace Restaurant.Core.Application.Interfaces.Persistence.Services
{
    public interface IUriServices
    {
        public string CreateResetPasswordURl(string token, string userId);
    }
}
