using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Common.Models;

namespace UrlShortener.ApiService.Interface
{
	public interface IShortUrlService
	{
		List<ShortUrl> GetAll();
		ShortUrl GetUrl(string UniqueId);
		ShortUrl GetUniqueId(string Url);
	}
}
