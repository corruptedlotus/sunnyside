using System.ComponentModel;
using System.Text.Json.Serialization;
using Anovase.Sunnyside.Backlog;

namespace Anovase.Sunnyside.Cycles;

public enum TaskStatus
{
	Open = 0,
	Done = 1,
	Delegated = 2,
	Failed = -1,
}

public class TaskInstance
{
	// Meta
	public Guid Id { get; set; }
	public Directive Directive { get; set; } = null!;

	[JsonIgnore]
	public Cycle Cycle { get; set; } = null!;
	public Guid CycleId { get; set; } = Guid.Empty;

	// Properties
	public string? Name { get; set; }
	public double TimeTracked { get; set; }
	public double TimeAllocated { get; set; }
	public TaskObjective? Objective { get; set; }
	public TaskStatus Status { get; set; } = TaskStatus.Open;
}
