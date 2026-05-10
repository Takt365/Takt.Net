// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/output
// 文件名称：assy-output-detail.ts
// 功能描述：AssyOutputDetail API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Output.TaktAssyOutputDetails
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  AssyOutputDetail,
  AssyOutputDetailQuery,
  AssyOutputDetailCreate,
  AssyOutputDetailUpdate
} from '@/types/logistics/manufacturing/output/assy-output-detail'

// ========================================
// AssyOutputDetail相关 API（按后端控制器顺序）
// ========================================
const assyOutputDetailUrl = '/api/TaktAssyOutputDetails';

/**
 * 获取AssyOutputDetail列表（分页）
 * 对应后端：GetAssyOutputDetailListAsync
 */
export function getAssyOutputDetailList(params: AssyOutputDetailQuery): Promise<TaktPagedResult<AssyOutputDetail>> {
  return request({
    url: `${assyOutputDetailUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取AssyOutputDetail
 * 对应后端：GetAssyOutputDetailByIdAsync
 */
export function getAssyOutputDetailById(id: string): Promise<AssyOutputDetail> {
  return request({
    url: `${assyOutputDetailUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取AssyOutputDetail选项列表（用于下拉框等）
 * 对应后端：GetAssyOutputDetailOptionsAsync
 */
export function getAssyOutputDetailOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${assyOutputDetailUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建AssyOutputDetail
 * 对应后端：CreateAssyOutputDetailAsync
 */
export function createAssyOutputDetail(data: AssyOutputDetailCreate): Promise<AssyOutputDetail> {
  return request({
    url: assyOutputDetailUrl,
    method: 'post',
    data
  })
}

/**
 * 更新AssyOutputDetail
 * 对应后端：UpdateAssyOutputDetailAsync
 */
export function updateAssyOutputDetail(id: string, data: AssyOutputDetailUpdate): Promise<AssyOutputDetail> {
  return request({
    url: `${assyOutputDetailUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除AssyOutputDetail（单条）
 * 对应后端：DeleteAssyOutputDetailByIdAsync
 */
export function deleteAssyOutputDetailById(id: string): Promise<void> {
  return request({
    url: `${assyOutputDetailUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除AssyOutputDetail
 * 对应后端：DeleteAssyOutputDetailBatchAsync
 */
export function deleteAssyOutputDetailBatch(ids: string[]): Promise<void> {
  return request({
    url: `${assyOutputDetailUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetAssyOutputDetailTemplateAsync；fileName 仅传名称不含后缀
 */
export function getAssyOutputDetailTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${assyOutputDetailUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入AssyOutputDetail
 * 对应后端：ImportAssyOutputDetailAsync
 */
export function importAssyOutputDetailData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${assyOutputDetailUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出AssyOutputDetail
 * 对应后端：ExportAssyOutputDetailAsync；fileName 仅传名称不含后缀
 */
export function exportAssyOutputDetailData(query: AssyOutputDetailQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${assyOutputDetailUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
