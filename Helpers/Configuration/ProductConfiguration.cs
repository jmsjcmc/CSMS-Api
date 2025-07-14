using CSMapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSMapi.Helpers.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.Customer)
                .WithMany(c => c.Product)
                .HasForeignKey(p => p.Customerid);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Product)
                .HasForeignKey(c => c.Categoryid);
        }
    }

    
}
