using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wikia.Configuration;
using wikia.Helper;
using wikia.Models.SearchSuggestions;

namespace wikia.Api
{
    public class WikiSearchSuggestions : IWikiSearchSuggestions
    {
        private readonly IWikiaHttpClient _wikiaHttpClient;
        private readonly string _wikiApiUrl;
        private readonly string _searchSuggestionsList = "SearchSuggestions/List";

        public WikiSearchSuggestions(string domainUrl)
            : this(domainUrl, WikiaSettings.RelativeApiUrl)
        {

        }

        public WikiSearchSuggestions(string domainUrl, string relativeApiUrl)
            : this(domainUrl, relativeApiUrl, new WikiaHttpClient())
        {

        }

        public WikiSearchSuggestions(string domainUrl, string relativeApiUrl, IWikiaHttpClient wikiaHttpClient)
        {
            _wikiApiUrl = UrlHelper.GenerateApiUrl(domainUrl, relativeApiUrl);
            _wikiaHttpClient = wikiaHttpClient;
        }

        public async Task<SearchSuggestionsPhrases> SuggestedPhrases(string query)
        {
            if(string.IsNullOrWhiteSpace(query))
                throw new ArgumentException("Search suggestion query required.", nameof(query));

            var requestUrl = UrlHelper.GenerateApiUrl(_wikiApiUrl, _searchSuggestionsList);
            var parameters = new Dictionary<string, string>{ ["query"] = query};
            var json = await _wikiaHttpClient.Get(requestUrl, parameters);

            return JsonHelper.Deserialize<SearchSuggestionsPhrases>(json);
        }

    }
}