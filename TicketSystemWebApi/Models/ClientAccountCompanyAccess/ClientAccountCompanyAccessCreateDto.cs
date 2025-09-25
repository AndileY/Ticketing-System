using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Models.ClientAccountCompanyAccess
{
    public class ClientAccountCompanyAccessCreateDto
    {
        public int ClientAccountId { get; set; }
        public int CompanyId { get; set; }

    }
}
