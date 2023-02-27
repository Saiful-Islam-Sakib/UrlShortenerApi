using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.ApiService.Interface;
using UrlShortener.ApiService.Utility;
using UrlShortener.Common.Models;
using UrlShortener.DAService.Context;

namespace UrlShortener.ApiService.Service
{
    public class ShortUrlService : IShortUrlService
    {
        public readonly ShortUrlDbContext _ShortUrlDbContext;
		public readonly IUniqueIdGeneratorService _UniqueIdGenerator;

		public ShortUrlService(
            IUniqueIdGeneratorService uniqueIdGenerator,
			ShortUrlDbContext shortUrlDbContext)
        {
			_UniqueIdGenerator = uniqueIdGenerator;
            _ShortUrlDbContext = shortUrlDbContext;
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

			ShortUrl oShortUrl = _ShortUrlDbContext.tblShortUrl.FirstOrDefault(item => item.MainUrl == urlToBeProcessed);

			if (oShortUrl == null)
			{
				oShortUrl = new ShortUrl
				{
					ID = _UniqueIdGenerator.GenerateNextId(),
					MainUrl = urlToBeProcessed
				};

				_ShortUrlDbContext.tblShortUrl.Add(oShortUrl);
				_ShortUrlDbContext.SaveChangesAsync();
			}

			return oShortUrl;
		}

        public ShortUrl GetUrl(string UniqueId)
        {
            return _ShortUrlDbContext.tblShortUrl.FirstOrDefault(item => item.ID == UniqueId);
		}
    }
}