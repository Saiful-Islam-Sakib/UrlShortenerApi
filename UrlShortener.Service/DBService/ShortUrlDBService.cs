using UrlShortener.Common.Models;
using UrlShortener.Common.Interface.Repository;
using UrlShortener.Repository.Context;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace UrlShortener.DBServices.Service
{
	public class ShortUrlDBService : IShortUrlDBService
	{
		public readonly ShortUrlDbContext _shortUrlDbContext;
		private readonly IDistributedCache _distributedCache;

		public ShortUrlDBService(
			ShortUrlDbContext shortUrlDbContext, 
			IDistributedCache distributedCache)
		{
			_shortUrlDbContext = shortUrlDbContext;
			_distributedCache = distributedCache;
		}

		public ShortUrl GetById(string id)
		{
			ShortUrl? oShortUrl = null;

			string? chachedShortUrl = _distributedCache.GetString(id);

			if (string.IsNullOrEmpty(chachedShortUrl))
			{
				oShortUrl = _shortUrlDbContext.ShortUrl.FirstOrDefault(item => item.ID == id);

				if (oShortUrl != null)
				{
					_distributedCache.SetString(id, JsonConvert.SerializeObject(oShortUrl));
				}
			}
			else
			{
				oShortUrl = JsonConvert.DeserializeObject<ShortUrl>(chachedShortUrl);
			}

			return oShortUrl;
		}

		public ShortUrl GetByUrl(string url)
		{
			ShortUrl? oShortUrl = null;

			string? chachedShortUrl = _distributedCache.GetString(url);

			if (string.IsNullOrEmpty(chachedShortUrl))
			{
				oShortUrl = _shortUrlDbContext.ShortUrl.FirstOrDefault(item => item.Url == url);

				if (oShortUrl != null)
				{
					_distributedCache.SetString(url, JsonConvert.SerializeObject(oShortUrl));
				}
			}
			else
			{
				oShortUrl = JsonConvert.DeserializeObject<ShortUrl>(chachedShortUrl);
			}

			return oShortUrl;
		}

		public void Save(ShortUrl shrtUrlsToBeSaved)
		{
			_shortUrlDbContext.ShortUrl.Add(shrtUrlsToBeSaved);
			_shortUrlDbContext.SaveChangesAsync();

			_distributedCache.SetString(shrtUrlsToBeSaved.ID, JsonConvert.SerializeObject(shrtUrlsToBeSaved));
		}
	}
}
