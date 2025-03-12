using Anovase.Sunnyside.Backlog;

namespace Anovase.Sunnyside.Cycles;

public enum TaskStatus
{
	Open,
	Done,
	Delegated,
	Failed,
}

public class TaskInstance
{
	// Meta
	public Guid Id { get; set; }
	public DateTime CreatedOn { get; set; }
	public Directive Directive { get; set; } = null!;
	public Cycle Cycle { get; set; } = null!;

	// Properties
	public string? Name { get; set; }
	public TimeOnly TimeTracked { get; set; }
	public TimeOnly TimeAllocated { get; set; }
	public TaskObjective? Objective { get; set; }
	public TaskStatus Done { get; set; } = TaskStatus.Open;
}
