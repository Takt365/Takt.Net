// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/materials/plant
// 文件名称：plant.ts
// 创建时间：2025-02-13
// 创建人：Takt365(Cursor AI)
// 功能描述：工厂相关 API，对应后端 TaktPlantsController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../../request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Plant,
  PlantQuery,
  PlantCreate,
  PlantUpdate,
  PlantStatus
} from '@/types/logistics/materials/plant'

/**
 * 获取工厂列表（分页）
 * 对应后端：GetListAsync
 */
export function getList(params: PlantQuery): Promise<TaktPagedResult<Plant>> {
  return request({
    url: '/api/TaktPlants/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取工厂
 * 对应后端：GetByIdAsync
 */
export function getById(id: string): Promise<Plant> {
  return request({
    url: `/api/TaktPlants/${id}`,
    method: 'get'
  })
}

/**
 * 获取工厂选项列表（用于下拉框等）
 * 对应后端：GetOptionsAsync
 */
export function getOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: '/api/TaktPlants/options',
    method: 'get'
  })
}

/**
 * 创建工厂
 * 对应后端：CreateAsync
 */
export function create(data: PlantCreate): Promise<Plant> {
  return request({
    url: '/api/TaktPlants',
    method: 'post',
    data
  })
}

/**
 * 更新工厂
 * 对应后端：UpdateAsync
 */
export function update(id: string, data: PlantUpdate): Promise<Plant> {
  return request({
    url: `/api/TaktPlants/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除工厂
 * 对应后端：DeleteAsync
 */
export function remove(id: string): Promise<void> {
  return request({
    url: `/api/TaktPlants/${id}`,
    method: 'delete'
  })
}

/**
 * 更新工厂状态
 * 对应后端：UpdateStatusAsync
 */
export function updateStatus(data: PlantStatus): Promise<Plant> {
  return request({
    url: '/api/TaktPlants/status',
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTemplateAsync；fileName 仅传名称不含后缀
 */
export function getTemplate(sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktPlants/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 导入工厂
 * 对应后端：ImportAsync
 */
export function importPlants(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: '/api/TaktPlants/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出工厂
 * 对应后端：ExportAsync；fileName 仅传名称不含后缀
 */
export function exportPlants(
  query: PlantQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: '/api/TaktPlants/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
