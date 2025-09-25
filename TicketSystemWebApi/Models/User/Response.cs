namespace TicketSystemWebApi.Models.User
{
    public class Response
    {
        public string UserId {  get; set; }

        public string Email { get; set; }

        public string Token { get; set; }

        public bool IsTemporaryPassword { get; set; } 

    }
}
