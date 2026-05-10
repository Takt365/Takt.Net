/**
 * ========================================
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 文件名称：wrap-locale-page.mjs
 * 创建时间：2025-02-02
 * 功能描述：国际化locales业务模块文案包装工具
 *   1. 将export default {...}统一包一层page:{...}
 *   2. 使用字符串/注释感知的括号匹配，避免文案中的{}误判
 *   3. 去掉page:{...}块内的多余空行
 * 
 * 使用方法：
 *   1. 规范化locales文件：node scripts/wrap-locale-page.mjs
 *   2. 自动扫描src/locales目录下所有.ts文件
 *   3. 跳过文件：仅index.ts（所有业务模块都需要page包装）
 * 
 * 注意事项：
 *   - 所有翻译文件都必须包含page:{}包装
 *   - 使用智能括号匹配，支持字符串和注释中的{}
 * ========================================
 */

import fs from 'fs'
import path from 'path'
import { fileURLToPath } from 'url'

const __dirname = path.dirname(fileURLToPath(import.meta.url))
const localesRoot = path.join(__dirname, '../src/locales')

function norm(p) {
  return p.replace(/\\/g, '/')
}

function shouldSkip(filePath) {
  const n = norm(filePath)
  // 仅跳过index.ts文件
  if (/\/locales\/index\.ts$/i.test(n)) return true
  return false
}

function isLocaleIndexTs(full) {
  return full.endsWith(`${path.sep}index.ts`) || full.endsWith('/index.ts')
}

function findExportDefaultOpenBrace(content) {
  const idx = content.indexOf('export default')
  if (idx === -1) return -1
  return content.indexOf('{', idx)
}

/** 从 openBraceIndex 处的 `{` 起，找到与之配对的 `}`（深度 0） */
function findMatchingClosingBrace(content, openBraceIndex) {
  let i = openBraceIndex
  let depth = 0
  /** 0 code, 1 line comment, 2 block comment, 3 ', 4 ", 5 ` */
  let mode = 0

  while (i < content.length) {
    const c = content[i]
    const n = content[i + 1]

    if (mode === 0) {
      if (c === '/' && n === '/') {
        mode = 1
        i += 2
        continue
      }
      if (c === '/' && n === '*') {
        mode = 2
        i += 2
        continue
      }
      if (c === "'") {
        mode = 3
        i++
        continue
      }
      if (c === '"') {
        mode = 4
        i++
        continue
      }
      if (c === '`') {
        mode = 5
        i++
        continue
      }
      if (c === '{') depth++
      else if (c === '}') {
        depth--
        if (depth === 0) return i
      }
      i++
      continue
    }

    if (mode === 1) {
      if (c === '\n' || c === '\r') mode = 0
      i++
      continue
    }

    if (mode === 2) {
      if (c === '*' && n === '/') {
        mode = 0
        i += 2
        continue
      }
      i++
      continue
    }

    if (mode === 3) {
      if (c === '\\') {
        i += 2
        continue
      }
      if (c === "'") {
        mode = 0
        i++
        continue
      }
      i++
      continue
    }

    if (mode === 4) {
      if (c === '\\') {
        i += 2
        continue
      }
      if (c === '"') {
        mode = 0
        i++
        continue
      }
      i++
      continue
    }

    if (mode === 5) {
      if (c === '\\') {
        i += 2
        continue
      }
      if (c === '`') {
        mode = 0
        i++
        continue
      }
      i++
      continue
    }
  }

  return -1
}

function alreadyWrapped(inner) {
  return /^page\s*:/m.test(inner.trimStart())
}

function indentBlock(text, spaces) {
  const pad = ' '.repeat(spaces)
  return text.split('\n').map((line) => pad + line).join('\n')
}

function wrapFile(filePath) {
  const content = fs.readFileSync(filePath, 'utf8')
  const open = findExportDefaultOpenBrace(content)
  if (open === -1) return false
  const close = findMatchingClosingBrace(content, open)
  if (close === -1) return false
  const inner = content.slice(open + 1, close)
  if (alreadyWrapped(inner)) return false
  const innerBody = inner.trim()
  const innerIndented = innerBody ? indentBlock(innerBody, 4) : ''
  const wrappedObj = innerIndented
    ? '{\n  page: {\n' + innerIndented + '\n  }\n}'
    : '{\n  page: {}\n}'
  const newContent = content.slice(0, open) + wrappedObj + content.slice(close + 1)
  fs.writeFileSync(filePath, newContent, 'utf8')
  return true
}

/** 去掉 page: { 后与闭合 `}` 前的多余空行（无 page 块则不改） */
function normalizePageBlankLines(content) {
  if (!/page:\s*\{/.test(content)) return null
  let c = content
  // 去掉 page: { 后的空行
  c = c.replace(/(page:\s*\{)\s*(?:\r?\n[ \t]*)+\r?\n/g, '$1\n')
  // 去掉闭合 } 前的空行
  const closingRegex = new RegExp('(?:\\r?\\n[ \\t]*)+\\r?\\n(\\s{2}\\}\\r?\\n\\}\\s*)$', 'm')
  c = c.replace(closingRegex, '\n$1')
  return c
}

function walkLocales(dir) {
  const entries = fs.readdirSync(dir, { withFileTypes: true })
  for (const e of entries) {
    const full = path.join(dir, e.name)
    if (e.isDirectory()) walkLocales(full)
    else if (e.isFile() && e.name.endsWith('.ts')) {
      if (isLocaleIndexTs(full)) continue
      if (!shouldSkip(full) && wrapFile(full)) {
        console.log('wrapped:', norm(full))
      }
      const raw = fs.readFileSync(full, 'utf8')
      const out = normalizePageBlankLines(raw)
      if (out !== null && out !== raw) {
        fs.writeFileSync(full, out, 'utf8')
        console.log('normalized:', norm(full))
      }
    }
  }
}

walkLocales(localesRoot)
console.log('done')
