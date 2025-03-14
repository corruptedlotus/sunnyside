using Anovase.Sunnyside.Backlog;
using Anovase.Sunnyside.Cycles;
using Microsoft.EntityFrameworkCore;

namespace Anovase.Sunnyside.Data;

public interface IDatabase
{
	DbSet<Directive> Backlog { get; set; }
	DbSet<Cycle> Cycles { get; set; }
	DbSet<TaskType> TaskTypes { get; set; }

	Task SaveAsync();
}

public class DataContext : DbContext, IDatabase
{
	public DbSet<Directive> Backlog { get; set; }
	public DbSet<Cycle> Cycles { get; set; }
	public DbSet<TaskType> TaskTypes { get; set; }

	public async Task InitializeAsync()
	{
		await Database.MigrateAsync();
	}

	public async Task SaveAsync()
	{
		await SaveChangesAsync();
	}
}
