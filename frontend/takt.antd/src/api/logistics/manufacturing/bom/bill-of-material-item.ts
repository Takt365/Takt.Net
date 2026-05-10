// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/bom
// 文件名称：bill-of-material-item.ts
// 功能描述：BillOfMaterialItem API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Bom.TaktBillOfMaterialItems
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  BillOfMaterialItem,
  BillOfMaterialItemQuery,
  BillOfMaterialItemCreate,
  BillOfMaterialItemUpdate
} from '@/types/logistics/manufacturing/bom/bill-of-material-item'

// ========================================
// BillOfMaterialItem相关 API（按后端控制器顺序）
// ========================================
const billOfMaterialItemUrl = '/api/TaktBillOfMaterialItems';

/**
 * 获取BillOfMaterialItem列表（分页）
 * 对应后端：GetBillOfMaterialItemListAsync
 */
export function getBillOfMaterialItemList(params: BillOfMaterialItemQuery): Promise<TaktPagedResult<BillOfMaterialItem>> {
  return request({
    url: `${billOfMaterialItemUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取BillOfMaterialItem
 * 对应后端：GetBillOfMaterialItemByIdAsync
 */
export function getBillOfMaterialItemById(id: string): Promise<BillOfMaterialItem> {
  return request({
    url: `${billOfMaterialItemUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取BillOfMaterialItem选项列表（用于下拉框等）
 * 对应后端：GetBillOfMaterialItemOptionsAsync
 */
export function getBillOfMaterialItemOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${billOfMaterialItemUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建BillOfMaterialItem
 * 对应后端：CreateBillOfMaterialItemAsync
 */
export function createBillOfMaterialItem(data: BillOfMaterialItemCreate): Promise<BillOfMaterialItem> {
  return request({
    url: billOfMaterialItemUrl,
    method: 'post',
    data
  })
}

/**
 * 更新BillOfMaterialItem
 * 对应后端：UpdateBillOfMaterialItemAsync
 */
export function updateBillOfMaterialItem(id: string, data: BillOfMaterialItemUpdate): Promise<BillOfMaterialItem> {
  return request({
    url: `${billOfMaterialItemUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除BillOfMaterialItem（单条）
 * 对应后端：DeleteBillOfMaterialItemByIdAsync
 */
export function deleteBillOfMaterialItemById(id: string): Promise<void> {
  return request({
    url: `${billOfMaterialItemUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除BillOfMaterialItem
 * 对应后端：DeleteBillOfMaterialItemBatchAsync
 */
export function deleteBillOfMaterialItemBatch(ids: string[]): Promise<void> {
  return request({
    url: `${billOfMaterialItemUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetBillOfMaterialItemTemplateAsync；fileName 仅传名称不含后缀
 */
export function getBillOfMaterialItemTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${billOfMaterialItemUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入BillOfMaterialItem
 * 对应后端：ImportBillOfMaterialItemAsync
 */
export function importBillOfMaterialItemData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${billOfMaterialItemUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出BillOfMaterialItem
 * 对应后端：ExportBillOfMaterialItemAsync；fileName 仅传名称不含后缀
 */
export function exportBillOfMaterialItemData(query: BillOfMaterialItemQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${billOfMaterialItemUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
