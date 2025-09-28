using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.SettingsEF.Permissions
{
    public class PermissionConfig : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("permissions");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .ValueGeneratedNever()
             .IsRequired();

            builder.Property(x => x.Key)
             .HasColumnName("Key")
             .IsRequired();

            builder.Property(x => x.Description)
             .HasColumnName("Description")
             .IsRequired();

            builder.Property(x => x.CreatedAt)
             .HasColumnName("CreatedAt")
             .IsRequired();

            builder.Property(x => x.UpdatedAt)
             .HasColumnName("UpdatedAt")
             .IsRequired();
        }
    }
}
