using System;
using API_ToDo.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_ToDo.Data.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder
                .HasKey(t => t.Id);

            builder
                .Property(t => t.Title)
                .IsRequired();

            builder
                .Property(t => t.Observation)
                .HasColumnType("text");

            builder
                .Property<DateTime>("last_update")
                .HasDefaultValueSql("getdate()");
        }
    }
}
