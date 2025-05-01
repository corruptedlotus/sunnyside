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

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<TaskType>(b =>
		{
			b.HasData(
				new TaskType { Id = Guid.Parse("6f90d276-4cdc-4790-a5ba-34a58cca88c2"), Name = "Void", IsCounted = false },
				new TaskType { Id = Guid.Parse("ccb39899-3229-4d42-b28b-1f655adbb84c"), Name = "Chore", IsCounted = true },
				new TaskType { Id = Guid.Parse("e567611b-ba13-49e9-b668-02c964a90391"), Name = "Work", IsCounted = true }
			);
		});

		modelBuilder.Entity<Directive>(b =>
		{
			b.Navigation(x => x.Type).AutoInclude();
		});

		modelBuilder.Entity<TaskInstance>(b =>
		{
			b.Navigation(x => x.Directive).AutoInclude();
		});

		modelBuilder.Entity<Cycle>(b =>
		{
		});

		base.OnModelCreating(modelBuilder);
	}
}
