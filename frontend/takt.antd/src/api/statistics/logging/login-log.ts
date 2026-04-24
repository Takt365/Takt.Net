// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/api/logging/login-log
// 文件名称：login-log.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：登录日志相关 API，对应后端 TaktLoginLogsController
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '@/api/request'
import type { TaktPagedResult } from '@/types/common'
import type { LoginLog, LoginLogQuery } from '@/types/statistics/logging/login-log'

const loginLogUrl = '/api/TaktLoginLogs'

/**
 * 获取登录日志列表（分页）
 * 对应后端应用服务：GetLoginLogListAsync（TaktLoginLogsController.GetLoginLogListAsync）
 */
export function getLoginLogList(params: LoginLogQuery): Promise<TaktPagedResult<LoginLog>> {
  return request({
    url: `${loginLogUrl}/list`,
    method: 'get',
    params
  })
}

/**
 * 根据ID获取登录日志
 * 对应后端：GetByIdAsync
 */
export function getLoginLogById(id: string): Promise<LoginLog> {
  return request({
    url: `${loginLogUrl}/${id}`,
    method: 'get'
  })
}

/**
 * 删除登录日志（单条）
 * 对应后端应用服务：DeleteLoginLogByIdAsync（TaktLoginLogsController.DeleteLoginLogByIdAsync）
 */
export function deleteLoginLogById(id: string): Promise<void> {
  return request({
    url: `${loginLogUrl}/${id}`,
    method: 'delete'
  })
}

/**
 * 批量删除登录日志
 * 对应后端：DeleteBatchAsync
 */
export function deleteLoginLogBatch(ids: number[]): Promise<void> {
  return request({
    url: `${loginLogUrl}/batch`,
    method: 'delete',
    data: ids
  })
}

/**
 * 导出登录日志
 * 对应后端应用服务：ExportLoginLogAsync（TaktLoginLogsController.ExportLoginLogAsync）
 */
export function exportLoginLogData(query: LoginLogQuery, sheetName?: string, fileName?: string): Promise<Blob> {
  return request({
    url: `${loginLogUrl}/export`,
    method: 'get',
    params: { ...query, sheetName, fileName },
    responseType: 'blob'
  })
}
