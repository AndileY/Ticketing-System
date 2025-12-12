using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSystemWebApi.Data;

public partial class Ticket
{
    public string UserId { get; set; }

    public int TicketId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int TicketCategoryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? ResolvedAt { get; set; }

    public int ClientAccountId { get; set; }

    public string? AssignToUserId { get; set; }

    [ForeignKey("AssignToUserId")]
    public virtual User? AssignedTo { get; set; }

    public string? Comments { get; set; }

    //[ForeignKey("TicketCategoryId")]
    //public virtual TicketCategory TicketCategory { get; set; }


    public ClientAccount ClientAccount { get; set; }
    public ICollection<TicketDetail> TicketDetails { get; set; }




}
