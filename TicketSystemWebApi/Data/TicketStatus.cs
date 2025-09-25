using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Data
{
    public class TicketStatus
    {
        public int TicketStatusId { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty; // e.g. Logged, Assigned

        public string? Description { get; set; } // E.g. "Client logged a new ticket"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<TicketDetail> TicketDetails { get; set; } = new List<TicketDetail>();
    }
}
