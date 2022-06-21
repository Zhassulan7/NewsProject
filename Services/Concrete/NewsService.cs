using Models.DTO;
using Repository;
using Services.Abstract;

namespace Services.Concrete
{
    public class NewsService : INewsService
    {
        private readonly NewsDbContext _newsDbContext;
        public NewsService(NewsDbContext newsDbContext)
        {
            _newsDbContext = newsDbContext;
        }

        public IEnumerable<News> GetNewsByDate(DateTime from, DateTime to)
        {
            return _newsDbContext.News.Where(n=>n.Id < 10);
        }
    }
}
