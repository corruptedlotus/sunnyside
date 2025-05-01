export class TaskType {
	id = '00000000-0000-0000-0000-000000000000'
	derivative?: TaskType

	name: string = null!
	description: string = ''
	isCounted: boolean = true
}
