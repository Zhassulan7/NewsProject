using Microsoft.EntityFrameworkCore;
using Models.Tables;
using NLog;
using Repository;
using Services.Abstract;

namespace Services.Concrete
{
    public class NewsService : INewsService
    {
        private readonly NewsDbContext _newsDbContext;
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public NewsService(NewsDbContext newsDbContext)
        {
            _newsDbContext = newsDbContext;
        }

        public async Task<IEnumerable<News>> GetNewsByDate(DateTime from, DateTime to)
        {
            try
            {
                return await _newsDbContext.News.Where(n => n.CreateDate >= from && n.CreateDate <= to).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            } 
        }

        public async Task<IEnumerable<string>> GetTopTenWordsInNews()
        {
            var separators = new char[] { ',', '.', '!', '?', ';', ':', ' ', '"', ')', '(', '«', '»', '\t', '-', '\\' };

            try
            {
                var result = string.Join(" ", await _newsDbContext.News.Select(n => n.Text.ToLower())
                .ToListAsync())
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
                .Select(o => o.Word);

                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
            
        }

        public async Task<IEnumerable<News>> SearchByText(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new NullReferenceException("Text is null or empty");

            try
            {
                return await _newsDbContext.News.Where(n => !string.IsNullOrEmpty(n.Text)
                            && n.Text.ToLower().Contains(text.ToLower())).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
            
        }
    }
}
