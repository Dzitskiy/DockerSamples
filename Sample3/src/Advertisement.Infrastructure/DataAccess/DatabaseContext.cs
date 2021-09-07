using System.Xml.Xsl;
using Advertisement.Domain;
using Advertisement.Infrastructure.DataAccess.EntitiesConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Advertisement.Infrastructure.DataAccess
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            
        }

        public DbSet<Ad> Ads { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AdConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}