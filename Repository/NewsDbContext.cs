using Microsoft.EntityFrameworkCore;
using Models.DTO;

namespace Repository
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext(DbContextOptions<NewsDbContext> options) :
            base(options) => Database.EnsureCreated();

        public DbSet<News> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>().HasData(new InformParse().GetParsedData());            
        }
    }
}