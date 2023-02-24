using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.ApiService.Utility;
using UrlShortener.Common.Interface;
using UrlShortener.Common.Models;

namespace UrlShortener.ApiService.Service
{
    public class ShortUrlService : IShortUrl
    {
        public List<ShortUrl> GetAll()
        {
            throw new NotImplementedException();
        }

        public string MakeUniqueId(string Url)
        {
			return UniqueIdGenerator.GenerateId();
		}

        public string GetUrl(string UniqueId)
        {
            throw new NotImplementedException();
        }
    }
}