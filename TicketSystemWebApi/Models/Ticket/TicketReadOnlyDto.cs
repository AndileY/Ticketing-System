namespace TicketSystemWebApi.Models.Ticket
{
    public class TicketReadOnlyDto
    {
        public string UserId { get; set; }

        public int TicketId { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int TicketCategoryId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ResolvedAt { get; set; }

        public int ClientAccountId { get; set; }


        // ✅ Add this
        public string TicketCategoryName { get; set; } = null!;

        public string? AssignToUserId { get; set; }

        public string? Comments { get; set; }
    }
}
