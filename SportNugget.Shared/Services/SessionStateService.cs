using SportNugget.Common.API;
using SportNugget.Common.API.Interfaces;
using SportNugget.Common.Formatters;
using SportNugget.Logging.Interfaces;
using SportNugget.Shared.Services.Interfaces;

namespace SportNugget.Shared.Services
{
    public class SessionStateService : ISessionStateService
    {
        private readonly IDataAccessManager _dataAccessManager;
        private readonly ILogger _logger;

        public SessionStateService(IDataAccessManager dataAccessManager, ILogger logger)
        {
            _dataAccessManager = dataAccessManager;
            _logger = logger;
        }

        public async Task<T> GetSessionStateData<T>(string key)
        {
            try
            {
                var result = await _dataAccessManager.Get<string>($"api/SessionState/{key}");
                if(typeof(T) == typeof(string))
                {
                    return (T)Convert.ChangeType(result, typeof(T)); ;
                }
                if(result != null)
                {
                    var convertedData = StringConverter.Convert<T>(result);
                    return convertedData;
                }
                return default(T);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Error getting session data from API.");
                return default(T);
            }
        }

        public async Task SetSessionStateData<T>(string key, T input)
        {
            try
            {
                var result = await _dataAccessManager.Post<object>($"api/SessionState/{key}", input);

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error setting session data from API.");
            }
        }
    }
}
