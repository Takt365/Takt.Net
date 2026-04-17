// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/controlling/cost-center
// 文件名称：cost-center.ts
// 功能描述：成本中心 API，对应后端 Controlling.TaktCostCentersController
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  CostCenter,
  CostCenterCreate,
  CostCenterQuery,
  CostCenterStatus,
  CostCenterUpdate
} from '@/types/accounting/controlling/cost-center'

const baseUrl = '/api/TaktCostCenters'

export function getCostCenterList(params: CostCenterQuery): Promise<TaktPagedResult<CostCenter>> {
  return request({
    url: `${baseUrl}/list`,
    method: 'get',
    params
  })
}

export function getCostCenterById(id: string): Promise<CostCenter> {
  return request({
    url: `${baseUrl}/${id}`,
    method: 'get'
  })
}

export function getCostCenterOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${baseUrl}/options`,
    method: 'get'
  })
}

export function createCostCenter(data: CostCenterCreate): Promise<CostCenter> {
  return request({
    url: baseUrl,
    method: 'post',
    data
  })
}

export function updateCostCenter(id: string, data: CostCenterUpdate): Promise<CostCenter> {
  return request({
    url: `${baseUrl}/${id}`,
    method: 'put',
    data
  })
}

export function deleteCostCenterById(id: string): Promise<void> {
  return request({
    url: `${baseUrl}/${id}`,
    method: 'delete'
  })
}

export function updateCostCenterStatus(data: CostCenterStatus): Promise<CostCenter> {
  return request({
    url: `${baseUrl}/status`,
    method: 'put',
    data
  })
}

export function getCostCenterTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${baseUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

export function importCostCenterData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${baseUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

export function exportCostCenterData(query: CostCenterQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${baseUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
