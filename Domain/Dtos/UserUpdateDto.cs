namespace API_ToDo.Domain.Dtos
{
    public class UserUpdateDto
    {
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
