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

import request from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type { QuartzLog, QuartzLogQuery } from '@/types/statistics/logging/quartz-log'

const quartzLogUrl = '/api/TaktQuartzLogs'

/**
 * 获取任务日志列表（分页）
 * 对应后端应用服务：GetQuartzLogListAsync（TaktQuartzLogsController.GetQuartzLogListAsync）
 */
export function getQuartzLogList(params: QuartzLogQuery): Promise<TaktPagedResult<QuartzLog>> {
  return request({
    url: `${quartzLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取任务日志
 * 对应后端应用服务：GetQuartzLogByIdAsync（TaktQuartzLogsController.GetQuartzLogByIdAsync）
 */
export function getQuartzLogById(id: string): Promise<QuartzLog> {
  return request({
    url: `${quartzLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 删除任务日志（单条）
 * 对应后端应用服务：DeleteQuartzLogByIdAsync（TaktQuartzLogsController.DeleteQuartzLogByIdAsync）
 */
export function deleteQuartzLogById(id: string): Promise<void> {
  return request({
    url: `${quartzLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除任务日志
 * 对应后端应用服务：DeleteQuartzLogBatchAsync（TaktQuartzLogsController.DeleteQuartzLogBatchAsync）
 */
export function deleteQuartzLogBatch(ids: number[]): Promise<void> {
  return request({
    url: `${quartzLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 导出任务日志
 * 对应后端应用服务：ExportQuartzLogAsync（TaktQuartzLogsController.ExportQuartzLogAsync）
 */
export function exportQuartzLogData(query: QuartzLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${quartzLogUrl}/export`,
    method: 'get',
    params: { ...query, sheetName, fileName },
    responseType: 'blob'
  })
}
