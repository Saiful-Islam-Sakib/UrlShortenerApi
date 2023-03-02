using UrlShortener.Common.Models;
using UrlShortener.Common.Interface.Repository;
using UrlShortener.Repository.Context;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using UrlShortener.Services.Service.CachingManagerService;

namespace UrlShortener.DBServices.Service
{
	public class ShortUrlDBService : IShortUrlDBService
	{
		public readonly ShortUrlDbContext _shortUrlDbContext;
		public readonly CacheManagerService _cacheManagerService;

		public ShortUrlDBService(
			ShortUrlDbContext shortUrlDbContext,
			CacheManagerService cacheManagerService)
		{
			_shortUrlDbContext = shortUrlDbContext;
			_cacheManagerService = cacheManagerService;
		}

		public ShortUrl GetById(string id)
		{
			ShortUrl? oShortUrl = _cacheManagerService.GetData<ShortUrl>(id, () => _shortUrlDbContext.ShortUrl.FirstOrDefault(item => item.ID == id));

			return oShortUrl;
		}

		public ShortUrl GetByUrl(string url)
		{
			ShortUrl? oShortUrl = _cacheManagerService.GetData<ShortUrl>(url, () => _shortUrlDbContext.ShortUrl.FirstOrDefault(item => item.Url == url));

			return oShortUrl;
		}

		public void Save(ShortUrl shrtUrlsToBeSaved)
		{
			_cacheManagerService.SetData<ShortUrl>(shrtUrlsToBeSaved.ID, shrtUrlsToBeSaved, () =>
			{
				_shortUrlDbContext.ShortUrl.Add(shrtUrlsToBeSaved);
				_shortUrlDbContext.SaveChangesAsync();

				return shrtUrlsToBeSaved;
			});
		}
	}
}
