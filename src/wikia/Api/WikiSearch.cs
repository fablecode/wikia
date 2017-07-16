using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wikia.Configuration;
using wikia.Helper;
using wikia.Models.Search;

namespace wikia.Api
{
    public class WikiSearch : IWikiSearch
    {
        private readonly IWikiaHttpClient _wikiaHttpClient;
        private readonly string _wikiApiUrl;
        private readonly string _searchList = "Search/List";

        public WikiSearch(string domainUrl)
            : this(domainUrl, WikiaSettings.RelativeApiUrl)
        {

        }

        public WikiSearch(string domainUrl, string relativeApiUrl)
            : this(domainUrl, relativeApiUrl, new WikiaHttpClient())
        {

        }

        public WikiSearch(string domainUrl, string relativeApiUrl, IWikiaHttpClient wikiaHttpClient)
        {
            _wikiApiUrl = UrlHelper.GenerateApiUrl(domainUrl, relativeApiUrl);
            _wikiaHttpClient = wikiaHttpClient;
        }

        public async Task<LocalWikiSearchResultSet> SearchList(SearchListRequestParameter requestParameters)
        {
            if (requestParameters == null)
                throw new ArgumentNullException(nameof(requestParameters));

            var requestUrl = UrlHelper.GenerateApiUrl(_wikiApiUrl, _searchList);
            var parameters = GetSearchListParameters(requestParameters);
            var json = await _wikiaHttpClient.Get(requestUrl, parameters);

            return JsonHelper.Deserialize<LocalWikiSearchResultSet>(json);
        }

        #region private helpers

        private IDictionary<string, string> GetSearchListParameters(SearchListRequestParameter requestParameters)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                ["query"] = string.Join(",", requestParameters.Query),
                ["limit"] = requestParameters.Limit.ToString(),
                ["minArticleQuality"] = requestParameters.MinArticleQuality.ToString(),
                ["batch"] = requestParameters.Batch.ToString(),
                ["namespaces"] = string.Join(",", requestParameters.Namespaces)
            };

            if (!string.IsNullOrEmpty(requestParameters.Type))
                parameters["type"] = requestParameters.Type;

            if (!string.IsNullOrEmpty(requestParameters.Type))
                parameters["rank"] = requestParameters.Rank;

            return parameters;
        }

        #endregion
    }
}