using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace csms_backend.Models.Configurations
{
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
