using Microsoft.EntityFrameworkCore;
using Models.Tables;
using Repository.ForInitializingDb;

namespace Repository
{
    public class DbDataInitializer
    {
        public static void Initialize(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>().HasData(new InformNews().GetData());
            modelBuilder.Entity<User>().HasData(new UsersData().Get());
        }
    }
}
