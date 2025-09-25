namespace TicketSystemWebApi.Models.ClientAccountCompanyAccess
{
    public class ClientAccountCompanyAccessReadOnly
    {
        public int ClientAccountId { get; set; }
        public string ClientFullName { get; set; } = string.Empty;

        public int CompanyId { get; set; }
        public string CompanyName { get; set; } = string.Empty;

    }
}
