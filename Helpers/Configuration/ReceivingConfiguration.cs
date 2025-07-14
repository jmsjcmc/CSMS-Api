using CSMapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSMapi.Helpers.Configuration
{
    public class ReceivingConfiguration : IEntityTypeConfiguration<Receiving>
    {
        public void Configure(EntityTypeBuilder<Receiving> builder)
        {
            builder.HasOne(r => r.Document)
                .WithMany(d => d.Receiving)
                .HasForeignKey(r => r.Documentid)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.Product)
                .WithMany(p => p.Receiving)
                .HasForeignKey(r => r.Productid)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(r => r.Receivingdetails)
                .WithOne(r => r.Receiving)
                .HasForeignKey(r => r.Receivingid)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
