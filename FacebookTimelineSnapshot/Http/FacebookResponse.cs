using System.IO;
using System.Net;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;

namespace FacebookTimelineSnapshot.Http
{
    public class FacebookResponse
    {
        public bool IsAuthenticated { get; }
        public IHtmlDocument Html { get; }
        public Stream Stream { get; }

        public FacebookResponse(HttpWebResponse response)
        {
            Stream = response.GetResponseStream();
            Html = new HtmlParser().Parse(Stream);
            IsAuthenticated = response.Cookies.Count > 6;
        }
    }
}
