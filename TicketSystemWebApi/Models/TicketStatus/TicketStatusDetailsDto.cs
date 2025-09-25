using Microsoft.Identity.Client;

namespace TicketSystemWebApi.Models.TicketStatus
{
    public class TicketStatusDetailsDto
    {
        public int TicketStatusId { get; set; }
        public string Status { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
