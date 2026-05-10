// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/output
// 文件名称：equipment-operation-rate.ts
// 功能描述：EquipmentOperationRate API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Output.TaktEquipmentOperationRates
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  EquipmentOperationRate,
  EquipmentOperationRateQuery,
  EquipmentOperationRateCreate,
  EquipmentOperationRateUpdate,
  EquipmentOperationRateStatus
} from '@/types/logistics/manufacturing/output/equipment-operation-rate'

// ========================================
// EquipmentOperationRate相关 API（按后端控制器顺序）
// ========================================
const equipmentOperationRateUrl = '/api/TaktEquipmentOperationRates';

/**
 * 获取EquipmentOperationRate列表（分页）
 * 对应后端：GetEquipmentOperationRateListAsync
 */
export function getEquipmentOperationRateList(params: EquipmentOperationRateQuery): Promise<TaktPagedResult<EquipmentOperationRate>> {
  return request({
    url: `${equipmentOperationRateUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取EquipmentOperationRate
 * 对应后端：GetEquipmentOperationRateByIdAsync
 */
export function getEquipmentOperationRateById(id: string): Promise<EquipmentOperationRate> {
  return request({
    url: `${equipmentOperationRateUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取EquipmentOperationRate选项列表（用于下拉框等）
 * 对应后端：GetEquipmentOperationRateOptionsAsync
 */
export function getEquipmentOperationRateOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${equipmentOperationRateUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建EquipmentOperationRate
 * 对应后端：CreateEquipmentOperationRateAsync
 */
export function createEquipmentOperationRate(data: EquipmentOperationRateCreate): Promise<EquipmentOperationRate> {
  return request({
    url: equipmentOperationRateUrl,
    method: 'post',
    data
  })
}

/**
 * 更新EquipmentOperationRate
 * 对应后端：UpdateEquipmentOperationRateAsync
 */
export function updateEquipmentOperationRate(id: string, data: EquipmentOperationRateUpdate): Promise<EquipmentOperationRate> {
  return request({
    url: `${equipmentOperationRateUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除EquipmentOperationRate（单条）
 * 对应后端：DeleteEquipmentOperationRateByIdAsync
 */
export function deleteEquipmentOperationRateById(id: string): Promise<void> {
  return request({
    url: `${equipmentOperationRateUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除EquipmentOperationRate
 * 对应后端：DeleteEquipmentOperationRateBatchAsync
 */
export function deleteEquipmentOperationRateBatch(ids: string[]): Promise<void> {
  return request({
    url: `${equipmentOperationRateUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新EquipmentOperationRate状态
 * 对应后端：UpdateEquipmentOperationRateStatusAsync
 */
export function updateEquipmentOperationRateStatus(data: EquipmentOperationRateStatus): Promise<EquipmentOperationRateStatus> {
  return request({
    url: `${equipmentOperationRateUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEquipmentOperationRateTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEquipmentOperationRateTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${equipmentOperationRateUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入EquipmentOperationRate
 * 对应后端：ImportEquipmentOperationRateAsync
 */
export function importEquipmentOperationRateData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${equipmentOperationRateUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出EquipmentOperationRate
 * 对应后端：ExportEquipmentOperationRateAsync；fileName 仅传名称不含后缀
 */
export function exportEquipmentOperationRateData(query: EquipmentOperationRateQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${equipmentOperationRateUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
