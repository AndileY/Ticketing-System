using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Models.TicketCategory
{
    public class TicketCategoryCreateDto
    {
        [Required]
        [StringLength (50)]
        public string Name { get; set; }
    }
}
