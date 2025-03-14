import { EntityDialogComponent } from "@3mo/del"
import { Api } from "@a11d/api"
import { component, html, nothing } from "@a11d/lit"
import { SDK } from "modules"

type DialogDirectiveParameters = {
	readonly id?: string
}

@component('sunny-dialog-directive')
export class DialogDirective extends EntityDialogComponent<SDK.Directive, DialogDirectiveParameters> {
	
	protected entity = new SDK.Directive()
	protected fetch = (id: string | number) => Api.get<SDK.Directive>(`/backlog/${id}`)
	protected save = (entity: SDK.Directive) => Api.put<SDK.Directive>('/backlog', entity)
	protected delete? = (entity: SDK.Directive) => Api.delete(`/backlog/${entity.id}`)

	protected override get template() {
		const { bind } = this.entityBinder
		return html`
			<mo-entity-dialog size='small' heading=${!this.entity?.id ? 'New Directive' : '' + this.entity.name}>
				${!this.entity ? nothing : html`
					<mo-flex gap='10px'>
						<mo-field-text label='Name' ${bind('name')}></mo-field-text>
						
						<mo-field-select label='Type' ${bind('type')}></mo-field-select>

						<mo-field-select label='Management' ${bind('management')}>
							<mo-option value=${SDK.TaskManagement.oneshot}>Oneshot</mo-option>
							<mo-option value=${SDK.TaskManagement.routine}>Routine</mo-option>
							<mo-option value=${SDK.TaskManagement.project}>Project</mo-option>
						</mo-field-select>

						<mo-field-text-area rows='5' label='Description' ${bind('description')}></mo-field-text-area>
					</mo-flex>
				`}
			</mo-entity-dialog>
		`
	}
}

declare global {
	interface HtmlElementTagNameMap {
		'sunny-dialog-directive': DialogDirective
	}
}
