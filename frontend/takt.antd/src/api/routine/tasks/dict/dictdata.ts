// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/api/routine/dict/dictdata
// 文件名称：dictdata.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：字典数据相关 API，对应后端 TaktDictDatasController
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '../../../request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  DictData,
  DictDataQuery,
  DictDataCreate,
  DictDataUpdate
} from '@/types/routine/tasks/dict/dictdata'

// ========================================
// 字典数据相关 API（按后端控制器顺序）
// ========================================

const dictDataUrl = '/api/TaktDictDatas'

/**
 * 获取字典数据列表（分页）
 * 对应后端：GetListAsync
 */
export function getDictDataList(params: DictDataQuery): Promise<TaktPagedResult<DictData>> {
  return request({
    url: `${dictDataUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取字典数据
 * 对应后端：GetByIdAsync
 */
export function getDictDataById(id: string): Promise<DictData> {
  return request({
    url: `${dictDataUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取字典数据选项列表（用于下拉框等）
 * 对应后端：GetOptionsAsync
 */
export function getDictDataOptions(dictTypeCode?: string): Promise<TaktSelectOption[]> {
  return request({
    url: `${dictDataUrl}/options`,
    method: 'get',
    params: { dictTypeCode }
  })
}

/**
 * 创建字典数据
 * 对应后端：CreateAsync
 */
export function createDictData(data: DictDataCreate): Promise<DictData> {
  return request({
    url: dictDataUrl,
    method: 'post',
    data
  })
}

/**
 * 更新字典数据
 * 对应后端：UpdateAsync
 */
export function updateDictData(id: string, data: DictDataUpdate): Promise<DictData> {
  return request({
    url: `${dictDataUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除字典数据
 * 对应后端：DeleteAsync
 */
export function deleteDictDataById(id: string): Promise<void> {
  return request({
    url: `${dictDataUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTemplateAsync
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
 * 导入字典数据
 * 对应后端：ImportAsync
 */
export function importDictDatas(file: File, sheetName?: string): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: `${dictDataUrl}/import`,
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出字典数据
 * 对应后端：ExportAsync
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
