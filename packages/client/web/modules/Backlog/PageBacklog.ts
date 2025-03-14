import { component, html } from '@a11d/lit'
import { EntitiesPageComponent } from '@3mo/del'
import { route } from '@a11d/lit-application'
import { PageDashboard } from 'app'
import { SDK } from 'modules'
import { Api } from '@a11d/api'
import { DialogDirective } from './DialogDirective'

@route(PageDashboard, '/backlog')
@component('sunny-page-backlog')
export class PageBacklog extends EntitiesPageComponent<SDK.Directive> {
	protected override get template() {
		return html`
			<lit-page fullHeight>
				<mo-card>
					<mo-entity-data-grid pagination='auto'
						.fetch=${() => Api.get<Array<SDK.Directive>>('/backlog')}
						.delete=${(entity: SDK.Directive) => Api.delete(`/backlog/${entity.id}`)}
						.createOrEdit=${DialogDirective}
					>
						<mo-data-grid-column-text heading='Directive' dataSelector=${getKeyPath<SDK.Directive>('name')}></mo-data-grid-column-text>
						<mo-data-grid-column-text heading='Type' dataSelector=${getKeyPath<SDK.Directive>('type.name')}></mo-data-grid-column-text>
						<mo-data-grid-column-text heading='Management' dataSelector=${getKeyPath<SDK.Directive>('management')}></mo-data-grid-column-text>
						<mo-data-grid-column-text heading='Description' dataSelector=${getKeyPath<SDK.Directive>('description')}></mo-data-grid-column-text>
					</mo-entity-data-grid>
				</mo-card>
			</lit-page>
		`
	}
}

declare global {
	interface HtmlElementTagNameMap {
		'sunny-page-backlog': PageBacklog
	}
}
