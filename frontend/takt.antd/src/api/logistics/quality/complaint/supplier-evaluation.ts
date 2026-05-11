// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/quality/complaint
// 文件名称：supplier-evaluation.ts
// 功能描述：SupplierEvaluation API，对应后端 Takt.WebApi.Controllers.Logistics.Quality.Complaint.TaktSupplierEvaluations
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  SupplierEvaluation,
  SupplierEvaluationQuery,
  SupplierEvaluationCreate,
  SupplierEvaluationUpdate,
  SupplierEvaluationSort
} from '@/types/logistics/quality/complaint/supplier-evaluation'

// ========================================
// SupplierEvaluation相关 API（按后端控制器顺序）
// ========================================
const supplierEvaluationUrl = '/api/TaktSupplierEvaluations';

/**
 * 获取SupplierEvaluation列表（分页）
 * 对应后端：GetSupplierEvaluationListAsync
 */
export function getSupplierEvaluationList(params: SupplierEvaluationQuery): Promise<TaktPagedResult<SupplierEvaluation>> {
  return request({
    url: `${supplierEvaluationUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取SupplierEvaluation
 * 对应后端：GetSupplierEvaluationByIdAsync
 */
export function getSupplierEvaluationById(id: string): Promise<SupplierEvaluation> {
  return request({
    url: `${supplierEvaluationUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取SupplierEvaluation选项列表（用于下拉框等）
 * 对应后端：GetSupplierEvaluationOptionsAsync
 */
export function getSupplierEvaluationOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${supplierEvaluationUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建SupplierEvaluation
 * 对应后端：CreateSupplierEvaluationAsync
 */
export function createSupplierEvaluation(data: SupplierEvaluationCreate): Promise<SupplierEvaluation> {
  return request({
    url: supplierEvaluationUrl,
    method: 'post',
    data
  })
}

/**
 * 更新SupplierEvaluation
 * 对应后端：UpdateSupplierEvaluationAsync
 */
export function updateSupplierEvaluation(id: string, data: SupplierEvaluationUpdate): Promise<SupplierEvaluation> {
  return request({
    url: `${supplierEvaluationUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除SupplierEvaluation（单条）
 * 对应后端：DeleteSupplierEvaluationByIdAsync
 */
export function deleteSupplierEvaluationById(id: string): Promise<void> {
  return request({
    url: `${supplierEvaluationUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除SupplierEvaluation
 * 对应后端：DeleteSupplierEvaluationBatchAsync
 */
export function deleteSupplierEvaluationBatch(ids: string[]): Promise<void> {
  return request({
    url: `${supplierEvaluationUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新SupplierEvaluation排序
 * 对应后端：UpdateSupplierEvaluationSortAsync
 */
export function updateSupplierEvaluationSort(data: SupplierEvaluationSort): Promise<SupplierEvaluationSort> {
  return request({
    url: `${supplierEvaluationUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetSupplierEvaluationTemplateAsync；fileName 仅传名称不含后缀
 */
export function getSupplierEvaluationTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${supplierEvaluationUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入SupplierEvaluation
 * 对应后端：ImportSupplierEvaluationAsync
 */
export function importSupplierEvaluationData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${supplierEvaluationUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出SupplierEvaluation
 * 对应后端：ExportSupplierEvaluationAsync；fileName 仅传名称不含后缀
 */
export function exportSupplierEvaluationData(query: SupplierEvaluationQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${supplierEvaluationUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
