using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UrlShortener.ApiService.Utility
{
	public static class GlobalFunctions
	{
		public static bool IsValidUrl(this string str, out string pUrlToBeProcessed)
		{
			pUrlToBeProcessed = str.Trim().ToLower();

			pUrlToBeProcessed = !pUrlToBeProcessed.Contains("http") ? "https://" + pUrlToBeProcessed : pUrlToBeProcessed;

			Regex validateDateRegex = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");

			return validateDateRegex.IsMatch(pUrlToBeProcessed);
		}
	}
}
