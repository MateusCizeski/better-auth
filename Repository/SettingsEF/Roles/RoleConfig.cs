using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.SettingsEF.Roles
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .ValueGeneratedNever()
             .IsRequired();

            builder.Property(x => x.Name)
             .HasColumnName("Name")
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
