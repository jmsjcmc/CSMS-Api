using CSMapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSMapi.Helpers.Configuration
{
    public class RepalletizationDetailConfiguration : IEntityTypeConfiguration<RepalletizationDetail>
    {
        public void Configure(EntityTypeBuilder<RepalletizationDetail> builder)
        {
            builder.HasOne(r => r.Repalletization)
                .WithMany(r => r.RepalletizationDetail)
                .HasForeignKey(r => r.Repalletizationid)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Receivingdetail)
                .WithOne(r => r.RepalletizationDetail)
                .HasForeignKey<RepalletizationDetail>(r => r.Receivingdetailid);
        }
    }
}
