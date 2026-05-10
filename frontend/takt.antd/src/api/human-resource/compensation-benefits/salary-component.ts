// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/compensation-benefits
// 文件名称：salary-component.ts
// 功能描述：SalaryComponent API，对应后端 Takt.WebApi.Controllers.HumanResource.CompensationBenefits.TaktSalaryComponents
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  SalaryComponent,
  SalaryComponentQuery,
  SalaryComponentCreate,
  SalaryComponentUpdate,
  SalaryComponentStatus,
  SalaryComponentSort
} from '@/types/human-resource/compensation-benefits/salary-component'

// ========================================
// SalaryComponent相关 API（按后端控制器顺序）
// ========================================
const salaryComponentUrl = '/api/TaktSalaryComponents';

/**
 * 获取SalaryComponent列表（分页）
 * 对应后端：GetSalaryComponentListAsync
 */
export function getSalaryComponentList(params: SalaryComponentQuery): Promise<TaktPagedResult<SalaryComponent>> {
  return request({
    url: `${salaryComponentUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取SalaryComponent
 * 对应后端：GetSalaryComponentByIdAsync
 */
export function getSalaryComponentById(id: string): Promise<SalaryComponent> {
  return request({
    url: `${salaryComponentUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取SalaryComponent选项列表（用于下拉框等）
 * 对应后端：GetSalaryComponentOptionsAsync
 */
export function getSalaryComponentOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${salaryComponentUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建SalaryComponent
 * 对应后端：CreateSalaryComponentAsync
 */
export function createSalaryComponent(data: SalaryComponentCreate): Promise<SalaryComponent> {
  return request({
    url: salaryComponentUrl,
    method: 'post',
    data
  })
}

/**
 * 更新SalaryComponent
 * 对应后端：UpdateSalaryComponentAsync
 */
export function updateSalaryComponent(id: string, data: SalaryComponentUpdate): Promise<SalaryComponent> {
  return request({
    url: `${salaryComponentUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除SalaryComponent（单条）
 * 对应后端：DeleteSalaryComponentByIdAsync
 */
export function deleteSalaryComponentById(id: string): Promise<void> {
  return request({
    url: `${salaryComponentUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除SalaryComponent
 * 对应后端：DeleteSalaryComponentBatchAsync
 */
export function deleteSalaryComponentBatch(ids: string[]): Promise<void> {
  return request({
    url: `${salaryComponentUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新SalaryComponent状态
 * 对应后端：UpdateSalaryComponentStatusAsync
 */
export function updateSalaryComponentStatus(data: SalaryComponentStatus): Promise<SalaryComponentStatus> {
  return request({
    url: `${salaryComponentUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新SalaryComponent排序
 * 对应后端：UpdateSalaryComponentSortAsync
 */
export function updateSalaryComponentSort(data: SalaryComponentSort): Promise<SalaryComponentSort> {
  return request({
    url: `${salaryComponentUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetSalaryComponentTemplateAsync；fileName 仅传名称不含后缀
 */
export function getSalaryComponentTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${salaryComponentUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入SalaryComponent
 * 对应后端：ImportSalaryComponentAsync
 */
export function importSalaryComponentData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${salaryComponentUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出SalaryComponent
 * 对应后端：ExportSalaryComponentAsync；fileName 仅传名称不含后缀
 */
export function exportSalaryComponentData(query: SalaryComponentQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${salaryComponentUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
