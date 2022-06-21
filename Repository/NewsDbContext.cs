using Microsoft.EntityFrameworkCore;
using Models.DTO;
using Repository.ForInitializingDb;

namespace Repository
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) :
            base(options) => Database.EnsureCreated();

        public DbSet<News> News { get; set; }
        public DbSet<Login> Logins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>().HasData(new InformParse().GetParsedData());    
            modelBuilder.Entity<Login>().HasData(new UsersCreater().GetLogins());
        }
    }
}