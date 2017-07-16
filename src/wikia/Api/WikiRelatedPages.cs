using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wikia.Configuration;
using wikia.Helper;
using wikia.Models.RelatedPages;

namespace wikia.Api
{
    public class WikiRelatedPages : IWikiRelatedPages
    {
        private readonly IWikiaHttpClient _wikiaHttpClient;
        private readonly string _wikiApiUrl;
        private readonly string _relatedPages = "RelatedPages/List";

        public WikiRelatedPages(string domainUrl)
            : this(domainUrl, WikiaSettings.RelativeApiUrl)
        {

        }

        public WikiRelatedPages(string domainUrl, string relativeApiUrl)
            : this(domainUrl, relativeApiUrl, new WikiaHttpClient())
        {

        }

        public WikiRelatedPages(string domainUrl, string relativeApiUrl, IWikiaHttpClient wikiaHttpClient)
        {
            _wikiApiUrl = UrlHelper.GenerateApiUrl(domainUrl, relativeApiUrl);
            _wikiaHttpClient = wikiaHttpClient;
        }

        public Task<RelatedPages> Articles(params int[] ids)
        {
            return Articles(new RelatedArticlesRequestParameters {Ids = ids});
        }
        public async Task<RelatedPages> Articles(RelatedArticlesRequestParameters requestParameters)
        {
            if (requestParameters == null)
                throw new ArgumentNullException(nameof(requestParameters));

            if (requestParameters.Ids == null || !requestParameters.Ids.Any())
                throw new ArgumentException("Article Id(s) required.", nameof(requestParameters.Ids));

            var requestUrl = UrlHelper.GenerateApiUrl(_wikiApiUrl, _relatedPages);
            var parameters = GetArticlesParameters(requestParameters);
            var json = await _wikiaHttpClient.Get(requestUrl, parameters);

            return JsonHelper.Deserialize<RelatedPages>(json);
        }

        #region private helpers

        private IDictionary<string, string> GetArticlesParameters(RelatedArticlesRequestParameters requestParameters)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                ["ids"] = string.Join(",", requestParameters.Ids),
                ["limit"] = requestParameters.Limit.ToString(),
            };

            return parameters;
        }

        #endregion
    }
}