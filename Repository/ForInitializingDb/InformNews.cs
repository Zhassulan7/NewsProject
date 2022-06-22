using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Models;
using System.Net;
using System.Text;

namespace Repository.ForInitializingDb
{
    public class InformNews
    {
        private readonly string _mainUrl = "https://lenta.inform.kz";
        private readonly string _linksPage = "https://lenta.inform.kz/ru/archive/?date=";

        public async Task<IEnumerable<News>> GetData()
        {
            var getHtml = GetHtmlByLink(_linksPage);
            var links = GetLinksOnNews(await getHtml);
            var newsPages = GetNewsPagesByUrls(links);
            var news = GetNewsByPages(await newsPages);
            return news;
        }

        private async Task<IEnumerable<string>> GetNewsPagesByUrls(IEnumerable<string> urls)
        {
            List<string> htmls = new List<string>();
            foreach (var url in urls)
            {
                htmls.Add(await GetHtmlByLink(url));
            }
            return htmls;
        }

        private async Task<string> GetHtmlByLink(string url)
        {
            using (HttpClient httpClient = new())
            {
                string data = await httpClient.GetStringAsync(url);
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
