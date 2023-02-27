using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using UrlShortener.ApiService.Interface;

namespace UrlShortener.API.CustomMiddleware
{
	public class ProcessingTimeCalculatorMiddleware
	{
		private ILoggerManagerService _Logger;
		private readonly RequestDelegate _next;

		public ProcessingTimeCalculatorMiddleware(RequestDelegate next, ILoggerManagerService loggerManagerService)
		{
			_next = next;
			_Logger = loggerManagerService;
		}

		public Task Invoke(HttpContext httpContext)
		{
			var watch = new Stopwatch();
			watch.Start();
			httpContext.Response.OnStarting(() => {
				watch.Stop();

				var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;

				_Logger.LogInfo("Total Elapsed Time(ms): " + responseTimeForCompleteRequest);

				return Task.CompletedTask;
			});
			return _next(httpContext);
		}
	}
}
