import { Binder, component, html, nothing, query, state, style } from '@a11d/lit'
import { DataGridCell, EntitiesPageComponent, EntityDataGrid } from '@3mo/del'
import { route } from '@a11d/lit-application'
import { PageDashboard } from 'app'
import { SDK } from 'modules'
import { Api } from '@a11d/api'

@route(PageDashboard, '/cycle')
@component('sunny-page-cycle')
export class PageCycle extends EntitiesPageComponent<SDK.TaskInstance> {

	@state() newTask?: SDK.TaskInstance
	@state() protected currentCycle?: SDK.Cycle

	protected newTaskBinder = new Binder<SDK.TaskInstance>(this, 'newTask')
	
	@query('mo-entity-data-grid') protected dataGrid!: Promise<EntityDataGrid<SDK.TaskInstance>>

	protected async fetchCycle() {
		let cycle = await Api.get<SDK.Cycle | null>('/cycle/current')
		this.currentCycle = cycle === null ? undefined : cycle
		if (!!this.currentCycle) this.newTask = new SDK.TaskInstance(this.currentCycle!.id)
		return this.currentCycle?.tasks ?? []
	}

	protected async startNewCycle() {
		await Api.patch('/cycle/startnew')
		const dataGrid = await this.dataGrid
		dataGrid.requestFetch()
	}

	protected async finishCycle() {
		await Api.patch('/cycle/finish')
		const dataGrid = await this.dataGrid
		dataGrid.requestFetch()
	}

	protected async saveTask(task: SDK.TaskInstance) {
		await Api.put('/cycle/task', task)
		const dataGrid = await this.dataGrid
		dataGrid.requestFetch()
		
	}
	
	protected async saveNewTask() {
		this.saveTask(this.newTask!)
		this.newTask = new SDK.TaskInstance(this.currentCycle?.id!)
	}

	protected get newTaskTemplate() {
		let { bind } = this.newTaskBinder
		return html`
			<mo-flex direction='horizontal' alignItems='center' gap='10px' slot='footer' ${style({ paddingBlock: '5px' })}>
				<mo-icon-button icon='delete'></mo-icon-button>
				<mo-field-number ${bind('timeAllocated')} label='ALOC' ${style({ width: '80px' })}></mo-field-number>
				<sunny-select-directive .data=${bind('directive')}></sunny-select-directive>
				<mo-field-text label='Name' ${bind('name')}></mo-field-text>
				<mo-icon-button @click=${() => this.saveNewTask()} icon='add'></mo-icon-button>
			</mo-flex>
		`
	}

	protected override get template() {
		const dateFormat: Intl.DateTimeFormatOptions = { dateStyle: 'medium', day: 'numeric', hourCycle: 'h23', timeStyle: 'short' }
		return html`
			<lit-page fullHeight>
				<mo-card>
					<div slot='heading'>${!this.currentCycle ? '' : `Current Cycle: ${this.currentCycle.start}`}</div>
					<mo-entity-data-grid .pagination=${undefined} editability='cell'
						.fetch=${() => this.fetchCycle()}
						.delete=${(entity: SDK.TaskInstance) => Api.delete(`/cycle/task/${entity.id}`)}
						@cellEdit=${({ detail: { data } }: { detail: { data: SDK.TaskInstance } }) => this.saveTask(data)}
					>
						${!!this.currentCycle ? html`
							<mo-empty-state slot='error-no-content' icon='more_horiz'>
								<div>No Tasks</div>
							</mo-empty-state>
						` : html`
							<mo-empty-state slot='error-no-content' icon='hourglass_disabled'>
								<div>No Active Cycle</div>
								<mo-button @click=${this.startNewCycle}>Start New</mo-button>
							</mo-empty-state>
						`}

						${!this.currentCycle ? nothing : html`
							<mo-button slot='toolbar' @click=${this.finishCycle}>Finish Cycle</mo-button>
						`}

						<mo-data-grid-column-boolean heading='Done' trueIcon='check_box' falseIcon='check_box_outline_blank' dataSelector=${getKeyPath<SDK.TaskInstance>('done')}></mo-data-grid-column-boolean>
						<mo-data-grid-column-number width='60px' heading='TRAC/' dataSelector=${getKeyPath<SDK.TaskInstance>('timeTracked')}></mo-data-grid-column-number>
						<mo-data-grid-column-number width='60px' heading='/ALOC' dataSelector=${getKeyPath<SDK.TaskInstance>('timeAllocated')}></mo-data-grid-column-number>
						<sunny-data-grid-column-directive heading='Directive' dataSelector=${getKeyPath<SDK.TaskInstance>('directive')}></sunny-data-grid-column-directive>
						<mo-data-grid-column-text heading='Name' dataSelector=${getKeyPath<SDK.TaskInstance>('name')}></mo-data-grid-column-text>
					
						<mo-data-grid-footer-sum heading='Tracked' slot='sum'>
							${this.currentCycle?.tasks.reduce((c, val) => c + val.timeTracked, 0)}
						</mo-data-grid-footer-sum>
						<span slot='sum'>/</span>
						<mo-data-grid-footer-sum heading='Allocated' slot='sum'>
							${this.currentCycle?.tasks.reduce((c, val) => c + val.timeAllocated, 0)}
						</mo-data-grid-footer-sum>

					</mo-entity-data-grid>

					${!this.currentCycle ? nothing : this.newTaskTemplate}
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
