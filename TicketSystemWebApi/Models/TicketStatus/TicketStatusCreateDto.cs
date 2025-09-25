using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Models.TicketStatus
{
    public class TicketStatusCreateDto
    {
        [Required]
        public string Status { get; set; }

        [Required]
        public string Description { get; set; }




    }
}
