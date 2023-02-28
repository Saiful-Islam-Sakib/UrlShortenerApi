using UrlShortener.Common.Interface;
using UrlShortener.Services.Utility;
using UrlShortener.Common.Models;
using UrlShortener.Common.Interface.Repository;
using UrlShortener.Common.CustomeException;

namespace UrlShortener.Services.Service.ShortUrlService
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly IShortUrlDBService _shortUrlDA;
        private readonly IUniqueIdGeneratorService _uniqueIdGenerator;

		public ShortUrlService(
            IUniqueIdGeneratorService uniqueIdGenerator,
            IShortUrlDBService ShortUrlDBService)
        {
            _uniqueIdGenerator = uniqueIdGenerator;
            _shortUrlDA = ShortUrlDBService;
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
                throw new CustomInvalidException("Please Provide correct URL");
            }

			ShortUrl oShortUrl = _shortUrlDA.GetByUrl(urlToBeProcessed);

            if (oShortUrl == null)
            {
				oShortUrl = new ShortUrl
				{
					ID = _uniqueIdGenerator.GenerateNextId(),
					Url = urlToBeProcessed
				};
				_shortUrlDA.Save(oShortUrl);
			}

			return oShortUrl;
        }

        public ShortUrl GetUrl(string UniqueId)
        {
            ShortUrl oShortUrl = _shortUrlDA.GetById(UniqueId);

			return oShortUrl;
        }
    }
}