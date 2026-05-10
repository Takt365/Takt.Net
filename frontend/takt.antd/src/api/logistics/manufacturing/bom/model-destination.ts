// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/bom
// 文件名称：model-destination.ts
// 功能描述：ModelDestination API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Bom.TaktModelDestinations
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  ModelDestination,
  ModelDestinationQuery,
  ModelDestinationCreate,
  ModelDestinationUpdate,
  ModelDestinationSort
} from '@/types/logistics/manufacturing/bom/model-destination'

// ========================================
// ModelDestination相关 API（按后端控制器顺序）
// ========================================
const modelDestinationUrl = '/api/TaktModelDestinations';

/**
 * 获取ModelDestination列表（分页）
 * 对应后端：GetModelDestinationListAsync
 */
export function getModelDestinationList(params: ModelDestinationQuery): Promise<TaktPagedResult<ModelDestination>> {
  return request({
    url: `${modelDestinationUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取ModelDestination
 * 对应后端：GetModelDestinationByIdAsync
 */
export function getModelDestinationById(id: string): Promise<ModelDestination> {
  return request({
    url: `${modelDestinationUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取ModelDestination选项列表（用于下拉框等）
 * 对应后端：GetModelDestinationOptionsAsync
 */
export function getModelDestinationOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${modelDestinationUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建ModelDestination
 * 对应后端：CreateModelDestinationAsync
 */
export function createModelDestination(data: ModelDestinationCreate): Promise<ModelDestination> {
  return request({
    url: modelDestinationUrl,
    method: 'post',
    data
  })
}

/**
 * 更新ModelDestination
 * 对应后端：UpdateModelDestinationAsync
 */
export function updateModelDestination(id: string, data: ModelDestinationUpdate): Promise<ModelDestination> {
  return request({
    url: `${modelDestinationUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ModelDestination（单条）
 * 对应后端：DeleteModelDestinationByIdAsync
 */
export function deleteModelDestinationById(id: string): Promise<void> {
  return request({
    url: `${modelDestinationUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ModelDestination
 * 对应后端：DeleteModelDestinationBatchAsync
 */
export function deleteModelDestinationBatch(ids: string[]): Promise<void> {
  return request({
    url: `${modelDestinationUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新ModelDestination排序
 * 对应后端：UpdateModelDestinationSortAsync
 */
export function updateModelDestinationSort(data: ModelDestinationSort): Promise<ModelDestinationSort> {
  return request({
    url: `${modelDestinationUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetModelDestinationTemplateAsync；fileName 仅传名称不含后缀
 */
export function getModelDestinationTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${modelDestinationUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入ModelDestination
 * 对应后端：ImportModelDestinationAsync
 */
export function importModelDestinationData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${modelDestinationUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出ModelDestination
 * 对应后端：ExportModelDestinationAsync；fileName 仅传名称不含后缀
 */
export function exportModelDestinationData(query: ModelDestinationQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${modelDestinationUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
