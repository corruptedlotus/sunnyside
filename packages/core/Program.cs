global using A11d.Module;
using System.Text.Json;
using System.Text.Json.Serialization;

var hostBuilder = WebApplication.CreateBuilder(args);

hostBuilder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(jo =>
{
	jo.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	jo.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

var webHost = hostBuilder.Install<SunnysideCore>().Build();

webHost.UseRouting();

webHost.Configure<SunnysideCore>().Run("http://0.0.0.0:13666");

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1050", Justification = "This is a marker class.")]
public class SunnysideCore { }
