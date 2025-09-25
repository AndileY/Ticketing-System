using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Models.User
{
    public class UserUpdateDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public int UserGroupId { get; set; }
    }
}
