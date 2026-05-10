// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/visiting
// 文件名称：visit.ts
// 功能描述：Visit API，对应后端 Takt.WebApi.Controllers.Routine.Business.Visiting.TaktVisits
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Visit,
  VisitQuery,
  VisitCreate,
  VisitUpdate
} from '@/types/routine/business/visiting/visit'

// ========================================
// Visit相关 API（按后端控制器顺序）
// ========================================
const visitUrl = '/api/TaktVisits';

/**
 * 获取Visit列表（分页）
 * 对应后端：GetVisitListAsync
 */
export function getVisitList(params: VisitQuery): Promise<TaktPagedResult<Visit>> {
  return request({
    url: `${visitUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Visit
 * 对应后端：GetVisitByIdAsync
 */
export function getVisitById(id: string): Promise<Visit> {
  return request({
    url: `${visitUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Visit选项列表（用于下拉框等）
 * 对应后端：GetVisitOptionsAsync
 */
export function getVisitOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${visitUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Visit
 * 对应后端：CreateVisitAsync
 */
export function createVisit(data: VisitCreate): Promise<Visit> {
  return request({
    url: visitUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Visit
 * 对应后端：UpdateVisitAsync
 */
export function updateVisit(id: string, data: VisitUpdate): Promise<Visit> {
  return request({
    url: `${visitUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Visit（单条）
 * 对应后端：DeleteVisitByIdAsync
 */
export function deleteVisitById(id: string): Promise<void> {
  return request({
    url: `${visitUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Visit
 * 对应后端：DeleteVisitBatchAsync
 */
export function deleteVisitBatch(ids: string[]): Promise<void> {
  return request({
    url: `${visitUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetVisitTemplateAsync；fileName 仅传名称不含后缀
 */
export function getVisitTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${visitUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Visit
 * 对应后端：ImportVisitAsync
 */
export function importVisitData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${visitUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Visit
 * 对应后端：ExportVisitAsync；fileName 仅传名称不含后缀
 */
export function exportVisitData(query: VisitQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${visitUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
