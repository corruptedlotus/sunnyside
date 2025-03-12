namespace Anovase.Sunnyside.Backlog;

public enum TaskManagement
{
	Oneshot = 0,
	/* Only exists on a single day and then vanishes. */

	Routine = 1,
	/* Continues to exist and cannot be set to "finished".
	Doing so will only flush the current workload, effectively marking it as "carried out". */

	Project = 2,
	/* Continues to exist until flagged as done which will mark it as "finished".
	Filling the workload will considered it "carried out",
	and not doing so will mark it as "failed" and copy it to the next day. */
}

public enum TaskObjective
{
	/// <summary>
	/// <para>Default task behaviour.</para>
	/// The task must be set as done to be completed.
	/// Time allocation is treated as an estimate.
	/// </summary>
	DoneOnly,

	/// <summary>
	/// Time allocation is treated as a maximum.
	/// The task fails if surpasses its allocated time, can't be marked as done.
	/// </summary>
	Minimise,

	/*/// <summary>
	/// The completion of the task is merely scoring the time.
	/// If the time allocation, which is treated as a minimum, is scored, the task is considered done.
	/// </summary>
	TimeOnly,*/

	/*/// <summary>
	/// Time allocation is treated as minimum,
	/// and it has to be scored before being able to mark the task as done.
	/// </summary>
	Timed,*/
}

public class Directive
{
	// Meta
	public Guid Id { get; init; }
	public DateTime CreatedOn { get; set; }
	public required TaskType Type { get; set; }

	// Properties
	public required string Name { get; set; }
	public bool Immediate { get; set; } = false;
	public TaskManagement? Management { get; set; } = TaskManagement.Oneshot;
	public TaskObjective? Objective { get; set; }

	// Transforms

	public void MoveToLongBacklog() => Immediate = false;
	public void MoveToShortBacklog() => Immediate = true;
}
