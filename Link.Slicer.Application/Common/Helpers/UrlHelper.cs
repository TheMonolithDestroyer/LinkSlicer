using System.Text.RegularExpressions;

namespace Link.Slicer.Application.Common.Helpers
{
    public static class UrlHelper
    {
        /// <summary>
        /// Splits an url into distinct parts.
        /// </summary>
        /// <param name="url">Url to split.</param>
        /// <returns>Dictionary of split url.</returns>
        /// <exception cref="ArgumentException"></exception>
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

        /// <summary>
        /// Generates a short url
        /// </summary>
        /// <param name="authority">Scheme://domain.</param>
        /// <param name="address">Addess or path.</param>
        /// <returns>Generated short url.</returns>
        public static string GenerateShortUrl(string authority, string address)
        {
            return $"{authority}/{address}";
        }

        /// <summary>
        /// Generates a long/original url.
        /// </summary>
        /// <param name="scheme">Protocol.</param>
        /// <param name="host">Domain</param>
        /// <param name="path">Address or path.</param>
        /// <returns>Generated an original long url.</returns>
        public static string GenerateLongUrl(string scheme, string host, string path)
        {
            return $"{scheme}://{host}{path}";
        }

        /// <summary>
        /// Validates an url for nullabilit, emptiness and regex.
        /// </summary>
        /// <param name="url">An incoming url to validate.</param>
        /// <returns>True or false indicating validity.</returns>
        public static bool ValidUrlFormat(string url)
        {
            if (string.IsNullOrEmpty(url)) return false;
            if (!Regex.IsMatch(url, @"^(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$")) return false;

            return true;
        }
    }
}
