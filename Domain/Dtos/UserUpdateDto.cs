namespace API_ToDo.Domain.Dtos
{
    public class UserUpdateDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string CurrentPassword { get; internal set; }
        public string NewPassword { get; internal set; }
    }
}
