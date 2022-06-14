namespace SportNugget.Common.API.Interfaces
{
    public interface IDataAccessManager
    {
        Task<T> Get<T>(string apiRoute, string bearerToken = null);
        Task<T> Post<T>(string apiRoute, T postObject, string bearerToken = null);
        T PostBlah<T>(string apiRoute, T postObject, string bearerToken = null);
    }
}
