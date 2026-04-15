// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/utils/logger
// 文件名称：logger.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：前端统一的日志处理工具（基于 Consola）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import { consola, type ConsolaInstance } from 'consola'

/**
 * 安全获取开发环境标识
 * 在 Node.js 环境（如 vite.config.ts）中，import.meta.env 可能不存在
 */
function isDev(): boolean {
  try {
    // 浏览器环境：使用 import.meta.env.DEV
    if (typeof import.meta !== 'undefined' && import.meta.env) {
      return import.meta.env.DEV ?? true
    }
  } catch {
    // 忽略错误
  }
  
  // Node.js 环境：使用 process.env.NODE_ENV
  try {
    if (typeof process !== 'undefined' && process.env) {
      return process.env.NODE_ENV !== 'production'
    }
  } catch {
    // 忽略错误
  }
  
  // 默认返回 true（开发模式）
  return true
}

/**
 * 敏感字段列表（用于日志脱敏）
 */
const SENSITIVE_FIELDS = ['password', 'token', 'authorization', 'csrf', 'cookie', 'secret', 'key'] as const

/**
 * 检查字段名是否为敏感字段
 * @param fieldName 字段名
 * @returns 是否为敏感字段
 */
function isSensitiveField(fieldName: string): boolean {
  const lowerFieldName = fieldName.toLowerCase()
  return SENSITIVE_FIELDS.some(field => lowerFieldName.includes(field.toLowerCase()))
}

/**
 * 脱敏处理对象（用于日志输出）
 * @param obj 要脱敏的对象
 * @returns 脱敏后的对象
 */
export function sanitizeForLogging(obj: any): any {
  if (obj === null || obj === undefined) {
    return obj
  }

  if (typeof obj !== 'object') {
    return obj
  }

  if (Array.isArray(obj)) {
    return obj.map(item => sanitizeForLogging(item))
  }

  const sanitized: Record<string, any> = {}
  for (const [key, value] of Object.entries(obj)) {
    if (isSensitiveField(key)) {
      sanitized[key] = '***'
    } else if (typeof value === 'object' && value !== null) {
      sanitized[key] = sanitizeForLogging(value)
    } else {
      sanitized[key] = value
    }
  }

  return sanitized
}

/**
 * 创建带标签的日志实例
 */
const createScopedLogger = (tag: string): ConsolaInstance => {
  return consola.withTag(tag)
}

/**
 * API 日志工具类（扩展 Consola 功能）
 */
class ApiLogger {
  private logger: ConsolaInstance

  constructor(tag: string = 'API') {
    this.logger = createScopedLogger(tag)
  }

  /**
   * API 请求日志
   */
  apiRequest(method: string, url: string, config?: any): void {
    if (isDev()) {
      this.logger.debug('[API Request]', {
        method,
        url,
        baseURL: config?.baseURL || '',
        params: sanitizeForLogging(config?.params),
        hasAuth: !!config?.headers?.Authorization,
        hasCsrfToken: !!config?.headers?.['X-CSRF-Token']
      })
    }
  }

  /**
   * API 响应日志
   */
  apiResponse(status: number, method: string, url: string, data?: any, message?: string): void {
    if (isDev()) {
      const logData = {
        status,
        code: data?.code || data?.Code || null,
        url,
        method,
        data: sanitizeForLogging(data),
        message: message || null
      }

      if (status >= 400) {
        this.logger.error('[API Response]', logData)
      } else {
        this.logger.debug('[API Response]', logData)
      }
    }
  }

  /**
   * API 错误日志
   */
  apiError(status: number, method: string, url: string, error: any): void {
    const message = error?.message || error?.response?.data?.message || '请求失败'
    const summary = `${status} ${method} ${url} - ${message}`
    this.logger.error('[API Error]', summary, {
      status,
      statusText: error?.statusText || null,
      url,
      method,
      message,
      data: sanitizeForLogging(error?.response?.data)
    })
  }

  /**
   * 网络错误日志
   */
  networkError(method: string, url: string, error: any): void {
    this.logger.error('[Network Error]', {
      message: error?.message || '网络错误',
      code: error?.code,
      url,
      method
    })
  }

  /**
   * 健康检查错误日志
   */
  healthCheckError(error: any): void {
    this.logger.error('[Health Check Error]', {
      message: error?.message || '健康检查失败',
      code: error?.code
    })
  }
}

/**
 * 默认日志实例（基于 Consola）
 * 配置：开发环境显示所有日志，生产环境只显示 warn 和 error
 */
const baseLogger = consola.create({
  level: isDev() ? 4 : 2, // 开发环境: debug, 生产环境: warn
  defaults: {
    tag: 'Takt'
  },
  formatOptions: {
    colors: true,
    date: true
  }
})

/**
 * 默认 API 日志实例
 */
const apiLogger = new ApiLogger('API')

/**
 * 统一日志导出接口（兼容原有代码）
 */
export const logger = {
  // 基础日志方法（直接使用 Consola）
  debug: baseLogger.debug.bind(baseLogger),
  info: baseLogger.info.bind(baseLogger),
  warn: baseLogger.warn.bind(baseLogger),
  error: baseLogger.error.bind(baseLogger),
  success: baseLogger.success.bind(baseLogger),

  // API 专用方法
  apiRequest: apiLogger.apiRequest.bind(apiLogger),
  apiResponse: apiLogger.apiResponse.bind(apiLogger),
  apiError: apiLogger.apiError.bind(apiLogger),
  networkError: apiLogger.networkError.bind(apiLogger),
  healthCheckError: apiLogger.healthCheckError.bind(apiLogger),

  // 创建带标签的日志实例（Consola 原生功能）
  withTag: (tag: string) => createScopedLogger(tag),

  // 导出原始 Consola 实例供高级用法
  consola: baseLogger
}

/**
 * 导出类型
 */
export type { ConsolaInstance }

/**
 * 导出 ApiLogger 类供扩展使用
 */
export { ApiLogger }

// ========================================
// Vite 插件：开发服务器终端日志
// ========================================

/**
 * 格式化日期时间：YYYY-MM-DD HH:mm:ss
 */
function formatDateTime(date: Date = new Date()): string {
  const year = date.getFullYear()
  const month = String(date.getMonth() + 1).padStart(2, '0')
  const day = String(date.getDate()).padStart(2, '0')
  const hours = String(date.getHours()).padStart(2, '0')
  const minutes = String(date.getMinutes()).padStart(2, '0')
  const seconds = String(date.getSeconds()).padStart(2, '0')
  return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}`
}

/**
 * Vite 插件：在开发服务器终端输出 API 请求和响应日志
 * 格式：[YYYY-MM-DD HH:mm:ss] [状态码 状态] 方法 URL (耗时ms)
 */
export function vitePluginLogger(): import('vite').Plugin {
  return {
    name: 'vite-plugin-logger',
    configureServer(server: import('vite').ViteDevServer) {
      // 在所有中间件之前插入日志中间件
      server.middlewares.use((req, res, next) => {
        const startTime = Date.now()
        const { method, url } = req

        // 只记录 API 请求，跳过静态资源
        if (!url?.startsWith('/api')) {
          return next()
        }

        // 请求完成时触发
        res.on('finish', () => {
          const duration = Date.now() - startTime
          const statusCode = res.statusCode
          const statusText = statusCode >= 400 ? `${statusCode} Error` : `${statusCode} OK`
          const timestamp = formatDateTime()

          console.log(`[${timestamp}] [${statusText}] ${method} ${url} (${duration}ms)`)
        })

        // 响应错误（如连接失败、异常）
        res.on('error', (err) => {
          const duration = Date.now() - startTime
          const timestamp = formatDateTime()
          console.error(`[${timestamp}] [500 Error] ${method} ${url} - ${err.message} (${duration}ms)`)
        })

        next()
      })
    }
  }
}
