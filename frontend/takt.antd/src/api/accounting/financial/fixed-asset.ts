/**
 * 固定资产相关 API，对应后端 TaktFixedAssetsController
 */

import request from '../../request'
import type { TaktPagedResult } from '@/types/common'
import type {
  FixedAsset,
  FixedAssetQuery,
  FixedAssetCreate,
  FixedAssetUpdate,
  FixedAssetStatus
} from '@/types/accounting/financial/fixed-asset'

export function getList(params: FixedAssetQuery): Promise<TaktPagedResult<FixedAsset>> {
  return request({
    url: '/api/TaktFixedAssets/list',
    method: 'get',
    params
  })
}

export function getById(id: string): Promise<FixedAsset> {
  return request({
    url: `/api/TaktFixedAssets/${id}`,
    method: 'get'
  })
}

export function create(data: FixedAssetCreate): Promise<FixedAsset> {
  return request({
    url: '/api/TaktFixedAssets',
    method: 'post',
    data
  })
}

export function update(id: string, data: FixedAssetUpdate): Promise<FixedAsset> {
  return request({
    url: `/api/TaktFixedAssets/${id}`,
    method: 'put',
    data
  })
}

export function deleteFixedAsset(id: string): Promise<void> {
  return request({
    url: `/api/TaktFixedAssets/${id}`,
    method: 'delete'
  })
}

export function updateStatus(data: FixedAssetStatus): Promise<FixedAsset> {
  return request({
    url: '/api/TaktFixedAssets/status',
    method: 'put',
    data
  })
}

export function getTemplate(sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktFixedAssets/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

export function importFixedAssets(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: '/api/TaktFixedAssets/import',
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

export function exportFixedAssets(
  query: FixedAssetQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: '/api/TaktFixedAssets/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
