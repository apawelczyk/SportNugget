namespace SportNugget.Shared.Services.Interfaces
{
    public interface ISessionStateService
    {
        T GetSessionStateData<T>(string key);
        void SetSessionStateData<T>(string key, T input);
    }
}
