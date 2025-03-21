using Anovase.Sunnyside.Backlog;
using Anovase.Sunnyside.Cycles;
using Microsoft.EntityFrameworkCore;

namespace Anovase.Sunnyside.Data;

public class DataContext : DbContext
{
	public DbSet<Directive> Backlog { get; set; }
	public DbSet<Cycle> Cycles { get; set; }
	public DbSet<TaskType> TaskTypes { get; set; }

	public DataContext(DbContextOptions<DataContext> options) : base(options) { }

	public async Task InitializeAsync()
	{
		await Database.MigrateAsync();
	}
}
