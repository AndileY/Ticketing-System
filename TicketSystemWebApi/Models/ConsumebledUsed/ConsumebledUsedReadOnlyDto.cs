namespace TicketSystemWebApi.Models.ConsumebledUsed
{ 

    public class ConsumebledUsedReadOnlyDto
    {
        public int Id { get; set; }

        public int ConsumebleUsedId { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Quantity { get; set; }
    }

}

