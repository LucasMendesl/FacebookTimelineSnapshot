using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System.Net;

namespace FacebookTimelineSnapshot
{
    public class FacebookResponse
    {
        public bool IsAuthenticated { get; private set; }
        public IHtmlDocument Html { get; private set; }

        public FacebookResponse(HttpWebResponse response)
        {
            Html = new HtmlParser().Parse(response.GetResponseStream());
            IsAuthenticated = response.Cookies.Count > 6;
        }
    }
}
