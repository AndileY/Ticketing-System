using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Models.TicketCategory
{
    public class TicketCategoryUpdateDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
