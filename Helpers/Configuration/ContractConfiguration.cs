using CSMapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSMapi.Helpers.Configuration
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasMany(c => c.Leasedpremises)
                .WithOne(l => l.Contract)
                .HasForeignKey(c => c.Contractid)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
