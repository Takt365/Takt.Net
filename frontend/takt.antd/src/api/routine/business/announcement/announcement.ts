// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/announcement
// 文件名称：announcement.ts
// 功能描述：Announcement API，对应后端 Takt.WebApi.Controllers.Routine.Business.Announcement.TaktAnnouncements
// ========================================

import request, { type BlobDownloadWithMeta } from '@/api/request'
import type { TaktPagedResult, TaktSelectOption } from '@/types/common'
import type {
  Announcement,
  AnnouncementQuery,
  AnnouncementCreate,
  AnnouncementUpdate,
  AnnouncementStatus,
  AnnouncementSort
} from '@/types/routine/business/announcement/announcement'

// ========================================
// Announcement相关 API（按后端控制器顺序）
// ========================================
const announcementUrl = '/api/TaktAnnouncements';

/**
 * 获取Announcement列表（分页）
 * 对应后端：GetAnnouncementListAsync
 */
export function getAnnouncementList(params: AnnouncementQuery): Promise<TaktPagedResult<Announcement>> {
  return request({
    url: `${announcementUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取Announcement
 * 对应后端：GetAnnouncementByIdAsync
 */
export function getAnnouncementById(id: string): Promise<Announcement> {
  return request({
    url: `${announcementUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 获取Announcement选项列表（用于下拉框等）
 * 对应后端：GetAnnouncementOptionsAsync
 */
export function getAnnouncementOptions(): Promise<TaktSelectOption[]> {
  return request({
    url: `${announcementUrl}/options`,
    method: 'get'
  })
}

/**
 * 创建Announcement
 * 对应后端：CreateAnnouncementAsync
 */
export function createAnnouncement(data: AnnouncementCreate): Promise<Announcement> {
  return request({
    url: announcementUrl,
    method: 'post',
    data
  })
}

/**
 * 更新Announcement
 * 对应后端：UpdateAnnouncementAsync
 */
export function updateAnnouncement(id: string, data: AnnouncementUpdate): Promise<Announcement> {
  return request({
    url: `${announcementUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除Announcement（单条）
 * 对应后端：DeleteAnnouncementByIdAsync
 */
export function deleteAnnouncementById(id: string): Promise<void> {
  return request({
    url: `${announcementUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除Announcement
 * 对应后端：DeleteAnnouncementBatchAsync
 */
export function deleteAnnouncementBatch(ids: string[]): Promise<void> {
  return request({
    url: `${announcementUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 更新Announcement状态
 * 对应后端：UpdateAnnouncementStatusAsync
 */
export function updateAnnouncementStatus(data: AnnouncementStatus): Promise<AnnouncementStatus> {
  return request({
    url: `${announcementUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 更新Announcement排序
 * 对应后端：UpdateAnnouncementSortAsync
 */
export function updateAnnouncementSort(data: AnnouncementSort): Promise<AnnouncementSort> {
  return request({
    url: `${announcementUrl}/sort`,
    method: 'put',
    data
  })
}

/**
 * 获取导入模板
 * 对应后端：GetAnnouncementTemplateAsync；fileName 仅传名称不含后缀
 */
export function getAnnouncementTemplate(sheetName?: string, fileName?: string): Promise<BlobDownloadWithMeta> {
  return request({
    url: `${announcementUrl}/template`,
    method: 'get',
    params: { sheetName, fileName },
    responseType: 'blob',
    blobWithHeaders: true
  })
}

/**
 * 导入Announcement
 * 对应后端：ImportAnnouncementAsync
 */
export function importAnnouncementData(
  file: File,
  sheetName?: string
): Promise<{ success: number; fail: number; errors: string[] }> {
  const formData = new FormData()
  formData.append('file', file)
  if (sheetName) formData.append('sheetName', sheetName)
  return request({
    url: `${announcementUrl}/import`,
    method: 'post',
    data: formData,
    headers: { 'Content-Type': 'multipart/form-data' }
  })
}

/**
 * 导出Announcement
 * 对应后端：ExportAnnouncementAsync；fileName 仅传名称不含后缀
 */
export function exportAnnouncementData(query: AnnouncementQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${announcementUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
