// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/statistics/kanban
// 文件名称：kanban-scheme.ts
// 功能描述：KanbanScheme API，对应后端 Takt.WebApi.Controllers.Statistics.Kanban.TaktKanbanSchemes
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  KanbanScheme,
  KanbanSchemeQuery,
  KanbanSchemeCreate,
  KanbanSchemeUpdate,
  KanbanSchemeStatus
} from '@/types/statistics/kanban/kanban-scheme'

// ========================================
// KanbanScheme相关 API（按后端控制器顺序）
// ========================================
const kanbanSchemeUrl = '/api/TaktKanbanSchemes';

/**
 * 获取KanbanScheme列表（分页）
 * 对应后端：GetKanbanSchemeListAsync
 */
export function getKanbanSchemeList(params: KanbanSchemeQuery): Promise<TaktPagedResult<KanbanScheme>> {
  return request({
    url: `${kanbanSchemeUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取KanbanScheme
 * 对应后端：GetKanbanSchemeByIdAsync
 */
export function getKanbanSchemeById(id: string): Promise<KanbanScheme> {
  return request({
    url: `${kanbanSchemeUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取KanbanScheme选项列表（用于下拉框等）
 * 对应后端：GetKanbanSchemeOptionsAsync
 */
export function getKanbanSchemeOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${kanbanSchemeUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建KanbanScheme
 * 对应后端：CreateKanbanSchemeAsync
 */
export function createKanbanScheme(data: KanbanSchemeCreate): Promise<KanbanScheme> {
  return request({
    url: kanbanSchemeUrl,
    method: 'post',
    data
  })
}

/**
 * 更新KanbanScheme
 * 对应后端：UpdateKanbanSchemeAsync
 */
export function updateKanbanScheme(id: string, data: KanbanSchemeUpdate): Promise<KanbanScheme> {
  return request({
    url: `${kanbanSchemeUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除KanbanScheme（单条）
 * 对应后端：DeleteKanbanSchemeByIdAsync
 */
export function deleteKanbanSchemeById(id: string): Promise<void> {
  return request({
    url: `${kanbanSchemeUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除KanbanScheme
 * 对应后端：DeleteKanbanSchemeBatchAsync
 */
export function deleteKanbanSchemeBatch(ids: string[]): Promise<void> {
  return request({
    url: `${kanbanSchemeUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新KanbanScheme状态
 * 对应后端：UpdateKanbanSchemeStatusAsync
 */
export function updateKanbanSchemeStatus(data: KanbanSchemeStatus): Promise<KanbanSchemeStatus> {
  return request({
    url: `${kanbanSchemeUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetKanbanSchemeTemplateAsync；fileName 仅传名称不含后缀
 */
export function getKanbanSchemeTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${kanbanSchemeUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入KanbanScheme
 * 对应后端：ImportKanbanSchemeAsync
 */
export function importKanbanSchemeData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${kanbanSchemeUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出KanbanScheme
 * 对应后端：ExportKanbanSchemeAsync；fileName 仅传名称不含后缀
 */
export function exportKanbanSchemeData(query: KanbanSchemeQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${kanbanSchemeUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
