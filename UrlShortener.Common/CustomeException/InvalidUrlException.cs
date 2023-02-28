using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrlShortener.Common.CustomeException
{
	public class InvalidUrlException : Exception
	{
		public InvalidUrlException() { }
		public InvalidUrlException(string url) : base($"Invalid: {url}")
		{

		}
	}
}
