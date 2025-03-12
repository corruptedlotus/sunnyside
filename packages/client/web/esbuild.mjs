import { build, context } from 'esbuild'
import { resolve } from 'path'
import copy from 'esbuild-plugin-copy'

const args = process.argv.slice(2)
const mode = args.includes('--mode=development') ? 'development' : 'production'
const isWatch = args.includes('--watch')

var buildConf = {
	entryPoints: {
		modules: './modules/index.ts',
		admin: './admin/index.ts',
	},
	outdir: mode === 'production' ? resolve('../../bin/client/dist') : resolve('../../dbg/client/dist'),
	bundle: true,
	minify: mode === 'production',
	sourcemap: mode === 'development',
	format: 'esm',
	target: 'esnext',
	platform: 'browser',
	loader: { '.ts': 'ts' },
	plugins: [
		copy({
			resolveFrom: 'out',
			assets: [
				{
					from: ['./assets/**/*'],
					to: ['../assets'],
				},
				{
					from: ['./static/**/*'],
					to: ['../'],
				},
			],
			watch: true,
		}),
	],
	tsconfig: './tsconfig.json',
}

if (!isWatch) {
	await build(buildConf).catch(() => process.exit(1))
} else {
	await context(buildConf).then(ctx => ctx.watch()).catch(() => process.exit(1))
}
