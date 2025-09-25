using System;
using System.Collections.Generic;

namespace TicketSystemWebApi.Data;

public partial class ClientAccount
{
    public int ClientAccountId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int CompanyId { get; set; }
    // ✅ Add this:
    public Company Company { get; set; }

    public string Email { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string QuickBooksUid { get; set; } = null!;

    public int Slaid { get; set; }

    // 🔹 FK to Identity User
    public string? UserId { get; set; } = null!;


    // 🔸 New field
    public bool IsApproved { get; set; } = false;





}
