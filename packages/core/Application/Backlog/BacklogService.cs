using Anovase.Sunnyside.Backlog;
using Anovase.Sunnyside.Data;
using Microsoft.EntityFrameworkCore;

namespace Anovase.Sunnyside.Backlog;

public record BacklogDisplayArgs(bool? Immediate);

public interface IBacklogService
{
	Task<IEnumerable<Directive>> ListAsync(BacklogDisplayArgs immediate);
	Task<IEnumerable<Directive>> SearchAsync(string query);
	Task<Directive> GetAsync(Guid id);
	
	Task<Directive> AddAsync(Directive item);
	Task<Directive> EditAsync(Directive item);
	Task DeleteAsync(Guid id);
	Task DeleteAsync(IEnumerable<Guid> ids);
}

public class BacklogService(DataContext Database) : IBacklogService
{
	protected DbSet<Directive> Backlog => Database.Backlog;

	public async Task<Directive> AddAsync(Directive item)
	{
		var e = await Backlog.AddAsync(item);
		await Database.SaveChangesAsync();
		return e.Entity;
	}

	public async Task DeleteAsync(Guid id)
	{
		var e = Backlog.Find(id);
		if (e is not null)
			Backlog.Remove(e);

		await Database.SaveChangesAsync();
	}

	public async Task DeleteAsync(IEnumerable<Guid> ids)
	{
		Backlog.RemoveRange(Backlog.Where(x => ids.Contains(x.Id)));
		await Database.SaveChangesAsync();
	}

	public async Task<Directive> EditAsync(Directive item)
	{
		var e = Backlog.Update(item);
		await Database.SaveChangesAsync();
		return e.Entity;
	}

	public async Task<Directive> GetAsync(Guid id)
	{
		var e = await Backlog.FindAsync(id);
		return e ?? throw new EntityNotFoundException(typeof(Directive));
	}

	public async Task<IEnumerable<Directive>> ListAsync(BacklogDisplayArgs args)
	{
		return await Task.Run(delegate {
			var q = Backlog.AsNoTracking();
			
			if (args.Immediate is bool _immediate)
				q = q.Where(x => x.Immediate == _immediate);
			
			return q.AsEnumerable();
		});
	}

	public async Task<IEnumerable<Directive>> SearchAsync(string query)
	{
		return await Task.Run(delegate {
			var q = Backlog.AsNoTracking().Where(x => EF.Functions.FreeText(x.Name, query));
			return q.AsEnumerable();
		});
	}

	public async Task<IEnumerable<TaskType>> GetAllTypes()
	{
		return await Task.Run(() => Database.TaskTypes.AsEnumerable());
	}
}
