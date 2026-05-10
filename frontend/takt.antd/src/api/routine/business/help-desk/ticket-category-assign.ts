// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/help-desk
// 文件名称：ticket-category-assign.ts
// 功能描述：TicketCategoryAssign API，对应后端 Takt.WebApi.Controllers.Routine.Business.HelpDesk.TaktTicketCategoryAssigns
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  TicketCategoryAssign,
  TicketCategoryAssignQuery,
  TicketCategoryAssignCreate,
  TicketCategoryAssignUpdate,
  TicketCategoryAssignSort
} from '@/types/routine/business/help-desk/ticket-category-assign'

// ========================================
// TicketCategoryAssign相关 API（按后端控制器顺序）
// ========================================
const ticketCategoryAssignUrl = '/api/TaktTicketCategoryAssigns';

/**
 * 获取TicketCategoryAssign列表（分页）
 * 对应后端：GetTicketCategoryAssignListAsync
 */
export function getTicketCategoryAssignList(params: TicketCategoryAssignQuery): Promise<TaktPagedResult<TicketCategoryAssign>> {
  return request({
    url: `${ticketCategoryAssignUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取TicketCategoryAssign
 * 对应后端：GetTicketCategoryAssignByIdAsync
 */
export function getTicketCategoryAssignById(id: string): Promise<TicketCategoryAssign> {
  return request({
    url: `${ticketCategoryAssignUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取TicketCategoryAssign选项列表（用于下拉框等）
 * 对应后端：GetTicketCategoryAssignOptionsAsync
 */
export function getTicketCategoryAssignOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${ticketCategoryAssignUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建TicketCategoryAssign
 * 对应后端：CreateTicketCategoryAssignAsync
 */
export function createTicketCategoryAssign(data: TicketCategoryAssignCreate): Promise<TicketCategoryAssign> {
  return request({
    url: ticketCategoryAssignUrl,
    method: 'post',
    data
  })
}

/**
 * 更新TicketCategoryAssign
 * 对应后端：UpdateTicketCategoryAssignAsync
 */
export function updateTicketCategoryAssign(id: string, data: TicketCategoryAssignUpdate): Promise<TicketCategoryAssign> {
  return request({
    url: `${ticketCategoryAssignUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除TicketCategoryAssign（单条）
 * 对应后端：DeleteTicketCategoryAssignByIdAsync
 */
export function deleteTicketCategoryAssignById(id: string): Promise<void> {
  return request({
    url: `${ticketCategoryAssignUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除TicketCategoryAssign
 * 对应后端：DeleteTicketCategoryAssignBatchAsync
 */
export function deleteTicketCategoryAssignBatch(ids: string[]): Promise<void> {
  return request({
    url: `${ticketCategoryAssignUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新TicketCategoryAssign排序
 * 对应后端：UpdateTicketCategoryAssignSortAsync
 */
export function updateTicketCategoryAssignSort(data: TicketCategoryAssignSort): Promise<TicketCategoryAssignSort> {
  return request({
    url: `${ticketCategoryAssignUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTicketCategoryAssignTemplateAsync；fileName 仅传名称不含后缀
 */
export function getTicketCategoryAssignTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${ticketCategoryAssignUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入TicketCategoryAssign
 * 对应后端：ImportTicketCategoryAssignAsync
 */
export function importTicketCategoryAssignData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${ticketCategoryAssignUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出TicketCategoryAssign
 * 对应后端：ExportTicketCategoryAssignAsync；fileName 仅传名称不含后缀
 */
export function exportTicketCategoryAssignData(query: TicketCategoryAssignQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${ticketCategoryAssignUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
