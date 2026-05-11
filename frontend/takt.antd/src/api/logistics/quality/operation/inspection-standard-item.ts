// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/operation
// 文件名称：inspection-standard-item.ts
// 功能描述：InspectionStandardItem API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Operation.TaktInspectionStandardItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  InspectionStandardItem,
  InspectionStandardItemQuery,
  InspectionStandardItemCreate,
  InspectionStandardItemUpdate
} from '@/types/logistics/quality/operation/inspection-standard-item'

// ========================================
// InspectionStandardItem相关 API（按后端控制器顺序）
// ========================================
const inspectionStandardItemUrl = '/api/TaktInspectionStandardItems';

/**
 * 获取InspectionStandardItem列表（分页）
 * 对应后端：GetInspectionStandardItemListAsync
 */
export function getInspectionStandardItemList(params: InspectionStandardItemQuery): Promise<TaktPagedResult<InspectionStandardItem>> {
  return request({
    url: `${inspectionStandardItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取InspectionStandardItem
 * 对应后端：GetInspectionStandardItemByIdAsync
 */
export function getInspectionStandardItemById(id: string): Promise<InspectionStandardItem> {
  return request({
    url: `${inspectionStandardItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取InspectionStandardItem选项列表（用于下拉框等）
 * 对应后端：GetInspectionStandardItemOptionsAsync
 */
export function getInspectionStandardItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${inspectionStandardItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建InspectionStandardItem
 * 对应后端：CreateInspectionStandardItemAsync
 */
export function createInspectionStandardItem(data: InspectionStandardItemCreate): Promise<InspectionStandardItem> {
  return request({
    url: inspectionStandardItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新InspectionStandardItem
 * 对应后端：UpdateInspectionStandardItemAsync
 */
export function updateInspectionStandardItem(id: string, data: InspectionStandardItemUpdate): Promise<InspectionStandardItem> {
  return request({
    url: `${inspectionStandardItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除InspectionStandardItem（单条）
 * 对应后端：DeleteInspectionStandardItemByIdAsync
 */
export function deleteInspectionStandardItemById(id: string): Promise<void> {
  return request({
    url: `${inspectionStandardItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除InspectionStandardItem
 * 对应后端：DeleteInspectionStandardItemBatchAsync
 */
export function deleteInspectionStandardItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${inspectionStandardItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetInspectionStandardItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getInspectionStandardItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${inspectionStandardItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入InspectionStandardItem
 * 对应后端：ImportInspectionStandardItemAsync
 */
export function importInspectionStandardItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${inspectionStandardItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出InspectionStandardItem
 * 对应后端：ExportInspectionStandardItemAsync；fileName 仅传名称不含后缀
 */
export function exportInspectionStandardItemData(query: InspectionStandardItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${inspectionStandardItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
