namespace SportNugget.Caching.Interfaces
{
    public interface ICacheManager
    {
        Task<bool> SetItem<T>(string key, T data, TimeSpan? expireTime = null);
        Task<T> GetItem<T>(string key);
        Task<bool> SetItem(string key, object data, TimeSpan? expireTime = null);
        Task<string> GetItem(string key);
    }
}
