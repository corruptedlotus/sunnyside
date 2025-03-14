using Anovase.Sunnyside.Cycles;
using Anovase.Sunnyside.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Anovase.Sunnyside.Cycles;

public class CycleService(DataContext Database)
{
	protected DbSet<Cycle> Cycles => Database.Cycles;
	protected DbSet<TaskInstance> Tasks => Database.Set<TaskInstance>();

	public async Task DeleteTaskAsync(Guid id)
	{
		var e = await Tasks.FindAsync(id) ?? throw new EntityNotFoundException(typeof(Cycle));
		Tasks.Remove(e);
		await Database.SaveChangesAsync();

		return;
	}

	public async Task DeleteTask(IEnumerable<Guid> ids)
	{
		var e = Tasks.Where(x => ids.Contains(x.Id));
		Tasks.RemoveRange(e);
		await Database.SaveChangesAsync();

		return;
	}

	public async Task<TaskInstance> SaveTaskAsync(TaskInstance item)
	{
		var e = Tasks.Update(item);
		await Database.SaveChangesAsync();
		return e.Entity;
	}

	public async Task<Cycle> EndActiveAsync(DateTime time)
	{
		var e = await Cycles.Where(x => x.Status == CycleStatus.Active).FirstAsync();
		
		if (time <= e.Start)
			throw new InvalidOperationException("Cannot end the cycle before it began.");
			

		e.Finish(time);

		await Database.SaveChangesAsync();
		return e;
	}

	public async Task<Cycle?> GetActiveAsync()
	{
		var e = await Cycles.AsNoTracking().Where(x => x.Status == CycleStatus.Active).FirstOrDefaultAsync();
		return e;
	}

	public async Task<Cycle> GetAsync(Guid id)
		=> await Cycles.FindAsync(id) ?? throw new EntityNotFoundException(typeof(Cycle));

	public async Task<Cycle> NewAsync()
	{
		var e = await Cycles.AddAsync(new Cycle
		{
			Status = CycleStatus.Planning,
		});

		await Database.SaveChangesAsync();

		return e.Entity;
	}

	public async Task<Cycle> StartAsync(Cycle cycle, DateTime time)
	{
		if (Cycles.Any(x => x.Status == CycleStatus.Active || x.End >= time))
			throw new InvalidOperationException("New cycle cannot collide with any previous ones.");

		if (cycle.Status != CycleStatus.Planning)
			throw new InvalidOperationException("Cannot start a cycle that is already past planning phase.");

		cycle.Begin(time);
		await Database.SaveChangesAsync();

		return cycle;
	}

	public async Task<Cycle> StartAsync(Guid id, DateTime time)
	{
		var e = await Cycles.FindAsync(id) ?? throw new EntityNotFoundException(typeof(Cycle));
		return await StartAsync(e, time);
	}

	public Task<Cycle> StartNowAsync(Guid id) => StartAsync(id, DateTime.UtcNow);
	public Task<Cycle> StartNowAsync(Cycle cycle) => StartAsync(cycle, DateTime.UtcNow);
	public Task<Cycle> EndNowAsync() => EndActiveAsync(DateTime.UtcNow);
}
