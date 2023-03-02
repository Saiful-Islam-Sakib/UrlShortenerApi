using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using UrlShortener.Common.Interface;

namespace UrlShortener.Services.Service.CachingService
{
    public class RedisCacheManagerService : ICacheService
    {
		private readonly IDistributedCache _distributedCache;
        
        public RedisCacheManagerService(IDistributedCache distributedCache)
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
