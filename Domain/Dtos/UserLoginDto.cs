using System.ComponentModel.DataAnnotations;

namespace API_ToDo.Domain.Dtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "Field Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Field Password is required")]
        public string Password { get; set; }
    }
}
