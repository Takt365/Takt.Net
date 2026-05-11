// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/complaint
// 文件名称：supplier-evaluation-item.ts
// 功能描述：SupplierEvaluationItem API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Complaint.TaktSupplierEvaluationItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  SupplierEvaluationItem,
  SupplierEvaluationItemQuery,
  SupplierEvaluationItemCreate,
  SupplierEvaluationItemUpdate,
  SupplierEvaluationItemSort
} from '@/types/logistics/quality/complaint/supplier-evaluation-item'

// ========================================
// SupplierEvaluationItem相关 API（按后端控制器顺序）
// ========================================
const supplierEvaluationItemUrl = '/api/TaktSupplierEvaluationItems';

/**
 * 获取SupplierEvaluationItem列表（分页）
 * 对应后端：GetSupplierEvaluationItemListAsync
 */
export function getSupplierEvaluationItemList(params: SupplierEvaluationItemQuery): Promise<TaktPagedResult<SupplierEvaluationItem>> {
  return request({
    url: `${supplierEvaluationItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取SupplierEvaluationItem
 * 对应后端：GetSupplierEvaluationItemByIdAsync
 */
export function getSupplierEvaluationItemById(id: string): Promise<SupplierEvaluationItem> {
  return request({
    url: `${supplierEvaluationItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取SupplierEvaluationItem选项列表（用于下拉框等）
 * 对应后端：GetSupplierEvaluationItemOptionsAsync
 */
export function getSupplierEvaluationItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${supplierEvaluationItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建SupplierEvaluationItem
 * 对应后端：CreateSupplierEvaluationItemAsync
 */
export function createSupplierEvaluationItem(data: SupplierEvaluationItemCreate): Promise<SupplierEvaluationItem> {
  return request({
    url: supplierEvaluationItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新SupplierEvaluationItem
 * 对应后端：UpdateSupplierEvaluationItemAsync
 */
export function updateSupplierEvaluationItem(id: string, data: SupplierEvaluationItemUpdate): Promise<SupplierEvaluationItem> {
  return request({
    url: `${supplierEvaluationItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除SupplierEvaluationItem（单条）
 * 对应后端：DeleteSupplierEvaluationItemByIdAsync
 */
export function deleteSupplierEvaluationItemById(id: string): Promise<void> {
  return request({
    url: `${supplierEvaluationItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除SupplierEvaluationItem
 * 对应后端：DeleteSupplierEvaluationItemBatchAsync
 */
export function deleteSupplierEvaluationItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${supplierEvaluationItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新SupplierEvaluationItem排序
 * 对应后端：UpdateSupplierEvaluationItemSortAsync
 */
export function updateSupplierEvaluationItemSort(data: SupplierEvaluationItemSort): Promise<SupplierEvaluationItemSort> {
  return request({
    url: `${supplierEvaluationItemUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetSupplierEvaluationItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getSupplierEvaluationItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${supplierEvaluationItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入SupplierEvaluationItem
 * 对应后端：ImportSupplierEvaluationItemAsync
 */
export function importSupplierEvaluationItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${supplierEvaluationItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出SupplierEvaluationItem
 * 对应后端：ExportSupplierEvaluationItemAsync；fileName 仅传名称不含后缀
 */
export function exportSupplierEvaluationItemData(query: SupplierEvaluationItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${supplierEvaluationItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
