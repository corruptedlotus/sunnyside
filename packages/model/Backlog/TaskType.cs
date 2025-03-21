using Anovase.Sunnyside.Helpers;

namespace Anovase.Sunnyside.Backlog;

public class TaskType
{
	// Meta
	public Guid Id { get; init; }
	public TaskType? Derivative { get; init; }

	// Properties
	public required string Name { get; set; }
	public string Description { get; set; } = string.Empty;
	//public ClientColor? Color { get; set; }
	public bool IsCounted { get; set; }
}
