// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/api/logging/oper-log
// 文件名称：oper-log.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：操作日志相关 API，对应后端 TaktOperLogsController
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type { OperLog, OperLogQuery } from '@/types/statistics/logging/oper-log'

const operLogUrl = '/api/TaktOperLogs'

/**
 * 获取操作日志列表（分页）
 * 对应后端应用服务：GetOperLogListAsync（TaktOperLogsController.GetOperLogListAsync）
 */
export function getOperLogList(params: OperLogQuery): Promise<TaktPagedResult<OperLog>> {
  return request({
    url: `${operLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取操作日志
 * 对应后端应用服务：GetOperLogByIdAsync（TaktOperLogsController.GetOperLogByIdAsync）
 */
export function getOperLogById(id: string): Promise<OperLog> {
  return request({
    url: `${operLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 删除操作日志（单条）
 * 对应后端应用服务：DeleteOperLogByIdAsync（TaktOperLogsController.DeleteOperLogByIdAsync）
 */
export function deleteOperLogById(id: string): Promise<void> {
  return request({
    url: `${operLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除操作日志
 * 对应后端应用服务：DeleteOperLogBatchAsync（TaktOperLogsController.DeleteOperLogBatchAsync）
 */
export function deleteOperLogBatch(ids: number[]): Promise<void> {
  return request({
    url: `${operLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 导出操作日志
 * 对应后端应用服务：ExportOperLogAsync（TaktOperLogsController.ExportOperLogAsync）
 */
export function exportOperLogData(query: OperLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${operLogUrl}/export`,
    method: 'get',
    params: { ...query, sheetName, fileName },
    responseType: 'blob'
  })
}
