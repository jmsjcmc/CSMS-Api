using csms_backend.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace csms_backend.Models.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasMany(c => c.Representative)
                .WithOne(r => r.Company)
                .HasForeignKey(r => r.CompanyId);

            builder.HasMany(c => c.Product)
                .WithOne(p => p.Company)
                .HasForeignKey(p => p.CompanyId);
        }
    }
    public class RepresentativeConfiguration : IEntityTypeConfiguration<Representative>
    {
        public void Configure(EntityTypeBuilder<Representative> builder)
        {
            builder.HasOne(r => r.Company)
                .WithMany(c => c.Representative)
                .HasForeignKey(r => r.CompanyId);
        }
    }
}
