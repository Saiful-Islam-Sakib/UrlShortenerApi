using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UrlShortener.Common.Models;
using UrlShortener.DAService.Context;
using UrlShortener.DAService.Interface;

namespace UrlShortener.DAService.Service
{
	public class ShortUrlDAService : IShortUrlDAService
	{
		public readonly ShortUrlDbContext _ShortUrlDbContext;

		public ShortUrlDAService(ShortUrlDbContext shortUrlDbContext)
		{
			_ShortUrlDbContext = shortUrlDbContext;
		}

		public ShortUrl GetById(string id)
		{
			ShortUrl oShortUrl = _ShortUrlDbContext.tblShortUrl.FirstOrDefault(item => item.ID == id);
			return oShortUrl;
		}

		public ShortUrl GetByUrl(string url)
		{
			ShortUrl oShortUrl = _ShortUrlDbContext.tblShortUrl.FirstOrDefault(item => item.MainUrl == url);
			return oShortUrl;
		}

		public void Save(ShortUrl shrtUrlsToBeSaved)
		{
			_ShortUrlDbContext.tblShortUrl.Add(shrtUrlsToBeSaved);
			_ShortUrlDbContext.SaveChangesAsync();
		}
	}
}
