// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/signal-r
// 文件名称：message.ts
// 功能描述：Message API，对应后端 Takt.WebApi.Controllers.Routine.Tasks.SignalR.TaktMessages
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Message,
  MessageQuery,
  MessageCreate,
  MessageUpdate
} from '@/types/routine/tasks/signal-r/message'

// ========================================
// Message相关 API（按后端控制器顺序）
// ========================================
const messageUrl = '/api/TaktMessages';

/**
 * 获取Message列表（分页）
 * 对应后端：GetMessageListAsync
 */
export function getMessageList(params: MessageQuery): Promise<TaktPagedResult<Message>> {
  return request({
    url: `${messageUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Message
 * 对应后端：GetMessageByIdAsync
 */
export function getMessageById(id: string): Promise<Message> {
  return request({
    url: `${messageUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Message选项列表（用于下拉框等）
 * 对应后端：GetMessageOptionsAsync
 */
export function getMessageOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${messageUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Message
 * 对应后端：CreateMessageAsync
 */
export function createMessage(data: MessageCreate): Promise<Message> {
  return request({
    url: messageUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Message
 * 对应后端：UpdateMessageAsync
 */
export function updateMessage(id: string, data: MessageUpdate): Promise<Message> {
  return request({
    url: `${messageUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Message（单条）
 * 对应后端：DeleteMessageByIdAsync
 */
export function deleteMessageById(id: string): Promise<void> {
  return request({
    url: `${messageUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Message
 * 对应后端：DeleteMessageBatchAsync
 */
export function deleteMessageBatch(ids: string[]): Promise<void> {
  return request({
    url: `${messageUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetMessageTemplateAsync；fileName 仅传名称不含后缀
 */
export function getMessageTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${messageUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Message
 * 对应后端：ImportMessageAsync
 */
export function importMessageData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${messageUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Message
 * 对应后端：ExportMessageAsync；fileName 仅传名称不含后缀
 */
export function exportMessageData(query: MessageQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${messageUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
