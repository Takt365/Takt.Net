// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：attendance-device.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：考勤设备 API，对应后端 TaktAttendanceDevicesController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  AttendanceDevice,
  AttendanceDeviceCreate,
  AttendanceDeviceQuery,
  AttendanceDeviceUpdate
} from '@/types/human-resource/attendance-leave/attendance-device'

const deviceUrl = '/api/TaktAttendanceDevices'

/**
 * 获取考勤设备分页列表
 */
export function getAttendanceDeviceList(
  params: AttendanceDeviceQuery
): Promise<TaktPagedResult<AttendanceDevice>> {
  return request({ url: `${deviceUrl}/list`, method: 'get', params })
}

/**
 * 根据 ID 获取考勤设备详情
 */
export function getAttendanceDeviceById(id: string): Promise<AttendanceDevice> {
  return request({ url: `${deviceUrl}/${id}`, method: 'get' })
}

/**
 * 创建考勤设备
 */
export function createAttendanceDevice(data: AttendanceDeviceCreate): Promise<AttendanceDevice> {
  return request({ url: deviceUrl, method: 'post', data })
}

/**
 * 更新考勤设备
 */
export function updateAttendanceDevice(
  id: string,
  data: AttendanceDeviceUpdate
): Promise<AttendanceDevice> {
  return request({ url: `${deviceUrl}/${id}`, method: 'put', data })
}

/**
 * 删除考勤设备（单条）
 */
export function deleteAttendanceDeviceById(id: string): Promise<void> {
  return request({ url: `${deviceUrl}/${id}`, method: 'delete' })
}

/**
 * 批量删除考勤设备
 */
export function deleteAttendanceDeviceBatch(ids: string[]): Promise<void> {
  return request({ url: `${deviceUrl}/batch`, method: 'delete', data: ids.map((id) => Number(id)) })
}

/**
 * 获取考勤设备导入模板
 */
export function getAttendanceDeviceTemplate(
  sheetName?: string,
  fileName?: string
): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${deviceUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入考勤设备数据
 */
export function importAttendanceDeviceData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${deviceUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出考勤设备数据
 */
export function exportAttendanceDeviceData(
  query: AttendanceDeviceQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${deviceUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
