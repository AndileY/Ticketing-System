namespace TicketSystemWebApi.Models.TicketDetail
{
    public class TicketDetailReadOnlyDto
    {
        public int Id { get; set; }

        public int TicketDetailsId { get; set; }

        public int TicketId { get; set; }

        public string UserId { get; set; }

       

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TicketStatusId { get; set; }

        public string Status { get; set; }  // From TicketStatus.Status

        public string? Description { get; set; }

    }
}
