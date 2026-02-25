// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/api/humanresource/attendance-leave/holiday
// 文件名称：holiday.ts
// 功能描述：假日相关 API，对应后端 TaktHolidaysController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../../request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Holiday,
  HolidayQuery,
  HolidayCreate,
  HolidayUpdate
} from '@/types/humanresource/attendance-leave/holiday'

// ========================================
// 假日相关 API（按后端控制器顺序）
// ========================================

/**
 * 获取假日列表（分页）
 * 对应后端：GetListAsync
 */
export function getList(params: HolidayQuery): Promise<TaktPagedResult<Holiday>> {
  return request({
    url: '/api/TaktHolidays/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取假日
 * 对应后端：GetByIdAsync
 */
export function getById(id: string): Promise<Holiday> {
  return request({
    url: `/api/TaktHolidays/${id}`,
    method: 'get'
  })
}

/**
 * 获取假日选项列表（用于下拉框等）
 * 对应后端：GetOptionsAsync
 */
export function getOptions(region?: string): Promise<TaktSelectOption[]> {
  return request({
    url: '/api/TaktHolidays/options',
    method: 'get',
    params: region ? { region } : undefined
  })
}

/**
 * 创建假日
 * 对应后端：CreateAsync
 */
export function create(data: HolidayCreate): Promise<Holiday> {
  return request({
    url: '/api/TaktHolidays',
    method: 'post',
    data
  })
}

/**
 * 更新假日
 * 对应后端：UpdateAsync
 */
export function update(id: string, data: HolidayUpdate): Promise<Holiday> {
  return request({
    url: `/api/TaktHolidays/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除假日
 * 对应后端：DeleteAsync
 */
export function remove(id: string): Promise<void> {
  return request({
    url: `/api/TaktHolidays/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除假日
 * 对应后端：DeleteBatchAsync
 */
export function removeBatch(ids: string[]): Promise<void> {
  return request({
    url: '/api/TaktHolidays/batch',
    method: 'delete',
    data: ids.map((id) => Number(id))
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTemplateAsync
 */
export function getTemplate(sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktHolidays/template',
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 导入假日
 * 对应后端：ImportAsync
 */
export function importHolidays(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) {
    formData.append('sheetName', sheetName)
  }
  return request({
    url: '/api/TaktHolidays/import',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * 导出假日
 * 对应后端：ExportAsync
 */
export function exportHolidays(
  query: HolidayQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: '/api/TaktHolidays/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}

/**
 * 获取指定日期的假日主题色（用于前端根据假日动态显示主色调，支持未登录访问）
 * 对应后端：GetHolidayThemeAsync
 */
export function getHolidayTheme(
  date?: string,
  region?: string
): Promise<Holiday | null> {
  return request({
    url: '/api/TaktHolidays/theme',
    method: 'get',
    params: { date, region }
  })
}
