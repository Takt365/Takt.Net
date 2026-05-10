// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/defect
// 文件名称：assy-defect-detail.ts
// 功能描述：AssyDefectDetail API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Defect.TaktAssyDefectDetails
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  AssyDefectDetail,
  AssyDefectDetailQuery,
  AssyDefectDetailCreate,
  AssyDefectDetailUpdate
} from '@/types/logistics/manufacturing/defect/assy-defect-detail'

// ========================================
// AssyDefectDetail相关 API（按后端控制器顺序）
// ========================================
const assyDefectDetailUrl = '/api/TaktAssyDefectDetails';

/**
 * 获取AssyDefectDetail列表（分页）
 * 对应后端：GetAssyDefectDetailListAsync
 */
export function getAssyDefectDetailList(params: AssyDefectDetailQuery): Promise<TaktPagedResult<AssyDefectDetail>> {
  return request({
    url: `${assyDefectDetailUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取AssyDefectDetail
 * 对应后端：GetAssyDefectDetailByIdAsync
 */
export function getAssyDefectDetailById(id: string): Promise<AssyDefectDetail> {
  return request({
    url: `${assyDefectDetailUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取AssyDefectDetail选项列表（用于下拉框等）
 * 对应后端：GetAssyDefectDetailOptionsAsync
 */
export function getAssyDefectDetailOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${assyDefectDetailUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建AssyDefectDetail
 * 对应后端：CreateAssyDefectDetailAsync
 */
export function createAssyDefectDetail(data: AssyDefectDetailCreate): Promise<AssyDefectDetail> {
  return request({
    url: assyDefectDetailUrl,
    method: 'post',
    data
  })
}

/**
 * 更新AssyDefectDetail
 * 对应后端：UpdateAssyDefectDetailAsync
 */
export function updateAssyDefectDetail(id: string, data: AssyDefectDetailUpdate): Promise<AssyDefectDetail> {
  return request({
    url: `${assyDefectDetailUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除AssyDefectDetail（单条）
 * 对应后端：DeleteAssyDefectDetailByIdAsync
 */
export function deleteAssyDefectDetailById(id: string): Promise<void> {
  return request({
    url: `${assyDefectDetailUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除AssyDefectDetail
 * 对应后端：DeleteAssyDefectDetailBatchAsync
 */
export function deleteAssyDefectDetailBatch(ids: string[]): Promise<void> {
  return request({
    url: `${assyDefectDetailUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetAssyDefectDetailTemplateAsync；fileName 仅传名称不含后缀
 */
export function getAssyDefectDetailTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${assyDefectDetailUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入AssyDefectDetail
 * 对应后端：ImportAssyDefectDetailAsync
 */
export function importAssyDefectDetailData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${assyDefectDetailUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出AssyDefectDetail
 * 对应后端：ExportAssyDefectDetailAsync；fileName 仅传名称不含后缀
 */
export function exportAssyDefectDetailData(query: AssyDefectDetailQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${assyDefectDetailUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
