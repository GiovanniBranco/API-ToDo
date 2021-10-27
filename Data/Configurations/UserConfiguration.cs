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
            
            builder
                .HasIndex(u => u.UserName);

            builder
                .Property<DateTime>("last_update")
                .HasDefaultValueSql("getdate()");

            builder
                .Ignore(u => u.PhoneNumber);

            builder
                .Ignore(u => u.PhoneNumberConfirmed);

            builder
                .Ignore(u => u.EmailConfirmed);

            builder
                .Ignore(u => u.TwoFactorEnabled);

            builder
                .Ignore(u => u.LockoutEnd);

            builder
                .Ignore(u => u.AccessFailedCount);
        }
    }
}
