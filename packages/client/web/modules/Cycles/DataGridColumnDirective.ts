import { DataGridColumn, html } from '@3mo/del'
import { SDK } from 'modules'

export class DataGridColumnDirective extends DataGridColumn<SDK.TaskInstance, SDK.Directive> {

	override readonly editable = true

	override getContentTemplate(value: SDK.Directive, data: SDK.TaskInstance) {
		return html`
			<span>${value.name}</span>
		`
	}

	override getEditContentTemplate(value: SDK.Directive, data: SDK.TaskInstance) {
		return html`
			<sunny-select-directive
				.data=${value}
				@dataChange=${(newValue: SDK.Directive) => setValueByKeyPath(data, this.dataSelector, newValue)}
			></sunny-select-directive>
		`
	}
}


declare global {
	interface HTMLElementTagNameMap {
		'sunny-data-grid-column-directive': DataGridColumnDirective
	}
}
