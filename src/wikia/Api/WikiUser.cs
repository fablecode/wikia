using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wikia.Configuration;
using wikia.Helper;
using wikia.Models.User;

namespace wikia.Api
{
    public class WikiUser : IWikiUser
    {
        private readonly IWikiaHttpClient _wikiaHttpClient;
        private readonly string _wikiApiUrl;
        private readonly string _user = "User/Details";

        public WikiUser(string domainUrl)
            : this(domainUrl, WikiaSettings.RelativeApiUrl)
        {

        }

        public WikiUser(string domainUrl, string relativeApiUrl)
            : this(domainUrl, relativeApiUrl, new WikiaHttpClient())
        {

        }

        public WikiUser(string domainUrl, string relativeApiUrl, IWikiaHttpClient wikiaHttpClient)
        {
            _wikiApiUrl = UrlHelper.GenerateApiUrl(domainUrl, relativeApiUrl);
            _wikiaHttpClient = wikiaHttpClient;
        }

        public async Task<UserResultSet> Details(UserRequestParameters requestParameters)
        {
            if (requestParameters == null)
                throw new ArgumentNullException(nameof(requestParameters));

            if(requestParameters.Ids == null || !requestParameters.Ids.Any())
                throw new ArgumentException("User Ids are required.", nameof(requestParameters.Ids));

            if (requestParameters.Ids.Count > 100)
                throw new ArgumentException("Maximum size of id list is 100", nameof(requestParameters.Ids));

            var requestUrl = UrlHelper.GenerateApiUrl(_wikiApiUrl, _user);
            var parameters = GetDetailsParameters(requestParameters);
            var json = await _wikiaHttpClient.Get(requestUrl, parameters);

            return JsonHelper.Deserialize<UserResultSet>(json);
        }

        private IDictionary<string, string> GetDetailsParameters(UserRequestParameters requestParameters)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>
            {
                ["ids"] = string.Join(",", requestParameters.Ids),
                ["size"] = requestParameters.Size.ToString(),
            };

            return parameters;
        }
    }
}