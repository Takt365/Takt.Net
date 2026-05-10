// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/materials
// 文件名称：plant.ts
// 功能描述：Plant API，对应后端 Takt.WebApi.Controllers.Logistics.Materials.TaktPlants
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Plant,
  PlantQuery,
  PlantCreate,
  PlantUpdate,
  PlantStatus,
  PlantSort
} from '@/types/logistics/materials/plant'

// ========================================
// Plant相关 API（按后端控制器顺序）
// ========================================
const plantUrl = '/api/TaktPlants';

/**
 * 获取Plant列表（分页）
 * 对应后端：GetPlantListAsync
 */
export function getPlantList(params: PlantQuery): Promise<TaktPagedResult<Plant>> {
  return request({
    url: `${plantUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Plant
 * 对应后端：GetPlantByIdAsync
 */
export function getPlantById(id: string): Promise<Plant> {
  return request({
    url: `${plantUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Plant选项列表（用于下拉框等）
 * 对应后端：GetPlantOptionsAsync
 */
export function getPlantOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${plantUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Plant
 * 对应后端：CreatePlantAsync
 */
export function createPlant(data: PlantCreate): Promise<Plant> {
  return request({
    url: plantUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Plant
 * 对应后端：UpdatePlantAsync
 */
export function updatePlant(id: string, data: PlantUpdate): Promise<Plant> {
  return request({
    url: `${plantUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Plant（单条）
 * 对应后端：DeletePlantByIdAsync
 */
export function deletePlantById(id: string): Promise<void> {
  return request({
    url: `${plantUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Plant
 * 对应后端：DeletePlantBatchAsync
 */
export function deletePlantBatch(ids: string[]): Promise<void> {
  return request({
    url: `${plantUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Plant状态
 * 对应后端：UpdatePlantStatusAsync
 */
export function updatePlantStatus(data: PlantStatus): Promise<PlantStatus> {
  return request({
    url: `${plantUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新Plant排序
 * 对应后端：UpdatePlantSortAsync
 */
export function updatePlantSort(data: PlantSort): Promise<PlantSort> {
  return request({
    url: `${plantUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPlantTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPlantTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${plantUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Plant
 * 对应后端：ImportPlantAsync
 */
export function importPlantData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${plantUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Plant
 * 对应后端：ExportPlantAsync；fileName 仅传名称不含后缀
 */
export function exportPlantData(query: PlantQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${plantUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
