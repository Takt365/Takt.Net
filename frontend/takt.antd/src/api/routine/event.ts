// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/api/routine/event
// 文件名称：event.ts
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：活动组织（Event）API，对应后端 TaktEventsController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../request'
import type { TaktPagedResult } from '@/types/common'
import type { Event, EventQuery, EventCreate, EventUpdate } from '@/types/routine/event'

/**
 * 获取活动列表（分页）
 * 对应后端：GetListAsync
 */
export function getList(params: EventQuery): Promise<TaktPagedResult<Event>> {
  return request({
    url: '/api/TaktEvents/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取活动
 * 对应后端：GetByIdAsync
 */
export function getById(id: string): Promise<Event> {
  return request({
    url: `/api/TaktEvents/${id}`,
    method: 'get'
  })
}

/**
 * 创建活动
 * 对应后端：CreateAsync
 */
export function create(data: EventCreate): Promise<Event> {
  return request({
    url: '/api/TaktEvents',
    method: 'post',
    data
  })
}

/**
 * 更新活动
 * 对应后端：UpdateAsync
 */
export function update(id: string, data: EventUpdate): Promise<Event> {
  return request({
    url: `/api/TaktEvents/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除活动
 * 对应后端：DeleteAsync
 */
export function remove(id: string): Promise<void> {
  return request({
    url: `/api/TaktEvents/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除活动
 * 对应后端：DeleteBatchAsync
 */
export function removeBatch(ids: string[]): Promise<void> {
  return request({
    url: '/api/TaktEvents/batch',
    method: 'delete',
    data: ids.map((id) => Number(id))
  })
}

/**
 * 导出活动
 * 对应后端：ExportAsync
 */
export function exportEvents(
  query: EventQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: '/api/TaktEvents/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
