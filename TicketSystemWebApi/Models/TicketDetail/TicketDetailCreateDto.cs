using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Models.TicketDetail
{
    public class TicketDetailCreateDto
    {
        [Required]
        public int TicketId { get; set; } 

        [Required]
        public string UserId { get; set; } 

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int TicketStatusId { get; set; }

        public string? Description { get; set; }


    }
}
