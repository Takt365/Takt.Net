// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/routine/signalr/message
// 文件名称：message.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：在线消息 API，对应后端 TaktMessagesController
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../../request'
import type { TaktPagedResult } from '@/types/common'
import type {
  Message,
  MessageQuery,
  MessageCreate,
  MessageUpdate,
  MessageRead
} from '@/types/routine/signalr/message'

/**
 * 获取消息列表（分页）
 */
export function getList(params: MessageQuery): Promise<TaktPagedResult<Message>> {
  return request({
    url: '/api/TaktMessages/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取消息
 */
export function getById(id: string): Promise<Message> {
  return request({
    url: `/api/TaktMessages/${id}`,
    method: 'get'
  })
}

/**
 * 创建消息
 */
export function create(data: MessageCreate): Promise<Message> {
  return request({
    url: '/api/TaktMessages',
    method: 'post',
    data
  })
}

/**
 * 更新消息
 */
export function update(id: string, data: MessageUpdate): Promise<Message> {
  return request({
    url: `/api/TaktMessages/${id}`,
    method: 'put',
    data
  })
}

/**
 * 删除消息
 */
export function remove(id: string): Promise<void> {
  return request({
    url: `/api/TaktMessages/${id}`,
    method: 'delete'
  })
}

/**
 * 标记消息已读
 */
export function markAsRead(data: MessageRead): Promise<Message> {
  return request({
    url: '/api/TaktMessages/read',
    method: 'put',
    data
  })
}

/**
 * 导出消息（POST body 为查询条件）
 */
export function exportMessage(
  query: MessageQuery,
  sheetName?: string,
  fileName?: string
): Promise<Blob> {
  return request({
    url: '/api/TaktMessages/export',
    method: 'post',
    data: query,
    params: { sheetName, fileName },
    responseType: 'blob'
  })
}
