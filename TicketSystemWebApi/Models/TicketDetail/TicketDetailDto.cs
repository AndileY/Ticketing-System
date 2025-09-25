namespace TicketSystemWebApi.Models.TicketDetail
{
    public class TicketDetailDto
    {
        public int Id { get; set; }

        public int TicketDetailsId { get; set; }
        public string ClientName { get; set; }   // From Ticket.ClientAccount
        public string TicketTitle { get; set; }  // From Ticket.Title

        public int TicketId { get; set; }

        public string UserId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int TicketStatusId { get; set; }
        public string Status { get; set; } // FROM related entity
        public string Description { get; set; }

     

    }
}
