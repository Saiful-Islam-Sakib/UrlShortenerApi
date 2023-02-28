using Microsoft.AspNetCore.Mvc;
using UrlShortener.Common.Interface;
using UrlShortener.Common.CustomeException;
using UrlShortener.Common.Models;

namespace UrlShortener.API.Controllers
{
	[ApiController]
	[Route("UrlShortener")]
	public class UrlShortenerController : ControllerBase
	{
		private readonly IShortUrlService _shortUrl;
		private readonly ILoggerManagerService _logger;

		public UrlShortenerController(
			IShortUrlService shortUrl, 
			ILoggerManagerService logger)
		{
			_shortUrl = shortUrl;
			_logger = logger;
		}

		[HttpPost]
		[Route("GetShortUrl/{UrlToBeShorten}")]
		public async Task<IActionResult> GetShortUrl(string UrlToBeShorten)
		{
			try
			{
				ShortUrl oShortUrl = _shortUrl.GetUniqueId(UrlToBeShorten);

				return Ok(oShortUrl);
			}
			catch (CustomInvalidException ex)
			{
				_logger.LogWarn(ex.Message);
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpGet]
		[Route("GetUrl/{IdOfTheUrl}")]
		public async Task<RedirectResult> GetUrl(string IdOfTheUrl)
		{
			ShortUrl oShortUrl = _shortUrl.GetUrl(IdOfTheUrl);

			return oShortUrl != null ? RedirectPermanent(oShortUrl.Url) : RedirectPermanent("~/UrlShortener/GetErrorMessage");
		}

		[HttpGet]
		[Route("GetErrorMessage")]
		public async Task<IActionResult> GetErrorMessage()
		{
			_logger.LogWarn("No Url Found For The Id");
			return BadRequest("No Url Found For The Id");
		}
	}
}
