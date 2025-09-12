using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace csms_backend.Models.Configurations
{
    public class PalletPositionConfiguration : IEntityTypeConfiguration<PalletPosition>
    {
        public void Configure(EntityTypeBuilder<PalletPosition> builder)
        {
            builder.HasOne(p => p.ColdStorage)
                .WithMany(c => c.PalletPosition)
                .HasForeignKey(p => p.CsId);
        }
    }
}
