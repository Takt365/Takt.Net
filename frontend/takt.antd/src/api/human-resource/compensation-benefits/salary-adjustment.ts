// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/compensation-benefits
// 文件名称：salary-adjustment.ts
// 功能描述：SalaryAdjustment API，对应后端 Takt.WebApi.Controllers.HumanResource.CompensationBenefits.TaktSalaryAdjustments
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  SalaryAdjustment,
  SalaryAdjustmentQuery,
  SalaryAdjustmentCreate,
  SalaryAdjustmentUpdate,
  SalaryAdjustmentStatus
} from '@/types/human-resource/compensation-benefits/salary-adjustment'

// ========================================
// SalaryAdjustment相关 API（按后端控制器顺序）
// ========================================
const salaryAdjustmentUrl = '/api/TaktSalaryAdjustments';

/**
 * 获取SalaryAdjustment列表（分页）
 * 对应后端：GetSalaryAdjustmentListAsync
 */
export function getSalaryAdjustmentList(params: SalaryAdjustmentQuery): Promise<TaktPagedResult<SalaryAdjustment>> {
  return request({
    url: `${salaryAdjustmentUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取SalaryAdjustment
 * 对应后端：GetSalaryAdjustmentByIdAsync
 */
export function getSalaryAdjustmentById(id: string): Promise<SalaryAdjustment> {
  return request({
    url: `${salaryAdjustmentUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取SalaryAdjustment选项列表（用于下拉框等）
 * 对应后端：GetSalaryAdjustmentOptionsAsync
 */
export function getSalaryAdjustmentOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${salaryAdjustmentUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建SalaryAdjustment
 * 对应后端：CreateSalaryAdjustmentAsync
 */
export function createSalaryAdjustment(data: SalaryAdjustmentCreate): Promise<SalaryAdjustment> {
  return request({
    url: salaryAdjustmentUrl,
    method: 'post',
    data
  })
}

/**
 * 更新SalaryAdjustment
 * 对应后端：UpdateSalaryAdjustmentAsync
 */
export function updateSalaryAdjustment(id: string, data: SalaryAdjustmentUpdate): Promise<SalaryAdjustment> {
  return request({
    url: `${salaryAdjustmentUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除SalaryAdjustment（单条）
 * 对应后端：DeleteSalaryAdjustmentByIdAsync
 */
export function deleteSalaryAdjustmentById(id: string): Promise<void> {
  return request({
    url: `${salaryAdjustmentUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除SalaryAdjustment
 * 对应后端：DeleteSalaryAdjustmentBatchAsync
 */
export function deleteSalaryAdjustmentBatch(ids: string[]): Promise<void> {
  return request({
    url: `${salaryAdjustmentUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新SalaryAdjustment状态
 * 对应后端：UpdateSalaryAdjustmentStatusAsync
 */
export function updateSalaryAdjustmentStatus(data: SalaryAdjustmentStatus): Promise<SalaryAdjustmentStatus> {
  return request({
    url: `${salaryAdjustmentUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetSalaryAdjustmentTemplateAsync；fileName 仅传名称不含后缀
 */
export function getSalaryAdjustmentTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${salaryAdjustmentUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入SalaryAdjustment
 * 对应后端：ImportSalaryAdjustmentAsync
 */
export function importSalaryAdjustmentData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${salaryAdjustmentUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出SalaryAdjustment
 * 对应后端：ExportSalaryAdjustmentAsync；fileName 仅传名称不含后缀
 */
export function exportSalaryAdjustmentData(query: SalaryAdjustmentQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${salaryAdjustmentUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
