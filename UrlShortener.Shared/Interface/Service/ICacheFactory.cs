using UrlShortener.Shared.Enums;

namespace UrlShortener.Common.Interface
{
	public interface ICacheFactory
	{
		ICacheService GetCacheProvide();
	}
}
