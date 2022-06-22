using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Models;
using System.Net;
using System.Text;

namespace Repository.ForInitializingDb
{
    public class InformParse
    {
        private readonly string _mainUrl = "https://lenta.inform.kz";
        private readonly string _linksPage = "https://lenta.inform.kz/ru/archive/?date=";

        public IEnumerable<News> GetParsedData()
        {
            var getHtml = GetHtmlByLink(_linksPage);
            var links = GetLinksOnNews(getHtml);
            var newsPages = GetNewsPagesByUrls(links);
            var news = GetNewsByPages(newsPages);
            return news;
        }
        private IEnumerable<string> GetNewsPagesByUrls(IEnumerable<string> urls)
        {
            foreach (var url in urls)
            {
                yield return GetHtmlByLink(url);
            }
        }

        private string GetHtmlByLink(string url)
        {
            using (WebClient webClient = new())
            {
                string data = webClient.DownloadString(url);
                return data;
            }
        }

        private IEnumerable<News> GetNewsByPages(IEnumerable<string> htmlPages)
        {
            int calc = 1;
            foreach (var html in htmlPages)
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);

                StringBuilder text = new StringBuilder();

                foreach (var innerText in htmlDocument.DocumentNode.QuerySelectorAll("p").Select(p => p.InnerText))
                {
                    text.Append(innerText ?? "");
                }

                yield return new News
                {
                    Id = calc++,
                    CreateDate = DateTime.Parse(htmlDocument.DocumentNode.QuerySelector(".date_article").InnerText),
                    Title = htmlDocument.DocumentNode.QuerySelector("h1").InnerText,
                    Text = text.ToString()
                };
            }
        }

        private IEnumerable<string> GetLinksOnNews(string html)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);
            var links = htmlDocument.DocumentNode
                .QuerySelectorAll(".lenta_news_title")
                .Select(t => _mainUrl + t.Attributes["href"].Value);
            return links;
        }

    }
}
