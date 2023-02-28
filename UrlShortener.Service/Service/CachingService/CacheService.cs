using UrlShortener.Common.Interface;

namespace UrlShortener.Services.Service.CachingService
{
    public class CacheService : ICacheService
    {
        public T GetData<T>(string key) where T : class
        {
            throw new NotImplementedException();
        }

        public bool SetData<T>(string key, T value, DateTime expirationTime) where T : class
        {
            throw new NotImplementedException();
        }

        public T RemoveData<T>(string key) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
