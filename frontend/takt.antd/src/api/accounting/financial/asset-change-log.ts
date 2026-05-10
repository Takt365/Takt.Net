// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/accounting/financial
// 文件名称：asset-change-log.ts
// 功能描述：AssetChangeLog API，对应后端 Takt.WebApi.Controllers.Accounting.Financial.TaktAssetChangeLogs
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  AssetChangeLog,
  AssetChangeLogQuery,
  AssetChangeLogCreate,
  AssetChangeLogUpdate
} from '@/types/accounting/financial/asset-change-log'

// ========================================
// AssetChangeLog相关 API（按后端控制器顺序）
// ========================================
const assetChangeLogUrl = '/api/TaktAssetChangeLogs';

/**
 * 获取AssetChangeLog列表（分页）
 * 对应后端：GetAssetChangeLogListAsync
 */
export function getAssetChangeLogList(params: AssetChangeLogQuery): Promise<TaktPagedResult<AssetChangeLog>> {
  return request({
    url: `${assetChangeLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取AssetChangeLog
 * 对应后端：GetAssetChangeLogByIdAsync
 */
export function getAssetChangeLogById(id: string): Promise<AssetChangeLog> {
  return request({
    url: `${assetChangeLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取AssetChangeLog选项列表（用于下拉框等）
 * 对应后端：GetAssetChangeLogOptionsAsync
 */
export function getAssetChangeLogOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${assetChangeLogUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建AssetChangeLog
 * 对应后端：CreateAssetChangeLogAsync
 */
export function createAssetChangeLog(data: AssetChangeLogCreate): Promise<AssetChangeLog> {
  return request({
    url: assetChangeLogUrl,
    method: 'post',
    data
  })
}

/**
 * 更新AssetChangeLog
 * 对应后端：UpdateAssetChangeLogAsync
 */
export function updateAssetChangeLog(id: string, data: AssetChangeLogUpdate): Promise<AssetChangeLog> {
  return request({
    url: `${assetChangeLogUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除AssetChangeLog（单条）
 * 对应后端：DeleteAssetChangeLogByIdAsync
 */
export function deleteAssetChangeLogById(id: string): Promise<void> {
  return request({
    url: `${assetChangeLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除AssetChangeLog
 * 对应后端：DeleteAssetChangeLogBatchAsync
 */
export function deleteAssetChangeLogBatch(ids: string[]): Promise<void> {
  return request({
    url: `${assetChangeLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetAssetChangeLogTemplateAsync；fileName 仅传名称不含后缀
 */
export function getAssetChangeLogTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${assetChangeLogUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入AssetChangeLog
 * 对应后端：ImportAssetChangeLogAsync
 */
export function importAssetChangeLogData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${assetChangeLogUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出AssetChangeLog
 * 对应后端：ExportAssetChangeLogAsync；fileName 仅传名称不含后缀
 */
export function exportAssetChangeLogData(query: AssetChangeLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${assetChangeLogUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
