using Microsoft.AspNetCore.Mvc;
using UrlShortener.ApiService.Interface;
using UrlShortener.Common.CustomeException;
using UrlShortener.Common.Models;

namespace UrlShortener.API.Controllers
{
	[ApiController]
	[Route("UrlShortener")]
	public class UrlShortenerController : ControllerBase
	{
		private readonly IShortUrlService _ShortUrl;
		private readonly ILoggerManagerService _Logger;

		public UrlShortenerController(
			IShortUrlService shortUrl, 
			ILoggerManagerService logger)
		{
			_ShortUrl = shortUrl;
			_Logger = logger;
		}

		[HttpPost]
		[Route("GetShortUrl/{UrlToBeShorten}")]
		public async Task<IActionResult> GetShortUrl(string UrlToBeShorten)
		{
			try
			{
				ShortUrl oShortUrl = _ShortUrl.GetUniqueId(UrlToBeShorten);

				return Ok(oShortUrl);
			}
			catch (InvalidUrlException ex)
			{
				_Logger.LogWarn(ex.Message);
				return BadRequest(ex.Message);
			}
			catch (Exception ex)
			{
				_Logger.LogError(ex.Message);
				return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
			}
		}

		[HttpGet]
		[Route("GetUrl/{IdOfTheUrl}")]
		public async Task<RedirectResult> GetUrl(string IdOfTheUrl)
		{
			ShortUrl oShortUrl = _ShortUrl.GetUrl(IdOfTheUrl);

			return oShortUrl != null ? RedirectPermanent(oShortUrl.MainUrl) : RedirectPermanent("~/UrlShortener/GetErrorMessage");
		}

		[HttpGet]
		[Route("GetErrorMessage")]
		public async Task<IActionResult> GetErrorMessage()
		{
			_Logger.LogWarn("No Url Found For The Id");
			return BadRequest("No Url Found For The Id");
		}
	}
}
