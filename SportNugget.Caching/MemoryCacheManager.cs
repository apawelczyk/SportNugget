using SportNugget.Caching.Interfaces;

namespace SportNugget.Caching
{
    public class MemoryCacheManager : ICacheManager
    {
        public Task<T> GetItem<T>(string key)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetItem(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetItem<T>(string key, T data, TimeSpan? expireTime = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetItem(string key, object data, TimeSpan? expireTime = null)
        {
            throw new NotImplementedException();
        }
    }
}
