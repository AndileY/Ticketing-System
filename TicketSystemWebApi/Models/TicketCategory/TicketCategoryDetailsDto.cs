using TicketSystemWebApi.Models.Ticket;

namespace TicketSystemWebApi.Models.TicketCategory
{
    public class TicketCategoryDetailsDto
    {
        public int TicketCategoryId { get; set; }

        public string Name { get; set; }

        public List<TicketReadOnlyDto> Tickets { get; set; } = new();
    }
}
