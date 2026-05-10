// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：leave.ts
// 功能描述：Leave API，对应后端 Takt.WebApi.Controllers.HumanResource.AttendanceLeave.TaktLeaves
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Leave,
  LeaveQuery,
  LeaveCreate,
  LeaveUpdate,
  LeaveStatus
} from '@/types/human-resource/attendance-leave/leave'

// ========================================
// Leave相关 API（按后端控制器顺序）
// ========================================
const leaveUrl = '/api/TaktLeaves';

/**
 * 获取Leave列表（分页）
 * 对应后端：GetLeaveListAsync
 */
export function getLeaveList(params: LeaveQuery): Promise<TaktPagedResult<Leave>> {
  return request({
    url: `${leaveUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Leave
 * 对应后端：GetLeaveByIdAsync
 */
export function getLeaveById(id: string): Promise<Leave> {
  return request({
    url: `${leaveUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Leave选项列表（用于下拉框等）
 * 对应后端：GetLeaveOptionsAsync
 */
export function getLeaveOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${leaveUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Leave
 * 对应后端：CreateLeaveAsync
 */
export function createLeave(data: LeaveCreate): Promise<Leave> {
  return request({
    url: leaveUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Leave
 * 对应后端：UpdateLeaveAsync
 */
export function updateLeave(id: string, data: LeaveUpdate): Promise<Leave> {
  return request({
    url: `${leaveUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Leave（单条）
 * 对应后端：DeleteLeaveByIdAsync
 */
export function deleteLeaveById(id: string): Promise<void> {
  return request({
    url: `${leaveUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Leave
 * 对应后端：DeleteLeaveBatchAsync
 */
export function deleteLeaveBatch(ids: string[]): Promise<void> {
  return request({
    url: `${leaveUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Leave状态
 * 对应后端：UpdateLeaveStatusAsync
 */
export function updateLeaveStatus(data: LeaveStatus): Promise<LeaveStatus> {
  return request({
    url: `${leaveUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetLeaveTemplateAsync；fileName 仅传名称不含后缀
 */
export function getLeaveTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${leaveUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Leave
 * 对应后端：ImportLeaveAsync
 */
export function importLeaveData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${leaveUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Leave
 * 对应后端：ExportLeaveAsync；fileName 仅传名称不含后缀
 */
export function exportLeaveData(query: LeaveQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${leaveUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
