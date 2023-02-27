using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Common.Models;

namespace UrlShortener.DAService.Interface
{
	public interface IShortUrlDAService
	{
		public void Save(List<ShortUrl> shrtUrlsToBeSaved);
		public void GetUrl(string id);
		public void GetId(string url);
	}
}
