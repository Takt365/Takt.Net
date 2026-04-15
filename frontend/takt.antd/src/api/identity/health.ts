// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/api/identity/health
// 文件名称：health.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：健康检查 API，对应后端 TaktHealthController
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import request from '@/api/request'
import type { HealthCheck, HealthCheckDetailed, HealthCheckSignalR } from '@/types/identity/health'

const healthUrl = '/api/TaktHealth'

/**
 * 健康检查
 * @returns 健康状态
 */
export function check(): Promise<HealthCheck> {
  return request({
    url: healthUrl,
    method: 'get'
  })
}

/**
 * 详细健康检查（包含更多系统信息）
 * @returns 详细健康状态
 */
export function checkDetailed(): Promise<HealthCheckDetailed> {
  return request({
    url: `${healthUrl}/detailed`,
    method: 'get'
  })
}

/**
 * SignalR健康检查
 * @returns SignalR健康状态
 */
export function checkSignalR(): Promise<HealthCheckSignalR> {
  return request({
    url: `${healthUrl}/signalr`,
    method: 'get'
  })
}
