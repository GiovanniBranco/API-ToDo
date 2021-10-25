using API_ToDo.Data.Configurations;
using API_ToDo.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API_ToDo.Data
{
    public class ToDoContext : IdentityDbContext<User, Role, long,
                                                    IdentityUserClaim<long>, UserRole, IdentityUserLogin<long>,
                                                    IdentityRoleClaim<long>, IdentityUserToken<long>>
    {
        public DbSet<User> User { get; set; }
        public DbSet<Task> Tasks { get; set; }

        public ToDoContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new UserRoleConfiguration());
            builder.ApplyConfiguration(new TaskConfiguration());
        }
    }
}
