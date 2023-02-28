using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.ApiService.Interface;
using UrlShortener.ApiService.Utility;
using UrlShortener.Common.Models;
using UrlShortener.DAService.Interface;
using UrlShortener.Common.CustomeException;

namespace UrlShortener.ApiService.Service
{
    public class ShortUrlService : IShortUrlService
    {
		private readonly IShortUrlDAService _ShortUrlDA;
		private readonly IUniqueIdGeneratorService _UniqueIdGenerator;

		public ShortUrlService(
            IUniqueIdGeneratorService uniqueIdGenerator,
			IShortUrlDAService shortUrlDAService)
        {
			_UniqueIdGenerator = uniqueIdGenerator;
			_ShortUrlDA= shortUrlDAService;
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
					MainUrl = urlToBeProcessed
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