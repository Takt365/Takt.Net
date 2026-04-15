// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/identity/health
// 文件名称：health.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：健康检查类型定义
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

/**
 * 健康检查响应（对应后端健康检查接口）
 */
export interface HealthCheck {
  /** 健康状态 */
  status: 'healthy' | 'unhealthy'
  /** 时间戳 */
  timestamp: string
  /** 版本号 */
  version: string
}

/**
 * 详细健康检查响应
 */
export interface HealthCheckDetailed extends HealthCheck {
  /** 环境名称 */
  environment: string
  /** 机器名称 */
  machineName: string
  /** 操作系统版本 */
  osVersion: string
  /** 处理器数量 */
  processorCount: number
  /** 工作集内存（字节） */
  workingSet: number
  /** 运行时间 */
  uptime: string
}

/**
 * SignalR 健康检查响应
 */
export interface HealthCheckSignalR {
  /** 健康状态 */
  status: 'healthy' | 'unhealthy'
  /** 时间戳 */
  timestamp: string
  /** SignalR 状态信息 */
  signalR: {
    /** 连接Hub状态 */
    connectHub: 'available' | 'unavailable'
    /** 通知Hub状态 */
    notificationHub: 'available' | 'unavailable'
  }
  /** 错误信息（如果状态为 unhealthy） */
  error?: string
}
