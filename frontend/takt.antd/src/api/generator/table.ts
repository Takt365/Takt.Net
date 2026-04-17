// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/api/generator
// 文件名称：table.ts
// 功能描述：代码生成表配置 API，对应后端 TaktGenTablesController
// ========================================

import request from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type { GenTable, GenTableQuery, GenTableCreate, GenTableUpdate } from '@/types/generator/table'

export type { GenTable, GenTableQuery, GenTableCreate, GenTableUpdate } from '@/types/generator/table'

const tableUrl = '/api/TaktGenTables'

/** 获取代码生成表配置列表（分页），对应后端 GetListAsync */
export function getGenTableList(params: GenTableQuery): Promise<TaktPagedResult<GenTable>> {
  return request({
    url: `${tableUrl}/list`,
    method: 'get',
    params
  })
}

/** 根据 ID 获取代码生成表配置，对应后端 GetByIdAsync */
export function getGenTableById(id: string): Promise<GenTable> {
  return request({
    url: `${tableUrl}/${id}`,
    method: 'get'
  })
}

/** 创建代码生成表配置，对应后端 CreateAsync */
export function createGenTable(data: GenTableCreate): Promise<GenTable> {
  return request({
    url: tableUrl,
    method: 'post',
    data
  })
}

/** 更新代码生成表配置，对应后端 UpdateAsync */
export function updateGenTable(id: string, data: GenTableUpdate): Promise<GenTable> {
  return request({
    url: `${tableUrl}/${id}`,
    method: 'put',
    data
  })
}

/** 删除代码生成表配置，对应后端 DeleteAsync */
export function deleteGenTable(id: string): Promise<void> {
  return request({
    url: `${tableUrl}/${id}`,
    method: 'delete'
  })
}

/** 获取默认生成路径（当前项目路径），对应后端 GetDefaultGenPath */
export function getDefaultGenPath(): Promise<{ path: string }> {
  return request({
    url: `${tableUrl}/default-gen-path`,
    method: 'get'
  })
}

/** 生成代码返回：ZIP 时为 Blob；自定义路径时为后端返回的 JSON */
export interface GenerateCodeResult {
  message: string
  count: number
  files: string[]
}

/** 生成预览返回（自定义路径时）：将生成的文件列表与已存在的文件列表，用于覆盖确认。 */
export interface GenerateCodePreviewFile {
  path: string
  content: string
  isExisting: boolean
}

export interface GenerateCodePreviewValidationIssue {
  templateKey: string
  targetPath?: string
  message: string
}

/** 生成预览返回：兼容旧版 files/existingFiles，同时提供 previewFiles（含渲染后的代码内容）。 */
export interface GenerateCodePreviewResult {
  files: string[]
  existingFiles: string[]
  previewFiles?: GenerateCodePreviewFile[]
  validationIssues?: GenerateCodePreviewValidationIssue[]
  isValid?: boolean
}

/** 生成代码预览。GenMethod=1（自定义路径）或 2（当前项目）时有效，返回将生成的文件及其中已存在的文件。 */
export function generateCodePreview(id: string, genPath?: string): Promise<GenerateCodePreviewResult> {
  return request({
    url: `${tableUrl}/${id}/generate-preview`,
    method: 'get',
    params: genPath != null && genPath !== '' ? { genPath } : undefined
  })
}

/** 根据表配置生成代码。后端按表配置的 GenMethod：0=返回 ZIP（Blob），1=自定义路径、2=当前项目=写入磁盘并返回 JSON。genPathOverride 仅 GenMethod=1 时“另存为”有效。 */
export function generateCode(
  id: string,
  genMethod?: number,
  genPathOverride?: string
): Promise<Blob | GenerateCodeResult> {
  const isWriteToDisk = Number(genMethod) === 1 || Number(genMethod) === 2
  return request({
    url: `${tableUrl}/${id}/generate`,
    method: 'post',
    ...(Number(genMethod) === 1 && genPathOverride != null && genPathOverride !== ''
      ? { data: { genPath: genPathOverride } }
      : {}),
    ...(isWriteToDisk ? {} : { responseType: 'blob' })
  })
}

/** 数据库配置项（对应后端 TaktDatabaseInfo） */
export interface DatabaseConfig {
  configId: string
  displayName: string
}

/** 数据库表信息（对应后端 TaktDatabaseTableInfoDto） */
export interface DatabaseTableInfo {
  tableName: string
  tableComment?: string
}

/** 导入表请求（对应后端 TaktImportTableRequestDto） */
export interface ImportTableRequest {
  configId: string
  tableName: string
  tableOverrides?: GenTableCreate
}

/** 获取所有数据库配置列表，对应后端 GetDatabaseConfigsAsync */
export function getDatabaseConfigs(): Promise<DatabaseConfig[]> {
  return request({
    url: `${tableUrl}/database-configs`,
    method: 'get'
  })
}

/** 根据 ConfigId 获取该数据库下的数据表列表，对应后端 GetDatabaseTablesAsync */
export function getDatabaseTables(configId: string): Promise<DatabaseTableInfo[]> {
  return request({
    url: `${tableUrl}/database-tables`,
    method: 'get',
    params: { configId }
  })
}

/** 从数据库导入指定表，对应后端 ImportTableAsync */
export function importTable(body: ImportTableRequest): Promise<GenTable> {
  return request({
    url: `${tableUrl}/import`,
    method: 'post',
    data: body
  })
}

/** 初始化表配置（如从数据源重新拉取结构），对应后端 InitializeAsync（待实现） */
export function initializeTable(id: string): Promise<void> {
  return request({
    url: `${tableUrl}/${id}/initialize`,
    method: 'post'
  })
}
