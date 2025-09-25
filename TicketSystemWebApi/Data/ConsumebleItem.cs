using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Data
{
    public class ConsumebleItem
    {
        public int ConsumebleUsedId { get; set; } // Will now be Identity
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<ConsumebledUsed> ConsumebledUseds { get; set; }








    }
}

