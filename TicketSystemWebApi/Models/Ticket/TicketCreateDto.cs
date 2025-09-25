using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Models.Ticket
{
    public class TicketCreateDto
    {

     
        public string? UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; } 

        [Required]
        [StringLength(250)]
        public string Description { get; set; }


        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int TicketCategoryId { get; set; }

        //public int ClientAccountId { get; set; }


        public string? AssignToUserId { get; set; }




        public DateTime? ResolvedAt { get; set; }

    
        public string? Comments { get; set; }


    }
}
