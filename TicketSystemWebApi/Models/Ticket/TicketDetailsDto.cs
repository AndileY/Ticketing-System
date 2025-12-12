using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Models.Ticket
{
    public class TicketDetailsDto
    {


        public string UserId { get; set; }

        public int TicketId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int TicketCategoryId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ResolvedAt { get; set; }

        public int ClientAccountId { get; set; }
       public string ClientName { get; set; }

        public string? AssignToUserId { get; set; }

        public string? AssignToUserName { get; set; }

        public string? Comments { get; set; }
    }
}
