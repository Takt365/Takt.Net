// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/output
// 文件名称：production-team.ts
// 功能描述：ProductionTeam API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Output.TaktProductionTeams
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  ProductionTeam,
  ProductionTeamQuery,
  ProductionTeamCreate,
  ProductionTeamUpdate,
  ProductionTeamStatus
} from '@/types/logistics/manufacturing/output/production-team'

// ========================================
// ProductionTeam相关 API（按后端控制器顺序）
// ========================================
const productionTeamUrl = '/api/TaktProductionTeams';

/**
 * 获取ProductionTeam列表（分页）
 * 对应后端：GetProductionTeamListAsync
 */
export function getProductionTeamList(params: ProductionTeamQuery): Promise<TaktPagedResult<ProductionTeam>> {
  return request({
    url: `${productionTeamUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取ProductionTeam
 * 对应后端：GetProductionTeamByIdAsync
 */
export function getProductionTeamById(id: string): Promise<ProductionTeam> {
  return request({
    url: `${productionTeamUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取ProductionTeam选项列表（用于下拉框等）
 * 对应后端：GetProductionTeamOptionsAsync
 */
export function getProductionTeamOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${productionTeamUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建ProductionTeam
 * 对应后端：CreateProductionTeamAsync
 */
export function createProductionTeam(data: ProductionTeamCreate): Promise<ProductionTeam> {
  return request({
    url: productionTeamUrl,
    method: 'post',
    data
  })
}

/**
 * 更新ProductionTeam
 * 对应后端：UpdateProductionTeamAsync
 */
export function updateProductionTeam(id: string, data: ProductionTeamUpdate): Promise<ProductionTeam> {
  return request({
    url: `${productionTeamUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除ProductionTeam（单条）
 * 对应后端：DeleteProductionTeamByIdAsync
 */
export function deleteProductionTeamById(id: string): Promise<void> {
  return request({
    url: `${productionTeamUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除ProductionTeam
 * 对应后端：DeleteProductionTeamBatchAsync
 */
export function deleteProductionTeamBatch(ids: string[]): Promise<void> {
  return request({
    url: `${productionTeamUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新ProductionTeam状态
 * 对应后端：UpdateProductionTeamStatusAsync
 */
export function updateProductionTeamStatus(data: ProductionTeamStatus): Promise<ProductionTeamStatus> {
  return request({
    url: `${productionTeamUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetProductionTeamTemplateAsync；fileName 仅传名称不含后缀
 */
export function getProductionTeamTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${productionTeamUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入ProductionTeam
 * 对应后端：ImportProductionTeamAsync
 */
export function importProductionTeamData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${productionTeamUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出ProductionTeam
 * 对应后端：ExportProductionTeamAsync；fileName 仅传名称不含后缀
 */
export function exportProductionTeamData(query: ProductionTeamQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${productionTeamUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
