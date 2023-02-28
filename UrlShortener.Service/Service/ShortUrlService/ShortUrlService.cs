using UrlShortener.Common.Interface;
using UrlShortener.Services.Utility;
using UrlShortener.Common.Models;
using UrlShortener.Common.Interface.Repository;
using UrlShortener.Common.CustomeException;

namespace UrlShortener.Services.Service.ShortUrlService
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlRepository _ShortUrlDA;
        private readonly IUniqueIdGeneratorService _UniqueIdGenerator;

        public ShortUrlService(
            IUniqueIdGeneratorService uniqueIdGenerator,
            IShortUrlRepository shortUrlRepository)
        {
            _UniqueIdGenerator = uniqueIdGenerator;
            _ShortUrlDA = shortUrlRepository;
        }

        public List<ShortUrl> GetAll()
        {
            throw new NotImplementedException();
        }

        public ShortUrl GetUniqueId(string UrlToBeShorten)
        {
            string urlToBeProcessed = string.Empty;

            if (!UrlToBeShorten.IsValidUrl(out urlToBeProcessed))
            {
                throw new InvalidUrlException("Please Provide correct URL");
            }

            ShortUrl oShortUrl = _ShortUrlDA.GetByUrl(urlToBeProcessed);

            if (oShortUrl == null)
            {
                oShortUrl = new ShortUrl
                {
                    ID = _UniqueIdGenerator.GenerateNextId(),
                    Url = urlToBeProcessed
                };
                _ShortUrlDA.Save(oShortUrl);
            }

            return oShortUrl;
        }

        public ShortUrl GetUrl(string UniqueId)
        {
            ShortUrl oShortUrl = _ShortUrlDA.GetById(UniqueId);

            return oShortUrl;
        }
    }
}