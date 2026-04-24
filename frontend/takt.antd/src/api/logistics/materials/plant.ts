// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/materials/plant
// 文件名称：plant.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：工厂表相关 API，对应后端 TaktPlantController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'

import type {

  Plant,

  PlantQuery,

  PlantCreate,

  PlantUpdate
} from '@/types/logistics/materials/plant'

// ========================================
// 工厂表相关 API（按 table.controller_actions 顺序，与 TaktGenTemplateContext.BuildControllerActions 一致；含 list、{id}、options、增删改、批量删除 POST /delete、导入导出等）
// ========================================

const plantUrl = '/api/TaktPlant'

/**
 * 获取工厂表列表（分页）
 * 对应后端：GetListAsync
 */
export function getPlantList(params: PlantQuery): Promise<TaktPagedResult<Plant>> {

  return request({

    url: `$plantUrl/list`,

    method: 'get',
    params
  })
}
/**
 * 根据ID获取工厂表
 * 对应后端：GetByIdAsync
 */
export function getPlantById(id: string): Promise<Plant> {

  return request({

    url: `$plantUrl/${id}`,

    method: 'get'
  })
}
/**
 * 获取工厂表下拉选项
 * 对应后端：GetPlantOptionsAsync — GET /options（返回 TaktSelectOption[]，与 getUserOptions 约定一致）
 */
export function getPlantOptions(): Promise<TaktSelectOption[]> {

  return request({

    url: `$plantUrl/options`,

    method: 'get'
  })
}
/**
 * 创建工厂表
 * 对应后端：CreateAsync
 */
export function createPlant(data: PlantCreate): Promise<Plant> {

  return request({

    url: plantUrl,

    method: 'post',
    data
  })
}
/**
 * 更新工厂表
 * 对应后端：UpdateAsync
 */
export function updatePlant(id: string, data: PlantUpdate): Promise<Plant> {

  return request({

    url: `$plantUrl/${id}`,

    method: 'put',
    data
  })
}
/**
 * 删除工厂表
 * 对应后端：DeleteAsync
 */
export function deletePlantById(id: string): Promise<void> {

  return request({

    url: `$plantUrl/${id}`,

    method: 'delete'
  })
}
/**
 * 批量删除工厂表
 * 对应后端：DeleteBatchAsync
 */
export function deletePlantBatch(ids: string[]): Promise<void> {

  return request({

    url: `$plantUrl/delete`,

    method: 'post',
    data: ids
  })
}
/**
 * 获取导入模板
 * 对应后端：GetTemplateAsync
 */
export function getPlantTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {

  return request({

    url: `$plantUrl/template`,

    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}
/**
 * 导入工厂表
 * 对应后端：ImportAsync
 */
export function importPlantData(file: File, sheetName?: string): Promise<{ success: number, fail: number, errors: string[] }> {

  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }

  return request({

    url: `$plantUrl/import`,

    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}
/**
 * 导出工厂表
 * 对应后端：ExportAsync
 */
export function exportPlantData(query: PlantQuery, sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {

  return request({

    url: `$plantUrl/export`,

    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}
