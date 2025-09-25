using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Models.ConsumebleItem
{
    public class ConsumebleItemCreateDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; } 
    }
}
