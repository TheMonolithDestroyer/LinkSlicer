using System.Text.RegularExpressions;

namespace Link.Slicer.Utils
{
    public static class UrlHelper
    {
        public static string GetDomain(string url)
        {
            if (!ValidUrlFormat(url))
                throw new ArgumentException(Resource.InvalidUrl);

            var uri = new Uri(url);
            var domain = uri.Host;

            return domain;
        }

        public static string GetScheme(string url)
        {
            if (!ValidUrlFormat(url))
                throw new ArgumentException(Resource.InvalidUrl);

            var uri = new Uri(url);
            var scheme = uri.Scheme;

            return scheme;
        }

        public static string GenerateShortUrl(string authority, string address)
        {
            return $"{authority}/{address}";
        }

        public static bool ValidUrlFormat(string url)
        {
            if (string.IsNullOrEmpty(url)) return false;
            if (!Regex.IsMatch(url, @"^(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$")) return false;

            return true;
        }
    }
}
