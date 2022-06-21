using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Models.DTO;
using NewsParser.Helpers;
using System.Text;

namespace NewsParser
{
    public class InformParse : Parser
    {
        private readonly string _mainUrl;
        private readonly string _linksPage;

        public InformParse(string mainUrl, string linksPage)
        {
            _mainUrl = mainUrl;
            _linksPage = linksPage;
        }

        public IEnumerable<News> GetParsedData()
        {
            var getHtml = GetHtmlByLink(_linksPage);
            var links = GetLinksOnNews(getHtml);
            var newsPages = GetNewsPagesByUrls(links);
            var news = GetNewsByPages(newsPages);
            return news;
        }

        private IEnumerable<News> GetNewsByPages(IEnumerable<string> htmlPages)
        {
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
                .Select(t=> _mainUrl + t.Attributes["href"].Value);
            return links;
        }   
        
    }
}
