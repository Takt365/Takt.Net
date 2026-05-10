// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/talent
// 文件名称：talent.ts
// 功能描述：Talent API，对应后端 Takt.WebApi.Controllers.HumanResource.Talent.TaktTalents
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Talent,
  TalentQuery,
  TalentCreate,
  TalentUpdate,
  TalentStatus
} from '@/types/human-resource/talent/talent'

// ========================================
// Talent相关 API（按后端控制器顺序）
// ========================================
const talentUrl = '/api/TaktTalents';

/**
 * 获取Talent列表（分页）
 * 对应后端：GetTalentListAsync
 */
export function getTalentList(params: TalentQuery): Promise<TaktPagedResult<Talent>> {
  return request({
    url: `${talentUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Talent
 * 对应后端：GetTalentByIdAsync
 */
export function getTalentById(id: string): Promise<Talent> {
  return request({
    url: `${talentUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Talent选项列表（用于下拉框等）
 * 对应后端：GetTalentOptionsAsync
 */
export function getTalentOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${talentUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Talent
 * 对应后端：CreateTalentAsync
 */
export function createTalent(data: TalentCreate): Promise<Talent> {
  return request({
    url: talentUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Talent
 * 对应后端：UpdateTalentAsync
 */
export function updateTalent(id: string, data: TalentUpdate): Promise<Talent> {
  return request({
    url: `${talentUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Talent（单条）
 * 对应后端：DeleteTalentByIdAsync
 */
export function deleteTalentById(id: string): Promise<void> {
  return request({
    url: `${talentUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Talent
 * 对应后端：DeleteTalentBatchAsync
 */
export function deleteTalentBatch(ids: string[]): Promise<void> {
  return request({
    url: `${talentUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Talent状态
 * 对应后端：UpdateTalentStatusAsync
 */
export function updateTalentStatus(data: TalentStatus): Promise<TalentStatus> {
  return request({
    url: `${talentUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTalentTemplateAsync；fileName 仅传名称不含后缀
 */
export function getTalentTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${talentUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Talent
 * 对应后端：ImportTalentAsync
 */
export function importTalentData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${talentUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Talent
 * 对应后端：ExportTalentAsync；fileName 仅传名称不含后缀
 */
export function exportTalentData(query: TalentQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${talentUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
