using System.ComponentModel.DataAnnotations;

namespace API_ToDo.Domain.Dtos
{
    public class UserReturnDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
