using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using Models.Tables;
using NLog;
using System.Text;

namespace Repository.ForInitializingDb
{
    public class InformNews
    {
        private readonly string _mainUrl = "https://lenta.inform.kz";
        private readonly string _linksPage = "https://lenta.inform.kz/ru/archive/?date=";
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public IEnumerable<News> GetData()
        {
            var getHtml = GetHtmlByLink(_linksPage);
            var links = GetLinksOnNews(getHtml);
            var newsPages = GetNewsPagesByUrls(links);
            var news = GetNewsByPages(newsPages);
            return news;
        }

        private IEnumerable<string> GetNewsPagesByUrls(IEnumerable<string> urls)
        {
            var htmls = new List<string>();

            foreach (var url in urls)
            {
                htmls.Add(GetHtmlByLink(url));
            }

            return htmls;
        }

        private string GetHtmlByLink(string url)
        {
            try
            {
                var httpClient = new HttpClient();
                var data = httpClient.GetStringAsync(url).Result;
                return data;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }           
        }

        private IEnumerable<News> GetNewsByPages(IEnumerable<string> htmlPages)
        {
            try
            {
                var result = new List<News>();

                foreach (var html in htmlPages)
                {
                    var htmlDocument = new HtmlDocument();
                    htmlDocument.LoadHtml(html);

                    var text = new StringBuilder();

                    foreach (var innerText in htmlDocument.DocumentNode.QuerySelectorAll("p").Select(p => p.InnerText))
                    {
                        text.Append(innerText ?? "");
                    }

                    result.Add(new News
                    {
                        CreateDate = DateTime.Parse(htmlDocument.DocumentNode.QuerySelector(".date_article").InnerText),
                        Title = htmlDocument.DocumentNode.QuerySelector("h1").InnerText,
                        Text = text.ToString()
                    });
                }

                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
            
        }

        private IEnumerable<string> GetLinksOnNews(string html)
        {
            try
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                var links = htmlDocument.DocumentNode
                    .QuerySelectorAll(".lenta_news_title")
                    .Select(t => _mainUrl + t.Attributes["href"].Value);
                return links;
            }
            catch (Exception e)
            {
                _logger.Error(e);
                throw;
            }
            
        }
    }
}
