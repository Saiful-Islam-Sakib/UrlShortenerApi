using Microsoft.Extensions.Configuration;
using Serilog;
using UrlShortener.Common.Interface;

namespace UrlShortener.Services.Service.LoggerService
{
    public class LoggerManagerService : ILoggerManagerService
    {
        private static ILogger? _logger;

        public LoggerManagerService(IConfiguration configuration)
        {
            _logger = new LoggerConfiguration().ReadFrom.Configuration(configuration, sectionName: nameof(Serilog)).CreateLogger();
		}
        public void LogDebug(string message) => _logger.Debug(message);

        public void LogError(string message) => _logger.Error(message);

        public void LogInfo(string message) => _logger.Information(message);

        public void LogWarn(string message) => _logger.Warning(message);
    }
}
