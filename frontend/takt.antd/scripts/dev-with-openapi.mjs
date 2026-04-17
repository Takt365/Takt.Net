/**
 * 启动 Vite 前先尝试拉取 OpenAPI 并生成 src/types/generated/*。
 * 若本机未启动后端或 TLS 失败，仅打印警告并继续（使用仓库已提交的 generated）。
 *
 * 跳过生成：环境变量 TAKT_OPENAPI_SKIP=1
 */
import { execFileSync, spawn } from 'node:child_process'
import { resolve, dirname } from 'node:path'
import { fileURLToPath } from 'node:url'
import process from 'node:process'

const __dirname = dirname(fileURLToPath(import.meta.url))
const root = resolve(__dirname, '..')
const genScript = resolve(__dirname, 'generate-openapi-types.mjs')
const shimsScript = resolve(__dirname, 'emit-type-shims.mjs')

if (process.env.TAKT_OPENAPI_SKIP === '1') {
  console.warn('[dev] TAKT_OPENAPI_SKIP=1，跳过 OpenAPI 与类型别名生成')
} else {
  try {
    execFileSync(process.execPath, [genScript], { cwd: root, stdio: 'inherit', env: process.env })
  } catch {
    console.warn(
      '[dev] OpenAPI 生成失败（后端未启动、证书或网络问题），继续使用仓库内已提交的 src/types/generated/'
    )
  }
  try {
    execFileSync(process.execPath, [shimsScript], { cwd: root, stdio: 'inherit', env: process.env })
  } catch {
    console.warn('[dev] emit-type-shims 失败，继续使用仓库内已提交的 src/types/**/*.ts 薄封装')
  }
}

const vite = spawn(process.execPath, [resolve(root, 'node_modules/vite/bin/vite.js'), ...process.argv.slice(2)], {
  cwd: root,
  stdio: 'inherit',
  env: process.env
})

vite.on('exit', (code) => process.exit(code ?? 0))
