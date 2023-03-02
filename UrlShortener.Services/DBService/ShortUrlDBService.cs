using UrlShortener.Common.Models;
using UrlShortener.Common.Interface.Repository;
using UrlShortener.Repository.Context;
using UrlShortener.Common.Interface;
using UrlShortener.Shared.Enums;
using Microsoft.Extensions.Configuration;

namespace UrlShortener.DBServices.Service
{
	public class ShortUrlDBService : IShortUrlDBService
	{
		private readonly ShortUrlDbContext _shortUrlDbContext;
		private readonly ICacheService _cacheService;
		private readonly ICacheFactory _cacheFactory;

		public ShortUrlDBService(
			ShortUrlDbContext shortUrlDbContext,
			ICacheFactory cacheFactory)
		{
			_shortUrlDbContext = shortUrlDbContext;
			_cacheFactory = cacheFactory;
			_cacheService = _cacheFactory.GetCacheProvide();
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
