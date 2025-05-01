import { FieldFetchableSelect, component, html, property } from '@3mo/del'
import { Api } from '@a11d/api'
import { TaskType } from 'sdk'

@component('sunny-select-type')
export class SelectType extends FieldFetchableSelect<TaskType, { query: string }> {
	@property() override label = 'Type'

	override readonly fetch = () => Api.get<Array<TaskType>>(`/types`)
	override readonly optionTemplate = (type: TaskType) => html`
		<mo-option value=${type.id} .data=${type}>${type.name}</mo-option>
	`
}

declare global {
	interface HTMLElementTagNameMap {
		'sunny-select-type': SelectType
	}
}
