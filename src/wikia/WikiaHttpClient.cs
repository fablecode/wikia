using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using wikia.Helper;

namespace wikia
{
    public class WikiaHttpClient : IWikiaHttpClient
    {
        public Task<string> Get(string url)
        {
            return Get(url, null);
        }

        public Task<string> Get(string url, IDictionary<string, string> parameters)
        {
            url = UrlHelper.GenerateUrl(url, parameters);

            using (var client = new HttpClient())
                return client.GetStringAsync(url);
        }
    }
}