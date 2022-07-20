using Microsoft.EntityFrameworkCore;
using ShortUrl.Domain;

namespace ShortUrl.Persistence
{
    public class ShortUrlContext : DbContext
    {
        public ShortUrlContext()
        {
            ConfigureLocal();
        }

        public ShortUrlContext(DbContextOptions<ShortUrlContext> options)
            : base(options)
        {
            ConfigureLocal();
        }

        public DbSet<ShortUrlItem> ShortUrlItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShortUrlContext).Assembly);
        }

        public static void ConfigureOptions(DbContextOptionsBuilder builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        private void ConfigureLocal()
        {
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}