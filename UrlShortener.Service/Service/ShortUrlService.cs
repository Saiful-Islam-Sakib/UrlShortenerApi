using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.ApiService.Interface;
using UrlShortener.ApiService.Utility;
using UrlShortener.Common.Models;
using UrlShortener.DAService.Context;
using UrlShortener.DAService.Interface;

namespace UrlShortener.ApiService.Service
{
    public class ShortUrlService : IShortUrlService
    {
		private readonly IShortUrlDAService _ShortUrlDAService;
		private readonly IUniqueIdGeneratorService _UniqueIdGenerator;

		public ShortUrlService(
            IUniqueIdGeneratorService uniqueIdGenerator,
			IShortUrlDAService shortUrlDAService)
        {
			_UniqueIdGenerator = uniqueIdGenerator;
			_ShortUrlDAService= shortUrlDAService;
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
				throw new Exception("Please Provide correct URL");
			}

			ShortUrl oShortUrl = _ShortUrlDAService.GetByUrl(urlToBeProcessed);

			if (oShortUrl == null)
			{
				oShortUrl = new ShortUrl
				{
					ID = _UniqueIdGenerator.GenerateNextId(),
					MainUrl = urlToBeProcessed
				};
				_ShortUrlDAService.Save(oShortUrl);
			}

			return oShortUrl;
		}

        public ShortUrl GetUrl(string UniqueId)
        {
			ShortUrl oShortUrl = _ShortUrlDAService.GetById(UniqueId);

			return oShortUrl;
		}
    }
}