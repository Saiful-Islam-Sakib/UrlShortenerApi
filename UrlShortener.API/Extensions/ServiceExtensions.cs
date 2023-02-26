using UrlShortener.ApiService.Interface;
using UrlShortener.ApiService.Service;

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
			services.AddTransient<IShortUrl, ShortUrlService>();
			services.AddTransient<IUniqueIdGenerator, UniqueIdGeneratorService>();
		}

		public static void ConfigureSqlDBContext(this IServiceCollection services, IConfiguration configuration) 
		{ 

		}
	}
}
