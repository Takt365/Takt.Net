// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/dict
// 文件名称：dict-type.ts
// 功能描述：DictType API，对应后端 Takt.WebApi.Controllers.Routine.Tasks.Dict.TaktDictTypes
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  DictType,
  DictTypeQuery,
  DictTypeCreate,
  DictTypeUpdate,
  DictTypeStatus,
  DictTypeSort
} from '@/types/routine/tasks/dict/dict-type'

// ========================================
// DictType相关 API（按后端控制器顺序）
// ========================================
const dictTypeUrl = '/api/TaktDictTypes';

/**
 * 获取DictType列表（分页）
 * 对应后端：GetDictTypeListAsync
 */
export function getDictTypeList(params: DictTypeQuery): Promise<TaktPagedResult<DictType>> {
  return request({
    url: `${dictTypeUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取DictType
 * 对应后端：GetDictTypeByIdAsync
 */
export function getDictTypeById(id: string): Promise<DictType> {
  return request({
    url: `${dictTypeUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取DictType选项列表（用于下拉框等）
 * 对应后端：GetDictTypeOptionsAsync
 */
export function getDictTypeOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${dictTypeUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建DictType
 * 对应后端：CreateDictTypeAsync
 */
export function createDictType(data: DictTypeCreate): Promise<DictType> {
  return request({
    url: dictTypeUrl,
    method: 'post',
    data
  })
}

/**
 * 更新DictType
 * 对应后端：UpdateDictTypeAsync
 */
export function updateDictType(id: string, data: DictTypeUpdate): Promise<DictType> {
  return request({
    url: `${dictTypeUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除DictType（单条）
 * 对应后端：DeleteDictTypeByIdAsync
 */
export function deleteDictTypeById(id: string): Promise<void> {
  return request({
    url: `${dictTypeUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除DictType
 * 对应后端：DeleteDictTypeBatchAsync
 */
export function deleteDictTypeBatch(ids: string[]): Promise<void> {
  return request({
    url: `${dictTypeUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新DictType状态
 * 对应后端：UpdateDictTypeStatusAsync
 */
export function updateDictTypeStatus(data: DictTypeStatus): Promise<DictTypeStatus> {
  return request({
    url: `${dictTypeUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新DictType排序
 * 对应后端：UpdateDictTypeSortAsync
 */
export function updateDictTypeSort(data: DictTypeSort): Promise<DictTypeSort> {
  return request({
    url: `${dictTypeUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetDictTypeTemplateAsync；fileName 仅传名称不含后缀
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
 * 导入DictType
 * 对应后端：ImportDictTypeAsync
 */
export function importDictTypeData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${dictTypeUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出DictType
 * 对应后端：ExportDictTypeAsync；fileName 仅传名称不含后缀
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
