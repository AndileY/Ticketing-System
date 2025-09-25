using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Models.Sla
{
    public class SlaUpdateDto
    {
        public double OnsiteHours { get; set; }

        public double RemoteHours { get; set; }

        public bool IsHardwareCovered { get; set; }

        public string MinResponseTime { get; set; }  // e.g. "02:00:00"
        public string MinResolvedTime { get; set; }
        [Range(1, 10)]
        public int WarrantyTime { get; set; }
    }
}
