// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/identity/health
// 文件名称：health.d.ts
// 创建时间：2026-05-03
// 创建人：Takt365
// 功能描述：健康检查相关类型定义
// ========================================

/**
 * HealthCheck类型（健康检查基础响应）
 */
export interface HealthCheck {
  /** 服务名称 */
  serviceName: string
  /** 服务状态（Healthy/Unhealthy/Degraded） */
  status: string
  /** 检查时间 */
  checkTime: string
  /** 运行时间（秒） */
  uptime: number
  /** 版本号 */
  version: string
}

/**
 * HealthCheckDetailed类型（详细健康检查响应）
 */
export interface HealthCheckDetailed {
  /** 服务名称 */
  serviceName: string
  /** 服务状态（Healthy/Unhealthy/Degraded） */
  status: string
  /** 检查时间 */
  checkTime: string
  /** 运行时间（秒） */
  uptime: number
  /** 版本号 */
  version: string
  
  /** 系统信息 */
  systemInfo: {
    /** 操作系统 */
    os: string
    /** 架构 */
    architecture: string
    /** CPU使用率（百分比） */
    cpuUsage: number
    /** 内存使用（MB） */
    memoryUsage: number
    /** 总内存（MB） */
    totalMemory: number
    /** 磁盘使用（MB） */
    diskUsage: number
    /** 总磁盘（MB） */
    totalDisk: number
  }
  
  /** 数据库状态 */
  database: {
    /** 数据库类型 */
    provider: string
    /** 连接状态 */
    status: string
    /** 响应时间（毫秒） */
    responseTime: number
  }
  
  /** 缓存状态 */
  cache: {
    /** 缓存类型 */
    provider: string
    /** 连接状态 */
    status: string
    /** 响应时间（毫秒） */
    responseTime: number
  }
  
  /** 组件健康状态 */
  components: Record<string, {
    /** 组件名称 */
    name: string
    /** 组件状态 */
    status: string
    /** 描述信息 */
    description?: string
    /** 响应时间（毫秒） */
    responseTime?: number
  }>
}

/**
 * HealthCheckSignalR类型（SignalR健康检查响应）
 */
export interface HealthCheckSignalR {
  /** 服务名称 */
  serviceName: string
  /** 服务状态（Healthy/Unhealthy/Degraded） */
  status: string
  /** 检查时间 */
  checkTime: string
  
  /** SignalR状态 */
  signalR: {
    /** 是否启用 */
    enabled: boolean
    /** 连接状态 */
    status: string
    /** Hub名称 */
    hubName: string
    /** 当前连接数 */
    activeConnections: number
    /** 最大连接数 */
    maxConnections: number
    /** 协议 */
    protocols: string[]
    /** 传输方式 */
    transports: string[]
  }
  
  /** 组件健康状态 */
  components: Record<string, {
    /** 组件名称 */
    name: string
    /** 组件状态 */
    status: string
    /** 描述信息 */
    description?: string
  }>
}
