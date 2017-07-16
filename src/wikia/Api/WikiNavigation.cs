using System.Threading.Tasks;
using wikia.Configuration;
using wikia.Helper;
using wikia.Models;

namespace wikia.Api
{
    public class WikiNavigation : IWikiNavigation
    {
        private readonly IWikiaHttpClient _wikiaHttpClient;
        private readonly string _wikiApiUrl;
        private readonly string _navigationLinks = "Navigation/Data";

        public WikiNavigation(string domainUrl)
            : this(domainUrl, WikiaSettings.RelativeApiUrl)
        {

        }

        public WikiNavigation(string domainUrl, string relativeApiUrl)
            : this(domainUrl, relativeApiUrl, new WikiaHttpClient())
        {

        }

        public WikiNavigation(string domainUrl, string relativeApiUrl, IWikiaHttpClient wikiaHttpClient)
        {
            _wikiApiUrl = UrlHelper.GenerateApiUrl(domainUrl, relativeApiUrl);
            _wikiaHttpClient = wikiaHttpClient;
        }

        public async Task<NavigationResultSet> NavigationLinks()
        {
            var requestUrl = UrlHelper.GenerateApiUrl(_wikiApiUrl, _navigationLinks);
            var json = await _wikiaHttpClient.Get(requestUrl);

            return JsonHelper.Deserialize<NavigationResultSet>(json);
        }
    }
}