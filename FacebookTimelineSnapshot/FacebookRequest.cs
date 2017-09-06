using System;
using System.IO;
using System.Net;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Html;
using System.Text;
using FacebookTimelineSnapshot.Request;

namespace FacebookTimelineSnapshot
{
    public class FacebookRequest
    {
        private CookieContainer cookieJar;

        public FacebookRequest(CookieContainer container)
        {
            cookieJar = container ?? throw new ArgumentNullException(nameof(container), "O parametro container não pode ser nulo");
        }

        public FacebookResponse Get(string url)
        {
            HttpWebRequest req = CreateDefaultRequest(url);

            return new FacebookResponse((HttpWebResponse)req.GetResponse());
        }

        public FacebookResponse Post(string url, object parameters, bool allowAutoRedirect = true)
        {
            HttpWebRequest req = CreateDefaultRequest(url, parameters, allowAutoRedirect);
            return new FacebookResponse((HttpWebResponse)req.GetResponse());
        }

        HttpWebRequest CreateDefaultRequest(string url, object parameters = null, bool allowAutoRedirect = true)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.UserAgent = Consts.UserAgent;
            req.Accept = Consts.Accept;
            req.Headers.Add(Consts.Origin, Consts.BaseUrl);
            req.Headers.Add(Consts.CacheControl, Consts.CacheControlValue);
            req.CookieContainer = cookieJar;
            req.AllowAutoRedirect = allowAutoRedirect;

            if (parameters != null)
            {
                req.Method = WebRequestMethods.Http.Post;
                byte[] dataBytes = Encoding.UTF8.GetBytes(parameters.AsFormUrlEncoded());

                req.ContentType = Consts.ContentType;
                req.ContentLength = dataBytes.Length;
                req.Referer = Consts.BaseUrl;

                using (Stream sendStream = req.GetRequestStream())
                    sendStream.Write(dataBytes, 0, dataBytes.Length);
            }

            return req;
        }
    }
}
