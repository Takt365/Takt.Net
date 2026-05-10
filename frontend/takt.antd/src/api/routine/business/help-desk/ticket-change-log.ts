// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/help-desk
// 文件名称：ticket-change-log.ts
// 功能描述：TicketChangeLog API，对应后端 Takt.WebApi.Controllers.Routine.Business.HelpDesk.TaktTicketChangeLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  TicketChangeLog,
  TicketChangeLogQuery,
  TicketChangeLogCreate,
  TicketChangeLogUpdate
} from '@/types/routine/business/help-desk/ticket-change-log'

// ========================================
// TicketChangeLog相关 API（按后端控制器顺序）
// ========================================
const ticketChangeLogUrl = '/api/TaktTicketChangeLogs';

/**
 * 获取TicketChangeLog列表（分页）
 * 对应后端：GetTicketChangeLogListAsync
 */
export function getTicketChangeLogList(params: TicketChangeLogQuery): Promise<TaktPagedResult<TicketChangeLog>> {
  return request({
    url: `${ticketChangeLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取TicketChangeLog
 * 对应后端：GetTicketChangeLogByIdAsync
 */
export function getTicketChangeLogById(id: string): Promise<TicketChangeLog> {
  return request({
    url: `${ticketChangeLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取TicketChangeLog选项列表（用于下拉框等）
 * 对应后端：GetTicketChangeLogOptionsAsync
 */
export function getTicketChangeLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${ticketChangeLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建TicketChangeLog
 * 对应后端：CreateTicketChangeLogAsync
 */
export function createTicketChangeLog(data: TicketChangeLogCreate): Promise<TicketChangeLog> {
  return request({
    url: ticketChangeLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新TicketChangeLog
 * 对应后端：UpdateTicketChangeLogAsync
 */
export function updateTicketChangeLog(id: string, data: TicketChangeLogUpdate): Promise<TicketChangeLog> {
  return request({
    url: `${ticketChangeLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除TicketChangeLog（单条）
 * 对应后端：DeleteTicketChangeLogByIdAsync
 */
export function deleteTicketChangeLogById(id: string): Promise<void> {
  return request({
    url: `${ticketChangeLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除TicketChangeLog
 * 对应后端：DeleteTicketChangeLogBatchAsync
 */
export function deleteTicketChangeLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${ticketChangeLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTicketChangeLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getTicketChangeLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${ticketChangeLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入TicketChangeLog
 * 对应后端：ImportTicketChangeLogAsync
 */
export function importTicketChangeLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${ticketChangeLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出TicketChangeLog
 * 对应后端：ExportTicketChangeLogAsync；fileName 仅传名称不含后缀
 */
export function exportTicketChangeLogData(query: TicketChangeLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${ticketChangeLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
