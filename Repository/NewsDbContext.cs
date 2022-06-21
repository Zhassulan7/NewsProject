using Microsoft.EntityFrameworkCore;
using Models.DTO;

namespace Repository
{
    public class NewsDbContext : DbContext
    {
        public NewsDbContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=NewsProject"); 
        }

        public DbSet<News> News { get; set; }
    }
}