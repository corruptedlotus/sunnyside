import { build, context } from 'esbuild'
import { resolve } from 'path'

const args = process.argv.slice(2)
const mode = args.includes('--mode=development') ? 'development' : 'production'
const isWatch = args.includes('--watch')

var buildConf = {
	entryPoints: {
		app: './index.ts',
	},
	outdir: mode === 'production' ? resolve('../../../bin/client') : resolve('../../../dbg/client'),
	bundle: true,
	minify: mode === 'production',
	sourcemap: mode === 'development',
	format: 'esm',
	target: 'esnext',
	platform: 'browser',
	loader: { '.ts': 'ts' },
	tsconfig: './tsconfig.json',
}

if (!isWatch) {
	await build(buildConf).catch(() => process.exit(1))
} else {
	await context(buildConf).then(ctx => {
		ctx.watch()
		console.log('✅ BUILD SUCCESS')
	}).catch(() => process.exit(1))
}
