namespace Restaurant.Core.Application.Interfaces.Persistence.Services
{
    public interface IUriServices
    {
        public string GetResetPasswordURl(string token, string userId);
    }
}
