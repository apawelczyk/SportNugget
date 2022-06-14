using Microsoft.Extensions.Caching.Distributed;
using SportNugget.Caching.Interfaces;
using SportNugget.Common.Formatters;
using SportNugget.Logging.Interfaces;
using System.Runtime.Serialization.Formatters.Binary;

namespace SportNugget.Caching
{
    public class DistributedCacheManager : ICacheManager
    {
        private readonly IDistributedCache _cache;
        private readonly ILogger _logger;

        public DistributedCacheManager(IDistributedCache cache, ILogger logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public async Task<T> GetItem<T>(string key)
        {
            try
            {
                var data = await _cache.GetStringAsync(key);

                if (data == null) 
                {
                    return default(T);
                };

                return StringConverter.Convert<T>(data);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error getting item - {key} from distributed cache.");
                return default(T);
            }
        }

        public async Task<string> GetItem(string key)
        {
            try
            {
                var data = await _cache.GetStringAsync(key);
                return data;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error getting item - {key} from distributed cache.");
                return null;
            }
        }

        public async Task<bool> SetItem<T>(string key, T data, TimeSpan? expireTime = null)
        {
            try
            {
                var options = new DistributedCacheEntryOptions();

                var stringData = StringConverter.Convert(data);

                // Might let saving do async in future.
                await _cache.SetStringAsync(key, stringData, options);
                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e, $"Error setting item - {key} to distributed cache.");
                return false;
            }
        }

        public async Task<bool> SetItem(string key, object data, TimeSpan? expireTime = null)
        {
            try
            {
                var options = new DistributedCacheEntryOptions();

                var stringData = StringConverter.Convert(data);

                // Might let saving do async in future.
                await _cache.SetStringAsync(key, stringData, options);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error setting item - {key} to distributed cache.");
                return false;
            }
        }
    }
}
