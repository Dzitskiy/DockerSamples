using Advertisement.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Advertisement.Infrastructure.DataAccess
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
                
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt).IsRequired(false);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(x => x.Password)
                .IsRequired();
        }
    }
}