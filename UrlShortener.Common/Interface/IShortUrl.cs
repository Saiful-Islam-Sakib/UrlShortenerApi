using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Common.Models;

namespace UrlShortener.Common.Interface
{
	public interface IShortUrl
	{
		List<ShortUrl> GetAll();
		string GetUrl(string UniqueId);
		string MakeUniqueId(string Url);
	}
}
