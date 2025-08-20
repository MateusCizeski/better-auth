using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.SettingsEF.Users
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .ValueGeneratedNever()
             .IsRequired();

            builder.Property(x => x.Name)
             .HasColumnName("Name")
             .IsRequired();
            
            builder.Property(x => x.Email)
             .HasColumnName("Email")
             .IsRequired();
            
            builder.Property(x => x.PasswordHash)
             .HasColumnName("PasswordHash")
             .IsRequired();
            
            builder.Property(x => x.PasswordSalt)
             .HasColumnName("PasswordSalt")
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
