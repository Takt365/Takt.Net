// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/human-resource/attendance-leave
// 文件名称：attendance-device.ts
// 功能描述：AttendanceDevice API，对应后端 Takt.WebApi.Controllers.HumanResource.AttendanceLeave.TaktAttendanceDevices
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  AttendanceDevice,
  AttendanceDeviceQuery,
  AttendanceDeviceCreate,
  AttendanceDeviceUpdate
} from '@/types/human-resource/attendance-leave/attendance-device'

// ========================================
// AttendanceDevice相关 API（按后端控制器顺序）
// ========================================
const attendanceDeviceUrl = '/api/TaktAttendanceDevices';

/**
 * 获取AttendanceDevice列表（分页）
 * 对应后端：GetAttendanceDeviceListAsync
 */
export function getAttendanceDeviceList(params: AttendanceDeviceQuery): Promise<TaktPagedResult<AttendanceDevice>> {
  return request({
    url: `${attendanceDeviceUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取AttendanceDevice
 * 对应后端：GetAttendanceDeviceByIdAsync
 */
export function getAttendanceDeviceById(id: string): Promise<AttendanceDevice> {
  return request({
    url: `${attendanceDeviceUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取AttendanceDevice选项列表（用于下拉框等）
 * 对应后端：GetAttendanceDeviceOptionsAsync
 */
export function getAttendanceDeviceOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${attendanceDeviceUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建AttendanceDevice
 * 对应后端：CreateAttendanceDeviceAsync
 */
export function createAttendanceDevice(data: AttendanceDeviceCreate): Promise<AttendanceDevice> {
  return request({
    url: attendanceDeviceUrl,
    method: 'post',
    data
  })
}

/**
 * 更新AttendanceDevice
 * 对应后端：UpdateAttendanceDeviceAsync
 */
export function updateAttendanceDevice(id: string, data: AttendanceDeviceUpdate): Promise<AttendanceDevice> {
  return request({
    url: `${attendanceDeviceUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除AttendanceDevice（单条）
 * 对应后端：DeleteAttendanceDeviceByIdAsync
 */
export function deleteAttendanceDeviceById(id: string): Promise<void> {
  return request({
    url: `${attendanceDeviceUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除AttendanceDevice
 * 对应后端：DeleteAttendanceDeviceBatchAsync
 */
export function deleteAttendanceDeviceBatch(ids: string[]): Promise<void> {
  return request({
    url: `${attendanceDeviceUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 获取导入模板
 * 对应后端：GetAttendanceDeviceTemplateAsync；fileName 仅传名称不含后缀
 */
export function getAttendanceDeviceTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${attendanceDeviceUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入AttendanceDevice
 * 对应后端：ImportAttendanceDeviceAsync
 */
export function importAttendanceDeviceData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${attendanceDeviceUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出AttendanceDevice
 * 对应后端：ExportAttendanceDeviceAsync；fileName 仅传名称不含后缀
 */
export function exportAttendanceDeviceData(query: AttendanceDeviceQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${attendanceDeviceUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
