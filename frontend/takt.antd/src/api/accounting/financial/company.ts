// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/financial
// 文件名称：company.ts
// 功能描述：Company API，对应后端 Takt.WebApi.Controllers.Accounting.Financial.TaktCompanys
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Company,
  CompanyQuery,
  CompanyCreate,
  CompanyUpdate,
  CompanyStatus,
  CompanySort
} from '@/types/accounting/financial/company'

// ========================================
// Company相关 API（按后端控制器顺序）
// ========================================
const companyUrl = '/api/TaktCompanys';

/**
 * 获取Company列表（分页）
 * 对应后端：GetCompanyListAsync
 */
export function getCompanyList(params: CompanyQuery): Promise<TaktPagedResult<Company>> {
  return request({
    url: `${companyUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Company
 * 对应后端：GetCompanyByIdAsync
 */
export function getCompanyById(id: string): Promise<Company> {
  return request({
    url: `${companyUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Company选项列表（用于下拉框等）
 * 对应后端：GetCompanyOptionsAsync
 */
export function getCompanyOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${companyUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Company
 * 对应后端：CreateCompanyAsync
 */
export function createCompany(data: CompanyCreate): Promise<Company> {
  return request({
    url: companyUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Company
 * 对应后端：UpdateCompanyAsync
 */
export function updateCompany(id: string, data: CompanyUpdate): Promise<Company> {
  return request({
    url: `${companyUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Company（单条）
 * 对应后端：DeleteCompanyByIdAsync
 */
export function deleteCompanyById(id: string): Promise<void> {
  return request({
    url: `${companyUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Company
 * 对应后端：DeleteCompanyBatchAsync
 */
export function deleteCompanyBatch(ids: string[]): Promise<void> {
  return request({
    url: `${companyUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Company状态
 * 对应后端：UpdateCompanyStatusAsync
 */
export function updateCompanyStatus(data: CompanyStatus): Promise<CompanyStatus> {
  return request({
    url: `${companyUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新Company排序
 * 对应后端：UpdateCompanySortAsync
 */
export function updateCompanySort(data: CompanySort): Promise<CompanySort> {
  return request({
    url: `${companyUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetCompanyTemplateAsync；fileName 仅传名称不含后缀
 */
export function getCompanyTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${companyUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Company
 * 对应后端：ImportCompanyAsync
 */
export function importCompanyData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${companyUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Company
 * 对应后端：ExportCompanyAsync；fileName 仅传名称不含后缀
 */
export function exportCompanyData(query: CompanyQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${companyUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
