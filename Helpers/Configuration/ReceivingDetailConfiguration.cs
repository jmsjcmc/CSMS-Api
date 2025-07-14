using CSMapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSMapi.Helpers.Configuration
{
    public class ReceivingDetailConfiguration : IEntityTypeConfiguration<ReceivingDetail>
    {
        public void Configure(EntityTypeBuilder<ReceivingDetail> builder)
        {
            builder.HasOne(r => r.Pallet)
                .WithMany(p => p.ReceivingDetail)
                .HasForeignKey(r => r.Palletid)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.PalletPosition)
                .WithMany(p => p.ReceivingDetail)
                .HasForeignKey(r => r.Positionid)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
