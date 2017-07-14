using System.Threading.Tasks;
using wikia.Enums;
using wikia.Models.Activity;

namespace wikia.Api
{
    public interface IWikiActivity
    {
        Task<ActivityResponseResult> Latest();
        Task<ActivityResponseResult> Latest(ActivityRequestParameters requestParameters);
        Task<ActivityResponseResult> RecentlyChangedArticles();
        Task<ActivityResponseResult> RecentlyChangedArticles(ActivityRequestParameters requestParameters);
        Task<ActivityResponseResult> Activity(ActivityRequestParameters requestParameters, ActivityEndpoint endpoint);
    }
}