// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/financial
// 文件名称：asset.ts
// 功能描述：Asset API，对应后端 Takt.WebApi.Controllers.Accounting.Financial.TaktAssets
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Asset,
  AssetQuery,
  AssetCreate,
  AssetUpdate,
  AssetStatus
} from '@/types/accounting/financial/asset'

// ========================================
// Asset相关 API（按后端控制器顺序）
// ========================================
const assetUrl = '/api/TaktAssets';

/**
 * 获取Asset列表（分页）
 * 对应后端：GetAssetListAsync
 */
export function getAssetList(params: AssetQuery): Promise<TaktPagedResult<Asset>> {
  return request({
    url: `${assetUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Asset
 * 对应后端：GetAssetByIdAsync
 */
export function getAssetById(id: string): Promise<Asset> {
  return request({
    url: `${assetUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Asset选项列表（用于下拉框等）
 * 对应后端：GetAssetOptionsAsync
 */
export function getAssetOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${assetUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Asset
 * 对应后端：CreateAssetAsync
 */
export function createAsset(data: AssetCreate): Promise<Asset> {
  return request({
    url: assetUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Asset
 * 对应后端：UpdateAssetAsync
 */
export function updateAsset(id: string, data: AssetUpdate): Promise<Asset> {
  return request({
    url: `${assetUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Asset（单条）
 * 对应后端：DeleteAssetByIdAsync
 */
export function deleteAssetById(id: string): Promise<void> {
  return request({
    url: `${assetUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Asset
 * 对应后端：DeleteAssetBatchAsync
 */
export function deleteAssetBatch(ids: string[]): Promise<void> {
  return request({
    url: `${assetUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Asset状态
 * 对应后端：UpdateAssetStatusAsync
 */
export function updateAssetStatus(data: AssetStatus): Promise<AssetStatus> {
  return request({
    url: `${assetUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetAssetTemplateAsync；fileName 仅传名称不含后缀
 */
export function getAssetTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${assetUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Asset
 * 对应后端：ImportAssetAsync
 */
export function importAssetData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${assetUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Asset
 * 对应后端：ExportAssetAsync；fileName 仅传名称不含后缀
 */
export function exportAssetData(query: AssetQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${assetUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
