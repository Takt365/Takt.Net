// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave/leave
// 文件名称：leave.ts
// 功能描述：请假相关 API，对应后端 TaktLeaveController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  Leave,
  LeaveQuery,
  LeaveCreate,
  LeaveUpdate,
  LeaveStatus
} from '@/types/human-resource/attendance-leave/leave'

// ========================================
// 请假相关 API（按后端控制器顺序）
// ========================================

/**
 * 获取请假列表（分页）
 * 对应后端：GetListAsync
 */
export function getLeaveList(params: LeaveQuery): Promise<TaktPagedResult<Leave>> {
  return request({
    url: '/api/TaktLeave/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取请假详情
 * 对应后端：GetByIdAsync
 */
export function getLeaveById(id: string): Promise<Leave> {
  return request({
    url: `/api/TaktLeave/${id}`,
    method: 'get'
  })
}

/**
 * 创建请假（草稿，不发起流程）
 * 对应后端：CreateAsync
 */
export function createLeave(data: LeaveCreate): Promise<Leave> {
  return request({
    url: '/api/TaktLeave',
    method: 'post',
    data
  })
}

/**
 * 更新请假
 * 对应后端：UpdateAsync
 */
export function updateLeave(id: string, data: LeaveUpdate): Promise<Leave> {
  return request({
    url: `/api/TaktLeave/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除请假
 * 对应后端：DeleteAsync
 */
export function deleteLeaveById(id: string): Promise<void> {
  return request({
    url: `/api/TaktLeave/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除请假
 * 对应后端：DeleteBatchAsync
 */
export function deleteLeaveBatch(ids: string[]): Promise<void> {
  return request({
    url: '/api/TaktLeave/batch',
    method: 'delete',
    data: ids.map((id) => Number(id))
  })
}

/**
 * 更新请假状态（与流程实例同步）
 * 对应后端：UpdateStatusAsync
 */
export function updateLeaveStatus(data: LeaveStatus): Promise<Leave> {
  return request({
    url: '/api/TaktLeave/status',
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTemplateAsync
 */
export function getLeaveTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: '/api/TaktLeave/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入请假数据
 * 对应后端：ImportAsync
 */
export function importLeaveData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: '/api/TaktLeave/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出请假数据
 * 对应后端：ExportAsync
 */
export function exportLeaveData(
  query: LeaveQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: '/api/TaktLeave/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
