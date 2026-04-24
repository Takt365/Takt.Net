// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/api/logging/aop-log
// 文件名称：aop-log.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：差异日志相关 API，对应后端 TaktAopLogsController
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type { AopLog, AopLogQuery } from '@/types/statistics/logging/aop-log'

const aopLogUrl = '/api/TaktAopLogs'

/**
 * 获取差异日志列表（分页）
 * 对应后端应用服务：GetAopLogListAsync（TaktAopLogsController.GetAopLogListAsync）
 */
export function getAopLogList(params: AopLogQuery): Promise<TaktPagedResult<AopLog>> {
  return request({
    url: `${aopLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取差异日志
 * 对应后端应用服务：GetAopLogByIdAsync（TaktAopLogsController.GetAopLogByIdAsync）
 */
export function getAopLogById(id: string): Promise<AopLog> {
  return request({
    url: `${aopLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 删除差异日志（单条）
 * 对应后端应用服务：DeleteAopLogByIdAsync（TaktAopLogsController.DeleteAopLogByIdAsync）
 */
export function deleteAopLogById(id: string): Promise<void> {
  return request({
    url: `${aopLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除差异日志
 * 对应后端应用服务：DeleteAopLogBatchAsync（TaktAopLogsController.DeleteAopLogBatchAsync）
 */
export function deleteAopLogBatch(ids: number[]): Promise<void> {
  return request({
    url: `${aopLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 导出差异日志
 * 对应后端应用服务：ExportAopLogAsync（TaktAopLogsController.ExportAopLogAsync）
 */
export function exportAopLogData(query: AopLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${aopLogUrl}/export`,
    method: 'get',
    params: { ...query, sheetName, fileName },
    responseType: 'blob'
  })
}
