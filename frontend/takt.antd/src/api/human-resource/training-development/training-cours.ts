// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/training-development
// 文件名称：training-cours.ts
// 功能描述：TrainingCours API，对应后端 Takt.WebApi.Controllers.HumanResource.TrainingDevelopment.TaktTrainingCourses
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  TrainingCours,
  TrainingCoursQuery,
  TrainingCoursCreate,
  TrainingCoursUpdate,
  TrainingCoursStatus,
  TrainingCoursSort
} from '@/types/human-resource/training-development/training-cours'

// ========================================
// TrainingCours相关 API（按后端控制器顺序）
// ========================================
const trainingCoursUrl = '/api/TaktTrainingCourses';

/**
 * 获取TrainingCours列表（分页）
 * 对应后端：GetTrainingCourseListAsync
 */
export function getTrainingCoursList(params: TrainingCoursQuery): Promise<TaktPagedResult<TrainingCourse>> {
  return request({
    url: `${trainingCoursUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取TrainingCours
 * 对应后端：GetTrainingCourseByIdAsync
 */
export function getTrainingCoursById(id: string): Promise<TrainingCours> {
  return request({
    url: `${trainingCoursUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取TrainingCours选项列表（用于下拉框等）
 * 对应后端：GetTrainingCourseOptionsAsync
 */
export function getTrainingCoursOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${trainingCoursUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建TrainingCours
 * 对应后端：CreateTrainingCourseAsync
 */
export function createTrainingCours(data: TrainingCoursCreate): Promise<TrainingCours> {
  return request({
    url: trainingCoursUrl,
    method: 'post',
    data
  })
}

/**
 * 更新TrainingCours
 * 对应后端：UpdateTrainingCourseAsync
 */
export function updateTrainingCours(id: string, data: TrainingCoursUpdate): Promise<TrainingCours> {
  return request({
    url: `${trainingCoursUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除TrainingCours（单条）
 * 对应后端：DeleteTrainingCourseByIdAsync
 */
export function deleteTrainingCoursById(id: string): Promise<void> {
  return request({
    url: `${trainingCoursUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除TrainingCours
 * 对应后端：DeleteTrainingCourseBatchAsync
 */
export function deleteTrainingCoursBatch(ids: string[]): Promise<void> {
  return request({
    url: `${trainingCoursUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新TrainingCours状态
 * 对应后端：UpdateTrainingCourseStatusAsync
 */
export function updateTrainingCoursStatus(data: TrainingCoursStatus): Promise<TrainingCoursStatus> {
  return request({
    url: `${trainingCoursUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新TrainingCours排序
 * 对应后端：UpdateTrainingCourseSortAsync
 */
export function updateTrainingCoursSort(data: TrainingCoursSort): Promise<TrainingCoursSort> {
  return request({
    url: `${trainingCoursUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTrainingCourseTemplateAsync；fileName 仅传名称不含后缀
 */
export function getTrainingCoursTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${trainingCoursUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入TrainingCours
 * 对应后端：ImportTrainingCourseAsync
 */
export function importTrainingCoursData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${trainingCoursUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出TrainingCours
 * 对应后端：ExportTrainingCourseAsync；fileName 仅传名称不含后缀
 */
export function exportTrainingCoursData(query: TrainingCoursQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${trainingCoursUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
