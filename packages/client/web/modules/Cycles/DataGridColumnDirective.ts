import { html } from '@a11d/lit'
import { DataGridColumnComponent } from '@3mo/del'
import { SDK } from 'modules'

export class DataGridColumnDirective extends DataGridColumnComponent<SDK.TaskInstance, SDK.Directive> {

	override getContentTemplate(value: SDK.Directive, _: SDK.TaskInstance) {
		return html`
			<span>${value.name}</span>
		`
	}

	override getEditContentTemplate(value: SDK.Directive, data: SDK.TaskInstance) {
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
		'sunny-data-grid-column-directive': DataGridColumnDirective
	}
}
