using Microsoft.EntityFrameworkCore;
using Models.Tables;
using NLog;
using Repository.ForInitializingDb;

namespace Repository
{
    public class DbDataInitializer
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public static void Initialize(ModelBuilder modelBuilder)
        {
            try
            {
                modelBuilder.Entity<News>().HasData(new InformNews().GetData());
                modelBuilder.Entity<User>().HasData(new UsersData().Get());
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
            
        }
    }
}
