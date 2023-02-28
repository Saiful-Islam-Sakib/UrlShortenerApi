using UrlShortener.Common.Models;

namespace UrlShortener.Common.Interface
{
	public interface IShortUrlService
	{
		List<ShortUrl> GetAll();
		ShortUrl GetUrl(string UniqueId);
		ShortUrl GetUniqueId(string Url);
	}
}
