// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：overtime-item.ts
// 功能描述：OvertimeItem API，对应后端 Takt.WebApi.Controllers.HumanResource.AttendanceLeave.TaktOvertimeItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  OvertimeItem,
  OvertimeItemQuery,
  OvertimeItemCreate,
  OvertimeItemUpdate
} from '@/types/human-resource/attendance-leave/overtime-item'

// ========================================
// OvertimeItem相关 API（按后端控制器顺序）
// ========================================
const overtimeItemUrl = '/api/TaktOvertimeItems';

/**
 * 获取OvertimeItem列表（分页）
 * 对应后端：GetOvertimeItemListAsync
 */
export function getOvertimeItemList(params: OvertimeItemQuery): Promise<TaktPagedResult<OvertimeItem>> {
  return request({
    url: `${overtimeItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取OvertimeItem
 * 对应后端：GetOvertimeItemByIdAsync
 */
export function getOvertimeItemById(id: string): Promise<OvertimeItem> {
  return request({
    url: `${overtimeItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取OvertimeItem选项列表（用于下拉框等）
 * 对应后端：GetOvertimeItemOptionsAsync
 */
export function getOvertimeItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${overtimeItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建OvertimeItem
 * 对应后端：CreateOvertimeItemAsync
 */
export function createOvertimeItem(data: OvertimeItemCreate): Promise<OvertimeItem> {
  return request({
    url: overtimeItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新OvertimeItem
 * 对应后端：UpdateOvertimeItemAsync
 */
export function updateOvertimeItem(id: string, data: OvertimeItemUpdate): Promise<OvertimeItem> {
  return request({
    url: `${overtimeItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除OvertimeItem（单条）
 * 对应后端：DeleteOvertimeItemByIdAsync
 */
export function deleteOvertimeItemById(id: string): Promise<void> {
  return request({
    url: `${overtimeItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除OvertimeItem
 * 对应后端：DeleteOvertimeItemBatchAsync
 */
export function deleteOvertimeItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${overtimeItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetOvertimeItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getOvertimeItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${overtimeItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入OvertimeItem
 * 对应后端：ImportOvertimeItemAsync
 */
export function importOvertimeItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${overtimeItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出OvertimeItem
 * 对应后端：ExportOvertimeItemAsync；fileName 仅传名称不含后缀
 */
export function exportOvertimeItemData(query: OvertimeItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${overtimeItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
