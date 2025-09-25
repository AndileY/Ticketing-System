using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Models.Ticket
{
    public class TicketUpdateDto
    {

        [Required]
        public int TicketCategoryId { get; set; }

        [Required]
        public string Title { get; set; } 

        [Required]
        public string Description { get; set; } 

       
        public DateTime? ResolvedAt { get; set; }

        public string? AssignToUserId { get; set; }

        public string? Comments { get; set; }

        [Required]
        public int ClientAccountId { get; set; }
    }
}
