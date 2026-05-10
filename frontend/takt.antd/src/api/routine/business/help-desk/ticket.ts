// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/help-desk
// 文件名称：ticket.ts
// 功能描述：Ticket API，对应后端 Takt.WebApi.Controllers.Routine.Business.HelpDesk.TaktTickets
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Ticket,
  TicketQuery,
  TicketCreate,
  TicketUpdate,
  TicketStatus
} from '@/types/routine/business/help-desk/ticket'

// ========================================
// Ticket相关 API（按后端控制器顺序）
// ========================================
const ticketUrl = '/api/TaktTickets';

/**
 * 获取Ticket列表（分页）
 * 对应后端：GetTicketListAsync
 */
export function getTicketList(params: TicketQuery): Promise<TaktPagedResult<Ticket>> {
  return request({
    url: `${ticketUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Ticket
 * 对应后端：GetTicketByIdAsync
 */
export function getTicketById(id: string): Promise<Ticket> {
  return request({
    url: `${ticketUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Ticket选项列表（用于下拉框等）
 * 对应后端：GetTicketOptionsAsync
 */
export function getTicketOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${ticketUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Ticket
 * 对应后端：CreateTicketAsync
 */
export function createTicket(data: TicketCreate): Promise<Ticket> {
  return request({
    url: ticketUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Ticket
 * 对应后端：UpdateTicketAsync
 */
export function updateTicket(id: string, data: TicketUpdate): Promise<Ticket> {
  return request({
    url: `${ticketUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Ticket（单条）
 * 对应后端：DeleteTicketByIdAsync
 */
export function deleteTicketById(id: string): Promise<void> {
  return request({
    url: `${ticketUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Ticket
 * 对应后端：DeleteTicketBatchAsync
 */
export function deleteTicketBatch(ids: string[]): Promise<void> {
  return request({
    url: `${ticketUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Ticket状态
 * 对应后端：UpdateTicketStatusAsync
 */
export function updateTicketStatus(data: TicketStatus): Promise<TicketStatus> {
  return request({
    url: `${ticketUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTicketTemplateAsync；fileName 仅传名称不含后缀
 */
export function getTicketTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${ticketUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Ticket
 * 对应后端：ImportTicketAsync
 */
export function importTicketData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${ticketUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Ticket
 * 对应后端：ExportTicketAsync；fileName 仅传名称不含后缀
 */
export function exportTicketData(query: TicketQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${ticketUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
