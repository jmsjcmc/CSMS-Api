using CSMapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSMapi.Helpers.Configuration
{
    public class DispatchingConfiguration : IEntityTypeConfiguration<Dispatching>
    {
        public void Configure(EntityTypeBuilder<Dispatching> builder)
        {
            builder.HasOne(d => d.Product)
                .WithMany(p => p.Dispatching)
                .HasForeignKey(d => d.Productid);

            builder.HasOne(d => d.Document)
                .WithMany(d => d.Dispatching)
                .HasForeignKey(d => d.Documentid);
        }
    }
}
