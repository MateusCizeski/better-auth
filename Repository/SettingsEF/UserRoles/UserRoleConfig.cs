using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.SettingsEF.UserRoles
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("user_roles");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .ValueGeneratedNever()
             .IsRequired();

            builder.Property(x => x.UserId)
             .HasColumnName("UserId")
             .IsRequired();

            builder.Property(x => x.RoleId)
             .HasColumnName("RoleId")
             .IsRequired();

            builder.Property(x => x.CreatedAt)
             .HasColumnName("CreatedAt")
             .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId);
        }
    }
}
