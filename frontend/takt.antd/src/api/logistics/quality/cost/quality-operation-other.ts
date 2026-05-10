// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/cost
// 文件名称：quality-operation-other.ts
// 功能描述：QualityOperationOther API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Cost.TaktQualityOperationOthers
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  QualityOperationOther,
  QualityOperationOtherQuery,
  QualityOperationOtherCreate,
  QualityOperationOtherUpdate
} from '@/types/logistics/quality/cost/quality-operation-other'

// ========================================
// QualityOperationOther相关 API（按后端控制器顺序）
// ========================================
const qualityOperationOtherUrl = '/api/TaktQualityOperationOthers';

/**
 * 获取QualityOperationOther列表（分页）
 * 对应后端：GetQualityOperationOtherListAsync
 */
export function getQualityOperationOtherList(params: QualityOperationOtherQuery): Promise<TaktPagedResult<QualityOperationOther>> {
  return request({
    url: `${qualityOperationOtherUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取QualityOperationOther
 * 对应后端：GetQualityOperationOtherByIdAsync
 */
export function getQualityOperationOtherById(id: string): Promise<QualityOperationOther> {
  return request({
    url: `${qualityOperationOtherUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取QualityOperationOther选项列表（用于下拉框等）
 * 对应后端：GetQualityOperationOtherOptionsAsync
 */
export function getQualityOperationOtherOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${qualityOperationOtherUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建QualityOperationOther
 * 对应后端：CreateQualityOperationOtherAsync
 */
export function createQualityOperationOther(data: QualityOperationOtherCreate): Promise<QualityOperationOther> {
  return request({
    url: qualityOperationOtherUrl,
    method: 'post',
    data
  })
}

/**
 * 更新QualityOperationOther
 * 对应后端：UpdateQualityOperationOtherAsync
 */
export function updateQualityOperationOther(id: string, data: QualityOperationOtherUpdate): Promise<QualityOperationOther> {
  return request({
    url: `${qualityOperationOtherUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除QualityOperationOther（单条）
 * 对应后端：DeleteQualityOperationOtherByIdAsync
 */
export function deleteQualityOperationOtherById(id: string): Promise<void> {
  return request({
    url: `${qualityOperationOtherUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除QualityOperationOther
 * 对应后端：DeleteQualityOperationOtherBatchAsync
 */
export function deleteQualityOperationOtherBatch(ids: string[]): Promise<void> {
  return request({
    url: `${qualityOperationOtherUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetQualityOperationOtherTemplateAsync；fileName 仅传名称不含后缀
 */
export function getQualityOperationOtherTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${qualityOperationOtherUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入QualityOperationOther
 * 对应后端：ImportQualityOperationOtherAsync
 */
export function importQualityOperationOtherData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${qualityOperationOtherUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出QualityOperationOther
 * 对应后端：ExportQualityOperationOtherAsync；fileName 仅传名称不含后缀
 */
export function exportQualityOperationOtherData(query: QualityOperationOtherQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${qualityOperationOtherUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
