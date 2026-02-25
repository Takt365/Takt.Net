// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/api/logging/quartz-log
// 文件名称：quartz-log.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：任务日志相关 API，对应后端 TaktQuartzLogsController
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '../../request'
import type { TaktPagedResult } from '@/types/common'
import type { QuartzLog, QuartzLogQuery } from '@/types/statistics/logging/quartz-log'

/**
 * 获取任务日志列表（分页）
 * 对应后端：GetListAsync
 */
export function getQuartzLogList(params: QuartzLogQuery): Promise<TaktPagedResult<QuartzLog>> {
  return request({
    url: '/api/TaktQuartzLogs/list',
    method: 'get',
    params
  })
}

/**
 * 根据ID获取任务日志
 * 对应后端：GetByIdAsync
 */
export function getQuartzLogById(id: string): Promise<QuartzLog> {
  return request({
    url: `/api/TaktQuartzLogs/${id}`,
    method: 'get'
  })
}

/**
 * 删除任务日志
 * 对应后端：DeleteAsync
 */
export function deleteQuartzLog(id: string): Promise<void> {
  return request({
    url: `/api/TaktQuartzLogs/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除任务日志
 * 对应后端：DeleteBatchAsync
 */
export function deleteQuartzLogBatch(ids: number[]): Promise<void> {
  return request({
    url: '/api/TaktQuartzLogs/batch',
    method: 'delete',
    data: ids
  })
}

/**
 * 导出任务日志
 * 对应后端：ExportAsync
 */
export function exportQuartzLog(query: QuartzLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: '/api/TaktQuartzLogs/export',
    method: 'get',
    params: { ...query, sheetName, fileName },
    responseType: 'blob'
  })
}
