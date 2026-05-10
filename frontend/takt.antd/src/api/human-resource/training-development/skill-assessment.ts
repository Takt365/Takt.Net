// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/training-development
// 文件名称：skill-assessment.ts
// 功能描述：SkillAssessment API，对应后端 Takt.WebApi.Controllers.HumanResource.TrainingDevelopment.TaktSkillAssessments
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  SkillAssessment,
  SkillAssessmentQuery,
  SkillAssessmentCreate,
  SkillAssessmentUpdate,
  SkillAssessmentStatus
} from '@/types/human-resource/training-development/skill-assessment'

// ========================================
// SkillAssessment相关 API（按后端控制器顺序）
// ========================================
const skillAssessmentUrl = '/api/TaktSkillAssessments';

/**
 * 获取SkillAssessment列表（分页）
 * 对应后端：GetSkillAssessmentListAsync
 */
export function getSkillAssessmentList(params: SkillAssessmentQuery): Promise<TaktPagedResult<SkillAssessment>> {
  return request({
    url: `${skillAssessmentUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取SkillAssessment
 * 对应后端：GetSkillAssessmentByIdAsync
 */
export function getSkillAssessmentById(id: string): Promise<SkillAssessment> {
  return request({
    url: `${skillAssessmentUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取SkillAssessment选项列表（用于下拉框等）
 * 对应后端：GetSkillAssessmentOptionsAsync
 */
export function getSkillAssessmentOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${skillAssessmentUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建SkillAssessment
 * 对应后端：CreateSkillAssessmentAsync
 */
export function createSkillAssessment(data: SkillAssessmentCreate): Promise<SkillAssessment> {
  return request({
    url: skillAssessmentUrl,
    method: 'post',
    data
  })
}

/**
 * 更新SkillAssessment
 * 对应后端：UpdateSkillAssessmentAsync
 */
export function updateSkillAssessment(id: string, data: SkillAssessmentUpdate): Promise<SkillAssessment> {
  return request({
    url: `${skillAssessmentUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除SkillAssessment（单条）
 * 对应后端：DeleteSkillAssessmentByIdAsync
 */
export function deleteSkillAssessmentById(id: string): Promise<void> {
  return request({
    url: `${skillAssessmentUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除SkillAssessment
 * 对应后端：DeleteSkillAssessmentBatchAsync
 */
export function deleteSkillAssessmentBatch(ids: string[]): Promise<void> {
  return request({
    url: `${skillAssessmentUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新SkillAssessment状态
 * 对应后端：UpdateSkillAssessmentStatusAsync
 */
export function updateSkillAssessmentStatus(data: SkillAssessmentStatus): Promise<SkillAssessmentStatus> {
  return request({
    url: `${skillAssessmentUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetSkillAssessmentTemplateAsync；fileName 仅传名称不含后缀
 */
export function getSkillAssessmentTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${skillAssessmentUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入SkillAssessment
 * 对应后端：ImportSkillAssessmentAsync
 */
export function importSkillAssessmentData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${skillAssessmentUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出SkillAssessment
 * 对应后端：ExportSkillAssessmentAsync；fileName 仅传名称不含后缀
 */
export function exportSkillAssessmentData(query: SkillAssessmentQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${skillAssessmentUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
