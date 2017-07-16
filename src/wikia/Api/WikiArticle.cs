using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wikia.Configuration;
using wikia.Enums;
using wikia.Helper;
using wikia.Models.Article;
using wikia.Models.Article.AlphabeticalList;
using wikia.Models.Article.Details;
using wikia.Models.Article.Simple;

namespace wikia.Api
{
    public class WikiArticle : IWikiArticle
    {
        private readonly IWikiaHttpClient _wikiaHttpClient;
        private readonly string _wikiApiUrl;

        private static readonly Dictionary<ArticleEndpoint, string> Endpoints;

        static WikiArticle()
        {
            Endpoints = new Dictionary<ArticleEndpoint, string>
            {
                {ArticleEndpoint.Simple, "Articles/AsSimpleJson"},
                {ArticleEndpoint.Details, "Articles/Details"},
                {ArticleEndpoint.List, "Articles/List"}
            };
        }

        public WikiArticle(string domainUrl)
            : this(domainUrl, WikiaSettings.RelativeApiUrl)
        {

        }

        public WikiArticle(string domainUrl, string relativeApiUrl)
            : this(domainUrl, relativeApiUrl, new WikiaHttpClient())
        {

        }

        public WikiArticle(string domainUrl, string relativeApiUrl, IWikiaHttpClient wikiaHttpClient)
        {
            _wikiApiUrl = UrlHelper.GenerateApiUrl(domainUrl, relativeApiUrl);
            _wikiaHttpClient = wikiaHttpClient;
        }

        public async Task<ContentResult> Simple(int id)
        {
            if(id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));

            var json = await ArticleRequest(ArticleEndpoint.Simple, () => new Dictionary<string, string> { ["id"] = id.ToString() });

            return Deserialize<ContentResult>(json);
        }

        public Task<ExpandedArticleResultSet> Details(params int[] ids)
        {
            return Details(new ArticleDetailsRequestParameters(ids));
        }

        public async Task<ExpandedArticleResultSet> Details(ArticleDetailsRequestParameters requestParameters)
        {
            if (requestParameters == null)
                throw new ArgumentNullException(nameof(requestParameters));

            var json = await ArticleRequest(ArticleEndpoint.Details, () => GetDetailsParameters(requestParameters));

            return Deserialize<ExpandedArticleResultSet>(json);
        }

        public Task<UnexpandedListArticleResultSet> AlphabeticalList(string category)
        {
            return AlphabeticalList(new ArticleListRequestParameters {Category = category});
        }

        public async Task<UnexpandedListArticleResultSet> AlphabeticalList(ArticleListRequestParameters requestParameters)
        {
            if (requestParameters == null)
                throw new ArgumentNullException(nameof(requestParameters));

            var json = await ArticleRequest(ArticleEndpoint.List, () => GetAlphabeticalListParameters(requestParameters));

            return Deserialize<UnexpandedListArticleResultSet>(json);

        }

        public Task<string> ArticleRequest(ArticleEndpoint endpoint, Func<IDictionary<string, string>> getParameters)
        {
            var requestUrl = GenerateApiUrl(endpoint);
            var parameters = getParameters.Invoke();
            return WebClient(requestUrl, parameters);
        }

        #region private helpers

        private IDictionary<string, string> GetDetailsParameters(ArticleDetailsRequestParameters requestParameters)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                ["ids"] = string.Join(",", requestParameters.Ids),
                ["abstract"] = requestParameters.Abstract.ToString(),
                ["width"] = requestParameters.ThumbnailWidth.ToString(),
                ["height"] = requestParameters.ThumbnailHeight.ToString(),
            };

            if (requestParameters.Titles != null && requestParameters.Titles.Any())
                parameters.Add("titles", string.Join(",", requestParameters.Titles));

            return parameters;
        }

        private IDictionary<string, string> GetAlphabeticalListParameters(ArticleListRequestParameters requestParameters)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                ["limit"] = requestParameters.Limit.ToString(),
            };

            if (!string.IsNullOrEmpty(requestParameters.Category))
                parameters["category"] = requestParameters.Category;

            if (requestParameters.Namespaces.Any())
                parameters["namespaces"] = string.Join(",", requestParameters.Namespaces);

            if (!string.IsNullOrEmpty(requestParameters.Offset))
                parameters["offset"] = requestParameters.Offset;

            return parameters;
        }

        private string GenerateApiUrl(ArticleEndpoint endpoint)
        {
            return UrlHelper.GenerateApiUrl(_wikiApiUrl, Endpoints[endpoint]);
        }

        private static T Deserialize<T>(string json)
        {
            return JsonHelper.Deserialize<T>(json);
        }

        private async Task<string> WebClient(string requestUrl, IDictionary<string, string> parameters)
        {
            return await _wikiaHttpClient.Get(requestUrl, parameters);
        }

        #endregion
    }
}