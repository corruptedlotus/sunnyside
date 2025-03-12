import { component, css } from '@a11d/lit'
import { Application } from '@a11d/lit-application'

Theme.accent.value = 'rgb(221, 137, 36)'

@component('sunnyside-app')
export class Sunnyside extends Application {
	static override get styles() {
		return css`
			@import url('https://fonts.googleapis.com/css2?family=Outfit:ital,wght@0,300&display=swap');

			:root {
				--mo-font-family: 'Outfit', 'Segoe UI', Helvetica, sans-serif;
				font-family: var(--mo-font-family);
				--mdc-typography-font-family: var(--mo-font-family);
				--mdc-typography-body1-font-family: var(--mo-font-family);
				--md-ref-typeface-plain: var(--mo-font-family);
			}

			${super.styles}
		`
	}
}
