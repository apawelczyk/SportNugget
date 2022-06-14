namespace SportNugget.Shared.Services.Interfaces
{
    public interface ISessionStateService
    {
        Task<T> GetSessionStateData<T>(string key);
        Task SetSessionStateData<T>(string key, T input);
    }
}
