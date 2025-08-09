using CSMapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSMapi.Helpers.Configuration
{
    public class RepalletizationConfiguration : IEntityTypeConfiguration<Repalletization>
    {
        public void Configure(EntityTypeBuilder<Repalletization> builder)
        {
            builder.HasOne(r => r.Fromreceivingdetail)
                .WithMany(r => r.Outgoingrepalletization)
                .HasForeignKey(r => r.Fromreceivingdetailid)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Toreceivingdetail)
                .WithMany(r => r.Incomingrepalletization)
                .HasForeignKey(r => r.Toreceivingdetailtid)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
