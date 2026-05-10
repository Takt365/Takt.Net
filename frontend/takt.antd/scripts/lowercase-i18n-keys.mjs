/**
 * ========================================
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 文件名称：lowercase-i18n-keys.mjs
 * 创建时间：2025-02-02
 * 功能描述：国际化翻译键全小写化工具
 *   1. locales下*.ts（除index）：对象键、引号键统一转小写
 *   2. src下*.ts/*.vue：t/$t/i18n.global.t的首参为字符串且命中根命名空间时整键转小写
 *   3. entity.*、validation.*除外，保持原有大小写
 *   4. 模板字符串：${entityKey}.*不动；${localeNs}.xxx将xxx转小写
 * 
 * 使用方法：
 *   1. 全量处理：node scripts/lowercase-i18n-keys.mjs
 *   2. 仅处理.vue：node scripts/lowercase-i18n-keys.mjs --vue-only
 *   3. 自动扫描src/locales和src目录下的所有.ts/.vue文件
 * 
 * 注意事项：
 *   - 根命名空间来源：扫描src/locales下第一层目录名
 *   - 合并EXTRA_ROOT_NAMESPACES（如tenant在identity/tenant下但运行时键为tenant.*）
 *   - 跳过注释行
 * ========================================
 */

import fs from 'fs'
import path from 'path'
import { fileURLToPath } from 'url'

const __dirname = path.dirname(fileURLToPath(import.meta.url))
const srcRoot = path.join(__dirname, '../src')
const localesDir = path.join(srcRoot, 'locales')
const vueOnly = process.argv.includes('--vue-only')

/** 非 locales 顶层目录、但代码里以「根」形式引用的命名空间 */
const EXTRA_ROOT_NAMESPACES = new Set(['tenant'])

function discoverLocaleTopSegments() {
  const set = new Set(EXTRA_ROOT_NAMESPACES)
  if (!fs.existsSync(localesDir)) return set
  for (const name of fs.readdirSync(localesDir)) {
    const full = path.join(localesDir, name)
    if (!fs.statSync(full).isDirectory()) continue
    set.add(name)
  }
  return set
}

const FIRST_SEG = discoverLocaleTopSegments()

function buildFrontendPrefixRe(firstSegSet) {
  const escaped = [...firstSegSet]
    .sort()
    .map((s) => s.replace(/[.*+?^${}()|[\]\\]/g, '\\$&'))
  return new RegExp(`^(${escaped.join('|')})\\.`)
}

const FRONTEND_PREFIX_RE = buildFrontendPrefixRe(FIRST_SEG)

function norm(p) {
  return p.replace(/\\/g, '/')
}

function shouldSkipCommentLine(trimmed) {
  return (
    trimmed.startsWith('//') ||
    trimmed.startsWith('*') ||
    trimmed.startsWith('/**') ||
    trimmed.startsWith('*/') ||
    trimmed.startsWith('<!--')
  )
}

function transformLocaleLine(line) {
  /** CRLF 下行尾 \\r 会导致 $ 锚定失败，先去掉行尾 \\r */
  line = line.replace(/\r$/, '')
  const trimmed = line.trimStart()
  if (shouldSkipCommentLine(trimmed)) return line

  let m = line.match(/^(\s*)'([^']*)'(\s*:\s*)(.*)$/)
  if (m) {
    const key = m[2]
    if (key !== key.toLowerCase()) {
      return m[1] + "'" + key.toLowerCase() + "'" + m[3] + m[4]
    }
    return line
  }

  m = line.match(/^(\s*)"([^"]*)"(\s*:\s*)(.*)$/)
  if (m) {
    const key = m[2]
    if (key !== key.toLowerCase()) {
      return m[1] + '"' + key.toLowerCase() + '"' + m[3] + m[4]
    }
    return line
  }

  m = line.match(/^(\s*)([a-zA-Z_][a-zA-Z0-9_]*)(\s*:\s*)(.*)$/)
  if (m) {
    const key = m[2]
    if (/^\d+$/.test(key)) return line
    const nk = key.toLowerCase()
    if (nk !== key) return m[1] + nk + m[3] + m[4]
  }
  return line
}

function processLocaleFile(filePath) {
  const content = fs.readFileSync(filePath, 'utf8')
  const out = content.split(/\r?\n/).map(transformLocaleLine).join('\n')
  const normNl = (s) => s.replace(/\r\n/g, '\n').replace(/\r/g, '\n')
  if (normNl(out) !== normNl(content)) {
    fs.writeFileSync(filePath, out, 'utf8')
    return true
  }
  return false
}

function lowerIfWhitelisted(key) {
  if (key.startsWith('entity.') || key.startsWith('validation.')) return key
  if (!FRONTEND_PREFIX_RE.test(key)) return key
  if (!/[A-Z]/.test(key)) return key
  return key.toLowerCase()
}

function transformTemplateInner(inner) {
  if (/^\$\{entityKey\}/.test(inner)) return inner

  const localeNsMatch = inner.match(/^\$\{localeNs\}\.([a-zA-Z0-9_]+)(.*)$/)
  if (localeNsMatch) {
    const suf = localeNsMatch[1]
    const rest = localeNsMatch[2] ?? ''
    const nsuf = suf.toLowerCase()
    if (nsuf !== suf) return `\${localeNs}.${nsuf}${rest}`
    return inner
  }

  const idx = inner.indexOf('${')
  const pre = idx === -1 ? inner : inner.slice(0, idx)
  const suf = idx === -1 ? '' : inner.slice(idx)

  if (pre === '') return inner

  const firstSeg = pre.split('.')[0] ?? ''
  if (!FIRST_SEG.has(firstSeg)) return inner

  const lowerPre = pre.replace(/[A-Z]/g, (c) => c.toLowerCase())
  return lowerPre + suf
}

function transformNonLocaleContent(content) {
  let out = content

  out = out.replace(/i18n\.global\.t\s*\(\s*(['"])((?:\\.|(?!\1).)*)\1/g, (full, q, key) => {
    const nk = lowerIfWhitelisted(key)
    return nk === key ? full : `i18n.global.t(${q}${nk}${q}`
  })

  out = out.replace(/(?<![.\w$])(\$t|t)\s*\(\s*(['"])((?:\\.|(?!\2).)*)\2/g, (full, fn, q, key) => {
    const nk = lowerIfWhitelisted(key)
    return nk === key ? full : `${fn}(${q}${nk}${q}`
  })

  out = out.replace(/(?<![.\w$])(\$t|t)\s*\(\s*`([^`]*?)`/g, (full, fn, inner) => {
    const transformed = transformTemplateInner(inner)
    return transformed === inner ? full : `${fn}(\`${transformed}\``
  })

  out = out.replace(/i18n\.global\.t\s*\(\s*`([^`]*?)`/g, (full, inner) => {
    const transformed = transformTemplateInner(inner)
    return transformed === inner ? full : `i18n.global.t(\`${transformed}\``
  })

  return out
}

function walk(dir, cb) {
  for (const name of fs.readdirSync(dir)) {
    const full = path.join(dir, name)
    const st = fs.statSync(full)
    if (st.isDirectory()) walk(full, cb)
    else cb(full)
  }
}

// --- 1) locales ---
let nLoc = 0
walk(localesDir, (full) => {
  if (!full.endsWith('.ts')) return
  if (full.endsWith(`${path.sep}index.ts`) || full.endsWith('/index.ts')) return
  if (processLocaleFile(full)) {
    nLoc++
    console.log('locale:', norm(full))
  }
})
console.log('locales updated:', nLoc)

// --- 2) src 其余（可选仅 .vue）---
let nSrc = 0
walk(srcRoot, (full) => {
  const n = norm(full)
  if (n.includes('/locales/')) return
  if (vueOnly && !full.endsWith('.vue')) return
  if (!full.endsWith('.ts') && !full.endsWith('.vue')) return
  const content = fs.readFileSync(full, 'utf8')
  const out = transformNonLocaleContent(content)
  if (out !== content) {
    fs.writeFileSync(full, out, 'utf8')
    nSrc++
    console.log('source:', n)
  }
})
console.log('sources updated:', nSrc)
console.log('vue-only:', vueOnly)
console.log('root namespaces:', [...FIRST_SEG].sort().join(', '))
console.log('done')
