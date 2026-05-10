// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/compensation-benefits
// 文件名称：salary-structure.ts
// 功能描述：SalaryStructure API，对应后端 Takt.WebApi.Controllers.HumanResource.CompensationBenefits.TaktSalaryStructures
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  SalaryStructure,
  SalaryStructureQuery,
  SalaryStructureCreate,
  SalaryStructureUpdate,
  SalaryStructureStatus
} from '@/types/human-resource/compensation-benefits/salary-structure'

// ========================================
// SalaryStructure相关 API（按后端控制器顺序）
// ========================================
const salaryStructureUrl = '/api/TaktSalaryStructures';

/**
 * 获取SalaryStructure列表（分页）
 * 对应后端：GetSalaryStructureListAsync
 */
export function getSalaryStructureList(params: SalaryStructureQuery): Promise<TaktPagedResult<SalaryStructure>> {
  return request({
    url: `${salaryStructureUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取SalaryStructure
 * 对应后端：GetSalaryStructureByIdAsync
 */
export function getSalaryStructureById(id: string): Promise<SalaryStructure> {
  return request({
    url: `${salaryStructureUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取SalaryStructure选项列表（用于下拉框等）
 * 对应后端：GetSalaryStructureOptionsAsync
 */
export function getSalaryStructureOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${salaryStructureUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建SalaryStructure
 * 对应后端：CreateSalaryStructureAsync
 */
export function createSalaryStructure(data: SalaryStructureCreate): Promise<SalaryStructure> {
  return request({
    url: salaryStructureUrl,
    method: 'post',
    data
  })
}

/**
 * 更新SalaryStructure
 * 对应后端：UpdateSalaryStructureAsync
 */
export function updateSalaryStructure(id: string, data: SalaryStructureUpdate): Promise<SalaryStructure> {
  return request({
    url: `${salaryStructureUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除SalaryStructure（单条）
 * 对应后端：DeleteSalaryStructureByIdAsync
 */
export function deleteSalaryStructureById(id: string): Promise<void> {
  return request({
    url: `${salaryStructureUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除SalaryStructure
 * 对应后端：DeleteSalaryStructureBatchAsync
 */
export function deleteSalaryStructureBatch(ids: string[]): Promise<void> {
  return request({
    url: `${salaryStructureUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新SalaryStructure状态
 * 对应后端：UpdateSalaryStructureStatusAsync
 */
export function updateSalaryStructureStatus(data: SalaryStructureStatus): Promise<SalaryStructureStatus> {
  return request({
    url: `${salaryStructureUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetSalaryStructureTemplateAsync；fileName 仅传名称不含后缀
 */
export function getSalaryStructureTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${salaryStructureUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入SalaryStructure
 * 对应后端：ImportSalaryStructureAsync
 */
export function importSalaryStructureData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${salaryStructureUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出SalaryStructure
 * 对应后端：ExportSalaryStructureAsync；fileName 仅传名称不含后缀
 */
export function exportSalaryStructureData(query: SalaryStructureQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${salaryStructureUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
