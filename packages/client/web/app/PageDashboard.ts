import { component, html } from '@a11d/lit'
import { PageComponent, RouterController, route, routerLink } from '@a11d/lit-application'
import { PageBacklog, PageCycle } from 'modules'

@route('/*')
@route('/:page?')
@component('sunny-dashboard')
export class PageDashboard extends PageComponent {
	readonly router = new RouterController(this, [], {
		fallback: {
			render: () => html`
				<mo-empty-state icon='touch_app'>Select a Page</mo-empty-state>
			`
		}
	})

	protected override get template() {
		return html`
			<lit-page fullHeight>
				<mo-split-page-host>
					<mo-flex slot='sidebar'>
						<mo-navigation-list-item icon='view_in_ar' ${routerLink(new PageCycle)}>This Cycle</mo-navigation-list-item>
						<mo-navigation-list-item icon='view_in_ar' ${routerLink(new PageBacklog)}>Backlog</mo-navigation-list-item>
					</mo-flex>

					${this.router.outlet}
				</mo-split-page-host>
			</lit-page>
		`
	}
}
