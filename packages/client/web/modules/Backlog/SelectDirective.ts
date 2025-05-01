import { FieldFetchableSelect, component, html, property } from '@3mo/del'
import { Api } from '@a11d/api'
import { Directive } from 'sdk'

type DirectiveSearchParams = { query: string }

@component('sunny-select-directive')
export class SelectDirective extends FieldFetchableSelect<Directive, DirectiveSearchParams> {
	@property() override label = 'Directive'

	override readonly searchable = true

	override readonly fetch = (params?: DirectiveSearchParams) => Api.get<Array<Directive>>(`/backlog/s/${params?.query}`)
	override readonly searchParameters = (keyword: string) => ({ query: keyword })
	override readonly optionTemplate = (directive: Directive) => html`
		<mo-option value=${directive.id} .data=${directive}>${directive.name}</mo-option>
	`
}

declare global {
	interface HTMLElementTagNameMap {
		'sunny-select-directive': SelectDirective
	}
}
