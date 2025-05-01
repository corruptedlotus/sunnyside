namespace Anovase.Sunnyside.Backlog;

public class BacklogModule : Module
{
	public override void ConfigureServices(IServiceCollection services)
	{
		services.AddTransient<BacklogService>();
	}

	public override void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
	{
		endpoints.MapGet("/api/backlog", (BacklogService backlog) => backlog.ListAsync(new(null)));
		endpoints.MapGet("/api/backlog/s/{query}", (BacklogService backlog, string query) => backlog.SearchAsync(query));

		endpoints.MapPut("/api/backlog", (BacklogService backlog, Directive item) => backlog.SaveAsync(item));
		endpoints.MapGet("/api/backlog/{id}", (BacklogService backlog, Guid id) => backlog.GetAsync(id));
		endpoints.MapDelete("/api/backlog/{id}", (BacklogService backlog, Guid id) => backlog.DeleteAsync(id));

		endpoints.MapGet("/api/types", (BacklogService backlog) => backlog.GetAllTypes());
	}
}
