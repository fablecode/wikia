using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wikia.Configuration;
using wikia.Enums;
using wikia.Helper;
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

            var requestUrl = GenerateApiUrl(ArticleEndpoint.Simple);
            var parameters = new Dictionary<string, string> { {"id", id.ToString()}};
            var json = await _wikiaHttpClient.Get(requestUrl, parameters);

            return JsonHelper.Deserialize<ContentResult>(json);
        }


        public Task<ExpandedArticleResultSet> Details(params int[] ids)
        {
            return Details(new ArticleDetailsRequestParameters(ids));
        }

        public async Task<ExpandedArticleResultSet> Details(ArticleDetailsRequestParameters requestParameters)
        {
            if (requestParameters == null)
                throw new ArgumentNullException(nameof(requestParameters));

            var requestUrl = GenerateApiUrl(ArticleEndpoint.Details);
            var parameters = GetDetailsParameters(requestParameters);
            var json = await _wikiaHttpClient.Get(requestUrl, parameters);

            return JsonHelper.Deserialize<ExpandedArticleResultSet>(json);
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

        private string GenerateApiUrl(ArticleEndpoint endpoint)
        {
            return UrlHelper.GenerateApiUrl(_wikiApiUrl, Endpoints[endpoint]);
        }

        #endregion
    }
}