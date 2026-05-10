// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/compensation-benefits
// 文件名称：employee-benefit.ts
// 功能描述：EmployeeBenefit API，对应后端 Takt.WebApi.Controllers.HumanResource.CompensationBenefits.TaktEmployeeBenefits
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  EmployeeBenefit,
  EmployeeBenefitQuery,
  EmployeeBenefitCreate,
  EmployeeBenefitUpdate,
  EmployeeBenefitStatus
} from '@/types/human-resource/compensation-benefits/employee-benefit'

// ========================================
// EmployeeBenefit相关 API（按后端控制器顺序）
// ========================================
const employeeBenefitUrl = '/api/TaktEmployeeBenefits';

/**
 * 获取EmployeeBenefit列表（分页）
 * 对应后端：GetEmployeeBenefitListAsync
 */
export function getEmployeeBenefitList(params: EmployeeBenefitQuery): Promise<TaktPagedResult<EmployeeBenefit>> {
  return request({
    url: `${employeeBenefitUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取EmployeeBenefit
 * 对应后端：GetEmployeeBenefitByIdAsync
 */
export function getEmployeeBenefitById(id: string): Promise<EmployeeBenefit> {
  return request({
    url: `${employeeBenefitUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取EmployeeBenefit选项列表（用于下拉框等）
 * 对应后端：GetEmployeeBenefitOptionsAsync
 */
export function getEmployeeBenefitOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${employeeBenefitUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建EmployeeBenefit
 * 对应后端：CreateEmployeeBenefitAsync
 */
export function createEmployeeBenefit(data: EmployeeBenefitCreate): Promise<EmployeeBenefit> {
  return request({
    url: employeeBenefitUrl,
    method: 'post',
    data
  })
}

/**
 * 更新EmployeeBenefit
 * 对应后端：UpdateEmployeeBenefitAsync
 */
export function updateEmployeeBenefit(id: string, data: EmployeeBenefitUpdate): Promise<EmployeeBenefit> {
  return request({
    url: `${employeeBenefitUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除EmployeeBenefit（单条）
 * 对应后端：DeleteEmployeeBenefitByIdAsync
 */
export function deleteEmployeeBenefitById(id: string): Promise<void> {
  return request({
    url: `${employeeBenefitUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除EmployeeBenefit
 * 对应后端：DeleteEmployeeBenefitBatchAsync
 */
export function deleteEmployeeBenefitBatch(ids: string[]): Promise<void> {
  return request({
    url: `${employeeBenefitUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新EmployeeBenefit状态
 * 对应后端：UpdateEmployeeBenefitStatusAsync
 */
export function updateEmployeeBenefitStatus(data: EmployeeBenefitStatus): Promise<EmployeeBenefitStatus> {
  return request({
    url: `${employeeBenefitUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEmployeeBenefitTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEmployeeBenefitTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${employeeBenefitUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入EmployeeBenefit
 * 对应后端：ImportEmployeeBenefitAsync
 */
export function importEmployeeBenefitData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${employeeBenefitUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出EmployeeBenefit
 * 对应后端：ExportEmployeeBenefitAsync；fileName 仅传名称不含后缀
 */
export function exportEmployeeBenefitData(query: EmployeeBenefitQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${employeeBenefitUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
