using System.Net;

namespace NewsParser.Helpers
{
    public abstract class Parser
    {
        public virtual IEnumerable<string> GetNewsPagesByUrls(IEnumerable<string> urls)
        {
            foreach (var url in urls)
            {
                yield return GetHtmlByLink(url);
            }
        }

        public virtual string GetHtmlByLink(string url)
        {
            using (WebClient webClient = new())
            {
                string data = webClient.DownloadString(url);
                return data;
            }
        }
    }
}
