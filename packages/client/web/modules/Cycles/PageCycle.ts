import { component, html } from '@a11d/lit'
import { DataGridCell, EntitiesPageComponent } from '@3mo/del'
import { route } from '@a11d/lit-application'
import { PageDashboard } from 'app'
import { SDK } from 'modules'
import { Api } from '@a11d/api'

@route(PageDashboard, '/cycle')
@component('sunny-page-cycle')
export class PageCycle extends EntitiesPageComponent<SDK.TaskInstance> {

	protected currentCycle?: SDK.Cycle

	protected async startNew() {
		await Api.get('/cycle/startnew')
		this.refresh()
	}

	protected override get template() {
		return html`
			<lit-page fullHeight>
				<mo-card>
					<div slot='heading'>${!this.currentCycle ? '' : `Current Cycle: ${this.currentCycle.start}`}</div>
					<mo-entity-data-grid pagination='auto' editability='always'
						.fetch=${async () => ((this.currentCycle = await Api.get<SDK.Cycle>('/cycle/current')).tasks)}
						.delete=${(entity: SDK.TaskInstance) => Api.delete(`/cycle/task/${entity.id}`)}
						.create=${this.currentCycle?.tasks.push(new SDK.TaskInstance())}
						@cellEdit=${(cell: DataGridCell<any, SDK.TaskInstance>) => Api.put('/cycle/task', cell.data)}
					>
						<mo-empty-state icon='hourglass_disabled'>
							<div>No Active Cycle</div>
							<mo-button @click=${this.startNew}>Start New</mo-button>
						</mo-empty-state>

						<mo-data-grid-column-boolean dataSelector=${getKeyPath<SDK.TaskInstance>('status')}></mo-data-grid-column-boolean>
						<mo-data-grid-column-number width='30px' heading='TRACK' dataSelector=${getKeyPath<SDK.TaskInstance>('timeTracked')}></mo-data-grid-column-number>
						<mo-data-grid-column-number width='30px' heading='ALLOC' dataSelector=${getKeyPath<SDK.TaskInstance>('timeAllocated')}></mo-data-grid-column-number>
						<mo-data-grid-column-text heading='Directive' dataSelector=${getKeyPath<SDK.TaskInstance>('directive.name')}></mo-data-grid-column-text>
						<mo-data-grid-column-text heading='Name' dataSelector=${getKeyPath<SDK.TaskInstance>('name')}></mo-data-grid-column-text>
					</mo-entity-data-grid>
				</mo-card>
			</lit-page>
		`
	}
}

declare global {
	interface HtmlElementTagNameMap {
		'sunny-page-cycle': PageCycle
	}
}
