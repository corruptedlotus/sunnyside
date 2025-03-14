
namespace Anovase.Sunnyside.WebClient;

public class WebClientModule : Module
{
	public override async void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
	{
		endpoints.MapGet("/*", async (HttpContext context) => {
			if (context.Request.Path.StartsWithSegments("/_webapp"))
				return;
				
			await context.Response.WriteAsync("""
				<head>
					<title>Sunnyside</title>
					<script type="module" src="/_webapp/app.js"></script>
				</head>
				<body>
					<sunny-app></sunny-app>
				</body>
			""");
		});
	}
}
