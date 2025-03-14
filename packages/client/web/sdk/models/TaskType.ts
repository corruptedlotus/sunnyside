export class TaskType {
	id?: string
	derivative?: TaskType

	name: string = null!
	description: string = ''
	isCounted: boolean = true
}
