using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace API_ToDo.Domain
{
    public class Role : IdentityRole<long>
    {
        public IList<UserRole> UserRoles { get; set; }
    }
}
