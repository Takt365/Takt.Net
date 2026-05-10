// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/statistics/logging
// 文件名称：server-monitor.ts
// 功能描述：服务器监控 API，对应后端 Takt.WebApi.Controllers.Statistics.Logging.TaktServerMonitorsController
// ========================================

import request from '@/api/request'
import type { TaktServerHardwareDto, TaktAppStatusDto } from '@/types/statistics/logging/server-monitor'

// 重新导出类型，方便视图文件使用
export type { TaktServerHardwareDto, TaktAppStatusDto }

// ========================================
// ServerMonitor 相关 API（特殊管理接口）
// ========================================
const serverMonitorUrl = 'api/TaktServerMonitors'

/**
 * 获取服务器硬件信息
 * 对应后端：GetServerHardwareAsync
 */
export function getServerHardware(): Promise<TaktServerHardwareDto> {
  return request({
    url: `${serverMonitorUrl}/hardware`,
    method: 'get'
  })
}

/**
 * 获取应用运行状态
 * 对应后端：GetAppStatusAsync
 */
export function getAppStatus(): Promise<TaktAppStatusDto> {
  return request({
    url: `${serverMonitorUrl}/app-status`,
    method: 'get'
  })
}

/**
 * 刷新硬件信息缓存
 * 对应后端：RefreshHardwareCache
 */
export function refreshHardwareCache(): Promise<{ message: string }> {
  return request({
    url: `${serverMonitorUrl}/refresh-cache`,
    method: 'post'
  })
}
