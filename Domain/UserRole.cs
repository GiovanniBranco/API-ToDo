using Microsoft.AspNetCore.Identity;

namespace API_ToDo.Domain
{
    public class UserRole : IdentityUserRole<long>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
