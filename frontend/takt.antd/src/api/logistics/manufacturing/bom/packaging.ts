// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/logistics/manufacturing/bom
// 文件名称：packaging.ts
// 功能描述：Packaging API，对应后端 Takt.WebApi.Controllers.Logistics.Manufacturing.Bom.TaktPackagings
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Packaging,
  PackagingQuery,
  PackagingCreate,
  PackagingUpdate,
  PackagingSort
} from '@/types/logistics/manufacturing/bom/packaging'

// ========================================
// Packaging相关 API（按后端控制器顺序）
// ========================================
const packagingUrl = '/api/TaktPackagings';

/**
 * 获取Packaging列表（分页）
 * 对应后端：GetPackagingListAsync
 */
export function getPackagingList(params: PackagingQuery): Promise<TaktPagedResult<Packaging>> {
  return request({
    url: `${packagingUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Packaging
 * 对应后端：GetPackagingByIdAsync
 */
export function getPackagingById(id: string): Promise<Packaging> {
  return request({
    url: `${packagingUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Packaging选项列表（用于下拉框等）
 * 对应后端：GetPackagingOptionsAsync
 */
export function getPackagingOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${packagingUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Packaging
 * 对应后端：CreatePackagingAsync
 */
export function createPackaging(data: PackagingCreate): Promise<Packaging> {
  return request({
    url: packagingUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Packaging
 * 对应后端：UpdatePackagingAsync
 */
export function updatePackaging(id: string, data: PackagingUpdate): Promise<Packaging> {
  return request({
    url: `${packagingUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Packaging（单条）
 * 对应后端：DeletePackagingByIdAsync
 */
export function deletePackagingById(id: string): Promise<void> {
  return request({
    url: `${packagingUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Packaging
 * 对应后端：DeletePackagingBatchAsync
 */
export function deletePackagingBatch(ids: string[]): Promise<void> {
  return request({
    url: `${packagingUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Packaging排序
 * 对应后端：UpdatePackagingSortAsync
 */
export function updatePackagingSort(data: PackagingSort): Promise<PackagingSort> {
  return request({
    url: `${packagingUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetPackagingTemplateAsync；fileName 仅传名称不含后缀
 */
export function getPackagingTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${packagingUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Packaging
 * 对应后端：ImportPackagingAsync
 */
export function importPackagingData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${packagingUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Packaging
 * 对应后端：ExportPackagingAsync；fileName 仅传名称不含后缀
 */
export function exportPackagingData(query: PackagingQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${packagingUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
