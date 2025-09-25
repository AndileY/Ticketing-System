namespace TicketsSystemBlazorApp.Service.Base
{
    public partial class Client : IClient
    {
        public HttpClient HttpClient => _httpClient;

        
    }
}
