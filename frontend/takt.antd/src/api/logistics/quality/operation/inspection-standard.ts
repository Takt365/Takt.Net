// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/operation
// 文件名称：inspection-standard.ts
// 功能描述：InspectionStandard API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Operation.TaktInspectionStandards
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  InspectionStandard,
  InspectionStandardQuery,
  InspectionStandardCreate,
  InspectionStandardUpdate
} from '@/types/logistics/quality/operation/inspection-standard'

// ========================================
// InspectionStandard相关 API（按后端控制器顺序）
// ========================================
const inspectionStandardUrl = '/api/TaktInspectionStandards';

/**
 * 获取InspectionStandard列表（分页）
 * 对应后端：GetInspectionStandardListAsync
 */
export function getInspectionStandardList(params: InspectionStandardQuery): Promise<TaktPagedResult<InspectionStandard>> {
  return request({
    url: `${inspectionStandardUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取InspectionStandard
 * 对应后端：GetInspectionStandardByIdAsync
 */
export function getInspectionStandardById(id: string): Promise<InspectionStandard> {
  return request({
    url: `${inspectionStandardUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取InspectionStandard选项列表（用于下拉框等）
 * 对应后端：GetInspectionStandardOptionsAsync
 */
export function getInspectionStandardOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${inspectionStandardUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建InspectionStandard
 * 对应后端：CreateInspectionStandardAsync
 */
export function createInspectionStandard(data: InspectionStandardCreate): Promise<InspectionStandard> {
  return request({
    url: inspectionStandardUrl,
    method: 'post',
    data
  })
}

/**
 * 更新InspectionStandard
 * 对应后端：UpdateInspectionStandardAsync
 */
export function updateInspectionStandard(id: string, data: InspectionStandardUpdate): Promise<InspectionStandard> {
  return request({
    url: `${inspectionStandardUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除InspectionStandard（单条）
 * 对应后端：DeleteInspectionStandardByIdAsync
 */
export function deleteInspectionStandardById(id: string): Promise<void> {
  return request({
    url: `${inspectionStandardUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除InspectionStandard
 * 对应后端：DeleteInspectionStandardBatchAsync
 */
export function deleteInspectionStandardBatch(ids: string[]): Promise<void> {
  return request({
    url: `${inspectionStandardUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetInspectionStandardTemplateAsync；fileName 仅传名称不含后缀
 */
export function getInspectionStandardTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${inspectionStandardUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入InspectionStandard
 * 对应后端：ImportInspectionStandardAsync
 */
export function importInspectionStandardData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${inspectionStandardUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出InspectionStandard
 * 对应后端：ExportInspectionStandardAsync；fileName 仅传名称不含后缀
 */
export function exportInspectionStandardData(query: InspectionStandardQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${inspectionStandardUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
