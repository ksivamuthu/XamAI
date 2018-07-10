using System;
using System.Collections.Specialized;
using System.Linq;
using System.Net;

namespace XamAI.Services
{
    public class UriExtensions
    {
        public static string ToQueryString(NameValueCollection nvc, bool encodeValue = true)
        {
            if(encodeValue)
            {
                var array = (from key in nvc.AllKeys
                             from value in nvc.GetValues(key)
                             select $"{WebUtility.UrlEncode(key)}={WebUtility.UrlEncode(value)}").ToArray();
                return "?" + string.Join("&", array);
            }
            else
            {
                var array = (from key in nvc.AllKeys
                             from value in nvc.GetValues(key)
                             select $"{WebUtility.UrlEncode(key)}={value}").ToArray();
                return "?" + string.Join("&", array);
            }
        }
    }
}
