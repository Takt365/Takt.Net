// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/mail
// 文件名称：mail.ts
// 功能描述：Mail API，对应后端 Takt.WebApi.Controllers.Routine.Business.Mail.TaktMails
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Mail,
  MailQuery,
  MailCreate,
  MailUpdate,
  MailStatus
} from '@/types/routine/business/mail/mail'

// ========================================
// Mail相关 API（按后端控制器顺序）
// ========================================
const mailUrl = '/api/TaktMails';

/**
 * 获取Mail列表（分页）
 * 对应后端：GetMailListAsync
 */
export function getMailList(params: MailQuery): Promise<TaktPagedResult<Mail>> {
  return request({
    url: `${mailUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Mail
 * 对应后端：GetMailByIdAsync
 */
export function getMailById(id: string): Promise<Mail> {
  return request({
    url: `${mailUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Mail选项列表（用于下拉框等）
 * 对应后端：GetMailOptionsAsync
 */
export function getMailOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${mailUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Mail
 * 对应后端：CreateMailAsync
 */
export function createMail(data: MailCreate): Promise<Mail> {
  return request({
    url: mailUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Mail
 * 对应后端：UpdateMailAsync
 */
export function updateMail(id: string, data: MailUpdate): Promise<Mail> {
  return request({
    url: `${mailUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Mail（单条）
 * 对应后端：DeleteMailByIdAsync
 */
export function deleteMailById(id: string): Promise<void> {
  return request({
    url: `${mailUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Mail
 * 对应后端：DeleteMailBatchAsync
 */
export function deleteMailBatch(ids: string[]): Promise<void> {
  return request({
    url: `${mailUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Mail状态
 * 对应后端：UpdateMailStatusAsync
 */
export function updateMailStatus(data: MailStatus): Promise<MailStatus> {
  return request({
    url: `${mailUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetMailTemplateAsync；fileName 仅传名称不含后缀
 */
export function getMailTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${mailUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Mail
 * 对应后端：ImportMailAsync
 */
export function importMailData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${mailUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Mail
 * 对应后端：ExportMailAsync；fileName 仅传名称不含后缀
 */
export function exportMailData(query: MailQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${mailUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
