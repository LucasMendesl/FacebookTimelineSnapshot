using System;
using System.IO;
using System.Net;
using System.Text;

namespace FacebookTimelineSnapshot.Http
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

        public FacebookResponse Post(string url, string parameters, bool allowAutoRedirect = true)
        {
            HttpWebRequest req = CreateDefaultRequest(url, parameters, allowAutoRedirect);
            return new FacebookResponse((HttpWebResponse)req.GetResponse());
        }

        HttpWebRequest CreateDefaultRequest(string url, string parameters = null, bool allowAutoRedirect = true)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.UserAgent = Consts.UserAgent;
            req.Accept = Consts.Accept;
            req.Headers.Add(Consts.Origin, Consts.MainUrl);
            req.Headers.Add(Consts.CacheControl, Consts.CacheControlValue);
            req.CookieContainer = cookieJar;
            req.AllowAutoRedirect = allowAutoRedirect;

            if (!string.IsNullOrEmpty(parameters))
            {
                req.Method = WebRequestMethods.Http.Post;
                byte[] dataBytes = Encoding.UTF8.GetBytes(parameters);

                req.ContentType = Consts.ContentType;
                req.ContentLength = dataBytes.Length;
                req.Referer = Consts.MainUrl;

                using (Stream sendStream = req.GetRequestStream())
                    sendStream.Write(dataBytes, 0, dataBytes.Length);
            }

            return req;
        }
    }
}
