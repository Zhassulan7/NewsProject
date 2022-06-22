using Models.Tables;

namespace Services.Abstract
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetNewsByDate(DateTime from, DateTime to);
        Task<IEnumerable<string>> GetTopTenWordsInNews();
        Task<IEnumerable<News>> SearchByText(string text);

    }
}
