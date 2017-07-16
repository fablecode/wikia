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
using wikia.Models.Article.NewArticles;
using wikia.Models.Article.PageList;
using wikia.Models.Article.Popular;
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
                {ArticleEndpoint.List, "Articles/List"},
                {ArticleEndpoint.NewArticles, "Articles/New"},
                {ArticleEndpoint.Popular, "Articles/Popular"}
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

            var json = await ArticleRequest(ArticleEndpoint.List, () => GetListParameters(requestParameters));

            return Deserialize<UnexpandedListArticleResultSet>(json);

        }

        public Task<ExpandedListArticleResultSet> PageList(string category)
        {
            return PageList(new ArticleListRequestParameters { Category = category });
        }

        public async Task<ExpandedListArticleResultSet> PageList(ArticleListRequestParameters requestParameters)
        {
            if (requestParameters == null)
                throw new ArgumentNullException(nameof(requestParameters));

            var json = await ArticleRequest(ArticleEndpoint.List, () => GetListParameters(requestParameters, true));

            return Deserialize<ExpandedListArticleResultSet>(json);
        }

        public async Task<NewArticleResultSet> NewArticles(NewArticleRequestParameters requestParameters)
        {
            if(requestParameters.Limit <= 0 || requestParameters.Limit > 100)
                throw new ArgumentOutOfRangeException(nameof(requestParameters.Limit), "Maximum limit is 100.");

            if(requestParameters.MinArticleQuality <= 0 || requestParameters.MinArticleQuality > 99)
                throw new ArgumentOutOfRangeException(nameof(requestParameters.MinArticleQuality), "Minimal value of article quality. Ranges from 0 to 99.");

            var json = await ArticleRequest(ArticleEndpoint.NewArticles, () => GetNewArticleParameters(requestParameters));

            return Deserialize<NewArticleResultSet>(json);
        }

        public Task<PopularListArticleResultSet> PopularArticleSimple(PopularArticleRequestParameters requestParameters)
        {
            return PopularArticle<PopularListArticleResultSet>(requestParameters, false);
        }

        public Task<PopularExpandedArticleResultSet> PopularArticleDetail(PopularArticleRequestParameters requestParameters)
        {
            return PopularArticle<PopularExpandedArticleResultSet>(requestParameters, true);
        }

        public async Task<T> PopularArticle<T>(PopularArticleRequestParameters requestParameters, bool expand)
        {
            if (requestParameters.Limit <= 0 || requestParameters.Limit > 10)
                throw new ArgumentOutOfRangeException(nameof(requestParameters.Limit), "Maximum limit is 10.");

            var json = await ArticleRequest(ArticleEndpoint.Popular, () => GetPopularArticleParameters(requestParameters, true));

            return Deserialize<T>(json);
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

        private IDictionary<string, string> GetListParameters(ArticleListRequestParameters requestParameters, bool expanded = false)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                ["limit"] = requestParameters.Limit.ToString(),
            };

            if(expanded)
                parameters["expand"] = "1";

            if (!string.IsNullOrEmpty(requestParameters.Category))
                parameters["category"] = requestParameters.Category;

            if (requestParameters.Namespaces.Any())
                parameters["namespaces"] = string.Join(",", requestParameters.Namespaces);

            if (!string.IsNullOrEmpty(requestParameters.Offset))
                parameters["offset"] = requestParameters.Offset;

            return parameters;
        }

        private IDictionary<string, string> GetNewArticleParameters(NewArticleRequestParameters requestParameters)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                ["limit"] = requestParameters.Limit.ToString(),
                ["minArticleQuality"] = requestParameters.MinArticleQuality.ToString(),
            };

            if (requestParameters.Namespaces.Any())
                parameters["namespaces"] = string.Join(",", requestParameters.Namespaces);

            return parameters;
        }

        private IDictionary<string, string> GetPopularArticleParameters(PopularArticleRequestParameters requestParameters, bool expanded = false)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                ["limit"] = requestParameters.Limit.ToString(),
            };

            if (expanded)
                parameters["expand"] = "1";

            if (requestParameters.BaseArticleId.HasValue)
                parameters["basearticleid"] = string.Join(",", requestParameters.BaseArticleId);

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