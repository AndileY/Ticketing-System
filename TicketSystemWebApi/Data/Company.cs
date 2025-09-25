namespace TicketSystemWebApi.Data
{
    public class Company
    {
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public ICollection<ClientAccount> ClientAccounts { get; set; }
    }

}

