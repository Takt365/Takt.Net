// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/help-desk
// 文件名称：ticket-evaluation.ts
// 功能描述：TicketEvaluation API，对应后端 Takt.WebApi.Controllers.Routine.Business.HelpDesk.TaktTicketEvaluations
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  TicketEvaluation,
  TicketEvaluationQuery,
  TicketEvaluationCreate,
  TicketEvaluationUpdate
} from '@/types/routine/business/help-desk/ticket-evaluation'

// ========================================
// TicketEvaluation相关 API（按后端控制器顺序）
// ========================================
const ticketEvaluationUrl = '/api/TaktTicketEvaluations';

/**
 * 获取TicketEvaluation列表（分页）
 * 对应后端：GetTicketEvaluationListAsync
 */
export function getTicketEvaluationList(params: TicketEvaluationQuery): Promise<TaktPagedResult<TicketEvaluation>> {
  return request({
    url: `${ticketEvaluationUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取TicketEvaluation
 * 对应后端：GetTicketEvaluationByIdAsync
 */
export function getTicketEvaluationById(id: string): Promise<TicketEvaluation> {
  return request({
    url: `${ticketEvaluationUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取TicketEvaluation选项列表（用于下拉框等）
 * 对应后端：GetTicketEvaluationOptionsAsync
 */
export function getTicketEvaluationOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${ticketEvaluationUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建TicketEvaluation
 * 对应后端：CreateTicketEvaluationAsync
 */
export function createTicketEvaluation(data: TicketEvaluationCreate): Promise<TicketEvaluation> {
  return request({
    url: ticketEvaluationUrl,
    method: 'post',
    data
  })
}

/**
 * 更新TicketEvaluation
 * 对应后端：UpdateTicketEvaluationAsync
 */
export function updateTicketEvaluation(id: string, data: TicketEvaluationUpdate): Promise<TicketEvaluation> {
  return request({
    url: `${ticketEvaluationUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除TicketEvaluation（单条）
 * 对应后端：DeleteTicketEvaluationByIdAsync
 */
export function deleteTicketEvaluationById(id: string): Promise<void> {
  return request({
    url: `${ticketEvaluationUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除TicketEvaluation
 * 对应后端：DeleteTicketEvaluationBatchAsync
 */
export function deleteTicketEvaluationBatch(ids: string[]): Promise<void> {
  return request({
    url: `${ticketEvaluationUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTicketEvaluationTemplateAsync；fileName 仅传名称不含后缀
 */
export function getTicketEvaluationTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${ticketEvaluationUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入TicketEvaluation
 * 对应后端：ImportTicketEvaluationAsync
 */
export function importTicketEvaluationData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${ticketEvaluationUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出TicketEvaluation
 * 对应后端：ExportTicketEvaluationAsync；fileName 仅传名称不含后缀
 */
export function exportTicketEvaluationData(query: TicketEvaluationQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${ticketEvaluationUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
