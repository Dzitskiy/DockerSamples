using System;
using Advertisement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Advertisement.Infrastructure.DataAccess.EntitiesConfiguration
{
    public class AdConfiguration : IEntityTypeConfiguration<Ad>
    {
        public void Configure(EntityTypeBuilder<Ad> builder)
        {
            builder.HasKey(x => x.Id);
                
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);
                
            builder.Property(x => x.FirstName).HasMaxLength(100).IsUnicode();
            builder.Property(x => x.LastName).HasMaxLength(100).IsUnicode();
            builder.Property(x => x.Price).HasColumnType("money");

            builder.Property(x => x.Status)
                .HasConversion<string>(s => s.ToString(), s => Enum.Parse<Ad.Statuses>(s));

            builder.HasOne(x => x.Owner)
                .WithMany()
                .HasForeignKey(s => s.OwnerId)
                .HasPrincipalKey(u => u.Id);
        }
    }
}