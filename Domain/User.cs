using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API_ToDo.Domain
{
    public class User : IdentityUser<long>
    {
        public string FullName { get; set; }
        public IList<UserRole> UserRoles { get; set; }

        public IList<Task> Tasks { get; set; }
    }
}
    