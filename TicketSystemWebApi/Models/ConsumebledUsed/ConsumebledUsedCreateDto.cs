using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Models.ConsumebledUsed
{
    public class ConsumebledUsedCreateDto
    {
        [Required]
        public int ConsumebleUsedId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}
