using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Models.Companies
{
    public class CompaniesCreateDto
    {
        [Required]
        public string CompanyName { get; set; }
    }
}
