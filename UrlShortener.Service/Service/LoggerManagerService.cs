using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.ApiService.Interface;

namespace UrlShortener.ApiService.Service
{
	public class LoggerManagerService : ILoggerManagerService
	{
		private static IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

		private static ILogger _logger = new LoggerConfiguration().ReadFrom.Configuration(configuration, sectionName: "Serilog").CreateLogger();

		public void LogDebug(string message) => _logger.Debug(message);

		public void LogError(string message) => _logger.Error(message);

		public void LogInfo(string message) => _logger.Information(message);

		public void LogWarn(string message) => _logger.Warning(message);
	}
}
