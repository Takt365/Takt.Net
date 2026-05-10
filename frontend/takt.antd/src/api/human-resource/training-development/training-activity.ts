// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/training-development
// 文件名称：training-activity.ts
// 功能描述：TrainingActivity API，对应后端 Takt.WebApi.Controllers.HumanResource.TrainingDevelopment.TaktTrainingActivitys
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  TrainingActivity,
  TrainingActivityQuery,
  TrainingActivityCreate,
  TrainingActivityUpdate,
  TrainingActivityStatus
} from '@/types/human-resource/training-development/training-activity'

// ========================================
// TrainingActivity相关 API（按后端控制器顺序）
// ========================================
const trainingActivityUrl = '/api/TaktTrainingActivitys';

/**
 * 获取TrainingActivity列表（分页）
 * 对应后端：GetTrainingActivityListAsync
 */
export function getTrainingActivityList(params: TrainingActivityQuery): Promise<TaktPagedResult<TrainingActivity>> {
  return request({
    url: `${trainingActivityUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取TrainingActivity
 * 对应后端：GetTrainingActivityByIdAsync
 */
export function getTrainingActivityById(id: string): Promise<TrainingActivity> {
  return request({
    url: `${trainingActivityUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取TrainingActivity选项列表（用于下拉框等）
 * 对应后端：GetTrainingActivityOptionsAsync
 */
export function getTrainingActivityOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${trainingActivityUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建TrainingActivity
 * 对应后端：CreateTrainingActivityAsync
 */
export function createTrainingActivity(data: TrainingActivityCreate): Promise<TrainingActivity> {
  return request({
    url: trainingActivityUrl,
    method: 'post',
    data
  })
}

/**
 * 更新TrainingActivity
 * 对应后端：UpdateTrainingActivityAsync
 */
export function updateTrainingActivity(id: string, data: TrainingActivityUpdate): Promise<TrainingActivity> {
  return request({
    url: `${trainingActivityUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除TrainingActivity（单条）
 * 对应后端：DeleteTrainingActivityByIdAsync
 */
export function deleteTrainingActivityById(id: string): Promise<void> {
  return request({
    url: `${trainingActivityUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除TrainingActivity
 * 对应后端：DeleteTrainingActivityBatchAsync
 */
export function deleteTrainingActivityBatch(ids: string[]): Promise<void> {
  return request({
    url: `${trainingActivityUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新TrainingActivity状态
 * 对应后端：UpdateTrainingActivityStatusAsync
 */
export function updateTrainingActivityStatus(data: TrainingActivityStatus): Promise<TrainingActivityStatus> {
  return request({
    url: `${trainingActivityUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTrainingActivityTemplateAsync；fileName 仅传名称不含后缀
 */
export function getTrainingActivityTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${trainingActivityUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入TrainingActivity
 * 对应后端：ImportTrainingActivityAsync
 */
export function importTrainingActivityData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${trainingActivityUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出TrainingActivity
 * 对应后端：ExportTrainingActivityAsync；fileName 仅传名称不含后缀
 */
export function exportTrainingActivityData(query: TrainingActivityQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${trainingActivityUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
