using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace API_ToDo.Domain
{
    public class Role : IdentityRole<long>
    {
        public IList<UserRole> UserRoles { get; set; }
    }
}
