import { Cycle, Directive, TaskObjective } from '.'

export const enum TaskStatus {
	open = 0,
	done = 1,
	delegated = 2,
	failed = -1,
}

export class TaskInstance {
	id: string = null!
	directive: Directive = null! //TODO: can this be nullable?
	cycle: Cycle = null!

	name?: string
	timeAllocated = 0
	timeTracked = 0
	objective? = TaskObjective.doneOnly
	status = TaskStatus.open
}
