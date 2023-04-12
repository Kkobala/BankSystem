using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using Newtonsoft.Json;
using Serilog;
using System.Text;

namespace BankSystem.Middlewares
{
	public class ErrorHandlerMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ErrorHandlerMiddleware> _logger;

		public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message, "Errooooor");
				
				var error = new { message = ex.Message };
				var errorJson = JsonConvert.SerializeObject(error);
				httpContext.Response.StatusCode = 500;
				httpContext.Response.ContentType = "application/json";
				await httpContext.Response.WriteAsync(errorJson, Encoding.UTF8);
			}
		}
	}
}
