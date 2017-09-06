using System;
using System.Web;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using FacebookTimelineSnapshot.Attributes;

namespace FacebookTimelineSnapshot
{
    public static class Extensions
    {
        public static string AsFormUrlEncoded(this object parameters)
        {
            Type type = parameters.GetType();
            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();

            PropertyInfo[] propsToIterate = type.GetProperties();

            foreach (PropertyInfo propertyInfo in propsToIterate)
            {
                FacebookRequestPropertyAttribute attribute = propertyInfo.GetCustomAttribute<FacebookRequestPropertyAttribute>(false);
                postData.Add(new KeyValuePair<string, string>(HttpUtility.UrlEncode(attribute?.Name ?? propertyInfo.Name), HttpUtility.UrlEncode(propertyInfo.GetValue(parameters, null)?.ToString() ?? string.Empty)));
            }

            string result = string.Join("&", postData.Select(s => $"{s.Key}={s.Value}"));
            return result;
        }
    }
}
