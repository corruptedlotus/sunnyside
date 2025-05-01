import { component, html } from '@a11d/lit'
import { DataGridColumnComponent } from '@3mo/del'
import { SDK } from 'modules'

@component('sunny-data-grid-column-directive')
export class DataGridColumnDirective<TData> extends DataGridColumnComponent<TData, SDK.Directive> {

	override getContentTemplate(value: SDK.Directive, _: TData) {
		return html`
			${value.name}
		`
	}

	override getEditContentTemplate(value: SDK.Directive, data: TData) {
		return html`
			<sunny-select-directive
				.data=${value}
				@dataChange=${(newValue: SDK.Directive) => this.handleEdit(newValue, data)}
			></sunny-select-directive>
		`
	}
}


declare global {
	interface HTMLElementTagNameMap {
		'sunny-data-grid-column-directive': DataGridColumnDirective<unknown>
	}
}
