// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/help-desk
// 文件名称：knowledge-change-log.ts
// 功能描述：KnowledgeChangeLog API，对应后端 Takt.WebApi.Controllers.Routine.Business.HelpDesk.TaktKnowledgeChangeLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  KnowledgeChangeLog,
  KnowledgeChangeLogQuery,
  KnowledgeChangeLogCreate,
  KnowledgeChangeLogUpdate
} from '@/types/routine/business/help-desk/knowledge-change-log'

// ========================================
// KnowledgeChangeLog相关 API（按后端控制器顺序）
// ========================================
const knowledgeChangeLogUrl = '/api/TaktKnowledgeChangeLogs';

/**
 * 获取KnowledgeChangeLog列表（分页）
 * 对应后端：GetKnowledgeChangeLogListAsync
 */
export function getKnowledgeChangeLogList(params: KnowledgeChangeLogQuery): Promise<TaktPagedResult<KnowledgeChangeLog>> {
  return request({
    url: `${knowledgeChangeLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取KnowledgeChangeLog
 * 对应后端：GetKnowledgeChangeLogByIdAsync
 */
export function getKnowledgeChangeLogById(id: string): Promise<KnowledgeChangeLog> {
  return request({
    url: `${knowledgeChangeLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取KnowledgeChangeLog选项列表（用于下拉框等）
 * 对应后端：GetKnowledgeChangeLogOptionsAsync
 */
export function getKnowledgeChangeLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${knowledgeChangeLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建KnowledgeChangeLog
 * 对应后端：CreateKnowledgeChangeLogAsync
 */
export function createKnowledgeChangeLog(data: KnowledgeChangeLogCreate): Promise<KnowledgeChangeLog> {
  return request({
    url: knowledgeChangeLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新KnowledgeChangeLog
 * 对应后端：UpdateKnowledgeChangeLogAsync
 */
export function updateKnowledgeChangeLog(id: string, data: KnowledgeChangeLogUpdate): Promise<KnowledgeChangeLog> {
  return request({
    url: `${knowledgeChangeLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除KnowledgeChangeLog（单条）
 * 对应后端：DeleteKnowledgeChangeLogByIdAsync
 */
export function deleteKnowledgeChangeLogById(id: string): Promise<void> {
  return request({
    url: `${knowledgeChangeLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除KnowledgeChangeLog
 * 对应后端：DeleteKnowledgeChangeLogBatchAsync
 */
export function deleteKnowledgeChangeLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${knowledgeChangeLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetKnowledgeChangeLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getKnowledgeChangeLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${knowledgeChangeLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入KnowledgeChangeLog
 * 对应后端：ImportKnowledgeChangeLogAsync
 */
export function importKnowledgeChangeLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${knowledgeChangeLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出KnowledgeChangeLog
 * 对应后端：ExportKnowledgeChangeLogAsync；fileName 仅传名称不含后缀
 */
export function exportKnowledgeChangeLogData(query: KnowledgeChangeLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${knowledgeChangeLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
