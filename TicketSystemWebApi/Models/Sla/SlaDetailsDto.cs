

namespace TicketSystemWebApi.Models.Sla
{
    public class SlaDetailsDto
    {
        public int Slaid { get; set; }

        public double OnsiteHours { get; set; }

        public double RemoteHours { get; set; }

        public bool IsHardwareCovered { get; set; }

        public string MinResponseTime { get; set; }  // e.g. "02:00:00"
        public string MinResolvedTime { get; set; }
        public int WarrantyTime { get; set; }

    }
}
