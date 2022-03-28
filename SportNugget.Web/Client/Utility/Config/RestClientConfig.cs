using Microsoft.Extensions.Configuration;
using SportNugget.Common.Configuration.Interfaces;

namespace SportNugget.Web.Client.Utility.Config
{
    public class RestClientConfig : IRestClientConfig
    {
        private readonly IConfiguration _configuration;

        public RestClientConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ClientName
        {
            get
            {
                return _configuration[Settings.ServerAPIClientName];
            }
        }

        public string BaseUrl 
        { 
            get
            {
                return _configuration[Settings.ServerAPIBaseUrl];
            }
        }
    }
}
