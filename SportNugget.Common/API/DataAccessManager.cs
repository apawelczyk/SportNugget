using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using SportNugget.Common.API.Interfaces;
using SportNugget.Common.Configuration.Interfaces;
using SportNugget.Logging.Interfaces;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace SportNugget.Common.API
{
    public class DataAccessManager : IDataAccessManager
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger _logger;
        private readonly IRestClientConfig _restClientConfig;

        public DataAccessManager(IHttpClientFactory httpClientFactory, ILogger logger, IRestClientConfig restClientConfig)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _restClientConfig = restClientConfig;
        }

        public async Task<T> Get<T>(string apiRoute, string bearerToken = null)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(_restClientConfig.ClientName);
                if (!string.IsNullOrWhiteSpace(bearerToken))
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
                }
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, IncludeFields = true };

                var response = await client.GetFromJsonAsync<T>(apiRoute, options);
                return (response is not null ? response : default(T));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error with GET.");
                return default(T);
            }
        }

        public async Task<T> Post<T>(string apiRoute, T postObject, string bearerToken = null)
        {
            try
            {
                var client = _httpClientFactory.CreateClient(_restClientConfig.ClientName);
                if (!string.IsNullOrWhiteSpace(bearerToken))
                {
                    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
                }
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true, IncludeFields = true };

                var response = await client.PostAsJsonAsync<T>(apiRoute, postObject);
                if (response?.IsSuccessStatusCode ?? false)
                {
                    var responseContent = await response?.Content?.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(responseContent))
                    {
                        var deserializedResult = JsonConvert.DeserializeObject<T>(responseContent);
                        if (deserializedResult != null)
                        {
                            return deserializedResult;
                        }
                    }
                }

                return default(T);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error with POST.");
                return default(T);
            }
        }
    }
}
