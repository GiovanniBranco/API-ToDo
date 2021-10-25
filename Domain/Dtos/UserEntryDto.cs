using System.ComponentModel.DataAnnotations;

namespace API_ToDo.Domain.Dtos
{
    public class UserEntryDto
    {
        [Required (ErrorMessage = "Field UserName is required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Field email is required") ]
        [EmailAddress(ErrorMessage = "This email is not valid!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Field Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters ")]
        public string Password { get; set; }
    }
}
