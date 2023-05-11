using Link.Slicer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Link.Slicer.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            
            modelBuilder.Entity<Url>().HasKey(p => p.UrlId);
            modelBuilder.Entity<Url>().HasIndex(e => e.UrlId).IsUnique();
            modelBuilder.Entity<Url>().Property(p => p.UrlId).HasDefaultValueSql("uuid_generate_v4()");
            modelBuilder.Entity<Url>().Property(p => p.CreatedAt).IsRequired();
            modelBuilder.Entity<Url>().Property(p => p.UpdatedAt).IsRequired();
            modelBuilder.Entity<Url>().Property(p => p.DomainName).IsRequired();
            modelBuilder.Entity<Url>().Property(p => p.Address).IsRequired();
            modelBuilder.Entity<Url>().Property(p => p.Target).IsRequired();
        }

        public DbSet<Url> Urls { get; set; }
    }
}
