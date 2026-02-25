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

import request from '../../request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  DictData,
  DictDataQuery,
  DictDataCreate,
  DictDataUpdate
} from '@/types/routine/dict/dictdata'

// ========================================
// 字典数据相关 API（按后端控制器顺序）
// ========================================

/**
 * 获取字典数据列表（分页）
 * 对应后端：GetListAsync
 */
export function getList(params: DictDataQuery): Promise<TaktPagedResult<DictData>> {
  return request({
    url: '/api/TaktDictDatas/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取字典数据
 * 对应后端：GetByIdAsync
 */
export function getById(id: string): Promise<DictData> {
  return request({
    url: `/api/TaktDictDatas/${id}`,
    method: 'get'
  })
}

/**
 * 获取字典数据选项列表（用于下拉框等）
 * 对应后端：GetOptionsAsync
 */
export function getOptions(dictTypeCode?: string): Promise<TaktSelectOption[]> {
  return request({
    url: '/api/TaktDictDatas/options',
    method: 'get',
    params: { dictTypeCode }
  })
}

/**
 * 创建字典数据
 * 对应后端：CreateAsync
 */
export function create(data: DictDataCreate): Promise<DictData> {
  return request({
    url: '/api/TaktDictDatas',
    method: 'post',
    data
  })
}

/**
 * 更新字典数据
 * 对应后端：UpdateAsync
 */
export function update(id: string, data: DictDataUpdate): Promise<DictData> {
  return request({
    url: `/api/TaktDictDatas/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除字典数据
 * 对应后端：DeleteAsync
 */
export function remove(id: string): Promise<void> {
  return request({
    url: `/api/TaktDictDatas/${id}`,
    method: 'delete'
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTemplateAsync
 */
export function getTemplate(sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktDictDatas/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
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
    url: '/api/TaktDictDatas/import',
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
export function exportDictDatas(query: DictDataQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktDictDatas/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
