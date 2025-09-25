using Blazored.LocalStorage;
using System.Net.Http.Headers;
using TicketsSystemBlazorApp.Service.Base;

namespace TicketsSystemBlazorApp.Service
{
    public class TicketServiceWrapper
    {
        private readonly IClient _client;
        private readonly ILocalStorageService _localStorage;

        public TicketServiceWrapper(IClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;
        }

        public async Task CreateTicketAsync(TicketCreateDto ticket)
        {
            // Get JWT token from local storage
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrEmpty(token))
                throw new Exception("User is not logged in");

            // Set Authorization header
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // Call the NSwag-generated method (void return)
            await _client.CreateAsync(ticket);
        }
    }
}
