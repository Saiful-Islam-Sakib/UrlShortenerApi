using Microsoft.AspNetCore.Mvc;
using UrlShortener.ApiService.Interface;

namespace UrlShortener.API.Controllers
{
	[ApiController]
	[Route("[Controller]")]
	public class UrlShortenerController : ControllerBase
	{
		private readonly IShortUrlService _ShortUrl;
		private readonly ILoggerManagerService _Logger;

		public UrlShortenerController(IShortUrlService shortUrl, ILoggerManagerService logger)
		{
			_ShortUrl = shortUrl;
			_Logger = logger;
		}

		[HttpPost]
		[Route("GetShortUrl/{UrlToBeShorten}")]
		public IActionResult GetShortUrl(string UrlToBeShorten)
		{
			try
			{
				_Logger.LogInfo("Started");

				var t = _ShortUrl.GetUniqueId(UrlToBeShorten.Trim());

				_Logger.LogInfo("finished");

				return Ok(t);
			}
			catch (Exception ex)
			{
				_Logger.LogError(ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}
	}
}
