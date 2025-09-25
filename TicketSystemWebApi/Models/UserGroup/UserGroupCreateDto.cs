using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Models.UserGroup
{
    public class UserGroupCreateDto
    {

        [Required]
        public string GroupName { get; set; }


    }
}
