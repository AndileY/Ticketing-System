namespace TicketSystemWebApi.Models.TicketStatus
{
    public class TicketStatusReadOnly
    {
        public int TicketStatusId { get; set; }


        public string Status { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
