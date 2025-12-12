using System;
using System.Collections.Generic;

namespace TicketSystemWebApi.Data;

public partial class TicketDetail
{
    public int Id { get; set; }

    public int TicketDetailsId { get; set; }

    public int TicketId { get; set; }

    public string UserId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; } 


    public int TicketStatusId { get; set; } 

    public string Description { get; set; } 

    public virtual Ticket Ticket { get; set; } = null!;

    public virtual TicketStatus TicketStatus { get; set; } = null!;

  
    
}
