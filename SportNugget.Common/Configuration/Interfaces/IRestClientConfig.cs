namespace SportNugget.Common.Configuration.Interfaces
{
    public interface IRestClientConfig
    {
        string ClientName { get; }
        string BaseUrl { get; }
    }
}