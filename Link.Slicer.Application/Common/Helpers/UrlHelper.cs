using Link.Slicer.Domain.Entities;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Link.Slicer.Application.Common.Helpers
{
    public static class UrlHelper
    {
        public static Dictionary<string, string> SplitUrl(string url)
        {
            if (!ValidUrlFormat(url))
                throw new ArgumentException(nameof(url));

            var uri = new Uri(url);

            return new Dictionary<string, string>()
            {
                { "scheme", uri.Scheme },
                { "host", uri.Host },
                { "path", uri.AbsolutePath },
                { "query", uri.Query },
                { "fragment", uri.Fragment }
            };
        }

        public static string GenerateShortUrl(string authority, string address)
        {
            return $"{authority}/{address}";
        }

        public static string GenerateLongUrl(string scheme, string host, string path)
        {
            return $"{scheme}://{host}{path}";
        }

        public static bool ValidUrlFormat(string url)
        {
            if (string.IsNullOrEmpty(url)) return false;
            if (!Regex.IsMatch(url, @"^(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$")) return false;

            return true;
        }
    }
}
