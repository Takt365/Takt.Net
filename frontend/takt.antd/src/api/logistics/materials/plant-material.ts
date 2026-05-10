// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/materials
// 文件名称：plant-material.ts
// 功能描述：PlantMaterial API，对应后端 Takt.WebApi.Controllers.Logistics.Materials.TaktPlantMaterials
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  PlantMaterial,
  PlantMaterialQuery,
  PlantMaterialCreate,
  PlantMaterialUpdate
} from '@/types/logistics/materials/plant-material'

// ========================================
// PlantMaterial相关 API（按后端控制器顺序）
// ========================================
const plantMaterialUrl = '/api/TaktPlantMaterials';

/**
 * 获取PlantMaterial列表（分页）
 * 对应后端：GetPlantMaterialListAsync
 */
export function getPlantMaterialList(params: PlantMaterialQuery): Promise<TaktPagedResult<PlantMaterial>> {
  return request({
    url: `${plantMaterialUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取PlantMaterial
 * 对应后端：GetPlantMaterialByIdAsync
 */
export function getPlantMaterialById(id: string): Promise<PlantMaterial> {
  return request({
    url: `${plantMaterialUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取PlantMaterial选项列表（用于下拉框等）
 * 对应后端：GetPlantMaterialOptionsAsync
 */
export function getPlantMaterialOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${plantMaterialUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建PlantMaterial
 * 对应后端：CreatePlantMaterialAsync
 */
export function createPlantMaterial(data: PlantMaterialCreate): Promise<PlantMaterial> {
  return request({
    url: plantMaterialUrl,
    method: 'post',
    data
  })
}

/**
 * 更新PlantMaterial
 * 对应后端：UpdatePlantMaterialAsync
 */
export function updatePlantMaterial(id: string, data: PlantMaterialUpdate): Promise<PlantMaterial> {
  return request({
    url: `${plantMaterialUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除PlantMaterial（单条）
 * 对应后端：DeletePlantMaterialByIdAsync
 */
export function deletePlantMaterialById(id: string): Promise<void> {
  return request({
    url: `${plantMaterialUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除PlantMaterial
 * 对应后端：DeletePlantMaterialBatchAsync
 */
export function deletePlantMaterialBatch(ids: string[]): Promise<void> {
  return request({
    url: `${plantMaterialUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPlantMaterialTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPlantMaterialTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${plantMaterialUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入PlantMaterial
 * 对应后端：ImportPlantMaterialAsync
 */
export function importPlantMaterialData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${plantMaterialUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出PlantMaterial
 * 对应后端：ExportPlantMaterialAsync；fileName 仅传名称不含后缀
 */
export function exportPlantMaterialData(query: PlantMaterialQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${plantMaterialUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
