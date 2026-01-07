
using Blazored.LocalStorage;
using System.Net.Http.Headers;
using TicketsSystemBlazorApp.Pages.Ticket;
using TicketsSystemBlazorApp.Service.Base;
using TicketSystemWebApi.Models.TicketDetail;
using static TicketsSystemBlazorApp.Pages.Ticket.TicketAssignment;

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

        // Create a ticket
        public async Task CreateTicketAsync(TicketCreateDto ticket)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
                throw new Exception("User is not logged in");

            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            await _client.CreateAsync(ticket); // NSwag method returns void
        }

        // Fetch all ticket categories
        public async Task<ICollection<TicketCategoryReadOnlyDto>> GetTicketCategoriesAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
                throw new Exception("User is not logged in");

            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            return await _client.TicketCategoryAllAsync();
        }




        

        public async Task<ICollection<TicketAssignmentDto>> GetTicketsWaitingForAssignmentAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // 1️⃣ Fetch all ticket details
            var allTicketDetails = await _client.TicketDetailAllAsync(new System.Threading.CancellationToken());

            // 2️⃣ Fetch all tickets
            var allTickets = await _client.TicketAllAsync(new System.Threading.CancellationToken());

            // 3️⃣ Fetch categories for mapping
            var categories = (await _client.TicketCategoryAllAsync())
                                .ToDictionary(c => c.TicketCategoryId, c => c.Name);

            // 4️⃣ Filter unassigned tickets
            var unassignedTickets = allTicketDetails
                .GroupBy(t => t.TicketId)
                .Where(g => !g.Any(d => d.Status == "Assigned"))
                .Select(g => g.OrderBy(d => d.StartDate).First()) // pick earliest detail
                .Select(t =>
                {
                    var ticket = allTickets.FirstOrDefault(x => x.TicketId == t.TicketId);
                    var categoryName = ticket != null && categories.ContainsKey(ticket.TicketCategoryId)
                                        ? categories[ticket.TicketCategoryId]
                                        : "Unknown";

                    return new TicketAssignmentDto
                    {
                        TicketId = t.TicketId,
                        Title = ticket?.Title ?? t.Description, // use Ticket.Title from TicketReadOnlyDto
                        ClientName = t.UserId,                  // this is the client ID
                        CreatedAt = ticket?.CreatedAt.DateTime ?? DateTime.MinValue,// safe
                        TicketCategoryName = categoryName,
                        AssigneeUserId = string.Empty
                    };
                })
                .ToList();

            return unassignedTickets;
        }
        public async Task<TicketDetailsDto?> GetTicketDetailsByIdAsync(int ticketId)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
                throw new Exception("User is not logged in");

            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.HttpClient.GetFromJsonAsync<TicketDetailsDto>($"api/TicketDetail/{ticketId}");
            return response;
        }

        public async Task<TicketDetailsDto?> GetTicketByIdAsync(int ticketId)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
                throw new Exception("User is not logged in");

            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.HttpClient.GetFromJsonAsync<TicketDetailsDto>($"api/Ticket/{ticketId}");
            return response;
        }



        





        // Assign ticket to a user
        public async Task AssignTicketAsync(int ticketId, string assigneeUserId)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);




            // Call NSwag-generated AssignAsync method
            await _client.AssignAsync(ticketId, assigneeUserId, new System.Threading.CancellationToken());
        }
        public class AssigneeDto
        {
            public string Id { get; set; } = string.Empty;
            public string FirstName { get; set; } = string.Empty;
            public string LastName { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
        }

        // DTO for ticket assignment page
        public class TicketAssignmentDto
        {
            public int TicketId { get; set; }
            public string Title { get; set; } = string.Empty;
            public string ClientName { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; }
            public string TicketCategoryName { get; set; } = string.Empty;
            public string AssigneeUserId { get; set; } = string.Empty; // input for assignment
        }

        // DTO for ticket approval page
        public class TicketApprovalDto
        {
            public int TicketId { get; set; }
            public string Title { get; set; } = string.Empty;
            public string ClientName { get; set; } = string.Empty;
            public DateTime CreatedAt { get; set; }
            public string TicketCategoryName { get; set; } = string.Empty;
        }

      
        

        // Fetch tickets waiting for approval
        public async Task<ICollection<TicketApprovalDto>> GetTicketsWaitingApprovalAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
                throw new Exception("User is not logged in");

            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // 1️⃣ Fetch all ticket details
            var allTicketDetails = await _client.TicketDetailAllAsync(new System.Threading.CancellationToken());

            // 2️⃣ Fetch all tickets
            var allTickets = await _client.TicketAllAsync(new System.Threading.CancellationToken());

            // 3️⃣ Fetch all categories
            var categories = (await _client.TicketCategoryAllAsync())
                                .ToDictionary(c => c.TicketCategoryId, c => c.Name);

            // 4️⃣ Filter ticket details for tickets not yet approved
            const int ApprovedStatusId = 6; // TicketStatusId for Approved
            var unapprovedTickets = allTicketDetails
                .GroupBy(t => t.TicketId)
                .Where(g => !g.Any(d => d.TicketStatusId == ApprovedStatusId)) // use TicketStatusId instead of Status string
                .Select(g => g.OrderBy(d => d.StartDate).First()) // pick earliest detail
                .Select(t =>
                {
                    var ticket = allTickets.FirstOrDefault(x => x.TicketId == t.TicketId);
                    var categoryName = ticket != null && categories.ContainsKey(ticket.TicketCategoryId)
                                        ? categories[ticket.TicketCategoryId]
                                        : "Unknown";

                    return new TicketApprovalDto
                    {
                        TicketId = t.TicketId,
                        Title = t.Description?.Length > 30 ? t.Description.Substring(0, 30) + "..." : t.Description ?? "",
                        ClientName = t.UserId,
                        CreatedAt = t.StartDate.UtcDateTime,
                        TicketCategoryName = categoryName
                    };
                })
                .ToList();

            return unapprovedTickets;
        }






        // Approve a ticket safely
        public async Task ApproveTicketAsync(int ticketId)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
                throw new Exception("User is not logged in");

            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            try
            {
                // NSwag-generated method
                await _client.Approve2Async(ticketId, new System.Threading.CancellationToken());
            }
            catch (ApiException ex)
            {
                // Treat 403 or misinterpreted 200 as success
                if (ex.StatusCode == 403 || ex.StatusCode == 200)
                    return;

                // Otherwise, rethrow
                throw;
            }

        }

        // Fetch all assignees
        public async Task<ICollection<AssigneeDto>> GetAssigneesAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var assigneesFromApi = await _client.AssigneesAsync(); // NSwag method

            return assigneesFromApi.Select(a => new AssigneeDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                Email = a.Email
            }).ToList();

        }

        // ✅ NEW METHOD
        public async Task<ICollection<TicketReadOnlyDto>> GetAssignedTicketsAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
                throw new Exception("User is not logged in");

            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            try
            {
                // Call NSwag-generated method with the correct auth header
                return await _client.AssignedToMeAsync();
            }
            catch (ApiException ex)
            {
                Console.WriteLine("Error fetching assigned tickets: " + ex.Message);
                return new List<TicketReadOnlyDto>();
            }
        }


        // Fetch a single client account by ID
        public async Task<ClientAccountDetailsDto?> GetClientAccountByIdAsync(int id)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
                throw new Exception("User is not logged in");

            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            return await _client.ClientAccountGETAsync(id);
        }

        
        // Start ticket
        public async Task StartTicketAsync(int ticketId)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
                throw new Exception("User is not logged in");

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/Ticket/{ticketId}/start");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.HttpClient.SendAsync(request);
            response.EnsureSuccessStatusCode(); // throws if 4xx/5xx
        }

        // Close ticket
        public async Task CloseTicketAsync(int ticketId)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
                throw new Exception("User is not logged in");

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/Ticket/{ticketId}/close");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.HttpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        // Reopen ticket
        public async Task ReopenTicketAsync(int ticketId)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (string.IsNullOrEmpty(token))
                throw new Exception("User is not logged in");

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/Ticket/{ticketId}/reopen");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.HttpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

    }
}


