using System.Collections.Generic;
using System.Threading.Tasks;
using wikia.Configuration;
using wikia.Helper;

namespace wikia.Api
{
    public class WikiMercury
    {
        private readonly IWikiaHttpClient _wikiaHttpClient;
        private readonly string _wikiApiUrl;
        private readonly string _wikiVariablesUrl = "Mercury/WikiVariables";

        public WikiMercury(string domainUrl)
            : this(domainUrl, WikiaSettings.RelativeApiUrl)
        {

        }

        public WikiMercury(string domainUrl, string relativeApiUrl)
            : this(domainUrl, relativeApiUrl, new WikiaHttpClient())
        {

        }

        public WikiMercury(string domainUrl, string relativeApiUrl, IWikiaHttpClient wikiaHttpClient)
        {
            _wikiApiUrl = UrlHelper.GenerateApiUrl(domainUrl, relativeApiUrl);
            _wikiaHttpClient = wikiaHttpClient;
        }

        public async Task<WikiDataContainer> WikiVariables()
        {
            var requestUrl = UrlHelper.GenerateApiUrl(_wikiApiUrl, _wikiVariablesUrl);
            var json = await _wikiaHttpClient.Get(requestUrl);

            return JsonHelper.Deserialize<WikiDataContainer>(json);
        }
    }

    public class WikiDataContainer
    {
        public WikiData Data { get; set; }
    }

    public class WikiData
    {
        public string CacheBuster { get; set; }
        public string DbName { get; set; }
        public string DefaultSkin { get; set; }
        public string Id { get; set; }
        public WikiLanguageData Language { get; set; }
        public IDictionary<string, string> Namespaces { get; set; }
        public string Sitename { get; set; }
        public string MainPageTitle { get; set; }
        public List<string> WikiCategories { get; set; }
        public NavigationResultSet NavData { get; set; }
        public string Vertical { get; set; }
        public string BasePath { get; set; }
        public string IsGaSpecialWiki { get; set; }
        public string ArticlePath { get; set; }
        public string FacebookAppId { get; set; }
    }

    public class WikiLanguageData
    {
        public string User { get; set; }
        public string UserDir { get; set; }
        public string Content { get; set; }
        public string ContentDir { get; set; }
    }

    public class NavigationResultSet
    {
        /// <summary>
        /// Wrapper for navigation objects
        /// </summary>
        public NavigationItem Navigation { get; set; } 
    }

    public class NavigationItem
    {
        /// <summary>
        /// On the wiki navigation bar data,
        /// </summary>
        public List<WikiaItem> Wikia { get; set; }

        /// <summary>
        /// User set navigation bars
        /// </summary>
        public List<WikiaItem> Wiki { get; set; }
    }

    public class WikiaItem
    {
        /// <summary>
        /// On wiki navigation bar text,
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The relative URL of the Page. Absolute URL: obtained from combining relative URL with basepath attribute from response.
        /// </summary>
        public string Href { get; set; }

        /// <summary>
        /// Children collection containing article or special pages data
        /// </summary>
        public List<ChildrenItem> Children { get; set; }
    }

    public class ChildrenItem
    {
        /// <summary>
        /// Article or special page title,
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The relative URL of the Page. Absolute URL: obtained from combining relative URL with basepath attribute from response.
        /// </summary>
        public string Href { get; set; }
    }
}