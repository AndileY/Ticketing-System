using System;
using System.Collections.Generic;

namespace TicketSystemWebApi.Data;

public partial class Sla
{
    public int Slaid { get; set; }

    public double OnsiteHours { get; set; }

    public double RemoteHours { get; set; }

    public bool IsHardwareCovered { get; set; }

    public TimeSpan MinResponseTime { get; set; }

    public TimeSpan MinResolvedTime { get; set; }

    public int WarrantyTime { get; set; }


   
}
