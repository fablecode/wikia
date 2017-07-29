using System;
using System.Collections.Generic;
using System.Linq;

namespace wikia.Helper
{
    public class UrlHelper
    {            
        public static string GenerateApiUrl(string absoluteUrl, string relativeUrl)
        {
            if (!absoluteUrl.EndsWith("/"))
                absoluteUrl += "/";

            if (relativeUrl.StartsWith("/"))
                relativeUrl = relativeUrl.TrimStart('/');

            var absoluteUri = new Uri(absoluteUrl);
            var relativeUri = new Uri(relativeUrl, UriKind.Relative);

            return new Uri(absoluteUri, relativeUri).AbsoluteUri;
        }

        public static string GenerateUrl(string url, IDictionary<string, string> parameters)
        {
            var isValidUrl = IsValidUrl(url);

            if (!isValidUrl)
                throw new ArgumentException($"{nameof(GenerateUrl)} method: {nameof(url)} parameter is invalid or Scheme should be either http or https.");

            var urlBuilder = new UriBuilder(url);

            if (parameters != null && parameters.Any())
            {
                IEnumerable<string> segments = 
                (
                    from keyvalue in parameters
                    select $"{keyvalue.Key}={keyvalue.Value}"
                )
                .ToArray();

                urlBuilder.Query = string.Join("&", segments);
            }

            return urlBuilder.Uri.AbsoluteUri;
        }

        public static bool IsValidUrl(string url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}