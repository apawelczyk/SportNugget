using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace SportNugget.Logging
{
    public class Logger : Interfaces.ILogger, ILoggerProvider
    {
        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            return null;
        }

        public void Dispose()
        {

        }

        public async Task LogError(Exception exception, string message, params object[] extraProperties)
        {
            Log.Error(exception, message, extraProperties);
            return;
        }

        public async Task LogInfo(string message, params object[] extraProperties)
        {
            Log.Information(message, extraProperties);
            return;
        }
    }
}
