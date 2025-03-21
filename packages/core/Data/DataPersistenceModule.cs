
using Microsoft.EntityFrameworkCore;

namespace Anovase.Sunnyside.Data;

public class DataPersistenceModule : Module
{
	protected static string? DATABASE_SERVER => Environment.GetEnvironmentVariable("DATABASE_SERVER");
	protected static string? DATABASE_USER => Environment.GetEnvironmentVariable("DATABASE_USER");
	protected static string? DATABASE_PASSWORD => Environment.GetEnvironmentVariable("DATABASE_PASSWORD");

	protected static string ConnectionString => DATABASE_SERVER is null || DATABASE_USER is null || DATABASE_PASSWORD is null
		? "Data Source=localhost;Database=HeavenFlooringPWS;Integrated Security=True;Encrypt=False;"
		: $"Server={DATABASE_SERVER};User ID={DATABASE_USER};Database=HeavenFlooringPWS;Password={DATABASE_PASSWORD};Encrypt=False;";

	public override void ConfigureServices(IServiceCollection services)
	{
		services.AddDbContext<DataContext>(options => {
			options.UseSqlServer(ConnectionString);
		}, ServiceLifetime.Scoped);
	}

	public override async void ConfigureApplication(WebApplication application)
	{
		await application.Services.GetService<DataContext>()?.InitializeAsync();
	}
}
