using Link.Slicer.Domain.Entities;
using Link.Slicer.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Link.Slicer.Infrastructure.Persistence
{
    /// <summary>
    /// Контекст БД
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.ApplyConfiguration(new UrlConfiguration());
        }

        public DbSet<Url> Urls { get; set; }
    }
}
