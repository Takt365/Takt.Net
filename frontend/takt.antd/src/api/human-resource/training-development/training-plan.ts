// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/training-development
// 文件名称：training-plan.ts
// 功能描述：TrainingPlan API，对应后端 Takt.WebApi.Controllers.HumanResource.TrainingDevelopment.TaktTrainingPlans
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  TrainingPlan,
  TrainingPlanQuery,
  TrainingPlanCreate,
  TrainingPlanUpdate,
  TrainingPlanStatus
} from '@/types/human-resource/training-development/training-plan'

// ========================================
// TrainingPlan相关 API（按后端控制器顺序）
// ========================================
const trainingPlanUrl = '/api/TaktTrainingPlans';

/**
 * 获取TrainingPlan列表（分页）
 * 对应后端：GetTrainingPlanListAsync
 */
export function getTrainingPlanList(params: TrainingPlanQuery): Promise<TaktPagedResult<TrainingPlan>> {
  return request({
    url: `${trainingPlanUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取TrainingPlan
 * 对应后端：GetTrainingPlanByIdAsync
 */
export function getTrainingPlanById(id: string): Promise<TrainingPlan> {
  return request({
    url: `${trainingPlanUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取TrainingPlan选项列表（用于下拉框等）
 * 对应后端：GetTrainingPlanOptionsAsync
 */
export function getTrainingPlanOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${trainingPlanUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建TrainingPlan
 * 对应后端：CreateTrainingPlanAsync
 */
export function createTrainingPlan(data: TrainingPlanCreate): Promise<TrainingPlan> {
  return request({
    url: trainingPlanUrl,
    method: 'post',
    data
  })
}

/**
 * 更新TrainingPlan
 * 对应后端：UpdateTrainingPlanAsync
 */
export function updateTrainingPlan(id: string, data: TrainingPlanUpdate): Promise<TrainingPlan> {
  return request({
    url: `${trainingPlanUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除TrainingPlan（单条）
 * 对应后端：DeleteTrainingPlanByIdAsync
 */
export function deleteTrainingPlanById(id: string): Promise<void> {
  return request({
    url: `${trainingPlanUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除TrainingPlan
 * 对应后端：DeleteTrainingPlanBatchAsync
 */
export function deleteTrainingPlanBatch(ids: string[]): Promise<void> {
  return request({
    url: `${trainingPlanUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新TrainingPlan状态
 * 对应后端：UpdateTrainingPlanStatusAsync
 */
export function updateTrainingPlanStatus(data: TrainingPlanStatus): Promise<TrainingPlanStatus> {
  return request({
    url: `${trainingPlanUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTrainingPlanTemplateAsync；fileName 仅传名称不含后缀
 */
export function getTrainingPlanTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${trainingPlanUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入TrainingPlan
 * 对应后端：ImportTrainingPlanAsync
 */
export function importTrainingPlanData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${trainingPlanUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出TrainingPlan
 * 对应后端：ExportTrainingPlanAsync；fileName 仅传名称不含后缀
 */
export function exportTrainingPlanData(query: TrainingPlanQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${trainingPlanUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
