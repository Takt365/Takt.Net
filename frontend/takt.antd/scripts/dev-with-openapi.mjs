/**
 * ========================================
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 文件名称：dev-with-openapi.mjs
 * 创建时间：2025-02-02
 * 功能描述：开发环境启动脚本
 *   1. 启动Vite前先运行emit-type-shims刷新类型定义
 *   2. 根据后端C# DTO自动生成前端TypeScript类型
 *   3. 不再生成src/types/generated，仅由emit-type-shims维护
 * 
 * 使用方法：
 *   1. 启动开发环境：npm run dev
 *   2. 跳过类型生成：TAKT_OPENAPI_SKIP=1 npm run dev
 *   3. 传递Vite参数：npm run dev -- --port 3000
 * 
 * 注意事项：
 *   - 如果emit-type-shims失败，会警告但继续启动Vite
 *   - 使用已提交的src/types/.d.ts文件作为fallback
 *   - 环境变量TAKT_OPENAPI_SKIP=1可跳过类型生成
 * ========================================
 */
import { execFileSync, spawn } from 'node:child_process'
import { resolve, dirname } from 'node:path'
import { fileURLToPath } from 'node:url'
import process from 'node:process'

const __dirname = dirname(fileURLToPath(import.meta.url))
const root = resolve(__dirname, '..')
const shimsScript = resolve(__dirname, 'emit-type-shims.mjs')

if (process.env.TAKT_OPENAPI_SKIP === '1') {
  console.warn('[dev] TAKT_OPENAPI_SKIP=1，跳过 emit-type-shims')
} else {
  try {
    execFileSync(process.execPath, [shimsScript], { cwd: root, stdio: 'inherit', env: process.env })
  } catch {
    console.warn('[dev] emit-type-shims 失败，继续使用仓库内已提交的 src/types 下 .d.ts 自动类型定义')
  }
}

const vite = spawn(process.execPath, [resolve(root, 'node_modules/vite/bin/vite.js'), ...process.argv.slice(2)], {
  cwd: root,
  stdio: 'inherit',
  env: process.env
})

vite.on('exit', (code) => process.exit(code ?? 0))
