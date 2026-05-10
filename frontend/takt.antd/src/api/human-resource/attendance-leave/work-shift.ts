// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：work-shift.ts
// 功能描述：WorkShift API，对应后端 Takt.WebApi.Controllers.HumanResource.AttendanceLeave.TaktWorkShifts
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  WorkShift,
  WorkShiftQuery,
  WorkShiftCreate,
  WorkShiftUpdate,
  WorkShiftSort
} from '@/types/human-resource/attendance-leave/work-shift'

// ========================================
// WorkShift相关 API（按后端控制器顺序）
// ========================================
const workShiftUrl = '/api/TaktWorkShifts';

/**
 * 获取WorkShift列表（分页）
 * 对应后端：GetWorkShiftListAsync
 */
export function getWorkShiftList(params: WorkShiftQuery): Promise<TaktPagedResult<WorkShift>> {
  return request({
    url: `${workShiftUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取WorkShift
 * 对应后端：GetWorkShiftByIdAsync
 */
export function getWorkShiftById(id: string): Promise<WorkShift> {
  return request({
    url: `${workShiftUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取WorkShift选项列表（用于下拉框等）
 * 对应后端：GetWorkShiftOptionsAsync
 */
export function getWorkShiftOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${workShiftUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建WorkShift
 * 对应后端：CreateWorkShiftAsync
 */
export function createWorkShift(data: WorkShiftCreate): Promise<WorkShift> {
  return request({
    url: workShiftUrl,
    method: 'post',
    data
  })
}

/**
 * 更新WorkShift
 * 对应后端：UpdateWorkShiftAsync
 */
export function updateWorkShift(id: string, data: WorkShiftUpdate): Promise<WorkShift> {
  return request({
    url: `${workShiftUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除WorkShift（单条）
 * 对应后端：DeleteWorkShiftByIdAsync
 */
export function deleteWorkShiftById(id: string): Promise<void> {
  return request({
    url: `${workShiftUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除WorkShift
 * 对应后端：DeleteWorkShiftBatchAsync
 */
export function deleteWorkShiftBatch(ids: string[]): Promise<void> {
  return request({
    url: `${workShiftUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新WorkShift排序
 * 对应后端：UpdateWorkShiftSortAsync
 */
export function updateWorkShiftSort(data: WorkShiftSort): Promise<WorkShiftSort> {
  return request({
    url: `${workShiftUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetWorkShiftTemplateAsync；fileName 仅传名称不含后缀
 */
export function getWorkShiftTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${workShiftUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入WorkShift
 * 对应后端：ImportWorkShiftAsync
 */
export function importWorkShiftData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${workShiftUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出WorkShift
 * 对应后端：ExportWorkShiftAsync；fileName 仅传名称不含后缀
 */
export function exportWorkShiftData(query: WorkShiftQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${workShiftUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
