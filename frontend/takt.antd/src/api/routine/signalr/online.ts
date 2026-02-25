// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/signalr/online
// 文件名称：online.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：在线用户 API，对应后端 TaktOnlinesController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../../request'
import type { TaktPagedResult } from '@/types/common'
import type { Online, OnlineQuery, OnlineCreate } from '@/types/routine/signalr/online'

/**
 * 获取在线用户列表（分页）
 */
export function getList(params: OnlineQuery): Promise<TaktPagedResult<Online>> {
  return request({
    url: '/api/TaktOnlines/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取在线用户
 */
export function getById(id: string): Promise<Online> {
  return request({
    url: `/api/TaktOnlines/${id}`,
    method: 'get'
  })
}

/**
 * 根据连接ID获取在线用户
 */
export function getByConnectionId(connectionId: string): Promise<Online> {
  return request({
    url: `/api/TaktOnlines/connection/${encodeURIComponent(connectionId)}`,
    method: 'get'
  })
}

/**
 * 创建在线用户（一般由 Hub 创建，管理端较少使用）
 */
export function create(data: OnlineCreate): Promise<Online> {
  return request({
    url: '/api/TaktOnlines',
    method: 'post',
    data
  })
}

/**
 * 删除在线用户
 */
export function remove(id: string): Promise<void> {
  return request({
    url: `/api/TaktOnlines/${id}`,
    method: 'delete'
  })
}

/**
 * 根据连接ID删除在线用户
 */
export function removeByConnectionId(connectionId: string): Promise<void> {
  return request({
    url: `/api/TaktOnlines/connection/${encodeURIComponent(connectionId)}`,
    method: 'delete'
  })
}

/**
 * 批量删除在线用户
 */
export function removeBatch(ids: string[]): Promise<void> {
  return request({
    url: '/api/TaktOnlines/batch',
    method: 'delete',
    data: ids.map((id) => Number(id))
  })
}

/**
 * 更新最后活动时间
 */
export function updateLastActiveTime(connectionId: string): Promise<void> {
  return request({
    url: `/api/TaktOnlines/active/${encodeURIComponent(connectionId)}`,
    method: 'put'
  })
}

/**
 * 导出在线用户（GET 查询参数）
 */
export function exportOnline(
  query: OnlineQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: '/api/TaktOnlines/export',
    method: 'get',
    params: { ...query, sheetName, fileName },
    responseType: 'blob'
  })
}
