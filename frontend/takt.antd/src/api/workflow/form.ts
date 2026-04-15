// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/workflow/form
// 文件名称：form.ts
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：流程表单 API，对应后端 TaktFlowFormsController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  FlowForm,
  FlowFormQuery,
  FlowFormCreate,
  FlowFormUpdate,
  FlowFormStatusUpdate
} from '@/types/workflow/form'

// ========================================
// 流程表单相关 API（按后端控制器顺序）
// ========================================

const formUrl = '/api/TaktFlowForms'

/**
 * 获取流程表单列表（分页）
 * 对应后端：GetList
 */
export function getFlowFormList(params: FlowFormQuery): Promise<TaktPagedResult<FlowForm>> {
  return request({
    url: `${formUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取流程表单
 * 对应后端：GetById
 */
export function getFlowFormById(id: string): Promise<FlowForm> {
  return request({
    url: `${formUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 根据表单编码获取流程表单
 * 对应后端：GetByFormCode
 */
export function getFlowFormByCode(formCode: string): Promise<FlowForm> {
  return request({
    url: `${formUrl}/by-code/${encodeURIComponent(formCode)}`,
    method: 'get'
  })
}

/**
 * 创建流程表单
 * 对应后端：Create
 */
export function createFlowForm(data: FlowFormCreate): Promise<FlowForm> {
  return request({
    url: formUrl,
    method: 'post',
    data
  })
}

/**
 * 更新流程表单
 * 对应后端：Update
 */
export function updateFlowForm(id: string, data: FlowFormUpdate): Promise<FlowForm> {
  return request({
    url: `${formUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除流程表单（单条）
 * 对应后端：Delete
 */
export function deleteFlowFormById(id: string): Promise<void> {
  return request({
    url: `${formUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除流程表单
 * 对应后端：DeleteBatch
 */
export function deleteFlowFormBatch(ids: string[]): Promise<void> {
  return request({
    url: `${formUrl}/delete`,
    method: 'post',
    data: ids
  })
}

/**
 * 更新流程表单状态
 * 对应后端：UpdateStatus
 */
export function updateFlowFormStatus(data: FlowFormStatusUpdate): Promise<FlowForm> {
  return request({
    url: `${formUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTemplate
 */
export function getFlowFormTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${formUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入流程表单
 * 对应后端：Import
 */
export function importFlowFormData(file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${formUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出流程表单
 * 对应后端：Export
 */
export function exportFlowFormData(query: FlowFormQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${formUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

// ========================================
// 数据源 / 数据表 / 表列（用于流程表单启用数据源时的级联选择）
// ========================================

/** 数据库源项（GetDatabaseConfigs 返回）；流程表单 RelatedDataBaseName 应使用 dataBaseName（如 Takt_Identity_Dev），与当前环境 appsettings 一致） */
export interface DatabaseConfigItem {
  configId: string
  displayName: string
  /** 数据库名（从 Conn 解析），用于流程表单 RelatedDataBaseName，开发/生产环境不同 */
  dataBaseName?: string
}

/** 数据表项（GetDatabaseTables 返回） */
export interface DatabaseTableItem {
  tableName: string
  tableComment?: string
}

/** 表列项（GetTableColumns 返回），后端基于 TaktDatabaseColumnInfo，再加前端使用的扩展字段 */
export interface TableColumnItem {
  // 后端基础字段
  dbColumnName: string
  columnDescription?: string
  dataType?: string
  length?: number
  decimalDigits?: number
  isPrimarykey?: boolean
  isIdentity?: boolean
  isNullable?: boolean

  // 前端扩展字段（用于流程表单字段配置）
  csharpType?: string
  csharpColumnName?: string
  isRequired?: number          // 0=是,1=否（对齐 sys_yes_no）
  displayType?: string         // 对齐 sys_display_type 的 DictValue，如 input/select
  dictTypeCode?: string        // 绑定字典类型编码
}

/**
 * 获取当前所有数据库源列表（用于 DataSource 下拉）
 * 对应后端：GetDatabaseConfigsAsync
 */
export function getDatabaseConfigs(): Promise<DatabaseConfigItem[]> {
  return request({
    url: `${formUrl}/database-configs`,
    method: 'get'
  })
}

/**
 * 根据数据源获取该库下的数据表列表（用于 DataTable 下拉）。
 * 流程表单场景传 requiredColumn: 'flow_instance_id'，仅返回带该列的表。
 *
 * @param configId - 数据源 ConfigId
 * @param options - 可选；requiredColumn 存在时仅返回包含该列的表（如 'flow_instance_id'）
 * @returns 表名与表描述列表
 */
export function getDatabaseTables(configId: string, options?: { requiredColumn?: string }): Promise<DatabaseTableItem[]> {
  return request({
    url: `${formUrl}/database-tables`,
    method: 'get',
    params: { configId, requiredColumn: options?.requiredColumn }
  })
}

/**
 * 根据数据源与表名获取该表的列列表（用于 RelatedFormField 多选）
 * 对应后端：GetTableColumnsAsync
 */
export function getTableColumns(configId: string, tableName: string): Promise<TableColumnItem[]> {
  return request({
    url: `${formUrl}/table-columns`,
    method: 'get',
    params: { configId, tableName }
  })
}

// ========================================
// 新增表单：第一步选数据源（数据库）→ 数据表，选中表后根据表列自动生成 FormConfig
// ========================================

/**
 * 根据数据源与表名获取表单配置 JSON（选中数据表后，将该表所有列还原成表单格式）
 * 对应后端：GetFormConfigFromTableAsync
 */
export function getFormConfigFromTable(configId: string, tableName: string): Promise<string> {
  return request({
    url: `${formUrl}/form-config-from-table`,
    method: 'get',
    params: { configId, tableName }
  })
}
