// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/cost
// 文件名称：quality-scrap-item.ts
// 功能描述：QualityScrapItem API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Cost.TaktQualityScrapItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  QualityScrapItem,
  QualityScrapItemQuery,
  QualityScrapItemCreate,
  QualityScrapItemUpdate
} from '@/types/logistics/quality/cost/quality-scrap-item'

// ========================================
// QualityScrapItem相关 API（按后端控制器顺序）
// ========================================
const qualityScrapItemUrl = '/api/TaktQualityScrapItems';

/**
 * 获取QualityScrapItem列表（分页）
 * 对应后端：GetQualityScrapItemListAsync
 */
export function getQualityScrapItemList(params: QualityScrapItemQuery): Promise<TaktPagedResult<QualityScrapItem>> {
  return request({
    url: `${qualityScrapItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取QualityScrapItem
 * 对应后端：GetQualityScrapItemByIdAsync
 */
export function getQualityScrapItemById(id: string): Promise<QualityScrapItem> {
  return request({
    url: `${qualityScrapItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取QualityScrapItem选项列表（用于下拉框等）
 * 对应后端：GetQualityScrapItemOptionsAsync
 */
export function getQualityScrapItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${qualityScrapItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建QualityScrapItem
 * 对应后端：CreateQualityScrapItemAsync
 */
export function createQualityScrapItem(data: QualityScrapItemCreate): Promise<QualityScrapItem> {
  return request({
    url: qualityScrapItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新QualityScrapItem
 * 对应后端：UpdateQualityScrapItemAsync
 */
export function updateQualityScrapItem(id: string, data: QualityScrapItemUpdate): Promise<QualityScrapItem> {
  return request({
    url: `${qualityScrapItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除QualityScrapItem（单条）
 * 对应后端：DeleteQualityScrapItemByIdAsync
 */
export function deleteQualityScrapItemById(id: string): Promise<void> {
  return request({
    url: `${qualityScrapItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除QualityScrapItem
 * 对应后端：DeleteQualityScrapItemBatchAsync
 */
export function deleteQualityScrapItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${qualityScrapItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetQualityScrapItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getQualityScrapItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${qualityScrapItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入QualityScrapItem
 * 对应后端：ImportQualityScrapItemAsync
 */
export function importQualityScrapItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${qualityScrapItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出QualityScrapItem
 * 对应后端：ExportQualityScrapItemAsync；fileName 仅传名称不含后缀
 */
export function exportQualityScrapItemData(query: QualityScrapItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${qualityScrapItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
