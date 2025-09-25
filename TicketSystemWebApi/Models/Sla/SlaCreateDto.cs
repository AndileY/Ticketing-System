using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Models.Sla
{
    public class SlaCreateDto
    {
        [Required]
        [Range(0, 1000)]
        public double OnsiteHours { get; set; }

        [Required]
        public double RemoteHours { get; set; }

        [Required]
        public bool IsHardwareCovered { get; set; }

        [Required]
        public string MinResponseTime { get; set; }  // e.g. "02:00:00"

        [Required]
        public string MinResolvedTime { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Warranty must be between 1 and 10 years.")]
        public int WarrantyTime { get; set; }
    }
}
