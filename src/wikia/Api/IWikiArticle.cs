using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using wikia.Enums;
using wikia.Models.Article;
using wikia.Models.Article.AlphabeticalList;
using wikia.Models.Article.Details;
using wikia.Models.Article.NewArticles;
using wikia.Models.Article.PageList;
using wikia.Models.Article.Popular;
using wikia.Models.Article.Simple;

namespace wikia.Api
{
    public interface IWikiArticle
    {
        Task<ContentResult> Simple(int id);
        Task<ExpandedArticleResultSet> Details(ArticleDetailsRequestParameters requestParameters);
        Task<string> ArticleRequest(ArticleEndpoint endpoint, Func<IDictionary<string, string>> getParameters);
        Task<ExpandedArticleResultSet> Details(params int[] ids);
        Task<UnexpandedListArticleResultSet> AlphabeticalList(string category);
        Task<UnexpandedListArticleResultSet> AlphabeticalList(ArticleListRequestParameters requestParameters);
        Task<ExpandedListArticleResultSet> PageList(ArticleListRequestParameters requestParameters);
        Task<ExpandedListArticleResultSet> PageList(string category);
        Task<NewArticleResultSet> NewArticles(NewArticleRequestParameters requestParameters);
        Task<PopularListArticleResultSet> PopularArticleSimple(PopularArticleRequestParameters requestParameters);
        Task<PopularExpandedArticleResultSet> PopularArticleDetail(PopularArticleRequestParameters requestParameters);
        Task<T> PopularArticle<T>(PopularArticleRequestParameters requestParameters, bool expand);
    }
}