using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.ApiService.Interface;

namespace UrlShortener.ApiService.Service
{
    public class UniqueIdGeneratorService : IUniqueIdGenerator
    {
        public string GenerateNextId()
        {
            // Twitter SnowFlake Id Generator
            long timeStamp = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();

            return "ID";
        }
    }
}
