using Domain;
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
             .HasMaxLength(100)
             .IsRequired();
            
            builder.Property(x => x.Email)
             .HasColumnName("Email")
             .HasMaxLength(100)
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

            builder.Property(x => x.UserName)
                .HasColumnName("Username")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.LastLoginAt)
             .HasColumnName("LastLoginAt")
             .IsRequired();

            builder.Property(x => x.FailedLoginAttempts)
             .HasColumnName("FailedLoginAttempts")
             .IsRequired();

            builder.Property(x => x.LockoutEnd)
             .HasColumnName("LockoutEnd");

            builder.Property(x => x.EmailConfirmed)
             .HasColumnName("EmailConfirmed")
             .IsRequired();
        }
    }
}
