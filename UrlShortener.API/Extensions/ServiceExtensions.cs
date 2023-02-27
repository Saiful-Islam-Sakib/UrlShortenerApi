using Microsoft.Extensions.Configuration;
using UrlShortener.API.CustomMiddleware;
using UrlShortener.ApiService.Interface;
using UrlShortener.ApiService.Service;
using UrlShortener.Common.ConfigurationModel;

namespace UrlShortener.API.Extensions
{
    public static class ServiceExtensions
	{
		public static void ConfigureCors(this IServiceCollection service, string corsName = "CorsPolicy")
		{
			service.AddCors(options =>
			{
				options.AddPolicy(corsName, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
			});
		}

		public static void ConfigureCommonService(this IServiceCollection services)
		{
			services.AddTransient<IShortUrlService, ShortUrlService>();
			services.AddSingleton<IUniqueIdGeneratorService, UniqueIdGeneratorService>();
		}

		public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<SnowFlakeConfigurationSettings>(configuration.GetSection("SnowFlakeConfigurationSettings"));
		}

		public static void ConfigureLoggerService(this IServiceCollection services)
		{
			services.AddSingleton<ILoggerManagerService, LoggerManagerService>();
		}

		public static IApplicationBuilder UseProcessingTimeCalculatorMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ProcessingTimeCalculatorMiddleware>();
		}

		public static void ConfigureSqlDBContext(this IServiceCollection services, IConfiguration configuration) 
		{ 

		}
	}
}
