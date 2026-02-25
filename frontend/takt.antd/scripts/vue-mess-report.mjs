#!/usr/bin/env node
/**
 * 运行 vue-mess-detector 并将输出写入文件（UTF-8 编码，避免 Windows 下乱码）
 * 用法：node scripts/vue-mess-report.mjs [format] [outputFile]
 * 默认：format=table, outputFile=report.txt
 */
import { spawn } from 'child_process'
import { writeFileSync } from 'fs'
import { resolve, dirname } from 'path'
import { fileURLToPath } from 'url'

const __dirname = dirname(fileURLToPath(import.meta.url))
const root = resolve(__dirname, '..')

const format = process.argv[2] || 'table'
const outputFile = process.argv[3] || (format === 'json' ? 'report.json' : 'report.txt')

const proc = spawn('vue-mess-detector', ['analyze', '--output', format], {
  shell: true,
  cwd: root,
  stdio: ['inherit', 'pipe', 'pipe']
})

let stdout = ''
let stderr = ''
proc.stdout?.on('data', (d) => { stdout += d.toString('utf8') })
proc.stderr?.on('data', (d) => { stderr += d.toString('utf8') })

proc.on('close', (code) => {
  const out = format === 'json' ? stdout : stdout + (stderr ? '\n' + stderr : '')
  const path = resolve(root, outputFile)
  writeFileSync(path, out, 'utf8')
  console.log(`Report written to ${outputFile} (UTF-8)`)
  process.exit(code ?? 0)
})
