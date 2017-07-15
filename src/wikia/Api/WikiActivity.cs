using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wikia.Configuration;
using wikia.Enums;
using wikia.Helper;
using wikia.Models.Activity;

namespace wikia.Api
{
    public class WikiActivity : IWikiActivity
    {
        private readonly IWikiaHttpClient _wikiaHttpClient;
        private readonly string _wikiApiUrl;

        private static readonly Dictionary<ActivityEndpoint, string> Endpoints;

        static WikiActivity()
        {
            Endpoints = new Dictionary<ActivityEndpoint, string>
            {
                {ActivityEndpoint.LatestActivity, "Activity/LatestActivity"},
                {ActivityEndpoint.RecentlyChangedArticles, "Activity/RecentlyChangedArticles"}
            };
        }


        public WikiActivity(string domainUrl)
            : this(domainUrl, WikiaSettings.RelativeApiUrl)
        {
            
        }

        public WikiActivity(string domainUrl, string relativeApiUrl)
            : this(domainUrl, relativeApiUrl, new WikiaHttpClient())
        {
            
        }

        public WikiActivity(string domainUrl, string relativeApiUrl, IWikiaHttpClient wikiaHttpClient)
        {
            _wikiApiUrl = UrlHelper.GenerateApiUrl(domainUrl, relativeApiUrl);
            _wikiaHttpClient = wikiaHttpClient;
        }

        public Task<ActivityResponseResult> Latest()
        {
            return Latest(new ActivityRequestParameters());
        }

        public Task<ActivityResponseResult> Latest(ActivityRequestParameters requestParameters)
        {
            return Activity(requestParameters, ActivityEndpoint.LatestActivity);
        }

        public Task<ActivityResponseResult> RecentlyChangedArticles()
        {
            return RecentlyChangedArticles(new ActivityRequestParameters());
        }

        public Task<ActivityResponseResult> RecentlyChangedArticles(ActivityRequestParameters requestParameters)
        {
            return Activity(requestParameters, ActivityEndpoint.RecentlyChangedArticles);
        }

        public async Task<ActivityResponseResult> Activity(ActivityRequestParameters requestParameters, ActivityEndpoint endpoint)
        {
            if(requestParameters == null)
                throw new ArgumentNullException(nameof(requestParameters));

            var requestUrl = UrlHelper.GenerateApiUrl(_wikiApiUrl, Endpoints[endpoint]);
            var parameters = GetParameters(requestParameters);
            var json = await _wikiaHttpClient.Get(requestUrl, parameters);

            return JsonHelper.Deserialize<ActivityResponseResult>(json);
        }

        #region private helpers

        private static IDictionary<string, string> GetParameters(ActivityRequestParameters requestParameters)
        {
            var parameters = new Dictionary<string, string>
            {
                ["limit"] = requestParameters.Limit.ToString(),
                ["namespaces"] = string.Join(",", requestParameters.Namespaces),
                ["allowduplicates"] = requestParameters.AllowDuplicates.ToString().ToLower()
            };

            return parameters;
        }

        #endregion
    }
}