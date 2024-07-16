namespace Restaurant.Core.Application.DTOs.Identity.Account.Authentitcation
{
    public class AuthenticationRequestDto
    {
        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
