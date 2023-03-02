using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UrlShortener.Common.Interface;
using UrlShortener.Common.Models;

namespace UrlShortener.Services.Service.CachingManagerService
{
    public class CacheManagerService : ICacheService
    {
		private readonly IDistributedCache _distributedCache;
        
        public CacheManagerService(IConfiguration configuration, IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
		}

		public T? GetData<T>(string key, Func<T> dbOperation) where T : class
		{
			T? value = null;

			string? cachedData = _distributedCache.GetString(key);

			if (string.IsNullOrEmpty(cachedData))
			{
				value = dbOperation();

				if (value != null)
				{
					_distributedCache.SetString(key, JsonConvert.SerializeObject(value));
				}
			}
			else
			{
				value = JsonConvert.DeserializeObject<T>(cachedData);
			}

			return value;
		}

		public void SetData<T>(string key, T value, Func<T> dbOperation) where T : class
		{
			dbOperation();
			_distributedCache.SetString(key, JsonConvert.SerializeObject(value));
		}

		public void RemoveData<T>(string key, Func<T> dbOperation) where T : class
		{
			dbOperation();
			_distributedCache.Remove(key);
		}
	}
}
