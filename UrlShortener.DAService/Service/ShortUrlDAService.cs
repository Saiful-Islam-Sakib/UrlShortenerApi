﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UrlShortener.Common.Models;
using UrlShortener.Common.Interface.Repository;
using UrlShortener.Repository.Context;

namespace UrlShortener.Repository.Service
{
	public class ShortUrlRepository : IShortUrlRepository
	{
		public readonly ShortUrlDbContext _shortUrlDbContext;

		public ShortUrlRepository(ShortUrlDbContext shortUrlDbContext)
		{
			_shortUrlDbContext = shortUrlDbContext;
		}

		public ShortUrl GetById(string id)
		{
			ShortUrl oShortUrl = _shortUrlDbContext.ShortUrl.FirstOrDefault(item => item.ID == id);
			return oShortUrl;
		}

		public ShortUrl GetByUrl(string url)
		{
			ShortUrl oShortUrl = _shortUrlDbContext.ShortUrl.FirstOrDefault(item => item.Url == url);
			return oShortUrl;
		}

		public void Save(ShortUrl shrtUrlsToBeSaved)
		{
			_shortUrlDbContext.ShortUrl.Add(shrtUrlsToBeSaved);
			_shortUrlDbContext.SaveChangesAsync();
		}
	}
}
