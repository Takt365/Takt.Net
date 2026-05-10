// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/scheduling
// 文件名称：aps-schedule.ts
// 功能描述：ApsSchedule API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Scheduling.TaktApsSchedules
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  ApsSchedule,
  ApsScheduleQuery,
  ApsScheduleCreate,
  ApsScheduleUpdate
} from '@/types/logistics/manufacturing/scheduling/aps-schedule'

// ========================================
// ApsSchedule相关 API（按后端控制器顺序）
// ========================================
const apsScheduleUrl = '/api/TaktApsSchedules';

/**
 * 获取ApsSchedule列表（分页）
 * 对应后端：GetApsScheduleListAsync
 */
export function getApsScheduleList(params: ApsScheduleQuery): Promise<TaktPagedResult<ApsSchedule>> {
  return request({
    url: `${apsScheduleUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取ApsSchedule
 * 对应后端：GetApsScheduleByIdAsync
 */
export function getApsScheduleById(id: string): Promise<ApsSchedule> {
  return request({
    url: `${apsScheduleUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取ApsSchedule选项列表（用于下拉框等）
 * 对应后端：GetApsScheduleOptionsAsync
 */
export function getApsScheduleOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${apsScheduleUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建ApsSchedule
 * 对应后端：CreateApsScheduleAsync
 */
export function createApsSchedule(data: ApsScheduleCreate): Promise<ApsSchedule> {
  return request({
    url: apsScheduleUrl,
    method: 'post',
    data
  })
}

/**
 * 更新ApsSchedule
 * 对应后端：UpdateApsScheduleAsync
 */
export function updateApsSchedule(id: string, data: ApsScheduleUpdate): Promise<ApsSchedule> {
  return request({
    url: `${apsScheduleUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ApsSchedule（单条）
 * 对应后端：DeleteApsScheduleByIdAsync
 */
export function deleteApsScheduleById(id: string): Promise<void> {
  return request({
    url: `${apsScheduleUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ApsSchedule
 * 对应后端：DeleteApsScheduleBatchAsync
 */
export function deleteApsScheduleBatch(ids: string[]): Promise<void> {
  return request({
    url: `${apsScheduleUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetApsScheduleTemplateAsync；fileName 仅传名称不含后缀
 */
export function getApsScheduleTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${apsScheduleUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入ApsSchedule
 * 对应后端：ImportApsScheduleAsync
 */
export function importApsScheduleData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${apsScheduleUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出ApsSchedule
 * 对应后端：ExportApsScheduleAsync；fileName 仅传名称不含后缀
 */
export function exportApsScheduleData(query: ApsScheduleQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${apsScheduleUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
