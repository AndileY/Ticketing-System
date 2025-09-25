using Blazored.LocalStorage;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using TicketsSystemBlazorApp.Service.Base;
using TicketSystemWebApi.Models.ClientAccount;
using ClientAccountDetailsDto = TicketsSystemBlazorApp.Service.Base.ClientAccountDetailsDto;
using ClientAccountUpdateDto = TicketsSystemBlazorApp.Service.Base.ClientAccountUpdateDto;


public class ClientAccountService
{
    private readonly HttpClient _client;
    private readonly ILocalStorageService _localStorage;

    public ClientAccountService(IHttpClientFactory factory, ILocalStorageService localStorage)
    {
        _client = factory.CreateClient("ApiClient");
        _localStorage = localStorage;
    }


    private async Task AttachTokenAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (!string.IsNullOrWhiteSpace(token))
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }




    // 1. Get all clients (for Admin)
    public async Task<List<ClientAccountReadOnlyDto>> GetAll()
    {
        await AttachTokenAsync();
        var response = await _client.GetAsync("api/clientaccount/all");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<ClientAccountReadOnlyDto>>() ?? new List<ClientAccountReadOnlyDto>();
    }
  
    public async Task<ApprovalResultDto> ApproveClient(int clientId)
    {
        await AttachTokenAsync();

        var response = await _client.PostAsync($"api/clientaccount/approve/{clientId}", null);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Approval failed: {error}");
        }

        var result = await response.Content.ReadFromJsonAsync<ApprovalResultDto>();
        if (result == null)
            throw new ApplicationException("Approval returned no data.");

        return result;
    }

    // DTO to match API return
    public class ApprovalResultDto
    {
        public string Email { get; set; } = string.Empty;
        public string TemporaryPassword { get; set; } = string.Empty;
    }

    public async Task<ClientAccountDetailsDto?> GetDetails(int id)
    {
        await AttachTokenAsync();

        var response = await _client.GetAsync($"api/clientaccount/{id}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ClientAccountDetailsDto>();
        }

        throw new ApplicationException($"Failed to load details. Status: {response.StatusCode}");
    }




    // 2. Get the current client's account (for Client)
    public async Task<ClientAccountReadOnlyDto?> GetMyClientAccount()
    {
        await AttachTokenAsync(); // 👈 Ensure token is added

        var response = await _client.GetAsync("api/clientaccount/me");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<ClientAccountReadOnlyDto>();
        }

        Console.WriteLine("❌ Failed to get client account. Status: " + response.StatusCode);
        return null;
    }

    // 4. Update client account (for Client)
    public async Task<bool> Update(int id, ClientAccountUpdateDto updateDto)
    {
        await AttachTokenAsync();

        try
        {
            Console.WriteLine($"🔹 Sending PUT to api/clientaccount/{id}");
            Console.WriteLine("Payload: " + System.Text.Json.JsonSerializer.Serialize(updateDto));

            var response = await _client.PutAsJsonAsync($"api/clientaccount/{id}", updateDto);
            Console.WriteLine("Response status: " + response.StatusCode);

            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("❌ Response content: " + content);
                return false;
            }

            Console.WriteLine("✅ Client updated successfully!");
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("❌ Exception: " + ex);
            return false;
        }
    }






    // 3. Delete a client account (Admin only)
    public async Task<bool> Delete(int id)
    {
        await AttachTokenAsync();
        var response = await _client.DeleteAsync($"api/clientaccount/{id}");
        return response.IsSuccessStatusCode;
    }
}


