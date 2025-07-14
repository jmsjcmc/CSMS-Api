using CSMapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSMapi.Helpers.Configuration
{
    public class DispatchingDetailConfiguration : IEntityTypeConfiguration<DispatchingDetail>
    {
        public void Configure(EntityTypeBuilder<DispatchingDetail> builder)
        {
            builder.HasOne(d => d.Pallet)
                .WithMany(p => p.DispatchDetail)
                .HasForeignKey(d => d.Palletid)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.Dispatching)
                .WithMany(d => d.Dispatchingdetails)
                .HasForeignKey(d => d.Dispatchingid)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.Receivingdetail)
                .WithMany(r => r.DispatchingDetail)
                .HasForeignKey(d => d.Receivingdetailid)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(d => d.PalletPosition)
                .WithMany(p => p.DispatchingDetail)
                .HasForeignKey(d => d.Positionid)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
