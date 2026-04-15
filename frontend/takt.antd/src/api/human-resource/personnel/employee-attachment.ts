// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/personnel
// 文件名称：employee-attachment.ts
// 功能描述：员工附件 API，对应后端 HumanResource/Personnel TaktEmployeeAttachmentsController
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  EmployeeAttachment,
  EmployeeAttachmentQuery,
  EmployeeAttachmentCreate,
  EmployeeAttachmentUpdate
} from '@/types/human-resource/personnel/employee-attachment'

// ========================================
// 员工附件相关 API（按后端控制器顺序）
// ========================================

const attachmentUrl = '/api/TaktEmployeeAttachments'

/**
 * 获取员工附件分页列表
 * 对应后端：GetListAsync
 */
export function getEmployeeAttachmentList(params: EmployeeAttachmentQuery): Promise<TaktPagedResult<EmployeeAttachment>> {
  return request({
    url: `${attachmentUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取员工附件详情
 * 对应后端：GetByIdAsync
 */
export function getEmployeeAttachmentById(id: string): Promise<EmployeeAttachment> {
  return request({
    url: `${attachmentUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 创建员工附件
 * 对应后端：CreateAsync
 */
export function createEmployeeAttachment(data: EmployeeAttachmentCreate): Promise<EmployeeAttachment> {
  return request({
    url: attachmentUrl,
    method: 'post',
    data
  })
}

/**
 * 更新员工附件
 * 对应后端：UpdateAsync
 */
export function updateEmployeeAttachment(id: string, data: EmployeeAttachmentUpdate): Promise<EmployeeAttachment> {
  return request({
    url: `${attachmentUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除员工附件（单条）
 * 对应后端：DeleteAsync
 */
export function deleteEmployeeAttachmentById(id: string): Promise<void> {
  return request({
    url: `${attachmentUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除员工附件
 * 对应后端：DeleteBatchAsync
 */
export function deleteEmployeeAttachmentBatch(ids: string[]): Promise<void> {
  return request({
    url: `${attachmentUrl}/batch`,
    method: 'delete',
    data: ids.map((id) => Number(id))
  })
}

/**
 * 获取员工附件导入模板
 * 对应后端：GetTemplateAsync
 */
export function getEmployeeAttachmentTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${attachmentUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入员工附件数据
 * 对应后端：ImportAsync
 */
export function importEmployeeAttachmentData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${attachmentUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出员工附件数据
 * 对应后端：ExportAsync
 */
export function exportEmployeeAttachmentData(
  query: EmployeeAttachmentQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${attachmentUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
