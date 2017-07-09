using System;
using System.Collections.Generic;
using System.Linq;

namespace wikia.Helper
{
    public class UrlHelper
    {
        public static string GenerateUrl(string url, IDictionary<string, string> parameters)
        {
            var isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isValidUrl)
                throw new ArgumentException($"{nameof(GenerateUrl)} method: {nameof(url)} parameter is invalid.");

            var urlBuilder = new UriBuilder(url);

            if (parameters != null && parameters.Any())
            {
                IEnumerable<string> segments = 
                (
                    from keyvalue in parameters
                    select $"{Uri.EscapeUriString(keyvalue.Key)}={Uri.EscapeUriString(keyvalue.Value)}"
                )
                .ToArray();

                urlBuilder.Query = string.Join("&", segments);
            }

            return urlBuilder.Uri.AbsoluteUri;
        }
    }
}