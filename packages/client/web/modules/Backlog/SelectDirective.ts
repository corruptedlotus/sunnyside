import { FieldFetchableSelect, component, html, property } from '@3mo/del'
import { Api } from '@a11d/api'
import { Directive } from 'sdk'

@component('sunny-select-directive')
export class SelectDirective extends FieldFetchableSelect<Directive, { query: string }> {
	@property() override label = 'Directive'

	override readonly fetch = () => Api.get<Array<Directive>>(`/backlog/s/`)
	override readonly searchParameters = (keyword: string) => ({ query: keyword })
	override readonly optionTemplate = (directive: Directive) => html`
		<mo-option value=${directive.id} .data=${directive}></mo-option>
	`
}

declare global {
	interface HTMLElementTagNameMap {
		'sunny-select-directive': SelectDirective
	}
}
