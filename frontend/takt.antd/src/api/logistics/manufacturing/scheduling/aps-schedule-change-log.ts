// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/scheduling
// 文件名称：aps-schedule-change-log.ts
// 功能描述：ApsScheduleChangeLog API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Scheduling.TaktApsScheduleChangeLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  ApsScheduleChangeLog,
  ApsScheduleChangeLogQuery,
  ApsScheduleChangeLogCreate,
  ApsScheduleChangeLogUpdate
} from '@/types/logistics/manufacturing/scheduling/aps-schedule-change-log'

// ========================================
// ApsScheduleChangeLog相关 API（按后端控制器顺序）
// ========================================
const apsScheduleChangeLogUrl = '/api/TaktApsScheduleChangeLogs';

/**
 * 获取ApsScheduleChangeLog列表（分页）
 * 对应后端：GetApsScheduleChangeLogListAsync
 */
export function getApsScheduleChangeLogList(params: ApsScheduleChangeLogQuery): Promise<TaktPagedResult<ApsScheduleChangeLog>> {
  return request({
    url: `${apsScheduleChangeLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取ApsScheduleChangeLog
 * 对应后端：GetApsScheduleChangeLogByIdAsync
 */
export function getApsScheduleChangeLogById(id: string): Promise<ApsScheduleChangeLog> {
  return request({
    url: `${apsScheduleChangeLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取ApsScheduleChangeLog选项列表（用于下拉框等）
 * 对应后端：GetApsScheduleChangeLogOptionsAsync
 */
export function getApsScheduleChangeLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${apsScheduleChangeLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建ApsScheduleChangeLog
 * 对应后端：CreateApsScheduleChangeLogAsync
 */
export function createApsScheduleChangeLog(data: ApsScheduleChangeLogCreate): Promise<ApsScheduleChangeLog> {
  return request({
    url: apsScheduleChangeLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新ApsScheduleChangeLog
 * 对应后端：UpdateApsScheduleChangeLogAsync
 */
export function updateApsScheduleChangeLog(id: string, data: ApsScheduleChangeLogUpdate): Promise<ApsScheduleChangeLog> {
  return request({
    url: `${apsScheduleChangeLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ApsScheduleChangeLog（单条）
 * 对应后端：DeleteApsScheduleChangeLogByIdAsync
 */
export function deleteApsScheduleChangeLogById(id: string): Promise<void> {
  return request({
    url: `${apsScheduleChangeLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ApsScheduleChangeLog
 * 对应后端：DeleteApsScheduleChangeLogBatchAsync
 */
export function deleteApsScheduleChangeLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${apsScheduleChangeLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetApsScheduleChangeLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getApsScheduleChangeLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${apsScheduleChangeLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入ApsScheduleChangeLog
 * 对应后端：ImportApsScheduleChangeLogAsync
 */
export function importApsScheduleChangeLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${apsScheduleChangeLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出ApsScheduleChangeLog
 * 对应后端：ExportApsScheduleChangeLogAsync；fileName 仅传名称不含后缀
 */
export function exportApsScheduleChangeLogData(query: ApsScheduleChangeLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${apsScheduleChangeLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
