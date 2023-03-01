namespace UrlShortener.Common.CustomeException
{
	public class CustomInvalidException : Exception
	{
		public CustomInvalidException() { }
		public CustomInvalidException(string url) : base($"Invalid: {url}")
		{

		}
	}
}
