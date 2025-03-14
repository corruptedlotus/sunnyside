import { TaskInstance } from "."

export const enum CycleStatus {
	planning = 0,
	active = 1,
	closed = -1,
}

export class Cycle {
	id: string = null!
	start?: DateTime
	end?: DateTime
	status = CycleStatus.planning

	tasks: Array<TaskInstance> = []
}
