using System.Net;
using System.Text.Json;

namespace Anovase.Sunnyside;

public class ErrorMessageMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ErrorMessageMiddleware> _logger;

	public ErrorMessageMiddleware(RequestDelegate next, ILogger<ErrorMessageMiddleware> logger)
	{
		_next = next;
		_logger = logger;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context); // Let the request continue down the pipeline
		}
		catch (Exception ex)
		{
			context.Response.ContentType = "application/json";
			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

			var response = new
			{
				error = new
				{
					message = ex.Message,
					type = ex.GetType().Name
				}
			};

			var json = JsonSerializer.Serialize(response);
			await context.Response.WriteAsync(json);
		}
	}
}
