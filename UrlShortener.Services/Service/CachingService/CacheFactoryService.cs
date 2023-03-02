using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using UrlShortener.Common.Interface;
using UrlShortener.Shared.ConfigurationModel;
using UrlShortener.Shared.Enums;

namespace UrlShortener.Services.Service.CachingService
{
	public class CacheFactoryService : ICacheFactory
	{
		private readonly Func<EnumCache, ICacheService> _cacheService;
		private readonly IOptions<CacheConfigurationSettings> _cacheConfigurationSettings;
		private readonly CacheConfigurationSettings _oCacheConfigurationSettings;

		public CacheFactoryService(
			IOptions<CacheConfigurationSettings> cacheConfigurationSettings,
			Func<EnumCache, ICacheService> cacheService)
		{
			_cacheService = cacheService;
			_cacheConfigurationSettings = cacheConfigurationSettings;
			_oCacheConfigurationSettings = _cacheConfigurationSettings.Value;
		}

		public ICacheService GetCacheProvide()
		{
			return this._cacheService((EnumCache)_oCacheConfigurationSettings.UseCache); 
		}
	}
}
