using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace csms_backend.Models.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRoleRelation>
    {
        public void Configure(EntityTypeBuilder<UserRoleRelation> builder)
        {
            builder.HasKey(ur => new
            {
                ur.UserId,
                ur.RoleId
            });

            builder.HasOne(ur => ur.User)
                .WithMany(u => u.UserRole)
                .HasForeignKey(ur => ur.UserId);

            builder.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRole)
                .HasForeignKey(ur => ur.RoleId);
        }
    }
}
