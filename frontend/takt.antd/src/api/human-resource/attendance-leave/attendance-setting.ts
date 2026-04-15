// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：attendance-setting.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤设置（上下班方案）API，对应后端 TaktAttendanceSettingsController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  AttendanceSetting,
  AttendanceSettingCreate,
  AttendanceSettingQuery,
  AttendanceSettingUpdate
} from '@/types/human-resource/attendance-leave/attendance-setting'

const settingUrl = '/api/TaktAttendanceSettings'

/**
 * 获取考勤设置分页列表
 */
export function getAttendanceSettingList(
  params: AttendanceSettingQuery
): Promise<TaktPagedResult<AttendanceSetting>> {
  return request({ url: `${settingUrl}/list`, method: 'get', params })
}

/**
 * 根据 ID 获取考勤设置详情
 */
export function getAttendanceSettingById(id: string): Promise<AttendanceSetting> {
  return request({ url: `${settingUrl}/${id}`, method: 'get' })
}

/**
 * 创建考勤设置
 */
export function createAttendanceSetting(data: AttendanceSettingCreate): Promise<AttendanceSetting> {
  return request({ url: settingUrl, method: 'post', data })
}

/**
 * 更新考勤设置
 */
export function updateAttendanceSetting(
  id: string,
  data: AttendanceSettingUpdate
): Promise<AttendanceSetting> {
  return request({ url: `${settingUrl}/${id}`, method: 'put', data })
}

/**
 * 删除考勤设置（单条）
 */
export function deleteAttendanceSettingById(id: string): Promise<void> {
  return request({ url: `${settingUrl}/${id}`, method: 'delete' })
}

/**
 * 批量删除考勤设置
 */
export function deleteAttendanceSettingBatch(ids: string[]): Promise<void> {
  return request({ url: `${settingUrl}/batch`, method: 'delete', data: ids.map((id) => Number(id)) })
}

/**
 * 获取考勤设置导入模板
 */
export function getAttendanceSettingTemplate(
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
 * 导入考勤设置数据
 */
export function importAttendanceSettingData(
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
 * 导出考勤设置数据
 */
export function exportAttendanceSettingData(
  query: AttendanceSettingQuery,
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
