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
            return _newsDbContext.News.Where(n=>n.CreateDate >= from && n.CreateDate <= to);
        }

        public IEnumerable<string> GetTopTenWordsInNews()
        {
            string[] separators = { ",", ".", "!", "?", ";", ":", " ", "\"", ")", "(", "'", "«", "»" };
            var allWords = string.Join(" ", _newsDbContext.News.Select(n=>n.Text.ToLower())
                .ToList())
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Where(s=>s.Length > 2 && !s.Any(i=>char.IsDigit(i)));
            var wordsGroup = allWords.GroupBy(w => w);
            var countOfWords = new Dictionary<string, int>();

            foreach (var word in wordsGroup)
            {
                countOfWords.Add(word.Key, allWords.Count(w => w == word.Key));
            }

            return countOfWords.OrderByDescending(c => c.Value).Take(10).Select(c => c.Key);
        }

        public IEnumerable<News> SearchByText(string text)
        {
            return _newsDbContext.News.Where(n => n.Text.ToLower().Contains(text.ToLower()));
        }
    }
}
