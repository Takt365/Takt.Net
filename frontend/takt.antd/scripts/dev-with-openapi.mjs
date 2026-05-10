/**
 * ========================================
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 文件名称：dev-with-openapi.mjs
 * 创建时间：2025-02-02
 * 功能描述：开发环境启动脚本
 *   1. 启动Vite前可选择性运行emit-type-shims刷新类型定义
 *   2. 根据后端C# DTO自动生成前端TypeScript类型
 *   3. 默认不自动生成，需要传参才生成
 * 
 * 使用方法：
 *   1. 启动开发环境（不生成类型）：npm run dev
 *   2. 生成指定模块后启动：npm run dev -- --module employee
 *   3. 全量生成后启动：npm run dev -- --all
 *   4. 跳过类型生成：npm run dev
 * 
 * 注意事项：
 *   - 默认不自动生成类型，使用已提交的类型文件
 *   - 如果需要生成类型，必须传 --module 或 --all 参数
 *   - 如果emit-type-shims失败，会警告但继续启动Vite
 * ========================================
 */
import { execFileSync, spawn } from 'node:child_process'
import { resolve, dirname } from 'node:path'
import { fileURLToPath } from 'node:url'
import process from 'node:process'

const __dirname = dirname(fileURLToPath(import.meta.url))
const root = resolve(__dirname, '..')
const shimsScript = resolve(__dirname, 'emit-type-shims.mjs')

// 检查是否需要生成类型（必须传 --module 或 --all 参数）
const args = process.argv.slice(2)
const shouldGenerateTypes = args.some(arg => 
  arg.startsWith('--module=') || arg === '--module' || arg === '--all'
)

if (shouldGenerateTypes) {
  try {
    console.log('[dev] 开始生成类型定义...')
    execFileSync(process.execPath, [shimsScript, ...process.argv.slice(2)], { cwd: root, stdio: 'inherit', env: process.env })
    console.log('[dev] 类型定义生成完成\n')
  } catch {
    console.warn('[dev] emit-type-shims 失败，继续使用仓库内已提交的 src/types 下 .d.ts 自动类型定义')
  }
} else {
  console.log('[dev] 未指定类型生成参数，使用已提交的类型文件')
  console.log('[dev] 提示：使用 --module <name> 或 --all 参数可自动生成类型\n')
}

const vite = spawn(process.execPath, [resolve(root, 'node_modules/vite/bin/vite.js'), ...process.argv.slice(2)], {
  cwd: root,
  stdio: 'inherit',
  env: process.env
})

vite.on('exit', (code) => process.exit(code ?? 0))
