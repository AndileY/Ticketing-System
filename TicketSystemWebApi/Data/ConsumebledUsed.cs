using System;
using System.Collections.Generic;

namespace TicketSystemWebApi.Data;

public partial class ConsumebledUsed
{
    public int Id { get; set; }

    public int ConsumebleUsedId { get; set; }

    public int Quantity { get; set; }

    public virtual ConsumebleItem ConsumebleItem { get; set; } = null!;
}
