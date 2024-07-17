namespace Restaurant.Core.Application.Interfaces.Services
{
    public interface IUriServices
    {
        public string GetResetPasswordURl(string token, string userId);
    }
}
