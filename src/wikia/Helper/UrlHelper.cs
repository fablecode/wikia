using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace wikia.Helper
{
    public class UrlHelper
    {
        public static string GenerateUrl(string url, NameValueCollection parameters)
        {
            var isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!isValidUrl)
                throw new ArgumentException($"{nameof(GenerateUrl)} method: {nameof(url)} parameter is invalid.");

            var urlBuilder = new UriBuilder(url);

            if (parameters != null && parameters.Count > 0)
            {
                IEnumerable<string> segments = 
                (
                    from key in parameters.AllKeys
                    from value in parameters.GetValues(key)
                    select $"{Uri.EscapeUriString(key)}={Uri.EscapeUriString(value)}"
                )
                .ToArray();

                urlBuilder.Query = string.Join("&", segments);
            }

            return urlBuilder.Uri.AbsoluteUri;
        }
    }
}