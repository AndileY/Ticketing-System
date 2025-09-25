namespace TicketSystemWebApi.Models.User
{
    public class UserDetailsDto
    {
        public string Id { get; set; }  

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int UserGroupId { get; set; }

        public string UserGroupName { get; set; }



    }
}
