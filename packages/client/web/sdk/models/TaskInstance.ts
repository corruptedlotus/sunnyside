import { Cycle, Directive, TaskObjective } from '.'

export const enum TaskStatus {
	open = 0,
	done = 1,
	delegated = 2,
	failed = -1,
}

export class TaskInstance {
	id = '00000000-0000-0000-0000-000000000000'
	cycleId = '00000000-0000-0000-0000-000000000000'
	directive: Directive = null! //TODO: can this be nullable?

	name?: string
	timeAllocated = 0
	timeTracked = 0
	objective? = TaskObjective.doneOnly
	status = TaskStatus.open

	get done() { return this.status === TaskStatus.done }
	set done(value: boolean) { this.status = value ? TaskStatus.done : TaskStatus.open }

	constructor(cycleId: string) {
		this.cycleId = cycleId
	}
}
