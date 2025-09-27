using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.SettingsEF.BlacklistedTokens
{
    public class BlacklistedTokenConfig : IEntityTypeConfiguration<BlacklistedToken>
    {
        public void Configure(EntityTypeBuilder<BlacklistedToken> builder)
        {
            builder.ToTable("black_listed_token");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .HasColumnName("Id")
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(x => x.Token)
                 .HasColumnName("Token")
                 .IsRequired();

            builder.Property(x => x.RevokedAt)
                 .HasColumnName("RevokedAt")
                 .IsRequired();

            builder.Property(x => x.UserId)
                 .HasColumnName("UserId")
                 .IsRequired();

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
