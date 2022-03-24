using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace SportNugget.Logging
{
    public class Logger : Interfaces.ILogger, ILoggerProvider
    {
        //private Serilog.ILogger _logger;

        public Microsoft.Extensions.Logging.ILogger CreateLogger(string categoryName)
        {
            var config = new ConfigurationBuilder()
                .Build();

            //var levelSwitch = new LoggingLevelSwitch();
            //_logger = new LoggerConfiguration()
            //    .MinimumLevel.ControlledBy(levelSwitch)
            //    .Enrich.WithProperty("InstanceId", Guid.NewGuid().ToString("n"))
            //    //.WriteTo.BrowserHttp(controlLevelSwitch: levelSwitch)
            //    //.WriteTo.BrowserConsole()
            //    .WriteTo.File(@"log.txt", Serilog.Events.LogEventLevel.Information, rollingInterval: RollingInterval.Day)
            //    .CreateLogger();

            //Log.Information("Hello, browser!");

            return null;
        }

        public void Dispose()
        {

        }

        public async Task LogError(Exception exception, string message, params object[] extraProperties)
        {
            return;
        }

        public async Task LogInfo(string message, params object[] extraProperties)
        {
            return;
        }
    }
}
