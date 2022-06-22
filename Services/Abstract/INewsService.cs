using Models.Tables;

namespace Services.Abstract
{
    public interface INewsService
    {
        IEnumerable<News> GetNewsByDate(DateTime from, DateTime to);
        IEnumerable<string> GetTopTenWordsInNews();
        IEnumerable<News> SearchByText(string text);

    }
}
