using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using TicketsSystemBlazorApp.Providers;
using TicketsSystemBlazorApp.Service.Base;


namespace TicketsSystemBlazorApp.Service.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthenticationService(IClient client, ILocalStorageService localStorage, AuthenticationStateProvider authStateProvider)
        {
            _client = client;
            _localStorage = localStorage;
            _authStateProvider = authStateProvider;
        }
        public async Task<Response?> LoginAsync(LoginUserDto loginModel)
        {
            try
            {
                var response = await _client.LoginAsync(loginModel);

                return response; // 🔁 Return the full response object
            }
            catch
            {
                // log error
            }

            return null;
        }


        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authStateProvider).NotifyUserLogout();
        }

        public async Task<bool> RegisterAsync(UserCreateDto registerModel)
        {
            try
            {
                await _client.RegisterAsync(registerModel); // Assuming your NSwag method is UserRegisterAsync
                return true;
            }
            catch
            {
                // log or handle registration failure
            }
            return false;
        }
    }
}

