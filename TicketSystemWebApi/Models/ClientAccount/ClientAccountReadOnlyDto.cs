using TicketSystemWebApi.Models.ClientAccount;

namespace TicketSystemWebApi.Models.ClientAccount
{
    public class ClientAccountReadOnlyDto
    {

        public int ClientAccountId { get; set; }

        public string FirstName { get; set; } 

        public string LastName { get; set; } 

        public int CompanyId { get; set; }

        public string Email { get; set; } 

        public string Telephone { get; set; } 

        public string Address { get; set; } 

        public string QuickBooksUid { get; set; }

        public int Slaid { get; set; }


        // ✅ Add this:
        public string? CompanyName { get; set; }

        public bool IsApproved { get; set; }

        // Add this property to help your client-side filtering:
        public bool HasUserAccount { get; set; }

    }
}
