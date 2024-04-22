using System.Net;
using System.Text.Json;
using Talabat.APIs.Errors;

namespace Talabat.APIs.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _log;
		private readonly IHostEnvironment _environment;

		public ExceptionMiddleware(RequestDelegate next , ILogger<ExceptionMiddleware> log,IHostEnvironment environment)
        {
			_next = next;
			_log = log;
			_environment = environment;
		}
		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}
			catch(Exception ex)
			{
				_log.LogError(ex, ex.Message);
				context.Response.ContentType = "application/json";
				context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

				var response = _environment.IsDevelopment()
											? new ExceptionResponseError((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
											: new ExceptionResponseError((int)HttpStatusCode.InternalServerError);
				var json = JsonSerializer.Serialize(response);
				await context.Response.WriteAsync(json);
			}
		}
	}
}
