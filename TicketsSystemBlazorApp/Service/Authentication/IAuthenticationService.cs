using TicketsSystemBlazorApp.Service.Base;

namespace TicketsSystemBlazorApp.Service.Authentication
{
    public interface IAuthenticationService
    {
        //Task<bool> LoginAsync(LoginUserDto loginModel);
        Task LogoutAsync();
        Task<bool> RegisterAsync(UserCreateDto registerModel);
        Task<Response?> LoginAsync(LoginUserDto loginModel);
    }
}