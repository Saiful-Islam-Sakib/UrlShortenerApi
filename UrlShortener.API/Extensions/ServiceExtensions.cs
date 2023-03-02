using Microsoft.EntityFrameworkCore;
using UrlShortener.API.CustomMiddleware;
using UrlShortener.Common.Interface;
using UrlShortener.Common.ConfigurationModel;
using UrlShortener.Repository.Context;
using UrlShortener.Common.Interface.Repository;
using UrlShortener.DBServices.Service;
using UrlShortener.Services.Service.LoggerService;
using UrlShortener.Services.Service.ShortUrlService;
using UrlShortener.Services.Service.HelperService;
using UrlShortener.Services.Service.CachingService;
using UrlShortener.Shared.Enums;
using UrlShortener.Shared.ConfigurationModel;

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
			// service
			services.AddTransient<IShortUrlService, ShortUrlService>();
			services.AddSingleton<IUniqueIdGeneratorService, UniqueIdGeneratorService>();

			// repository
			services.AddScoped<IShortUrlDBService, ShortUrlDBService>();

			// Logger
			services.AddSingleton<ILoggerManagerService, LoggerManagerService>();
		}

		public static void ConfigureCachingServices(this IServiceCollection services, IConfiguration configuration)
		{
			// cache
			services.AddMemoryCache();
			services.AddSingleton<ICacheFactory, CacheFactoryService>();
			services.AddSingleton<RedisCacheManagerService>();
			services.AddSingleton<InMemoryCacheManagerService>();

			// resolve service for ICacheService type
			services.AddSingleton<Func<EnumCache, ICacheService>>(serviceProvider => key =>
			{
				return key switch
				{
					EnumCache.Redis => serviceProvider.GetService<RedisCacheManagerService>(),
					EnumCache.InMemory => serviceProvider.GetService<InMemoryCacheManagerService>(),
					_ => throw new NotImplementedException(),
				};
			});
		}

		public static void ConfigureSnowflakeSettings(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<SnowFlakeConfigurationSettings>(configuration.GetSection(nameof(SnowFlakeConfigurationSettings)));
		}

		public static void ConfigureCacheSettings(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<CacheConfigurationSettings>(configuration.GetSection(nameof(CacheConfigurationSettings)));
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

		public static void ConfigureRedisDBContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddStackExchangeRedisCache(options => options.Configuration = configuration.GetConnectionString("RedisDbContext"));
		}
	}
}
