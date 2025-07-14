using CSMapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSMapi.Helpers.Configuration
{
    public class PalletConfiguration : IEntityTypeConfiguration<PalletPosition>
    {
        public void Configure(EntityTypeBuilder<PalletPosition> builder)
        {
            builder.HasOne(p => p.Coldstorage)
                .WithMany(c => c.Palletposition)
                .HasForeignKey(p => p.Csid);
        }
    }
}
