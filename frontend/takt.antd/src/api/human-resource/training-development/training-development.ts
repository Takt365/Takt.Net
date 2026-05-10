// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/training-development
// 文件名称：training-development.ts
// 功能描述：TrainingDevelopment API，对应后端 Takt.WebApi.Controllers.HumanResource.TrainingDevelopment.TaktTrainingDevelopments
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  TrainingDevelopment,
  TrainingDevelopmentQuery,
  TrainingDevelopmentCreate,
  TrainingDevelopmentUpdate,
  TrainingDevelopmentStatus
} from '@/types/human-resource/training-development/training-development'

// ========================================
// TrainingDevelopment相关 API（按后端控制器顺序）
// ========================================
const trainingDevelopmentUrl = '/api/TaktTrainingDevelopments';

/**
 * 获取TrainingDevelopment列表（分页）
 * 对应后端：GetTrainingDevelopmentListAsync
 */
export function getTrainingDevelopmentList(params: TrainingDevelopmentQuery): Promise<TaktPagedResult<TrainingDevelopment>> {
  return request({
    url: `${trainingDevelopmentUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取TrainingDevelopment
 * 对应后端：GetTrainingDevelopmentByIdAsync
 */
export function getTrainingDevelopmentById(id: string): Promise<TrainingDevelopment> {
  return request({
    url: `${trainingDevelopmentUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取TrainingDevelopment选项列表（用于下拉框等）
 * 对应后端：GetTrainingDevelopmentOptionsAsync
 */
export function getTrainingDevelopmentOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${trainingDevelopmentUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建TrainingDevelopment
 * 对应后端：CreateTrainingDevelopmentAsync
 */
export function createTrainingDevelopment(data: TrainingDevelopmentCreate): Promise<TrainingDevelopment> {
  return request({
    url: trainingDevelopmentUrl,
    method: 'post',
    data
  })
}

/**
 * 更新TrainingDevelopment
 * 对应后端：UpdateTrainingDevelopmentAsync
 */
export function updateTrainingDevelopment(id: string, data: TrainingDevelopmentUpdate): Promise<TrainingDevelopment> {
  return request({
    url: `${trainingDevelopmentUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除TrainingDevelopment（单条）
 * 对应后端：DeleteTrainingDevelopmentByIdAsync
 */
export function deleteTrainingDevelopmentById(id: string): Promise<void> {
  return request({
    url: `${trainingDevelopmentUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除TrainingDevelopment
 * 对应后端：DeleteTrainingDevelopmentBatchAsync
 */
export function deleteTrainingDevelopmentBatch(ids: string[]): Promise<void> {
  return request({
    url: `${trainingDevelopmentUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新TrainingDevelopment状态
 * 对应后端：UpdateTrainingDevelopmentStatusAsync
 */
export function updateTrainingDevelopmentStatus(data: TrainingDevelopmentStatus): Promise<TrainingDevelopmentStatus> {
  return request({
    url: `${trainingDevelopmentUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTrainingDevelopmentTemplateAsync；fileName 仅传名称不含后缀
 */
export function getTrainingDevelopmentTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${trainingDevelopmentUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入TrainingDevelopment
 * 对应后端：ImportTrainingDevelopmentAsync
 */
export function importTrainingDevelopmentData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${trainingDevelopmentUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出TrainingDevelopment
 * 对应后端：ExportTrainingDevelopmentAsync；fileName 仅传名称不含后缀
 */
export function exportTrainingDevelopmentData(query: TrainingDevelopmentQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${trainingDevelopmentUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
