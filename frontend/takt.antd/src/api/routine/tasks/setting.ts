// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/setting
// 文件名称：setting.ts
// 功能描述：设置相关 API，对应后端 TaktSettingsController
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  Setting,
  SettingQuery,
  SettingCreate,
  SettingUpdate,
  SettingStatus
} from '@/types/routine/tasks/setting/settings'
import type { TaktSelectOption } from '@/types/common'

const settingUrl = '/api/TaktSettings'

/**
 * 获取设置列表（分页）
 * 对应后端：GetListAsync
 */
export function getSettingList(params: SettingQuery): Promise<TaktPagedResult<Setting>> {
  return request({
    url: `${settingUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取设置
 * 对应后端：GetByIdAsync
 */
export function getSettingById(id: string): Promise<Setting> {
  return request({
    url: `${settingUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 根据设置键获取设置
 * 对应后端：GetByKeyAsync
 */
export function getSettingByKey(settingKey: string): Promise<Setting> {
  return request({
    url: `${settingUrl}/key/${encodeURIComponent(settingKey)}`,
    method: 'get'
  })
}

/**
 * 获取设置选项列表（用于下拉框等）
 * 对应后端：GetOptionsAsync
 */
export function getSettingOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${settingUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建设置
 * 对应后端：CreateAsync
 */
export function createSetting(data: SettingCreate): Promise<Setting> {
  return request({
    url: settingUrl,
    method: 'post',
    data
  })
}

/**
 * 更新设置
 * 对应后端：UpdateAsync
 */
export function updateSetting(id: string, data: SettingUpdate): Promise<Setting> {
  return request({
    url: `${settingUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除设置
 * 对应后端：DeleteAsync
 */
export function deleteSetting(id: string): Promise<void> {
  return request({
    url: `${settingUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 更新设置状态
 * 对应后端：UpdateStatusAsync
 */
export function updateSettingStatus(data: SettingStatus): Promise<Setting> {
  return request({
    url: `${settingUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetTemplateAsync
 */
export function getSettingTemplate(
  sheetName?: string,
  fileName?: string
): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${settingUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入设置
 * 对应后端：ImportAsync
 */
export function importSettings(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors?: string[] }> {
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
 * 导出设置
 * 对应后端：ExportAsync
 */
export function exportSettings(
  query: SettingQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${settingUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
