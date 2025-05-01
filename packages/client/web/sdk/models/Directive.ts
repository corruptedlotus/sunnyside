import { TaskType } from '.'

export const enum TaskManagement {
	oneshot = 0,
	routine = 1,
	project = 2,
}

export const enum TaskObjective {
	doneOnly = 0
}

export class Directive {
	id = '00000000-0000-0000-0000-000000000000'
	type: TaskType = null!

	name: string = null!
	description = ''
	immediate = false
	management = TaskManagement.oneshot
	objective? = TaskObjective.doneOnly
}
