using Microsoft.AspNetCore.Mvc;
using UrlShortener.Common.Interface;

namespace UrlShortener.API.Controllers
{
	[ApiController]
	[Route("[Controller]")]
	public class UrlShortenerController : ControllerBase
	{
		public readonly IShortUrl _ShortUrl;
		public UrlShortenerController(IShortUrl shortUrl)
		{
			_ShortUrl = shortUrl;
		}

		[HttpPost]
		[Route("GetShortUrl/{UrlToBeShorten}")]
		public IActionResult GetShortUrl(string UrlToBeShorten)
		{
			string t = "Api working correctly" + _ShortUrl.MakeUniqueId(UrlToBeShorten);
			return Ok(t);
		}
	}
}
