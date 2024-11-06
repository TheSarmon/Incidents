using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Incidents.Domain.Entities;

namespace Incidents.Infrastructure.Data.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.HasIndex(a => a.Name).IsUnique();

            builder.HasMany(a => a.Contacts)
                   .WithOne(c => c.Account)
                   .HasForeignKey(c => c.AccountId);

            builder.HasMany(a => a.Incidents)
                   .WithOne(i => i.Account)
                   .HasForeignKey(i => i.AccountId);
        }
    }
}
