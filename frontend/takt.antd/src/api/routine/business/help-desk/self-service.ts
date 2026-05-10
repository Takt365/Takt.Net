// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/help-desk
// 文件名称：self-service.ts
// 功能描述：SelfService API，对应后端 Takt.WebApi.Controllers.Routine.Business.HelpDesk.TaktSelfServices
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  SelfService,
  SelfServiceQuery,
  SelfServiceCreate,
  SelfServiceUpdate,
  SelfServiceStatus,
  SelfServiceSort
} from '@/types/routine/business/help-desk/self-service'

// ========================================
// SelfService相关 API（按后端控制器顺序）
// ========================================
const selfServiceUrl = '/api/TaktSelfServices';

/**
 * 获取SelfService列表（分页）
 * 对应后端：GetSelfServiceListAsync
 */
export function getSelfServiceList(params: SelfServiceQuery): Promise<TaktPagedResult<SelfService>> {
  return request({
    url: `${selfServiceUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取SelfService
 * 对应后端：GetSelfServiceByIdAsync
 */
export function getSelfServiceById(id: string): Promise<SelfService> {
  return request({
    url: `${selfServiceUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取SelfService选项列表（用于下拉框等）
 * 对应后端：GetSelfServiceOptionsAsync
 */
export function getSelfServiceOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${selfServiceUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建SelfService
 * 对应后端：CreateSelfServiceAsync
 */
export function createSelfService(data: SelfServiceCreate): Promise<SelfService> {
  return request({
    url: selfServiceUrl,
    method: 'post',
    data
  })
}

/**
 * 更新SelfService
 * 对应后端：UpdateSelfServiceAsync
 */
export function updateSelfService(id: string, data: SelfServiceUpdate): Promise<SelfService> {
  return request({
    url: `${selfServiceUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除SelfService（单条）
 * 对应后端：DeleteSelfServiceByIdAsync
 */
export function deleteSelfServiceById(id: string): Promise<void> {
  return request({
    url: `${selfServiceUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除SelfService
 * 对应后端：DeleteSelfServiceBatchAsync
 */
export function deleteSelfServiceBatch(ids: string[]): Promise<void> {
  return request({
    url: `${selfServiceUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新SelfService状态
 * 对应后端：UpdateSelfServiceStatusAsync
 */
export function updateSelfServiceStatus(data: SelfServiceStatus): Promise<SelfServiceStatus> {
  return request({
    url: `${selfServiceUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新SelfService排序
 * 对应后端：UpdateSelfServiceSortAsync
 */
export function updateSelfServiceSort(data: SelfServiceSort): Promise<SelfServiceSort> {
  return request({
    url: `${selfServiceUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetSelfServiceTemplateAsync；fileName 仅传名称不含后缀
 */
export function getSelfServiceTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${selfServiceUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入SelfService
 * 对应后端：ImportSelfServiceAsync
 */
export function importSelfServiceData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${selfServiceUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出SelfService
 * 对应后端：ExportSelfServiceAsync；fileName 仅传名称不含后缀
 */
export function exportSelfServiceData(query: SelfServiceQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${selfServiceUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
