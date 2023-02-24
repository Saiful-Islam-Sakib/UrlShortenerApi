using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.ApiService.Utility
{
	public static class UniqueIdGenerator
	{
		public static string GenerateId()
		{
			// Twitter SnowFlake Id Generator
			long timeStamp = ((DateTimeOffset)DateTime.Now).ToUnixTimeMilliseconds();

			return "ID";
		}
	}
}
