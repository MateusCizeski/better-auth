using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.SettingsEF.RefreshTokens
{
    public class RefreshTokenConfig : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refresh_tokens");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(x => x.UserId)
                .HasColumnName("UserId")
                .IsRequired();

            builder.Property(x => x.Token)
                .HasColumnName("Token")
                .IsRequired();

            builder.Property(x => x.Expires)
                .HasColumnName("Expires")
                .IsRequired();

            builder.Property(x => x.Created)
                .HasColumnName("Created")
                .IsRequired();

            builder.Property(x => x.CreatedByIp)
                .HasColumnName("CreatedByIp")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Revoked)
                .HasColumnName("Revoked");

            builder.Property(x => x.RevokedByIp)
                .HasColumnName("RevokedByIp")
                .HasMaxLength(50);

            builder.Property(x => x.ReplacedByToken)
                .HasColumnName("ReplacedByToken");

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
