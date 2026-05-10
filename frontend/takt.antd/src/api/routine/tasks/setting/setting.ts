// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/tasks/setting
// 文件名称：setting.ts
// 功能描述：Setting API，对应后端 Takt.WebApi.Controllers.Routine.Tasks.Setting.TaktSettings
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Setting,
  SettingQuery,
  SettingCreate,
  SettingUpdate,
  SettingStatus,
  SettingSort
} from '@/types/routine/tasks/setting/setting'

// ========================================
// Setting相关 API（按后端控制器顺序）
// ========================================
const settingUrl = '/api/TaktSettings';

/**
 * 获取Setting列表（分页）
 * 对应后端：GetSettingListAsync
 */
export function getSettingList(params: SettingQuery): Promise<TaktPagedResult<Setting>> {
  return request({
    url: `${settingUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Setting
 * 对应后端：GetSettingByIdAsync
 */
export function getSettingById(id: string): Promise<Setting> {
  return request({
    url: `${settingUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Setting选项列表（用于下拉框等）
 * 对应后端：GetSettingOptionsAsync
 */
export function getSettingOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${settingUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Setting
 * 对应后端：CreateSettingAsync
 */
export function createSetting(data: SettingCreate): Promise<Setting> {
  return request({
    url: settingUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Setting
 * 对应后端：UpdateSettingAsync
 */
export function updateSetting(id: string, data: SettingUpdate): Promise<Setting> {
  return request({
    url: `${settingUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Setting（单条）
 * 对应后端：DeleteSettingByIdAsync
 */
export function deleteSettingById(id: string): Promise<void> {
  return request({
    url: `${settingUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Setting
 * 对应后端：DeleteSettingBatchAsync
 */
export function deleteSettingBatch(ids: string[]): Promise<void> {
  return request({
    url: `${settingUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Setting状态
 * 对应后端：UpdateSettingStatusAsync
 */
export function updateSettingStatus(data: SettingStatus): Promise<SettingStatus> {
  return request({
    url: `${settingUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新Setting排序
 * 对应后端：UpdateSettingSortAsync
 */
export function updateSettingSort(data: SettingSort): Promise<SettingSort> {
  return request({
    url: `${settingUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetSettingTemplateAsync；fileName 仅传名称不含后缀
 */
export function getSettingTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${settingUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Setting
 * 对应后端：ImportSettingAsync
 */
export function importSettingData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${settingUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Setting
 * 对应后端：ExportSettingAsync；fileName 仅传名称不含后缀
 */
export function exportSettingData(query: SettingQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${settingUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
