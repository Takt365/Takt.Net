/**
 * ========================================
 * 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
 * 文件名称：emit-type-shims.mjs
 * 创建时间：2025-02-02
 * 功能描述：根据后端OpenAPI和DTO生成前端TypeScript薄类型
 *   1. 解析后端C# DTO类，提取属性和继承关系
 *   2. 生成src/types下各模块的.d.ts类型定义文件
 *   3. 自动推导模块导出，避免手工硬编码维护
 *   4. 支持TaktEntityBase、TaktPagedQuery等基类继承
 * 
 * 使用方法：
 *   1. 生成类型定义：npm run gen:contracts
 *   2. 手动运行：node scripts/emit-type-shims.mjs
 *   3. 纯Node执行，无需Java，按后端C# Dto生成薄类型
 * 
 * 注意事项：
 *   - common.d.ts、global-setting.d.ts为手写契约，不会被删除或覆盖
 *   - 自动删除旧版多一层实体目录下的同名文件
 *   - 类型生成失败不影响开发，会使用已提交的文件
 * ========================================
 */
import fs from 'node:fs'
import path from 'node:path'
import { fileURLToPath } from 'node:url'

const __dirname = path.dirname(fileURLToPath(import.meta.url))
const root = path.resolve(__dirname, '..')
const typesDir = path.join(root, 'src/types')

const DTO_SKIP_SUFFIXES = new Set(['Template', 'Import', 'Export'])
const ENTITY_BASE_FIELDS = new Set([
  'configId',
  'extFieldJson',
  'remark',
  'createdById',
  'createdBy',
  'createdAt',
  'updatedById',
  'updatedBy',
  'updatedAt',
  'isDeleted',
  'deletedById',
  'deletedBy',
  'deletedAt'
])
const PAGED_QUERY_FIELDS = new Set(['pageIndex', 'pageSize', 'keyWords'])

function pascalToKebab(input) {
  return input
    .replace(/([A-Z]+)([A-Z][a-z])/g, '$1-$2')
    .replace(/([a-z0-9])([A-Z])/g, '$1-$2')
    .toLowerCase()
}

function kebabToPascal(input) {
  return input
    .split('-')
    .filter(Boolean)
    .map(seg => seg.charAt(0).toUpperCase() + seg.slice(1))
    .join('')
}

function namespaceSegmentsToRelDir(segments) {
  if (!Array.isArray(segments) || segments.length === 0) return undefined
  const normalized = [...segments]
  if (normalized.length === 0) return undefined
  return normalized
    .map(seg => pascalToKebab(seg))
    .join('/')
}

function parseDtoExportMeta(schemaKey) {
  if (!schemaKey.startsWith('Takt.Application.Dtos.')) return undefined
  const namespacePart = schemaKey.replace('Takt.Application.Dtos.', '')
  const lastDot = namespacePart.lastIndexOf('.')
  if (lastDot < 0) return undefined

  const dtoNamespace = namespacePart.slice(0, lastDot)
  const className = namespacePart.slice(lastDot + 1)
  if (!className.startsWith('Takt')) return undefined

  const relDir = namespaceSegmentsToRelDir(dtoNamespace.split('.'))
  if (!relDir) return undefined

  let raw = className.slice('Takt'.length)
  if (raw.endsWith('Dto')) raw = raw.slice(0, -3)
  if (!raw) return undefined

  const skipSuffix = [...DTO_SKIP_SUFFIXES].find(s => raw.endsWith(s))
  if (skipSuffix) return undefined

  return {
    relDir,
    exportName: raw,
    schemaKey
  }
}

function inferGroupEntityFromDtoFile(filePath) {
  const base = path.basename(filePath, '.cs')
  const noPrefix = base.startsWith('Takt') ? base.slice(4) : base
  return noPrefix.replace(/Dtos?$/, '') || 'Unknown'
}

function walkFiles(dir, out) {
  const entries = fs.readdirSync(dir, { withFileTypes: true })
  for (const e of entries) {
    const p = path.join(dir, e.name)
    if (e.isDirectory()) walkFiles(p, out)
    else out.push(p)
  }
}

function toCamelCase(name) {
  if (!name) return name
  return name.charAt(0).toLowerCase() + name.slice(1)
}

function mapCSharpTypeToTs(typeName, forceString = false) {
  const t = typeName.trim().replace(/\?$/, '')
  const listMatch = t.match(/^(?:List|IList|IEnumerable|ICollection)<\s*([A-Za-z0-9_?.]+)\s*>$/)
  if (listMatch) {
    const itemTs = mapCSharpTypeToTs(listMatch[1], forceString)
    return `${itemTs}[]`
  }
  if (t.endsWith('[]')) {
    const itemTs = mapCSharpTypeToTs(t.slice(0, -2), forceString)
    return `${itemTs}[]`
  }

  if (forceString && /^(long|ulong|int64|uint64)$/i.test(t)) return 'string'
  if (/^(string|char)$/i.test(t)) return 'string'
  if (/^(int|uint|short|ushort|long|ulong|byte|sbyte|float|double|decimal)$/i.test(t)) return 'number'
  if (/^(bool|boolean)$/i.test(t)) return 'boolean'
  if (/^(datetime|datetimeoffset)$/i.test(t)) return 'string'
  if (/^guid$/i.test(t)) return 'string'
  return 'unknown'
}

function collectBackendDtoPropertyMap() {
  const backendDtoRoot = path.resolve(root, '../../backend/src/Takt.Application/Dtos')
  if (!fs.existsSync(backendDtoRoot)) return {}

  const allFiles = []
  walkFiles(backendDtoRoot, allFiles)
  const dtoFiles = allFiles
    .filter(p => p.endsWith('.cs') && path.basename(p).startsWith('Takt'))
    .sort((a, b) => a.localeCompare(b))

  const map = {}
  for (const file of dtoFiles) {
    const content = fs.readFileSync(file, 'utf8')
    const classRe = /\bclass\s+(Takt[A-Za-z0-9_]+Dto)\b/g
    let classMatch
    while ((classMatch = classRe.exec(content)) != null) {
      const className = classMatch[1]
      const classNameEnd = classRe.lastIndex
      const openBrace = content.indexOf('{', classNameEnd)
      if (openBrace < 0) continue

      let depth = 1
      let i = openBrace + 1
      while (i < content.length && depth > 0) {
        const ch = content[i]
        if (ch === '{') depth += 1
        else if (ch === '}') depth -= 1
        i += 1
      }
      if (depth !== 0) continue

      const classBody = content.slice(openBrace + 1, i - 1)
      const lines = classBody.split(/\r?\n/)
      const props = []
      for (let idx = 0; idx < lines.length; idx += 1) {
        const line = lines[idx].trim()
        // 允许同一行属性初始化器，如 `{ get; set; } = new();`、`= string.Empty;`（否则 List<string> Roles 等整行被跳过）
        const propMatch = line.match(
          /^public\s+([A-Za-z0-9_<>,?.\[\]]+)\s+([A-Za-z0-9_]+)\s*\{\s*get;\s*set;\s*\}(?:\s*=\s*[^;]+;)?\s*$/
        )
        if (!propMatch) continue

        const csharpType = propMatch[1]
        const propertyName = propMatch[2]
        const contextStart = Math.max(0, idx - 6)
        const context = lines.slice(contextStart, idx).join('\n')
        const hasStringConverter = /ValueToStringConverter/.test(context)
        const rawType = mapCSharpTypeToTs(csharpType, hasStringConverter)
        props.push({
          name: toCamelCase(propertyName),
          optional: /\?$/.test(csharpType),
          rawType
        })
      }
      map[className] = props
    }
  }
  return map
}

/** 解析 `class TaktXxxDto : TaktYyyDto`，供同模块 TS `extends` 对齐后端 DTO 继承关系 */
function collectBackendDtoExtendsMap() {
  const backendDtoRoot = path.resolve(root, '../../backend/src/Takt.Application/Dtos')
  if (!fs.existsSync(backendDtoRoot)) return {}

  const allFiles = []
  walkFiles(backendDtoRoot, allFiles)
  const dtoFiles = allFiles
    .filter(p => p.endsWith('.cs') && path.basename(p).startsWith('Takt'))
    .sort((a, b) => a.localeCompare(b))

  const extendsMap = {}
  for (const file of dtoFiles) {
    const content = fs.readFileSync(file, 'utf8')
    const classRe = /\bclass\s+(Takt[A-Za-z0-9_]+Dto)\b/g
    let classMatch
    while ((classMatch = classRe.exec(content)) != null) {
      const className = classMatch[1]
      const classNameEnd = classRe.lastIndex
      const openBrace = content.indexOf('{', classNameEnd)
      if (openBrace < 0) continue
      const headerSlice = content.slice(classNameEnd, openBrace)
      const inh = headerSlice.match(/:\s*(Takt[A-Za-z0-9_]+Dto)\b/)
      if (inh) extendsMap[className] = inh[1]
    }
  }
  return extendsMap
}

function dtoClassNameToTsExportName(dtoClassName) {
  if (!dtoClassName || !dtoClassName.startsWith('Takt') || !dtoClassName.endsWith('Dto')) return undefined
  return dtoClassName.slice(4, -3)
}

function collectModuleExportsFromGenerated() {
  const backendDtoRoot = path.resolve(root, '../../backend/src/Takt.Application/Dtos')
  if (!fs.existsSync(backendDtoRoot)) return {}

  const allFiles = []
  walkFiles(backendDtoRoot, allFiles)
  const dtoFiles = allFiles
    .filter(p => p.endsWith('.cs') && path.basename(p).startsWith('Takt'))
    .sort((a, b) => a.localeCompare(b))

  const modules = {}
  for (const file of dtoFiles) {
    const rel = path.relative(backendDtoRoot, file).replace(/\\/g, '/')
    const segments = rel.split('/')
    if (segments.length < 2) continue
    const namespaceSegments = segments.slice(0, -1)
    const relDir = namespaceSegmentsToRelDir(namespaceSegments)
    if (!relDir) continue

    const content = fs.readFileSync(file, 'utf8')
    // 模块键：后端 Dtos 相对目录（kebab）+ 该文件对应 stem，与 import 路径一致；写入磁盘时仅最后一段为文件名
    const groupEntity = inferGroupEntityFromDtoFile(file)
    const groupRelPath = `${relDir}/${pascalToKebab(groupEntity)}`
    const classRe = /\bclass\s+(Takt[A-Za-z0-9_]+Dto)\b/g
    let m
    while ((m = classRe.exec(content)) != null) {
      const className = m[1]
      const schemaKey = `Takt.Application.Dtos.${namespaceSegments.join('.')}.${className}`
      const meta = parseDtoExportMeta(schemaKey)
      if (!meta) continue

      if (!modules[groupRelPath]) modules[groupRelPath] = new Map()
      modules[groupRelPath].set(meta.exportName, meta.schemaKey)
    }
  }

  const result = {}
  for (const [relPath, map] of Object.entries(modules)) {
    result[relPath] = [...map.entries()]
      .map(([exportName, schemaKey]) => ({ exportName, schemaKey }))
  }
  return result
}

function collectSchemaPropertyMap() {
  const generatedDir = path.join(typesDir, 'generated')
  if (!fs.existsSync(generatedDir)) return {}

  const files = fs
    .readdirSync(generatedDir)
    .filter(name => /^openapi-.*\.d\.ts$/i.test(name))
    .map(name => path.join(generatedDir, name))

  const map = {}
  for (const file of files) {
    const lines = fs.readFileSync(file, 'utf8').split(/\r?\n/)
    let inSchemas = false
    let schemaName = ''
    let schemaDepth = 0
    let props = []

    const flushSchema = () => {
      if (!schemaName) return
      map[schemaName] = props
      schemaName = ''
      schemaDepth = 0
      props = []
    }

    for (const line of lines) {
      if (!inSchemas) {
        if (/^\s{4}schemas:\s*\{/.test(line)) inSchemas = true
        continue
      }

      if (/^\s{4}\};?\s*$/.test(line)) {
        flushSchema()
        inSchemas = false
        continue
      }

      const schemaStart = line.match(/^\s{8}([A-Za-z0-9_`]+):\s*\{\s*$/)
      if (schemaStart) {
        flushSchema()
        schemaName = schemaStart[1]
        schemaDepth = 1
        continue
      }

      if (!schemaName) continue

      schemaDepth += (line.match(/\{/g) ?? []).length
      schemaDepth -= (line.match(/\}/g) ?? []).length

      const prop = line.match(/^\s{12}([A-Za-z0-9_]+)(\?)?:\s*(.+);\s*$/)
      if (prop) {
        props.push({
          name: prop[1],
          optional: prop[2] === '?',
          rawType: prop[3]
        })
      }

      if (schemaDepth <= 0) {
        flushSchema()
      }
    }
    flushSchema()
  }
  return map
}

function getSchemaShortName(schemaKey) {
  const idx = schemaKey.lastIndexOf('.')
  return idx >= 0 ? schemaKey.slice(idx + 1) : schemaKey
}

function normalizeTsType(typeText, propName) {
  let t = typeText.trim().replace(/\s*\|\s*null/g, '').replace(/\s*\|\s*undefined/g, '')
  if (t.includes('components["schemas"]')) {
    t = t.endsWith('[]') ? 'Record<string, unknown>[]' : 'Record<string, unknown>'
  }
  if (/(^|[^A-Za-z0-9_])number([^A-Za-z0-9_]|$)/.test(t) && /Id(s)?$/i.test(propName)) {
    t = t.replace(/\bnumber\[\]/g, 'string[]')
    t = t.replace(/\bnumber\b/g, 'string')
  }
  return t
}

function inferEntityName(exportName) {
  if (exportName.endsWith('Update')) return exportName.slice(0, -'Update'.length)
  if (exportName.endsWith('Create')) return exportName.slice(0, -'Create'.length)
  if (exportName.endsWith('Query')) return exportName.slice(0, -'Query'.length)
  return exportName
}

function buildInterfaceForExport(exportName, schemaKey, entityName, schemaProps, moduleEntityName) {
  const suffix = exportName.startsWith(entityName) ? exportName.slice(entityName.length) : ''
  const isMain = exportName === moduleEntityName
  const isQuery = suffix === 'Query'
  const isUpdate = suffix === 'Update'
  /** 与后端一致：存在 CreateDto 时 UpdateDto 继承 CreateDto；OpenAPI 未生成时仍从 BACKEND_DTO_PROPS 识别 */
  const createDtoKey = `Takt${entityName}CreateDto`
  const createSchemaPropsMerged = schemaProps[createDtoKey] ?? BACKEND_DTO_PROPS[createDtoKey] ?? []
  const hasCreateSchema =
    createSchemaPropsMerged.length > 0 ||
    Object.prototype.hasOwnProperty.call(BACKEND_DTO_PROPS, createDtoKey)
  let extendsText =
    isMain ? ' extends TaktEntityBase'
      : isQuery ? ' extends TaktPagedQuery'
        : (isUpdate && hasCreateSchema) ? ` extends ${entityName}Create`
          : ''

  const schemaShortName = getSchemaShortName(schemaKey)
  const parentDto = BACKEND_DTO_EXTENDS[schemaShortName]
  if (!extendsText && parentDto) {
    const parentExport = dtoClassNameToTsExportName(parentDto)
    const modulePrefix = `Takt${moduleEntityName}`
    if (
      parentExport &&
      parentDto.startsWith(modulePrefix) &&
      parentDto.endsWith('Dto') &&
      parentExport !== exportName
    ) {
      extendsText = ` extends ${parentExport}`
    }
  }
  const props = [...(schemaProps[schemaShortName] ?? BACKEND_DTO_PROPS[schemaShortName] ?? [])]
  const createSchemaProps = createSchemaPropsMerged
  const createPropNames = new Set(createSchemaProps.map(p => p.name))
  const mainDtoProps = schemaProps[`Takt${entityName}Dto`] ?? BACKEND_DTO_PROPS[`Takt${entityName}Dto`] ?? []
  const lines = [
    '/**',
    ` * ${entityName}${suffix || ''}类型（对应后端 ${schemaKey}）`,
    ' */',
    `export interface ${exportName}${extendsText} {`
  ]

  let emittedCount = 0
  for (const p of props) {
    if (isMain && ENTITY_BASE_FIELDS.has(p.name)) continue
    if (isQuery && PAGED_QUERY_FIELDS.has(p.name)) continue
    if (isUpdate && hasCreateSchema && createPropNames.has(p.name)) continue
    const fieldType = normalizeTsType(p.rawType, p.name)
    lines.push(`  /** 对应后端字段 ${p.name} */`)
    lines.push(`  ${p.name}${p.optional ? '?' : ''}: ${fieldType}`)
    emittedCount += 1
  }

  if (isUpdate && hasCreateSchema && emittedCount === 0) {
    const preferredId = `${entityName.charAt(0).toLowerCase()}${entityName.slice(1)}Id`
    const idProp =
      mainDtoProps.find(p => p.name === preferredId) ??
      mainDtoProps.find(p => /Id$/i.test(p.name))
    if (idProp) {
      const idType = normalizeTsType(idProp.rawType, idProp.name)
      lines.push(`  /** 对应后端字段 ${idProp.name} */`)
      lines.push(`  ${idProp.name}${idProp.optional ? '?' : ''}: ${idType}`)
    }
  }
  lines.push('}')
  lines.push('')
  return lines
}

function dtoFileName(exportName, entityName) {
  return `${pascalToKebab(entityName)}`
}

/** 按后端 OpenAPI 产物自动推导模块导出，避免手工硬编码维护。 */
const MODULE_EXPORTS = collectModuleExportsFromGenerated()
const SCHEMA_PROPS = collectSchemaPropertyMap()
const BACKEND_DTO_PROPS = collectBackendDtoPropertyMap()
const BACKEND_DTO_EXTENDS = collectBackendDtoExtendsMap()

/**
 * `emitSpecial` 写入的路径（相对 `src/types`）：与 OpenAPI schema 非一一对应的手拼层，无统一 MARKER。
 * 须与下方 `emitSpecial` 内 `writeFileSync` 目标保持一致。
 */
export const SHIM_EMIT_SPECIAL_REL = [
  // 无手工特例：统一由自动规则生成；业务自定义类型请维护在独立静态文件中。
]

/**
 * 与 `emit-type-shims` 实际会写入的 `.ts` 一致的路径（相对 frontend 根，POSIX），供 `check-contracts` 做 `git diff`。
 * 其中 `MODULE_EXPORTS` 的键即各 openapi 分组下的薄封装模块；`SHIM_EMIT_SPECIAL_REL` 为手写补充。
 */
export function getShimGitDiffPathsFromRepoRoot() {
  const set = new Set()
  const moduleExports = collectModuleExportsFromGenerated()
  for (const mod of Object.keys(moduleExports)) {
    const segs = mod.split('/')
    const fileName = segs.at(-1) || mod
    const dir = segs.slice(0, -1).join('/')
    set.add(dir ? `src/types/${dir}/${fileName}.d.ts` : `src/types/${fileName}.d.ts`)
  }
  for (const f of SHIM_EMIT_SPECIAL_REL) {
    set.add(`src/types/${f.replace(/\\/g, '/')}`)
  }
  return [...set].sort()
}

/** 保证同一模块内 `{Entity}Create` 在 `{Entity}Update` 之前，满足 `extends` 解析顺序 */
function compareDtoExportsForEmit(a, b, moduleEntityName) {
  const order = name => {
    if (name === moduleEntityName) return 0
    if (name === `${moduleEntityName}Query`) return 10
    if (name === `${moduleEntityName}Create`) return 20
    if (name === `${moduleEntityName}Update`) return 30
    if (name === `${moduleEntityName}Status`) return 40
    if (name === `${moduleEntityName}Sort`) return 45
    if (name === `${moduleEntityName}Template`) return 50
    if (name === `${moduleEntityName}Import`) return 55
    if (name === `${moduleEntityName}Export`) return 56
    return 100
  }
  const da = order(a.exportName)
  const db = order(b.exportName)
  if (da !== db) return da - db
  return a.exportName.localeCompare(b.exportName)
}

function emitGenericModule(relPath, exports) {
  // relPath =「后端 Dtos 下目录 kebab」+「/」+「该 cs 对应的类型文件 stem kebab」，例如 code/generator/gen-table
  // 输出文件必须与后端文件夹一致：.../types/code/generator/gen-table.d.ts（不再多一层 gen-table 目录）
  const relSegs = relPath.split('/')
  const fileName = relSegs.at(-1) || relPath
  const dirSegs = relSegs.slice(0, -1)
  const outDir = path.join(typesDir, ...dirSegs)
  fs.mkdirSync(outDir, { recursive: true })

  const legacyMergedDts = path.join(typesDir, ...relSegs) + '.d.ts'
  const legacyMergedTs = path.join(typesDir, ...relSegs) + '.ts'
  if (fs.existsSync(legacyMergedDts)) fs.unlinkSync(legacyMergedDts)
  if (fs.existsSync(legacyMergedTs)) fs.unlinkSync(legacyMergedTs)

  const moduleEntityName = kebabToPascal(fileName)
  const sorted = [...exports].sort((x, y) => compareDtoExportsForEmit(x, y, moduleEntityName))
  const lines = [
    '// ========================================',
    '// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)',
    `// 命名空间：@/types/${relPath}`,
    `// 文件名称：${fileName}.d.ts`,
    '// 创建时间：2025-02-02',
    '// 创建人：Takt365',
    `// 功能描述：${fileName}相关类型定义（自动生成）`,
    '// ========================================',
    '',
    "import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'",
    ''
  ]
  for (const item of sorted) {
    const entityName = inferEntityName(item.exportName)
    lines.push(...buildInterfaceForExport(item.exportName, item.schemaKey, entityName, SCHEMA_PROPS, moduleEntityName))
  }
  const out = path.join(outDir, `${fileName}.d.ts`)
  fs.writeFileSync(out, lines.join('\n'), 'utf8')

  // 删除旧版「多一层实体目录」下的同名文件，例如 .../code/generator/gen-table/gen-table.d.ts
  const legacyNestedFile = path.join(typesDir, ...relSegs, `${fileName}.d.ts`)
  if (legacyNestedFile !== out && fs.existsSync(legacyNestedFile)) {
    fs.unlinkSync(legacyNestedFile)
  }
  try {
    const legacyNestedDir = path.join(typesDir, ...relSegs)
    if (legacyNestedDir !== outDir && fs.existsSync(legacyNestedDir)) {
      const left = fs.readdirSync(legacyNestedDir)
      if (left.length === 0) fs.rmdirSync(legacyNestedDir)
    }
  } catch {
    // 非空目录等忽略
  }

  const allLegacy = fs.readdirSync(outDir).filter(n =>
    n.endsWith('.d.ts') &&
    n !== `${fileName}.d.ts` &&
    n.endsWith('-dto.d.ts')
  )
  for (const legacy of allLegacy) {
    fs.unlinkSync(path.join(outDir, legacy))
  }
}

function emitSpecial() {
  // no-op: 移除手工模板硬编码，统一走自动生成/静态文件维护。
}

function main() {
  for (const [rel, exports] of Object.entries(MODULE_EXPORTS)) {
    emitGenericModule(rel, exports)
  }
  emitSpecial()
  // common.d.ts、global-setting.d.ts 为手写契约，不在此脚本中删除或覆盖。
  console.log('[emit-type-shims] wrote modules under', typesDir)
}

const _emitThisFile = path.resolve(fileURLToPath(import.meta.url))
const _emitEntry = process.argv[1] ? path.resolve(process.argv[1]) : ''
if (_emitEntry !== '' && _emitThisFile === _emitEntry) {
  main()
}
