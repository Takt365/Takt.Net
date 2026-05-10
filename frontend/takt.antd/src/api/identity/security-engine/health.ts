// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/api/identity/security-engine
// 文件名称：health.ts
// 功能描述：健康检查API，对应后端 TaktHealthsController
// 路由前缀：api/TaktHealths
// ========================================

import request from '@/api/request'

/** 健康状态响应 */
export interface HealthStatus {
  status: 'healthy' | 'unhealthy'
  timestamp: string
  version: string
  environment?: string
  machineName?: string
  osVersion?: string
  processorCount?: number
  workingSet?: number
  uptime?: string
  signalR?: {
    connectHub: string
    notificationHub: string
  }
  error?: string
}

/** 详细健康状态响应 */
export interface DetailedHealthStatus extends HealthStatus {
  environment: string
  machineName: string
  osVersion: string
  processorCount: number
  workingSet: number
  uptime: string
}

/** SignalR健康状态 */
export interface SignalRHealthStatus extends HealthStatus {
  signalR: {
    connectHub: string
    notificationHub: string
  }
}

// ========================================
// 健康检查API
// ========================================
const healthUrl = '/api/TaktHealths'

/**
 * 健康检查
 * 对应后端：Check
 */
export function check(): Promise<HealthStatus> {
  return request({
    url: healthUrl,
    method: 'get'
  })
}

/**
 * 详细健康检查（包含更多系统信息）
 * 对应后端：Detailed
 */
export function detailed(): Promise<DetailedHealthStatus> {
  return request({
    url: `${healthUrl}/detailed`,
    method: 'get'
  })
}

/**
 * SignalR健康检查
 * 对应后端：SignalR
 */
export function signalR(): Promise<SignalRHealthStatus> {
  return request({
    url: `${healthUrl}/signalr`,
    method: 'get'
  })
}