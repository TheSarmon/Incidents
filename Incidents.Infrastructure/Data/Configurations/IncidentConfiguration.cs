using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Incidents.Domain.Entities;

namespace Incidents.Infrastructure.Data.Configurations
{
    public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> builder)
        {
            builder.HasKey(i => i.Id);
            builder.Property(i => i.IncidentName).IsRequired().HasMaxLength(100);
            builder.Property(i => i.Description).HasMaxLength(500);

            builder.HasOne(i => i.Account)
                   .WithMany(a => a.Incidents)
                   .HasForeignKey(i => i.AccountId);
        }
    }
}