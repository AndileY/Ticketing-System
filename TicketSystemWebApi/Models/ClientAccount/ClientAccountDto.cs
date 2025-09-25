namespace TicketSystemWebApi.Models.ClientAccount
{
    public class ClientAccountDto
    {
        public int ClientAccountId { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public string CompanyName { get; set; } = "";
        public bool IsApproved { get; set; }
        public bool HasUserAccount { get; set; }  // Add this
    }

}
