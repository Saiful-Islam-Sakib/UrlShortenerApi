using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using UrlShortener.Common.Interface;

namespace UrlShortener.Services.Service.CachingService
{
	public class InMemoryCacheManagerService : ICacheService
	{
		private readonly IMemoryCache _memoryCache;

		public InMemoryCacheManagerService(IMemoryCache memoryCache)
		{
			_memoryCache = memoryCache;
		}

		public T? GetData<T>(string key, Func<T> dbOperation) where T : class
		{
			T? value = null;

			T? cachedData = (T)_memoryCache.Get(key);

			if (cachedData == null)
			{
				value = dbOperation();

				if (value != null)
				{
					_memoryCache.Set(key, value);
				}
			}
			else
			{
				value = cachedData;
			}

			return value;
		}

		public void SetData<T>(string key, T value, Func<T> dbOperation) where T : class
		{
			dbOperation();
			_memoryCache.Set(key, value);
		}

		public void RemoveData<T>(string key, Func<T> dbOperation) where T : class
		{
			dbOperation();
			_memoryCache.Remove(key);
		}
	}
}
