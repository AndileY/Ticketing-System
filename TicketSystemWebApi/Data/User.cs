using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketSystemWebApi.Data
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int UserGroupId { get; set; }   // FK
    

        public bool IsTemporaryPassword { get; set; } = false;
        public DateTime? TemporaryPasswordExpiry { get; set; }


        // ✅ Add this if it’s missing
        public virtual UserGroup UserGroup { get; set; }



    }
}
