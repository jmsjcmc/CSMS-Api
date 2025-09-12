using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace csms_backend.Models.Configurations
{
    public class ColdStorageConfiguration : IEntityTypeConfiguration<ColdStorage>
    {
        public void Configure(EntityTypeBuilder<ColdStorage> builder)
        {
            builder.HasMany(c => c.PalletPosition)
                .WithOne(pp => pp.ColdStorage)
                .HasForeignKey(pp => pp.CsId);
        }
    }
}
