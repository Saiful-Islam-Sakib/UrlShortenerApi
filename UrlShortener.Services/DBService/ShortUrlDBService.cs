using UrlShortener.Common.Models;
using UrlShortener.Common.Interface.Repository;
using UrlShortener.Repository.Context;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using UrlShortener.Services.Service.CachingManagerService;
using UrlShortener.Common.Interface;

namespace UrlShortener.DBServices.Service
{
	public class ShortUrlDBService : IShortUrlDBService
	{
		public readonly ShortUrlDbContext _shortUrlDbContext;
		public readonly ICacheService _cacheService;

		public ShortUrlDBService(
			ShortUrlDbContext shortUrlDbContext,
			ICacheService cacheManagerService)
		{
			_shortUrlDbContext = shortUrlDbContext;
			_cacheService = cacheManagerService;
		}

		public ShortUrl GetById(string id)
		{
			ShortUrl? oShortUrl = _cacheService.GetData<ShortUrl>(id, () => _shortUrlDbContext.ShortUrl.FirstOrDefault(item => item.ID == id));

			return oShortUrl;
		}

		public ShortUrl GetByUrl(string url)
		{
			ShortUrl? oShortUrl = _cacheService.GetData<ShortUrl>(url, () => _shortUrlDbContext.ShortUrl.FirstOrDefault(item => item.Url == url));

			return oShortUrl;
		}

		public void Save(ShortUrl shrtUrlsToBeSaved)
		{
			_cacheService.SetData<ShortUrl>(shrtUrlsToBeSaved.ID, shrtUrlsToBeSaved, () =>
			{
				_shortUrlDbContext.ShortUrl.Add(shrtUrlsToBeSaved);
				_shortUrlDbContext.SaveChangesAsync();

				return shrtUrlsToBeSaved;
			});
		}
	}
}
