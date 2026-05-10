// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/output
// 文件名称：personnel-operation-rate.ts
// 功能描述：PersonnelOperationRate API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Output.TaktPersonnelOperationRates
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PersonnelOperationRate,
  PersonnelOperationRateQuery,
  PersonnelOperationRateCreate,
  PersonnelOperationRateUpdate,
  PersonnelOperationRateStatus
} from '@/types/logistics/manufacturing/output/personnel-operation-rate'

// ========================================
// PersonnelOperationRate相关 API（按后端控制器顺序）
// ========================================
const personnelOperationRateUrl = '/api/TaktPersonnelOperationRates';

/**
 * 获取PersonnelOperationRate列表（分页）
 * 对应后端：GetPersonnelOperationRateListAsync
 */
export function getPersonnelOperationRateList(params: PersonnelOperationRateQuery): Promise<TaktPagedResult<PersonnelOperationRate>> {
  return request({
    url: `${personnelOperationRateUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PersonnelOperationRate
 * 对应后端：GetPersonnelOperationRateByIdAsync
 */
export function getPersonnelOperationRateById(id: string): Promise<PersonnelOperationRate> {
  return request({
    url: `${personnelOperationRateUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PersonnelOperationRate选项列表（用于下拉框等）
 * 对应后端：GetPersonnelOperationRateOptionsAsync
 */
export function getPersonnelOperationRateOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${personnelOperationRateUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PersonnelOperationRate
 * 对应后端：CreatePersonnelOperationRateAsync
 */
export function createPersonnelOperationRate(data: PersonnelOperationRateCreate): Promise<PersonnelOperationRate> {
  return request({
    url: personnelOperationRateUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PersonnelOperationRate
 * 对应后端：UpdatePersonnelOperationRateAsync
 */
export function updatePersonnelOperationRate(id: string, data: PersonnelOperationRateUpdate): Promise<PersonnelOperationRate> {
  return request({
    url: `${personnelOperationRateUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PersonnelOperationRate（单条）
 * 对应后端：DeletePersonnelOperationRateByIdAsync
 */
export function deletePersonnelOperationRateById(id: string): Promise<void> {
  return request({
    url: `${personnelOperationRateUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PersonnelOperationRate
 * 对应后端：DeletePersonnelOperationRateBatchAsync
 */
export function deletePersonnelOperationRateBatch(ids: string[]): Promise<void> {
  return request({
    url: `${personnelOperationRateUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新PersonnelOperationRate状态
 * 对应后端：UpdatePersonnelOperationRateStatusAsync
 */
export function updatePersonnelOperationRateStatus(data: PersonnelOperationRateStatus): Promise<PersonnelOperationRateStatus> {
  return request({
    url: `${personnelOperationRateUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPersonnelOperationRateTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPersonnelOperationRateTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${personnelOperationRateUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PersonnelOperationRate
 * 对应后端：ImportPersonnelOperationRateAsync
 */
export function importPersonnelOperationRateData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${personnelOperationRateUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PersonnelOperationRate
 * 对应后端：ExportPersonnelOperationRateAsync；fileName 仅传名称不含后缀
 */
export function exportPersonnelOperationRateData(query: PersonnelOperationRateQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${personnelOperationRateUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
