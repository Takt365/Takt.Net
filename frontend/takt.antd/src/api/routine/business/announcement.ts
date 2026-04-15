// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/business/announcement
// 文件名称：announcement.ts
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：公告通知相关 API，对应后端 TaktAnnouncementsController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type {
  Announcement,
  AnnouncementQuery,
  AnnouncementCreate,
  AnnouncementUpdate,
  AnnouncementStatus
} from '@/types/routine/business/announcement'

// ========================================
// 公告通知相关 API（按后端控制器顺序）
// ========================================

const announcementUrl = '/api/TaktAnnouncements'

/**
 * 获取公告列表（分页）
 * 对应后端：GetListAsync
 */
export function getAnnouncementList(params: AnnouncementQuery): Promise<TaktPagedResult<Announcement>> {
  return request({
    url: `${announcementUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取公告
 * 对应后端：GetByIdAsync
 */
export function getAnnouncementById(id: string): Promise<Announcement> {
  return request({
    url: `${announcementUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 创建公告
 * 对应后端：CreateAsync
 */
export function createAnnouncement(data: AnnouncementCreate): Promise<Announcement> {
  return request({
    url: announcementUrl,
    method: 'post',
    data
  })
}

/**
 * 更新公告
 * 对应后端：UpdateAsync
 */
export function updateAnnouncement(id: string, data: AnnouncementUpdate): Promise<Announcement> {
  return request({
    url: `${announcementUrl}/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除公告
 * 对应后端：DeleteAsync
 */
export function deleteAnnouncement(id: string): Promise<void> {
  return request({
    url: `${announcementUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除公告
 * 对应后端：DeleteBatchAsync
 */
export function deleteAnnouncementBatch(ids: string[]): Promise<void> {
  return request({
    url: `${announcementUrl}/batch`,
    method: 'delete',
    data: ids.map(id => Number(id))
  })
}

/**
 * 更新公告状态
 * 对应后端：UpdateStatusAsync
 */
export function updateAnnouncementStatus(data: AnnouncementStatus): Promise<Announcement> {
  return request({
    url: `${announcementUrl}/status`,
    method: 'put',
    data
  })
}

/**
 * 导出公告
 * 对应后端：ExportAsync；fileName 仅传名称不含后缀
 */
export function exportAnnouncements(
  query: AnnouncementQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: `${announcementUrl}/export`,
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
