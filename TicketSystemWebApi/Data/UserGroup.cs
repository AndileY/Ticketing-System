using System;
using System.Collections.Generic;

namespace TicketSystemWebApi.Data;

public partial class UserGroup
{
    public int UserGroupId { get; set; }

    public string GroupName { get; set; } 


    public ICollection<User> Users { get; set; }
}
