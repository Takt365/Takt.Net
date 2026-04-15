// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/api/routine/dict/dicttype
// 文件名称：dicttype.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：字典类型相关 API，对应后端 TaktDictTypesController
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '../../../request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  DictType,
  DictTypeQuery,
  DictTypeCreate,
  DictTypeUpdate,
  DictTypeStatus
} from '@/types/routine/tasks/dict/dicttype'

// ========================================
// 字典类型相关 API（按后端控制器顺序）
// ========================================

const dictTypeUrl = '/api/TaktDictTypes'

/**
 * 获取字典类型列表（分页）
 * 对应后端：GetListAsync
 */
export function getDictTypeList(params: DictTypeQuery): Promise<TaktPagedResult<DictType>> {
  return request({
    url: `${dictTypeUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取字典类型
 * 对应后端：GetByIdAsync
 */
export function getDictTypeById(id: string): Promise<DictType> {
  return request({
    url: `${dictTypeUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取字典类型选项列表（用于下拉框等）
 * 对应后端：GetOptionsAsync
 */
export function getDictTypeOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${dictTypeUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建字典类型
 * 对应后端：CreateAsync
 */
export function createDictType(data: DictTypeCreate): Promise<DictType> {
  return request({
    url: dictTypeUrl,
    method: 'post',
    data
  })
}

/**
 * 更新字典类型
 * 对应后端：UpdateAsync
 */
export function updateDictType(id: string, data: DictTypeUpdate): Promise<DictType> {
  return request({
    url: `${dictTypeUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除字典类型
 * 对应后端：DeleteAsync
 */
export function deleteDictTypeById(id: string): Promise<void> {
  return request({
    url: `${dictTypeUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除字典类型
 * 对应后端：DeleteAsync(IEnumerable<long>)
 */
export function deleteDictTypeBatch(ids: string[]): Promise<void> {
  return request({
    url: `${dictTypeUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新字典类型状态
 * 对应后端：UpdateStatusAsync
 */
export function updateDictTypeStatus(data: DictTypeStatus): Promise<DictType> {
  return request({
    url: `${dictTypeUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTemplateAsync
 */
export function getDictTypeTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${dictTypeUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入字典类型
 * 对应后端：ImportAsync
 */
export function importDictTypeData(file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: `${dictTypeUrl}/import`,
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出字典类型
 * 对应后端：ExportAsync
 */
export function exportDictTypeData(query: DictTypeQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${dictTypeUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
