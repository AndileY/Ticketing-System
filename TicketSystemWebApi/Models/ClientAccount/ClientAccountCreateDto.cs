using System.ComponentModel.DataAnnotations;

namespace TicketSystemWebApi.Models.ClientAccount
{
    public class ClientAccountCreateDto
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Company Id is required")]
        public int CompanyId { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telephone is required")]
        [RegularExpression(@"^\d{7,15}$", ErrorMessage = "Telephone must be between 7 and 15 digits and contain only numbers.")]
        public string Telephone { get; set; }

        public string? Address { get; set; }
        public string? QuickBooksUid { get; set; }

        [Required(ErrorMessage = "SLA ID is required")]
        public int Slaid { get; set; }











    }
    


}
