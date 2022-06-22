using Microsoft.EntityFrameworkCore;
using Models;
using Repository.ForInitializingDb;

namespace Repository
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) :
            base(options) => Database.EnsureCreated();

        public DbSet<News> News { get; set; }
        public DbSet<Login> Logins { get; set; }

        protected override async void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>().HasData(await new InformNews().GetData());    
            modelBuilder.Entity<Login>().HasData(new UsersData().Get());
        }
    }
}