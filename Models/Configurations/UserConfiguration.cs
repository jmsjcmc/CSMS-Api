using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace csms_backend.Models.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.BusinessUnit)
                .WithMany(b => b.User)
                .HasForeignKey(u => u.BusinessUnitId);

            builder.HasMany(u => u.UserRole)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);

            builder.HasMany(u => u.Pallet)
                .WithOne(p => p.Creator)
                .HasForeignKey(p => p.CreatorId);
        }
    }
}
