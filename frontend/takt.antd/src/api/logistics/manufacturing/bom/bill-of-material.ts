// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/bom
// 文件名称：bill-of-material.ts
// 功能描述：BillOfMaterial API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Bom.TaktBillOfMaterials
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  BillOfMaterial,
  BillOfMaterialQuery,
  BillOfMaterialCreate,
  BillOfMaterialUpdate,
  BillOfMaterialSort
} from '@/types/logistics/manufacturing/bom/bill-of-material'

// ========================================
// BillOfMaterial相关 API（按后端控制器顺序）
// ========================================
const billOfMaterialUrl = '/api/TaktBillOfMaterials';

/**
 * 获取BillOfMaterial列表（分页）
 * 对应后端：GetBillOfMaterialListAsync
 */
export function getBillOfMaterialList(params: BillOfMaterialQuery): Promise<TaktPagedResult<BillOfMaterial>> {
  return request({
    url: `${billOfMaterialUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取BillOfMaterial
 * 对应后端：GetBillOfMaterialByIdAsync
 */
export function getBillOfMaterialById(id: string): Promise<BillOfMaterial> {
  return request({
    url: `${billOfMaterialUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取BillOfMaterial选项列表（用于下拉框等）
 * 对应后端：GetBillOfMaterialOptionsAsync
 */
export function getBillOfMaterialOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${billOfMaterialUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建BillOfMaterial
 * 对应后端：CreateBillOfMaterialAsync
 */
export function createBillOfMaterial(data: BillOfMaterialCreate): Promise<BillOfMaterial> {
  return request({
    url: billOfMaterialUrl,
    method: 'post',
    data
  })
}

/**
 * 更新BillOfMaterial
 * 对应后端：UpdateBillOfMaterialAsync
 */
export function updateBillOfMaterial(id: string, data: BillOfMaterialUpdate): Promise<BillOfMaterial> {
  return request({
    url: `${billOfMaterialUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除BillOfMaterial（单条）
 * 对应后端：DeleteBillOfMaterialByIdAsync
 */
export function deleteBillOfMaterialById(id: string): Promise<void> {
  return request({
    url: `${billOfMaterialUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除BillOfMaterial
 * 对应后端：DeleteBillOfMaterialBatchAsync
 */
export function deleteBillOfMaterialBatch(ids: string[]): Promise<void> {
  return request({
    url: `${billOfMaterialUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新BillOfMaterial排序
 * 对应后端：UpdateBillOfMaterialSortAsync
 */
export function updateBillOfMaterialSort(data: BillOfMaterialSort): Promise<BillOfMaterialSort> {
  return request({
    url: `${billOfMaterialUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetBillOfMaterialTemplateAsync；fileName 仅传名称不含后缀
 */
export function getBillOfMaterialTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${billOfMaterialUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入BillOfMaterial
 * 对应后端：ImportBillOfMaterialAsync
 */
export function importBillOfMaterialData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${billOfMaterialUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出BillOfMaterial
 * 对应后端：ExportBillOfMaterialAsync；fileName 仅传名称不含后缀
 */
export function exportBillOfMaterialData(query: BillOfMaterialQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${billOfMaterialUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
