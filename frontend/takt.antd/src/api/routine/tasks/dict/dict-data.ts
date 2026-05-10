// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/dict
// 文件名称：dict-data.ts
// 功能描述：DictData API，对应后端 Takt.WebApi.Controllers.Routine.Tasks.Dict.TaktDictDatas
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  DictData,
  DictDataQuery,
  DictDataCreate,
  DictDataUpdate,
  DictDataSort
} from '@/types/routine/tasks/dict/dict-data'

// ========================================
// DictData相关 API（按后端控制器顺序）
// ========================================
const dictDataUrl = '/api/TaktDictDatas';

/**
 * 获取DictData列表（分页）
 * 对应后端：GetDictDataListAsync
 */
export function getDictDataList(params: DictDataQuery): Promise<TaktPagedResult<DictData>> {
  return request({
    url: `${dictDataUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取DictData
 * 对应后端：GetDictDataByIdAsync
 */
export function getDictDataById(id: string): Promise<DictData> {
  return request({
    url: `${dictDataUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取DictData选项列表（用于下拉框等）
 * 对应后端：GetDictDataOptionsAsync
 */
export function getDictDataOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${dictDataUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建DictData
 * 对应后端：CreateDictDataAsync
 */
export function createDictData(data: DictDataCreate): Promise<DictData> {
  return request({
    url: dictDataUrl,
    method: 'post',
    data
  })
}

/**
 * 更新DictData
 * 对应后端：UpdateDictDataAsync
 */
export function updateDictData(id: string, data: DictDataUpdate): Promise<DictData> {
  return request({
    url: `${dictDataUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除DictData（单条）
 * 对应后端：DeleteDictDataByIdAsync
 */
export function deleteDictDataById(id: string): Promise<void> {
  return request({
    url: `${dictDataUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除DictData
 * 对应后端：DeleteDictDataBatchAsync
 */
export function deleteDictDataBatch(ids: string[]): Promise<void> {
  return request({
    url: `${dictDataUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新DictData排序
 * 对应后端：UpdateDictDataSortAsync
 */
export function updateDictDataSort(data: DictDataSort): Promise<DictDataSort> {
  return request({
    url: `${dictDataUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetDictDataTemplateAsync；fileName 仅传名称不含后缀
 */
export function getDictDataTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${dictDataUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入DictData
 * 对应后端：ImportDictDataAsync
 */
export function importDictDataData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${dictDataUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出DictData
 * 对应后端：ExportDictDataAsync；fileName 仅传名称不含后缀
 */
export function exportDictDataData(query: DictDataQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${dictDataUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
