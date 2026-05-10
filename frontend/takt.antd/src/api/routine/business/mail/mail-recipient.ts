// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/mail
// 文件名称：mail-recipient.ts
// 功能描述：MailRecipient API，对应后端 Takt.WebApi.Controllers.Routine.Business.Mail.TaktMailRecipients
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  MailRecipient,
  MailRecipientQuery,
  MailRecipientCreate,
  MailRecipientUpdate
} from '@/types/routine/business/mail/mail-recipient'

// ========================================
// MailRecipient相关 API（按后端控制器顺序）
// ========================================
const mailRecipientUrl = '/api/TaktMailRecipients';

/**
 * 获取MailRecipient列表（分页）
 * 对应后端：GetMailRecipientListAsync
 */
export function getMailRecipientList(params: MailRecipientQuery): Promise<TaktPagedResult<MailRecipient>> {
  return request({
    url: `${mailRecipientUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取MailRecipient
 * 对应后端：GetMailRecipientByIdAsync
 */
export function getMailRecipientById(id: string): Promise<MailRecipient> {
  return request({
    url: `${mailRecipientUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取MailRecipient选项列表（用于下拉框等）
 * 对应后端：GetMailRecipientOptionsAsync
 */
export function getMailRecipientOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${mailRecipientUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建MailRecipient
 * 对应后端：CreateMailRecipientAsync
 */
export function createMailRecipient(data: MailRecipientCreate): Promise<MailRecipient> {
  return request({
    url: mailRecipientUrl,
    method: 'post',
    data
  })
}

/**
 * 更新MailRecipient
 * 对应后端：UpdateMailRecipientAsync
 */
export function updateMailRecipient(id: string, data: MailRecipientUpdate): Promise<MailRecipient> {
  return request({
    url: `${mailRecipientUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除MailRecipient（单条）
 * 对应后端：DeleteMailRecipientByIdAsync
 */
export function deleteMailRecipientById(id: string): Promise<void> {
  return request({
    url: `${mailRecipientUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除MailRecipient
 * 对应后端：DeleteMailRecipientBatchAsync
 */
export function deleteMailRecipientBatch(ids: string[]): Promise<void> {
  return request({
    url: `${mailRecipientUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetMailRecipientTemplateAsync；fileName 仅传名称不含后缀
 */
export function getMailRecipientTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${mailRecipientUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入MailRecipient
 * 对应后端：ImportMailRecipientAsync
 */
export function importMailRecipientData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${mailRecipientUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出MailRecipient
 * 对应后端：ExportMailRecipientAsync；fileName 仅传名称不含后缀
 */
export function exportMailRecipientData(query: MailRecipientQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${mailRecipientUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
