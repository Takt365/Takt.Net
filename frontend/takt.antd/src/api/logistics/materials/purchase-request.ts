// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/materials
// 文件名称：purchase-request.ts
// 功能描述：PurchaseRequest API，对应后端 Takt.WebApi.Controllers.Logistics.Materials.TaktPurchaseRequests
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PurchaseRequest,
  PurchaseRequestQuery,
  PurchaseRequestCreate,
  PurchaseRequestUpdate
} from '@/types/logistics/materials/purchase-request'

// ========================================
// PurchaseRequest相关 API（按后端控制器顺序）
// ========================================
const purchaseRequestUrl = '/api/TaktPurchaseRequests';

/**
 * 获取PurchaseRequest列表（分页）
 * 对应后端：GetPurchaseRequestListAsync
 */
export function getPurchaseRequestList(params: PurchaseRequestQuery): Promise<TaktPagedResult<PurchaseRequest>> {
  return request({
    url: `${purchaseRequestUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PurchaseRequest
 * 对应后端：GetPurchaseRequestByIdAsync
 */
export function getPurchaseRequestById(id: string): Promise<PurchaseRequest> {
  return request({
    url: `${purchaseRequestUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PurchaseRequest选项列表（用于下拉框等）
 * 对应后端：GetPurchaseRequestOptionsAsync
 */
export function getPurchaseRequestOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${purchaseRequestUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PurchaseRequest
 * 对应后端：CreatePurchaseRequestAsync
 */
export function createPurchaseRequest(data: PurchaseRequestCreate): Promise<PurchaseRequest> {
  return request({
    url: purchaseRequestUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PurchaseRequest
 * 对应后端：UpdatePurchaseRequestAsync
 */
export function updatePurchaseRequest(id: string, data: PurchaseRequestUpdate): Promise<PurchaseRequest> {
  return request({
    url: `${purchaseRequestUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PurchaseRequest（单条）
 * 对应后端：DeletePurchaseRequestByIdAsync
 */
export function deletePurchaseRequestById(id: string): Promise<void> {
  return request({
    url: `${purchaseRequestUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PurchaseRequest
 * 对应后端：DeletePurchaseRequestBatchAsync
 */
export function deletePurchaseRequestBatch(ids: string[]): Promise<void> {
  return request({
    url: `${purchaseRequestUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPurchaseRequestTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPurchaseRequestTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${purchaseRequestUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PurchaseRequest
 * 对应后端：ImportPurchaseRequestAsync
 */
export function importPurchaseRequestData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${purchaseRequestUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PurchaseRequest
 * 对应后端：ExportPurchaseRequestAsync；fileName 仅传名称不含后缀
 */
export function exportPurchaseRequestData(query: PurchaseRequestQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${purchaseRequestUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
