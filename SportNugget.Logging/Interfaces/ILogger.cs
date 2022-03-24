namespace SportNugget.Logging.Interfaces
{
    public interface ILogger
    {
        Task LogError(Exception exception, string message, params object[] extraProperties);
        Task LogInfo(string message, params object[] extraProperties);
    }
}
