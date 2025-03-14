global using A11d.Module;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.FileProviders;

var hostBuilder = WebApplication.CreateBuilder(args);

hostBuilder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(jo =>
{
	jo.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	jo.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

var webHost = hostBuilder.Install<SunnysideCore>().Build();

webHost.UseRouting();

var env = webHost.Services.GetRequiredService<IWebHostEnvironment>();

webHost.UseStaticFiles(new StaticFileOptions
{
	RequestPath = new PathString("/_webclient"),
#if DEBUG
	FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, @"../../dbg/client")),
#else
	FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, @"../client")),
#endif
});

webHost.Configure<SunnysideCore>().Run("http://0.0.0.0:13666");

[System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1050", Justification = "This is a marker class.")]
public class SunnysideCore { }
