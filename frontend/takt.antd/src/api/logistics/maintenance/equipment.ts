// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/maintenance
// 文件名称：equipment.ts
// 功能描述：Equipment API，对应后端 Takt.WebApi.Controllers.Logistics.Maintenance.TaktEquipments
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Equipment,
  EquipmentQuery,
  EquipmentCreate,
  EquipmentUpdate,
  EquipmentStatus
} from '@/types/logistics/maintenance/equipment'

// ========================================
// Equipment相关 API（按后端控制器顺序）
// ========================================
const equipmentUrl = '/api/TaktEquipments';

/**
 * 获取Equipment列表（分页）
 * 对应后端：GetEquipmentListAsync
 */
export function getEquipmentList(params: EquipmentQuery): Promise<TaktPagedResult<Equipment>> {
  return request({
    url: `${equipmentUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Equipment
 * 对应后端：GetEquipmentByIdAsync
 */
export function getEquipmentById(id: string): Promise<Equipment> {
  return request({
    url: `${equipmentUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Equipment选项列表（用于下拉框等）
 * 对应后端：GetEquipmentOptionsAsync
 */
export function getEquipmentOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${equipmentUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Equipment
 * 对应后端：CreateEquipmentAsync
 */
export function createEquipment(data: EquipmentCreate): Promise<Equipment> {
  return request({
    url: equipmentUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Equipment
 * 对应后端：UpdateEquipmentAsync
 */
export function updateEquipment(id: string, data: EquipmentUpdate): Promise<Equipment> {
  return request({
    url: `${equipmentUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Equipment（单条）
 * 对应后端：DeleteEquipmentByIdAsync
 */
export function deleteEquipmentById(id: string): Promise<void> {
  return request({
    url: `${equipmentUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Equipment
 * 对应后端：DeleteEquipmentBatchAsync
 */
export function deleteEquipmentBatch(ids: string[]): Promise<void> {
  return request({
    url: `${equipmentUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Equipment状态
 * 对应后端：UpdateEquipmentStatusAsync
 */
export function updateEquipmentStatus(data: EquipmentStatus): Promise<EquipmentStatus> {
  return request({
    url: `${equipmentUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetEquipmentTemplateAsync；fileName 仅传名称不含后缀
 */
export function getEquipmentTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${equipmentUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Equipment
 * 对应后端：ImportEquipmentAsync
 */
export function importEquipmentData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${equipmentUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Equipment
 * 对应后端：ExportEquipmentAsync；fileName 仅传名称不含后缀
 */
export function exportEquipmentData(query: EquipmentQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${equipmentUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
