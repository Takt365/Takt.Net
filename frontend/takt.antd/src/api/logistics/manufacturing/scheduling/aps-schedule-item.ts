// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/scheduling
// 文件名称：aps-schedule-item.ts
// 功能描述：ApsScheduleItem API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Scheduling.TaktApsScheduleItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  ApsScheduleItem,
  ApsScheduleItemQuery,
  ApsScheduleItemCreate,
  ApsScheduleItemUpdate
} from '@/types/logistics/manufacturing/scheduling/aps-schedule-item'

// ========================================
// ApsScheduleItem相关 API（按后端控制器顺序）
// ========================================
const apsScheduleItemUrl = '/api/TaktApsScheduleItems';

/**
 * 获取ApsScheduleItem列表（分页）
 * 对应后端：GetApsScheduleItemListAsync
 */
export function getApsScheduleItemList(params: ApsScheduleItemQuery): Promise<TaktPagedResult<ApsScheduleItem>> {
  return request({
    url: `${apsScheduleItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取ApsScheduleItem
 * 对应后端：GetApsScheduleItemByIdAsync
 */
export function getApsScheduleItemById(id: string): Promise<ApsScheduleItem> {
  return request({
    url: `${apsScheduleItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取ApsScheduleItem选项列表（用于下拉框等）
 * 对应后端：GetApsScheduleItemOptionsAsync
 */
export function getApsScheduleItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${apsScheduleItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建ApsScheduleItem
 * 对应后端：CreateApsScheduleItemAsync
 */
export function createApsScheduleItem(data: ApsScheduleItemCreate): Promise<ApsScheduleItem> {
  return request({
    url: apsScheduleItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新ApsScheduleItem
 * 对应后端：UpdateApsScheduleItemAsync
 */
export function updateApsScheduleItem(id: string, data: ApsScheduleItemUpdate): Promise<ApsScheduleItem> {
  return request({
    url: `${apsScheduleItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ApsScheduleItem（单条）
 * 对应后端：DeleteApsScheduleItemByIdAsync
 */
export function deleteApsScheduleItemById(id: string): Promise<void> {
  return request({
    url: `${apsScheduleItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ApsScheduleItem
 * 对应后端：DeleteApsScheduleItemBatchAsync
 */
export function deleteApsScheduleItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${apsScheduleItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetApsScheduleItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getApsScheduleItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${apsScheduleItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入ApsScheduleItem
 * 对应后端：ImportApsScheduleItemAsync
 */
export function importApsScheduleItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${apsScheduleItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出ApsScheduleItem
 * 对应后端：ExportApsScheduleItemAsync；fileName 仅传名称不含后缀
 */
export function exportApsScheduleItemData(query: ApsScheduleItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${apsScheduleItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
