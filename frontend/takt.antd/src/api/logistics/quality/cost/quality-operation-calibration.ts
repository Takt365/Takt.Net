// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/cost
// 文件名称：quality-operation-calibration.ts
// 功能描述：QualityOperationCalibration API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Cost.TaktQualityOperationCalibrations
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  QualityOperationCalibration,
  QualityOperationCalibrationQuery,
  QualityOperationCalibrationCreate,
  QualityOperationCalibrationUpdate
} from '@/types/logistics/quality/cost/quality-operation-calibration'

// ========================================
// QualityOperationCalibration相关 API（按后端控制器顺序）
// ========================================
const qualityOperationCalibrationUrl = '/api/TaktQualityOperationCalibrations';

/**
 * 获取QualityOperationCalibration列表（分页）
 * 对应后端：GetQualityOperationCalibrationListAsync
 */
export function getQualityOperationCalibrationList(params: QualityOperationCalibrationQuery): Promise<TaktPagedResult<QualityOperationCalibration>> {
  return request({
    url: `${qualityOperationCalibrationUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取QualityOperationCalibration
 * 对应后端：GetQualityOperationCalibrationByIdAsync
 */
export function getQualityOperationCalibrationById(id: string): Promise<QualityOperationCalibration> {
  return request({
    url: `${qualityOperationCalibrationUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取QualityOperationCalibration选项列表（用于下拉框等）
 * 对应后端：GetQualityOperationCalibrationOptionsAsync
 */
export function getQualityOperationCalibrationOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${qualityOperationCalibrationUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建QualityOperationCalibration
 * 对应后端：CreateQualityOperationCalibrationAsync
 */
export function createQualityOperationCalibration(data: QualityOperationCalibrationCreate): Promise<QualityOperationCalibration> {
  return request({
    url: qualityOperationCalibrationUrl,
    method: 'post',
    data
  })
}

/**
 * 更新QualityOperationCalibration
 * 对应后端：UpdateQualityOperationCalibrationAsync
 */
export function updateQualityOperationCalibration(id: string, data: QualityOperationCalibrationUpdate): Promise<QualityOperationCalibration> {
  return request({
    url: `${qualityOperationCalibrationUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除QualityOperationCalibration（单条）
 * 对应后端：DeleteQualityOperationCalibrationByIdAsync
 */
export function deleteQualityOperationCalibrationById(id: string): Promise<void> {
  return request({
    url: `${qualityOperationCalibrationUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除QualityOperationCalibration
 * 对应后端：DeleteQualityOperationCalibrationBatchAsync
 */
export function deleteQualityOperationCalibrationBatch(ids: string[]): Promise<void> {
  return request({
    url: `${qualityOperationCalibrationUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetQualityOperationCalibrationTemplateAsync；fileName 仅传名称不含后缀
 */
export function getQualityOperationCalibrationTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${qualityOperationCalibrationUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入QualityOperationCalibration
 * 对应后端：ImportQualityOperationCalibrationAsync
 */
export function importQualityOperationCalibrationData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${qualityOperationCalibrationUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出QualityOperationCalibration
 * 对应后端：ExportQualityOperationCalibrationAsync；fileName 仅传名称不含后缀
 */
export function exportQualityOperationCalibrationData(query: QualityOperationCalibrationQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${qualityOperationCalibrationUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
