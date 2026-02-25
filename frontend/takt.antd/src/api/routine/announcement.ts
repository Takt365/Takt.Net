// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/api/routine/announcement
// 文件名称：announcement.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：公告相关 API，对应后端 TaktAnnouncementsController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../request'
import type { TaktPagedResult } from '@/types/common'
import type {
  Announcement,
  AnnouncementQuery,
  AnnouncementCreate,
  AnnouncementUpdate,
  AnnouncementStatus,
  AnnouncementAttachment,
  AnnouncementRead
} from '@/types/routine/announcement'

/**
 * 获取公告列表（分页）
 * 对应后端：GetListAsync
 */
export function getList(params: AnnouncementQuery): Promise<TaktPagedResult<Announcement>> {
  return request({
    url: '/api/TaktAnnouncements/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取公告
 * 对应后端：GetByIdAsync
 */
export function getById(id: string): Promise<Announcement> {
  return request({
    url: `/api/TaktAnnouncements/${id}`,
    method: 'get'
  })
}

/**
 * 创建公告
 * 对应后端：CreateAsync
 */
export function create(data: AnnouncementCreate): Promise<Announcement> {
  return request({
    url: '/api/TaktAnnouncements',
    method: 'post',
    data
  })
}

/**
 * 更新公告
 * 对应后端：UpdateAsync
 */
export function update(id: string, data: AnnouncementUpdate): Promise<Announcement> {
  return request({
    url: `/api/TaktAnnouncements/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除公告
 * 对应后端：DeleteAsync
 */
export function remove(id: string): Promise<void> {
  return request({
    url: `/api/TaktAnnouncements/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除公告
 * 对应后端：DeleteBatchAsync
 */
export function removeBatch(ids: string[]): Promise<void> {
  return request({
    url: '/api/TaktAnnouncements/batch',
    method: 'delete',
    data: ids.map((id) => Number(id))
  })
}

/**
 * 更新公告状态（发布/撤回等）
 * 对应后端：UpdateStatusAsync
 */
export function updateStatus(data: AnnouncementStatus): Promise<Announcement> {
  return request({
    url: '/api/TaktAnnouncements/status',
    method: 'put',
    data
  })
}

/**
 * 获取公告附件列表
 * 对应后端：GetAttachmentsAsync
 */
export function getAttachments(announcementId: string): Promise<AnnouncementAttachment[]> {
  return request({
    url: `/api/TaktAnnouncements/${announcementId}/attachments`,
    method: 'get'
  })
}

/**
 * 获取公告阅读记录（分页）
 * 对应后端：GetReadsAsync
 */
export function getReads(
  announcementId: string,
  pageIndex = 1,
  pageSize = 20
): Promise<TaktPagedResult<AnnouncementRead>> {
  return request({
    url: `/api/TaktAnnouncements/${announcementId}/reads`,
    method: 'get',
    params: { pageIndex, pageSize }
  })
}

/**
 * 记录阅读
 * 对应后端：RecordReadAsync
 */
export function recordRead(announcementId: string, readDurationSeconds = 0): Promise<void> {
  return request({
    url: `/api/TaktAnnouncements/${announcementId}/read`,
    method: 'post',
    params: { readDurationSeconds }
  })
}
