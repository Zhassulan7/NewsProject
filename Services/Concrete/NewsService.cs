using Models;
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
            return _newsDbContext.News.Where(n=>n.CreateDate >= from && n.CreateDate <= to).ToList();
        }

        public IEnumerable<string> GetTopTenWordsInNews()
        {
            char[] separators = { ',', '.', '!', '?', ';', ':', ' ', '"', ')', '(', '«', '»', '\t', '-', '\\' };
            var result = string.Join(" ", _newsDbContext.News.Select(n => n.Text.ToLower())
                .ToList())
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Where(s => s.Length > 1 && !s.Any(i => !char.IsLetter(i)))
                .GroupBy(w => w.ToLower())
                .Select(g => new
                {
                    Word = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(s => s.Count)
                .Take(10)
                .Select(o=>o.Word);    

            return result;
        }

        public IEnumerable<News> SearchByText(string text)
        {
            return _newsDbContext.News.Where(n => n.Text.ToLower().Contains(text.ToLower())).ToList();
        }
    }
}
