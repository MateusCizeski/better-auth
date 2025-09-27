using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.SettingsEF.RolePermissions
{
    public class RolePermissionConfig : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("role_permissions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                 .HasColumnName("Id")
                 .ValueGeneratedNever()
                 .IsRequired();

            builder.Property(x => x.RoleId)
                 .HasColumnName("RoleId")
                 .IsRequired();

            builder.Property(x => x.PermissionId)
                 .HasColumnName("PermissionId")
                 .IsRequired();

            builder.Property(x => x.CreatedAt)
                 .HasColumnName("CreatedAt")
                 .IsRequired();

            builder.HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId);

            builder.HasOne(x => x.Permission)
                .WithMany()
                .HasForeignKey(x => x.PermissionId);
        }
    }
}
