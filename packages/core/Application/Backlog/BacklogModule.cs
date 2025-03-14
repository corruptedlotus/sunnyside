namespace Anovase.Sunnyside.Backlog;

public class BacklogModule : Module
{
	public override void ConfigureServices(IServiceCollection services)
	{
		services.AddTransient<BacklogService>();
	}

	public override void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
	{
		endpoints.MapGet("/backlog", (BacklogService backlog) => backlog.ListAsync(new(null)));
		endpoints.MapGet("/backlog/s/{query}", (BacklogService backlog, string query) => backlog.SearchAsync(query));

		endpoints.MapPut("/backlog", (BacklogService backlog, Directive item) => backlog.SaveAsync(item));
		endpoints.MapGet("/backlog/{id}", (BacklogService backlog, Guid id) => backlog.GetAsync(id));
		endpoints.MapDelete("/backlog/{id}", (BacklogService backlog, Guid id) => backlog.DeleteAsync(id));
	}
}
