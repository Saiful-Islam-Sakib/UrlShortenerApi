using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.ApiService.Interface;
using UrlShortener.Common.Models;

namespace UrlShortener.ApiService.Service
{
    public class ShortUrlService : IShortUrlService
    {
        public readonly IUniqueIdGeneratorService _UniqueIdGenerator;

		public ShortUrlService(IUniqueIdGeneratorService uniqueIdGenerator)
        {
			_UniqueIdGenerator = uniqueIdGenerator;
		}

        public List<ShortUrl> GetAll()
        {
            throw new NotImplementedException();
        }

        public string GetUniqueId(string Url)
        {
            // if found in redis do not go to db
            // if not go to db
            // save new url or return whatever id is saved
			return _UniqueIdGenerator.GenerateNextId();
		}

        public string GetUrl(string UniqueId)
        {
            throw new NotImplementedException();
        }
    }
}