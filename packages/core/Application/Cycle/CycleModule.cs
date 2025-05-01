namespace Anovase.Sunnyside.Cycles;

public class CycleModule : Module
{
	public override void ConfigureServices(IServiceCollection services)
	{
		services.AddTransient<CycleService>();
	}

	public override void ConfigureEndpoints(IEndpointRouteBuilder endpoints)
	{
		endpoints.MapGet("/api/cycle/current", (CycleService cycles) => cycles.GetActiveAsync());
		
		endpoints.MapPatch("/api/cycle/startnew", async (CycleService cycles) => await cycles.StartNowAsync(await cycles.NewAsync()));
		endpoints.MapPatch("/api/cycle/finish", (CycleService cycles) => cycles.EndNowAsync());

		endpoints.MapPut("/api/cycle/task", (CycleService cycles, TaskInstance task) => cycles.SaveTaskAsync(task));
		endpoints.MapDelete("/api/cycle/task/{id}", (CycleService cycles, Guid id) => cycles.DeleteTaskAsync(id));
	}
}
