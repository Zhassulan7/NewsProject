using Microsoft.EntityFrameworkCore;
using Models.Tables;

namespace Repository
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) :
            base(options) => Database.EnsureCreated();

        public DbSet<News> News { get; set; }
        public DbSet<User> Logins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            DbDataInitializer.Initialize(modelBuilder);
        }
    }
}