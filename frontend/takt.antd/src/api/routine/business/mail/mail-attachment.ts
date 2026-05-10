// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/mail
// 文件名称：mail-attachment.ts
// 功能描述：MailAttachment API，对应后端 Takt.WebApi.Controllers.Routine.Business.Mail.TaktMailAttachments
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  MailAttachment,
  MailAttachmentQuery,
  MailAttachmentCreate,
  MailAttachmentUpdate,
  MailAttachmentSort
} from '@/types/routine/business/mail/mail-attachment'

// ========================================
// MailAttachment相关 API（按后端控制器顺序）
// ========================================
const mailAttachmentUrl = '/api/TaktMailAttachments';

/**
 * 获取MailAttachment列表（分页）
 * 对应后端：GetMailAttachmentListAsync
 */
export function getMailAttachmentList(params: MailAttachmentQuery): Promise<TaktPagedResult<MailAttachment>> {
  return request({
    url: `${mailAttachmentUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取MailAttachment
 * 对应后端：GetMailAttachmentByIdAsync
 */
export function getMailAttachmentById(id: string): Promise<MailAttachment> {
  return request({
    url: `${mailAttachmentUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取MailAttachment选项列表（用于下拉框等）
 * 对应后端：GetMailAttachmentOptionsAsync
 */
export function getMailAttachmentOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${mailAttachmentUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建MailAttachment
 * 对应后端：CreateMailAttachmentAsync
 */
export function createMailAttachment(data: MailAttachmentCreate): Promise<MailAttachment> {
  return request({
    url: mailAttachmentUrl,
    method: 'post',
    data
  })
}

/**
 * 更新MailAttachment
 * 对应后端：UpdateMailAttachmentAsync
 */
export function updateMailAttachment(id: string, data: MailAttachmentUpdate): Promise<MailAttachment> {
  return request({
    url: `${mailAttachmentUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除MailAttachment（单条）
 * 对应后端：DeleteMailAttachmentByIdAsync
 */
export function deleteMailAttachmentById(id: string): Promise<void> {
  return request({
    url: `${mailAttachmentUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除MailAttachment
 * 对应后端：DeleteMailAttachmentBatchAsync
 */
export function deleteMailAttachmentBatch(ids: string[]): Promise<void> {
  return request({
    url: `${mailAttachmentUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新MailAttachment排序
 * 对应后端：UpdateMailAttachmentSortAsync
 */
export function updateMailAttachmentSort(data: MailAttachmentSort): Promise<MailAttachmentSort> {
  return request({
    url: `${mailAttachmentUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetMailAttachmentTemplateAsync；fileName 仅传名称不含后缀
 */
export function getMailAttachmentTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${mailAttachmentUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入MailAttachment
 * 对应后端：ImportMailAttachmentAsync
 */
export function importMailAttachmentData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${mailAttachmentUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出MailAttachment
 * 对应后端：ExportMailAttachmentAsync；fileName 仅传名称不含后缀
 */
export function exportMailAttachmentData(query: MailAttachmentQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${mailAttachmentUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
