using System.Threading.Tasks;
using wikia.Models.User;

namespace wikia.Api
{
    public interface IWikiUser
    {
        Task<UserResultSet> Details(UserRequestParameters requestParameters);
    }
}