using Microsoft.AspNetCore.Mvc;
using UrlShortener.ApiService.Interface;

namespace UrlShortener.API.Controllers
{
	[ApiController]
	[Route("[Controller]")]
	public class UrlShortenerController : ControllerBase
	{
		public readonly IShortUrlService _ShortUrl;

		public UrlShortenerController(IShortUrlService shortUrl)
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
