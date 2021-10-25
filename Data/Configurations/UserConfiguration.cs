using System;
using API_ToDo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_ToDo.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .ToTable("Users");

            builder
                .Property(u => u.Email)
                .IsRequired();

            builder
                .Property(u => u.UserName)
                .IsRequired();

            builder
                .HasIndex(u => u.Email);

            //builder
            //    .HasMany(u => u.Tasks)
            //    .WithOne(t => t.User);

            builder
                .Property<DateTime>("last_update")
                .HasDefaultValueSql("getdate()");
        }
    }
}
