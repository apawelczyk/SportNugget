using SportNugget.Common.Configuration.Interfaces;

namespace SportNugget.Web.Server.Utility.Configuration
{
    public class RestClientConfig : IRestClientConfig
    {
        private string _baseUrl;

        public RestClientConfig()
        {
            _baseUrl = "https://www.external.com/";
        }

        public string BaseUrl
        {
            get
            {
                return _baseUrl;
            }
        }

        public string ClientName => "blah";
    }
}
