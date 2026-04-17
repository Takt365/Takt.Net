// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/api/request
// 文件名称：request.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Axios 请求封装，提供统一的 API 请求处理和安全性保障
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import axios, { type AxiosResponse, type InternalAxiosRequestConfig } from 'axios'
import { message } from 'ant-design-vue'
import { eventBus, AuthEvents } from '@/utils/eventBus'
import { TaktResultCode } from '@/utils/enum'
import { showApiConnectFail, closeApiConnectFailNotification } from '@/utils/notification'
import { refreshToken as refreshTokenApi } from './identity/auth'
import i18n from '@/locales'

/** 获取 API 相关文案（request 在非组件中运行，使用 i18n.global.t） */
const t = (key: string) => (i18n.global.t as (k: string) => string)(key)

/**
 * Token 读取：Pinia（user store）管理全局认证状态，localStorage 为其持久化层（仅 store 写入）。
 * 本模块仅从 localStorage 读取，key 与 stores/identity/user 一致，确保刷新后请求仍能带 token。
 */
const TOKEN_KEY = 'token'
const REFRESH_TOKEN_KEY = 'refreshToken'

function getTokenFromStorage(): string | null {
  if (typeof localStorage === 'undefined') return null
  const t = localStorage.getItem(TOKEN_KEY)
  return typeof t === 'string' && t.length > 0 ? t : null
}

function getRefreshTokenFromStorage(): string | null {
  if (typeof localStorage === 'undefined') return null
  const r = localStorage.getItem(REFRESH_TOKEN_KEY)
  return typeof r === 'string' && r.length > 0 ? r : null
}

let _getToken: () => string | null = () => getTokenFromStorage()
let _getRefreshToken: () => string | null = () => getRefreshTokenFromStorage()
let _getExpiresAt: () => number | null = () => null
let _setToken: (token: string, refreshToken?: string, expiresIn?: number) => void = () => {}
export function setTokenGetter(fn: () => string | null): void { _getToken = fn }
export function setRefreshTokenGetter(fn: () => string | null): void { _getRefreshToken = fn }
export function setTokenSetter(fn: (token: string, refreshToken?: string, expiresIn?: number) => void): void { _setToken = fn }
export function setExpiresAtGetter(fn: () => number | null): void { _getExpiresAt = fn }

// ========================================
// 常量定义
// ========================================

/** CSRF Token Cookie 名称 */
const CSRF_TOKEN_COOKIE = 'CSRF-Token'

/** CSRF Token 请求头名称 */
const CSRF_TOKEN_HEADER = 'X-CSRF-Token'

/** 需要 CSRF 保护的 HTTP 方法 */
const PROTECTED_METHODS = ['POST', 'PUT', 'DELETE', 'PATCH'] as const

/** 默认请求超时时间（毫秒） */
const DEFAULT_TIMEOUT = 30000

/** 批量导入请求超时时间（毫秒，5 分钟） */
const IMPORT_TIMEOUT = 300000

/** 错误提示去重：记录最近显示的错误消息，避免短时间内重复显示 */
const recentErrorMessages = new Map<string, number>()
const ERROR_DEDUP_INTERVAL = 2000 // 2秒内相同错误消息只显示一次

/**
 * 检查错误消息是否应该显示（去重检查）
 * @param errorMessage 错误消息
 * @returns 是否应该显示
 */
function shouldShowErrorMessage(errorMessage: string): boolean {
  const now = Date.now()
  const lastShown = recentErrorMessages.get(errorMessage)
  
  if (lastShown && (now - lastShown) < ERROR_DEDUP_INTERVAL) {
    // 在去重时间间隔内，不显示
    return false
  }
  
  // 记录显示时间
  recentErrorMessages.set(errorMessage, now)
  
  // 清理过期的记录（超过10秒的记录）
  for (const [msg, timestamp] of recentErrorMessages.entries()) {
    if (now - timestamp > 10000) {
      recentErrorMessages.delete(msg)
    }
  }
  
  return true
}

/**
 * 显示错误提示（带去重，仅当 shouldShowErrorMessage 通过时展示）
 */
function showErrorOnce(msg: string): void {
  if (shouldShowErrorMessage(msg)) {
    message.error(msg)
  }
}

interface ApiResultLike {
  code?: number
  data?: unknown
  message?: string
  success?: boolean
}

interface ErrorResponseDataLike {
  message?: string
  error?: string
  title?: string
  detail?: string
  Message?: string
}

interface BusinessError extends Error {
  isBusinessError: true
  businessCode?: number
  validationErrors: unknown[]
  response: AxiosResponse
}

/**
 * 从 HTTP 响应中解析错误消息（兼容多种后端格式）
 */
function getHttpErrorMessage(response: { data?: unknown; statusText?: string } | undefined): string {
  const data = response?.data
  const dataObj = (typeof data === 'object' && data !== null ? data : undefined) as ErrorResponseDataLike | undefined
  return (
    (typeof data === 'string' && data.trim() !== '' ? data.trim() : null) ??
    dataObj?.Message ??
    dataObj?.message ??
    dataObj?.title ??
    dataObj?.detail ??
    dataObj?.error ??
    response?.statusText ??
    t('common.api.requestFail')
  )
}

// ========================================
// Axios 实例配置
// ========================================

const service = axios.create({
  baseURL: '', // 不使用 baseURL，因为所有 API 方法的 URL 都包含完整的 /api/xxx 路径
  timeout: Number(import.meta.env.VITE_API_TIMEOUT) || DEFAULT_TIMEOUT,
  withCredentials: true, // 允许发送 Cookie（用于 CSRF Token 和 Session）
  headers: {
    'Content-Type': 'application/json;charset=UTF-8'
  }
})

// ========================================
// 工具函数
// ========================================

/**
 * 从 Cookie 中安全读取 CSRF Token
 * @returns CSRF Token 或 null
 */
function getCsrfTokenFromCookie(): string | null {
  if (typeof document === 'undefined') {
    return null
  }

  try {
    const cookies = document.cookie.split(';')
    for (const cookie of cookies) {
      const trimmedCookie = cookie.trim()
      if (!trimmedCookie) {
        continue
      }

      const equalIndex = trimmedCookie.indexOf('=')
      if (equalIndex === -1) {
        continue
      }

      const name = trimmedCookie.substring(0, equalIndex).trim()
      const value = trimmedCookie.substring(equalIndex + 1).trim()

      if (name === CSRF_TOKEN_COOKIE && value) {
        // 安全解码，防止 XSS
        try {
          return decodeURIComponent(value)
        } catch {
          // 如果解码失败，返回原始值
          return value
        }
      }
    }
  } catch (error) {
    logger.error('[CSRF] 读取 CSRF Token 失败:', error)
  }

  return null
}

/**
 * 获取请求方法（标准化为大写）
 * @param config 请求配置
 * @returns 请求方法
 */
function getRequestMethod(config: InternalAxiosRequestConfig): string {
  return (config.method || 'GET').toUpperCase()
}

/**
 * 检查是否为受保护的 HTTP 方法
 * @param method HTTP 方法
 * @returns 是否为受保护的方法
 */
function isProtectedMethod(method: string): boolean {
  return (PROTECTED_METHODS as readonly string[]).includes(method)
}

/**
 * 检查是否为健康检查请求
 * @param url 请求 URL
 * @returns 是否为健康检查请求
 */
function isHealthCheckRequest(url?: string): boolean {
  if (!url) {
    return false
  }
  return url.includes('/api/TaktHealth') || url.includes('/TaktHealth')
}

/**
 * 是否正在刷新 token（防止并发刷新）
 */
let isRefreshing = false
/**
 * 是否正在跳转登录页（防止重复跳转）
 */
let isRedirectingToLogin = false
/**
 * 待重试的请求队列
 */
let failedQueue: Array<{
  resolve: (value?: unknown) => void
  reject: (error?: unknown) => void
  config: InternalAxiosRequestConfig
}> = []

/**
 * 处理待重试的请求队列
 */
function processQueue(error: unknown, token: string | null = null) {
  failedQueue.forEach((prom) => {
    if (error) {
      prom.reject(error)
    } else {
      if (token && prom.config.headers) {
        prom.config.headers.Authorization = `Bearer ${token}`
      }
      service.request(prom.config).then(prom.resolve).catch(prom.reject)
    }
  })
  failedQueue = []
}

/**
 * 尝试刷新 token
 * @returns 是否刷新成功
 */
export async function tryRefreshToken(): Promise<boolean> {
  const refreshToken = _getRefreshToken()
  if (!refreshToken) return false
  try {
    logger.info('[Token Refresh] 开始刷新 token')
    const result = await refreshTokenApi(refreshToken)
    _setToken(result.token, result.refreshToken, result.expiresIn)
    logger.info('[Token Refresh] Token 刷新成功')
    eventBus.$emit(AuthEvents.TokenRefreshed)
    return true
  } catch (error: unknown) {
    const errorMessage = error instanceof Error ? error.message : String(error)
    logger.error('[Token Refresh] Token 刷新失败:', errorMessage)
    return false
  }
}

/** 请求配置扩展：标记是否已重试过（防止 401 重试死循环） */
type ConfigWithRetry = InternalAxiosRequestConfig & { _retry?: boolean }

/** 401 时统一对用户展示的文案（避免出现「Token 刷新失败」等技术用语） */
const UNAUTHORIZED_USER_MESSAGE = () => t('common.api.loginExpired')

/**
 * 处理 401 未授权错误（统一处理逻辑）
 * @param errorMessage 错误消息（用于 UI 展示，如「登录已过期，请重新登录」）
 * @param config 请求配置（可选，用于重试）
 * @returns 刷新成功且传入 config 时返回重试请求的 Promise，否则 resolve/reject
 */
async function handleUnauthorized(errorMessage: string, config?: InternalAxiosRequestConfig): Promise<unknown> {
  logger.debug('[Auth] handleUnauthorized:', errorMessage)
  const configWithRetry = config as ConfigWithRetry | undefined
  if (isRedirectingToLogin) {
    return Promise.reject(new Error(t('common.api.redirectingToLogin')))
  }
  if (configWithRetry?._retry) {
    logger.warn('[Auth] 401 重试后仍失败，跳转登录页')
    isRedirectingToLogin = true
    showErrorOnce(UNAUTHORIZED_USER_MESSAGE())
    eventBus.$emit(AuthEvents.RedirectToLogin)
    return Promise.reject(new Error(UNAUTHORIZED_USER_MESSAGE()))
  }
  if (isRefreshing) {
    if (config) {
      return new Promise((resolve, reject) => {
        failedQueue.push({ resolve, reject, config })
      })
    }
    return
  }
  const hasRefreshToken = !!_getRefreshToken()
  if (!hasRefreshToken) {
    logger.warn('[Auth] 401 且无 refreshToken，直接跳转登录页')
    isRedirectingToLogin = true
    showErrorOnce(UNAUTHORIZED_USER_MESSAGE())
    eventBus.$emit(AuthEvents.RedirectToLogin)
    return Promise.reject(new Error(UNAUTHORIZED_USER_MESSAGE()))
  }
  isRefreshing = true
  const refreshSuccess = await tryRefreshToken()
  isRefreshing = false
  if (refreshSuccess) {
    const token = _getToken()
    processQueue(null, token)
    if (config) {
      if (config.headers && token) {
        (config.headers as Record<string, string>).Authorization = 'Bearer ' + token
      }
      (config as ConfigWithRetry)._retry = true
      return service.request(config)
    }
    return Promise.resolve()
  }
  const userMsg = UNAUTHORIZED_USER_MESSAGE()
  processQueue(new Error(userMsg))
  if (isRedirectingToLogin) {
    return Promise.reject(new Error(t('common.api.redirectingToLogin')))
  }
  isRedirectingToLogin = true
  showErrorOnce(userMsg)
  eventBus.$emit(AuthEvents.RedirectToLogin)
  return Promise.reject(new Error(userMsg))
}

/**
 * 统一处理 401：刷新 token 并可选重试原请求（业务 401 与 HTTP 401 共用）
 * 直接返回 handleUnauthorized 的 Promise，避免重复发起请求
 */
function handle401AndRetry(config?: InternalAxiosRequestConfig): Promise<unknown> {
  return handleUnauthorized(t('common.api.loginExpired'), config)
}

/** 启动 token 自动刷新定时器（由 main 订阅 LoginSuccess 后调用） */
export function startTokenRefreshTimer() {
  if (!_getToken()) return
  const doRefreshAndReschedule = () => {
    if (_getToken() && _getRefreshToken()) {
      tryRefreshToken().then((success) => success && startTokenRefreshTimer())
    }
  }
  const expiresAt = _getExpiresAt()
  if (expiresAt == null) {
    const defaultExpiresIn = 3600
    const refreshDelay = (defaultExpiresIn - 300) * 1000
    setTimeout(() => {
      logger.info('[Token Refresh] 定时刷新 token（默认过期时间）')
      doRefreshAndReschedule()
    }, refreshDelay)
    return
  }
  const now = Date.now()
  const refreshDelay = expiresAt - now - 5 * 60 * 1000
  if (refreshDelay <= 0) {
    logger.info('[Token Refresh] Token 即将过期，立即刷新')
    doRefreshAndReschedule()
    return
  }
  setTimeout(() => {
    logger.info('[Token Refresh] 定时刷新 token（提前 5 分钟）')
    doRefreshAndReschedule()
  }, refreshDelay)
  logger.debug(`[Token Refresh] Token 刷新定时器已设置，将在 ${Math.round(refreshDelay / 1000)} 秒后刷新`)
}

if (typeof window !== 'undefined') {
  setTimeout(() => {
    if (_getToken()) startTokenRefreshTimer()
  }, 1000)
}

// ========================================
// 请求拦截器
// ========================================

service.interceptors.request.use(
  (config: InternalAxiosRequestConfig) => {
    // 1. 添加认证 Token：优先读 localStorage（store 的持久化备份），再兜底注入的 getter
    const token = getTokenFromStorage() || _getToken()
    if (token) config.headers.Authorization = `Bearer ${token}`

    // 1.1 传递当前语言，供后端本地化（RequestCultureProviders 优先 QueryString → Cookie → Accept-Language）
    const locale = typeof localStorage !== 'undefined' ? (localStorage.getItem('locale') || 'zh-CN') : 'zh-CN'
    config.headers['Accept-Language'] = locale

    // 2. 处理受保护的 HTTP 方法（POST/PUT/DELETE/PATCH）
    const method = getRequestMethod(config)
    if (isProtectedMethod(method)) {
      // 2.1 确保 Content-Type 已设置
      if (!config.headers['Content-Type'] && !config.headers['content-type']) {
        config.headers['Content-Type'] = 'application/json;charset=UTF-8'
      }

      // 2.2 添加 CSRF Token
      const csrfToken = getCsrfTokenFromCookie()
      if (csrfToken) {
        config.headers[CSRF_TOKEN_HEADER] = csrfToken
      } else {
        // 开发环境下警告，生产环境下静默处理
        if (import.meta.env.DEV) {
          logger.warn('[CSRF] CSRF Token 未找到，请求可能被拒绝:', {
            url: config.url,
            method
          })
        }
      }
    }

    // 3. 批量导入接口使用更长超时
    const url = config.url || ''
    if (url.includes('/import')) {
      config.timeout = IMPORT_TIMEOUT
    }

    // 4. 输出请求日志（已脱敏）
    logger.apiRequest(method, url, {
      baseURL: config.baseURL || '',
      params: config.params,
      headers: config.headers
    })

    return config
  },
  (error) => {
    logger.error('[Request Error] 请求配置错误:', error)
    return Promise.reject(error)
  }
)

// ========================================
// 响应拦截器
// ========================================

/**
 * 从响应头中读取单个字段（大小写不敏感）。
 * 不调用 AxiosHeaders.prototype.get：在部分 CORS / 适配器场景下 AxiosHeaders 内部 map 异常时，
 * axios 内部 findKey 会对 null/undefined 执行 Object.keys，抛出 “Cannot convert undefined or null to object”。
 */
function safeGetResponseHeader(
  headers: AxiosResponse['headers'] | undefined | null,
  headerName: string
): string | undefined {
  if (headers == null) return undefined
  const target = headerName.toLowerCase()

  try {
    const maybeJson = (headers as { toJSON?: () => unknown }).toJSON
    if (typeof maybeJson === 'function') {
      const j = maybeJson.call(headers)
      if (j && typeof j === 'object' && !Array.isArray(j)) {
        const jo = j as Record<string, unknown>
        for (const k of Object.keys(jo)) {
          if (k.toLowerCase() === target) {
            const v = jo[k]
            return v == null ? undefined : String(v)
          }
        }
      }
    }
  } catch {
    /* 忽略，走下方遍历 */
  }

  try {
    const h = headers as Record<string, unknown>
    for (const k of Object.keys(h)) {
      if (k.toLowerCase() === target) {
        const v = h[k]
        return v == null ? undefined : String(v)
      }
    }
  } catch {
    return undefined
  }

  return undefined
}

service.interceptors.response.use(
  (response: AxiosResponse) => {
    const cfg = response.config
    if (cfg.blobWithHeaders && cfg.responseType === 'blob' && response.status === 200 && response.data instanceof Blob) {
      const hdrs = response.headers
      return {
        blob: response.data,
        contentDisposition: safeGetResponseHeader(hdrs, 'content-disposition'),
        contentType: safeGetResponseHeader(hdrs, 'content-type')
      }
    }

    const res = response.data
    const { url, method } = response.config || {}
    const requestMethod = method ? method.toUpperCase() : 'UNKNOWN'

    // 如果响应数据是 null 或 undefined，直接返回
    if (res == null) {
      logger.apiResponse(response.status, requestMethod, url || '', null)
      return res
    }

    // 后端已统一转换为 camelCase，直接使用 camelCase 格式
    const apiResult = (typeof res === 'object' && res !== null ? res : {}) as ApiResultLike
    const code = apiResult.code
    const data = apiResult.data
    const messageText = apiResult.message ?? ''
    const success = apiResult.success ?? (code === TaktResultCode.Success || code === 200)

    // 检查是否为标准的 TaktApiResult 格式
    const isApiResultFormat = code !== undefined && (messageText !== undefined || data !== undefined)

    // 如果返回的状态码为 200 且业务代码也为成功，说明接口请求成功
    if (response.status === 200 && (code === TaktResultCode.Success || code === 200 || success)) {
      closeApiConnectFailNotification()
      logger.apiResponse(response.status, requestMethod, url || '', data !== undefined ? data : res, messageText)
      // 如果存在 Data 字段（PascalCase 或 camelCase），返回 Data，否则返回整个响应
      return data !== undefined ? data : res
    }

    // 业务错误处理（status 200 但业务 code 表示失败）
    // 仅当 code 为数字（TaktResultCode）时按统一业务错误 reject；code 为字符串（如忘记密码的 ProtectedUser/EmailNotFound）时返回 res 由调用方处理
    if (isApiResultFormat && typeof code === 'number') {
      const errorMessage = messageText || t('common.api.requestFail')
      logger.apiResponse(response.status, requestMethod, url || '', data, errorMessage)
      
      const businessError = new Error(errorMessage) as BusinessError
      businessError.isBusinessError = true
      businessError.businessCode = code
      const dataObj = (typeof data === 'object' && data !== null ? data : undefined) as { validationErrors?: unknown[] } | undefined
      businessError.validationErrors = Array.isArray(dataObj?.validationErrors) ? dataObj.validationErrors : []
      businessError.response = response
      return Promise.reject(businessError)
    }

    // 非标准格式，直接返回
    logger.apiResponse(response.status, requestMethod, url || '', res)
    return res
  },
  async (error) => {
    // 请求被取消
    if (axios.isCancel(error)) {
      logger.warn('[Request Cancelled] 请求已取消:', error.message)
      return Promise.reject(error)
    }

    // 检查是否为健康检查请求（健康检查的错误会在 App.vue 中统一处理，这里不显示 UI 提示，但仍输出日志）
    const config = error.config || error.response?.config
    const isHealthCheck = isHealthCheckRequest(config?.url)
    const shouldShowError = !isHealthCheck // 健康检查不在这里显示 UI 错误提示
    
    // 如果是健康检查请求，输出错误日志（便于 npm run dev 窗口查看）
    if (isHealthCheck) {
      logger.healthCheckError(error)
    }

    // 处理业务错误（status 200 但业务code表示失败）
    if (error.isBusinessError && error.response) {
      const businessCode = error.businessCode
      const errorMessage = error.message || t('common.api.requestFail')

      if (shouldShowError) {
        if (businessCode === TaktResultCode.Unauthorized) {
          logger.warn('[Auth] 业务错误 401，尝试刷新 token')
          try {
            return await handle401AndRetry(error.config)
          } catch (refreshError) {
            return Promise.reject(refreshError)
          }
        } else if (businessCode === TaktResultCode.Forbidden) {
          showErrorOnce(t('common.api.forbidden'))
        } else if (businessCode === TaktResultCode.NotFound) {
          showErrorOnce(t('common.api.notFound'))
        } else if (businessCode === TaktResultCode.ServerError) {
          showErrorOnce(t('common.api.serverError'))
        } else if (businessCode === TaktResultCode.SystemError) {
          const msg = errorMessage || t('common.api.systemError')
          logger.error('[System Error] 系统内部错误:', {
            businessCode,
            errorMessage,
            url: config?.url ?? '',
            method: (config?.method ?? 'GET').toString().toUpperCase(),
            responseData: error.response?.data,
            fullResponse: error.response
          })
          showErrorOnce(msg)
        } else {
          showErrorOnce(errorMessage)
        }
      }
      
      return Promise.reject(error)
    }

    // 有响应但状态码错误（HTTP 状态码不是 200）
    if (error.response) {
      const { status, config, data } = error.response
      const { url, method } = config || {}
      const requestMethod = method ? method.toUpperCase() : 'UNKNOWN'
      const errorMessage = getHttpErrorMessage(error.response)

      logger.apiError(status, requestMethod, url || '', { statusText: error.response.statusText, response: { data } })

      if (shouldShowError) {
        switch (status) {
          case 401:
            logger.warn('[Auth] 收到 401 错误，尝试刷新 token')
            try {
              return await handle401AndRetry(config)
            } catch (refreshError) {
              return Promise.reject(refreshError)
            }

          case 403: {
            const isCsrfError =
              errorMessage?.toLowerCase().includes('csrf') ||
              data?.message?.toLowerCase().includes('csrf') ||
              data?.error?.toLowerCase().includes('csrf')
            if (isCsrfError) {
              logger.error('[CSRF] CSRF 验证失败:', { url, method: requestMethod })
              showErrorOnce(t('common.api.csrfFail'))
            } else {
              logger.warn('[Permission] 无权访问:', { url, method: requestMethod })
              showErrorOnce(t('common.api.forbidden'))
            }
            break
          }

          case 404:
            logger.warn('[Not Found] 请求的资源不存在:', { url, method: requestMethod })
            showErrorOnce(t('common.api.notFound'))
            break

          case 500:
          case 502:
          case 503:
          case 504:
            logger.error('[Server Error] 服务器错误:', { status, url, method: requestMethod, message: errorMessage })
            showErrorOnce(t('common.api.serverError'))
            break

          default: {
            const apiErrorSummary = `${status} ${requestMethod} ${url || '(no url)'} - ${errorMessage}`
            logger.error('[API Error] 请求失败:', apiErrorSummary, { status, url, method: requestMethod, message: errorMessage })
            showErrorOnce(errorMessage)
          }
        }
      }
    } else if (error.request || error.code === 'ECONNREFUSED' || error.code === 'ENOTFOUND' || error.code === 'ETIMEDOUT' || error.code === 'ERR_NETWORK' || error.message?.includes('Network Error')) {
      // 请求已发出但没有收到响应（网络错误），统一在此处理：提示 + 跳转登录（含健康检查失败）
      const { url, method } = config || {}
      const requestMethod = method ? method.toUpperCase() : 'UNKNOWN'
      logger.networkError(requestMethod, url || '', error)

      showApiConnectFail()
      if (!isRedirectingToLogin) {
        isRedirectingToLogin = true
        eventBus.$emit(AuthEvents.RedirectToLogin)
      }
    } else {
      logger.error('[Request Error] 请求配置错误:', error.message)
      if (shouldShowError) showErrorOnce(t('common.api.requestConfigError'))
    }

    return Promise.reject(error)
  }
)

// ========================================
// 导出
// ========================================

/** Blob 下载接口返回类型（responseType: 'blob' 时 Axios 返回 Blob） */
export type BlobDownloadResult = Blob

/** 带响应头元数据的 Blob（请求配置 blobWithHeaders: true 时由拦截器注入） */
export interface BlobDownloadWithMeta {
  blob: Blob
  contentDisposition?: string
  contentType?: string
}

/**
 * 重置跳转登录页标志（登录成功后调用）
 */
export function resetRedirectingToLoginFlag() {
  isRedirectingToLogin = false
}

export default service