using System;
using System.Configuration;
using System.Globalization;

namespace FacebookTimelineSnapshot
{
    class Consts
    {
        public const string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/59.0.3071.86 Safari/537.36";
        public const string ContentType = "application/x-www-form-urlencoded";

        public const string Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
        public const string Origin = "Origin";
        public const string CacheControl = "Cache-Control";
        public const string CacheControlValue = "max-age=0";

        public const string MainUrl = "https://www.facebook.com/";
        public const string AuthenticationUrl = "https://m.facebook.com/login/async/?refsrc=https%3A%2F%2Fm.facebook.com%2F&lwv=100";

        public static readonly string FileName = $"timeline_{DateTime.Now.ToString("yyyyMMddHHmmss.fff", CultureInfo.InvariantCulture)}.html";
    }
}
