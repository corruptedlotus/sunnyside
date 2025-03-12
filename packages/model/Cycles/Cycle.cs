namespace Anovase.Sunnyside.Cycles;

public enum CycleStatus
{
	Planning,
	Active,
	Closed,
}

public class Cycle
{
	#region Meta

	public Guid Id { get; init; }
	public Cycle? Previous { get; init; }
	
	#endregion

	#region Properties

	public DateTime? Start { get; set; }
	public DateTime? End { get; set; }
	public CycleStatus Status { get; set; } = CycleStatus.Planning;
	
	#endregion

	// Relations

	public ICollection<TaskInstance> Tasks { get; set; } = [];

	// Transforms

	public void Begin(DateTime time)
	{
		Status = CycleStatus.Active;
		Start = time;
	}

	public void Finish(DateTime time)
	{
		Status = CycleStatus.Closed;
		End = time;
	}

	public Cycle PlanNext()
	{
		return new Cycle { Previous = this };
	}
}
