using SportNugget.Common.API;
using SportNugget.Common.API.Interfaces;
using SportNugget.Logging.Interfaces;
using SportNugget.Shared.Services.Interfaces;

namespace SportNugget.Shared.Services
{
    // TODO: Decide to save to Redis or call Web API and save to Cosmos
    public class SessionStateService : ISessionStateService
    {
        private readonly IDataAccessManager _dataAccessManager;
        private readonly ILogger _logger;

        public SessionStateService(IDataAccessManager dataAccessManager, ILogger logger)
        {
            _dataAccessManager = dataAccessManager;
            _logger = logger;
        }

        public T GetSessionStateData<T>(string key)
        {
            var tests = _dataAccessManager.Get<ResponseWrapper<T>>($"api/SessionStateData/{key}").Result;
            return tests.Data;
        }

        public void SetSessionStateData<T>(string key, T input)
        {
            var result = _dataAccessManager.Post<T>($"api/SessionStateData/{key}", input).Result;
        }
    }
}
