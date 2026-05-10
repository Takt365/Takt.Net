// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/maintenance
// 文件名称：maintenance.ts
// 功能描述：Maintenance API，对应后端 Takt.WebApi.Controllers.Logistics.Maintenance.TaktMaintenances
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Maintenance,
  MaintenanceQuery,
  MaintenanceCreate,
  MaintenanceUpdate,
  MaintenanceStatus
} from '@/types/logistics/maintenance/maintenance'

// ========================================
// Maintenance相关 API（按后端控制器顺序）
// ========================================
const maintenanceUrl = '/api/TaktMaintenances';

/**
 * 获取Maintenance列表（分页）
 * 对应后端：GetMaintenanceListAsync
 */
export function getMaintenanceList(params: MaintenanceQuery): Promise<TaktPagedResult<Maintenance>> {
  return request({
    url: `${maintenanceUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Maintenance
 * 对应后端：GetMaintenanceByIdAsync
 */
export function getMaintenanceById(id: string): Promise<Maintenance> {
  return request({
    url: `${maintenanceUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Maintenance选项列表（用于下拉框等）
 * 对应后端：GetMaintenanceOptionsAsync
 */
export function getMaintenanceOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${maintenanceUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Maintenance
 * 对应后端：CreateMaintenanceAsync
 */
export function createMaintenance(data: MaintenanceCreate): Promise<Maintenance> {
  return request({
    url: maintenanceUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Maintenance
 * 对应后端：UpdateMaintenanceAsync
 */
export function updateMaintenance(id: string, data: MaintenanceUpdate): Promise<Maintenance> {
  return request({
    url: `${maintenanceUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Maintenance（单条）
 * 对应后端：DeleteMaintenanceByIdAsync
 */
export function deleteMaintenanceById(id: string): Promise<void> {
  return request({
    url: `${maintenanceUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Maintenance
 * 对应后端：DeleteMaintenanceBatchAsync
 */
export function deleteMaintenanceBatch(ids: string[]): Promise<void> {
  return request({
    url: `${maintenanceUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Maintenance状态
 * 对应后端：UpdateMaintenanceStatusAsync
 */
export function updateMaintenanceStatus(data: MaintenanceStatus): Promise<MaintenanceStatus> {
  return request({
    url: `${maintenanceUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetMaintenanceTemplateAsync；fileName 仅传名称不含后缀
 */
export function getMaintenanceTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${maintenanceUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Maintenance
 * 对应后端：ImportMaintenanceAsync
 */
export function importMaintenanceData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${maintenanceUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Maintenance
 * 对应后端：ExportMaintenanceAsync；fileName 仅传名称不含后缀
 */
export function exportMaintenanceData(query: MaintenanceQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${maintenanceUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
