using Microsoft.EntityFrameworkCore;
using UrlShortener.API.CustomMiddleware;
using UrlShortener.Common.Interface;
using UrlShortener.Common.ConfigurationModel;
using UrlShortener.Repository.Context;
using UrlShortener.Common.Interface.Repository;
using UrlShortener.Repository.Service;
using UrlShortener.Services.Service.LoggerService;
using UrlShortener.Services.Service.ShortUrlService;
using UrlShortener.Services.Service.HelperService;

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
			// service layer
			services.AddTransient<IShortUrlService, ShortUrlService>();
			services.AddSingleton<IUniqueIdGeneratorService, UniqueIdGeneratorService>();

			// data access layer
			services.AddTransient<IShortUrlRepository, ShortUrlRepository>();
		}

		public static void ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<SnowFlakeConfigurationSettings>(configuration.GetSection(nameof(SnowFlakeConfigurationSettings)));
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
			services.AddDbContext<ShortUrlDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(nameof(ShortUrlDbContext)),
				optionBuilder => optionBuilder.MigrationsAssembly("UrlShortener.API")));
		}
	}
}
