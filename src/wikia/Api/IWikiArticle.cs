using System.Threading.Tasks;
using wikia.Models.Article.Details;
using wikia.Models.Article.Simple;

namespace wikia.Api
{
    public interface IWikiArticle
    {
        Task<ContentResult> Simple(int id);
        Task<ExpandedArticleResultSet> Details(ArticleDetailsRequestParameters requestParameters);
    }
}