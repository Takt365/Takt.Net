import { defineStore } from 'pinia'
import { ref } from 'vue'
import { login, getUserInfo, logout as logoutApi } from '@/api/identity/auth'
import type { LoginParams, UserInfo } from '@/types/identity/auth'
import { decodeHolidayFromToken, type HolidayFromToken } from '@/utils/jwt'
import { logger } from '@/utils/logger'
import { eventBus, AuthEvents } from '@/utils/eventBus'
const toErrorMessage = (error: unknown): string =>
  error instanceof Error ? error.message : String(error)

/**
 * 认证状态约定：
 * - Pinia（本 store）管理全局认证状态（token、refreshToken 等），为唯一数据源。
 * - localStorage 仅作为持久化层，由本 store 在 setAuth/clearAuth 时统一写入；
 *   应用启动时从 localStorage 水合到 Pinia，确保刷新后数据不丢失。
 * - 其他模块（如 request、signalr）仅读取 localStorage 的持久化数据，不写入。
 */
const TOKEN_KEY = 'token'
const REFRESH_TOKEN_KEY = 'refreshToken'
const TOKEN_EXPIRES_AT_KEY = 'tokenExpiresAt'

/** 持久化层：仅本 store 调用，写入 token/refreshToken/expiresAt 到 localStorage */
function persistAuth(token: string | null, refreshTokenVal: string | null, expiresAt: number | null) {
  if (typeof localStorage === 'undefined') return
  if (token != null) localStorage.setItem(TOKEN_KEY, token)
  else localStorage.removeItem(TOKEN_KEY)
  if (refreshTokenVal != null) localStorage.setItem(REFRESH_TOKEN_KEY, refreshTokenVal)
  else localStorage.removeItem(REFRESH_TOKEN_KEY)
  if (expiresAt != null) localStorage.setItem(TOKEN_EXPIRES_AT_KEY, String(expiresAt))
  else localStorage.removeItem(TOKEN_EXPIRES_AT_KEY)
}

export const useUserStore = defineStore('user', () => {
  const token = ref<string | null>(typeof localStorage !== 'undefined' ? localStorage.getItem(TOKEN_KEY) : null)
  const refreshToken = ref<string | null>(typeof localStorage !== 'undefined' ? localStorage.getItem(REFRESH_TOKEN_KEY) : null)
  const userInfo = ref<UserInfo | null>(null)
  /** 从 token 解析的今日假期信息（登录时写入，用于问候语与主色） */
  const holidayFromToken = ref<HolidayFromToken | null>(
    typeof localStorage !== 'undefined' ? decodeHolidayFromToken(localStorage.getItem(TOKEN_KEY)) : null
  )

  /** 仅更新内存中的 token 与假日信息（内部用） */
  function setToken(newToken: string | null) {
    token.value = newToken
    holidayFromToken.value = decodeHolidayFromToken(newToken)
    const h = holidayFromToken.value
    logger.info('[User Store] 假日信息已更新:', h ? `今日假日 ${h.holidayName}，主题 key=${h.holidayTheme}` : '非假日/无')
  }

  /**
   * 更新当前会话（token / refreshToken / 过期时间），并同步写入 localStorage 作为持久化备份。
   * 登录、刷新 token 时调用；调用方只需调此方法，无需再写 localStorage。
   */
  function setAuth(
    newToken: string,
    options?: { refreshToken?: string; expiresIn?: number }
  ) {
    setToken(newToken)
    if (options?.refreshToken != null) refreshToken.value = options.refreshToken
    const expiresAt =
      options?.expiresIn != null ? Date.now() + options.expiresIn * 1000 : null
    persistAuth(newToken, refreshToken.value ?? null, expiresAt)
    if (expiresAt != null) {
      logger.debug('[User Store] Token 过期时间已保存:', new Date(expiresAt).toLocaleString())
    }
  }

  /** 清空当前会话并移除持久化备份（登出时调用） */
  function clearAuth() {
    setToken(null)
    refreshToken.value = null
    persistAuth(null, null, null)
  }

  // 登录
  const loginAction = async (params: LoginParams) => {
    try {
      logger.info('[User Store] 开始登录，用户名:', params.username)
      const data = await login(params)
      const authExtra: { refreshToken?: string; expiresIn?: number } = {}
      if (data.refreshToken != null) authExtra.refreshToken = data.refreshToken
      if (data.expiresIn != null) authExtra.expiresIn = data.expiresIn
      setAuth(data.token, Object.keys(authExtra).length > 0 ? authExtra : undefined)
      eventBus.$emit(AuthEvents.LoginSuccess)
      logger.info('[User Store] 登录成功，用户名:', params.username, '用户ID:', data.userInfo?.userId)
      return data
    } catch (error: unknown) {
      logger.error('[User Store] 登录失败，用户名:', params.username, '错误:', toErrorMessage(error))
      throw error
    }
  }

  // 获取用户信息（userinfo 返回假日信息时同步到 holidayFromToken，因 access_token 可能为 JWE 无法前端解析）
  const getUserInfoAction = async () => {
    if (!token.value) {
      throw new Error('未登录')
    }
    const info = await getUserInfo()
    userInfo.value = info
    if (info.holidayToday === true && (info.holidayName != null || info.holidayGreeting != null || info.holidayQuote != null || info.holidayTheme != null)) {
      const key = (info.holidayTheme ?? '').replace(/\s+/g, ' ').replace(/[\u3000-\u303f\uff00-\uffef]/g, '').trim().toLowerCase()
      const fromUserInfo: HolidayFromToken = {
        isHolidayToday: true,
        holidayName: info.holidayName ?? '',
        holidayGreeting: info.holidayGreeting ?? '',
        holidayTheme: key
      }
      const quoteRaw = info.holidayQuote
      if (typeof quoteRaw === 'string' && quoteRaw.trim() !== '') {
        fromUserInfo.holidayQuote = quoteRaw.trim()
      }
      holidayFromToken.value = fromUserInfo
      logger.info('[User Store] 假日信息已从 userinfo 同步: ', info.holidayName, ', 主题 key=', key)
    } else {
      // 未返回假日或今日非假日时清空，避免切换语言/地区后仍显示旧假日（如 zh-CN 有假日、en-US 无假日）
      holidayFromToken.value = null
      logger.info('[User Store] userinfo 无假日信息，假日状态: 非假日/无')
    }
    if (import.meta.env.DEV) {
      logger.debug('[User Store] 获取用户信息成功:', {
        userId: info.userId,
        userName: info.userName,
        roles: info.roles,
        permissions: info.permissions,
        permissionsCount: info.permissions?.length || 0
      })
    }
    return info
  }

  // 登出
  const logout = async () => {
    const currentUser = userInfo.value?.userName || '未知用户'
    logger.info('[User Store] 开始退出登录，用户:', currentUser)
    
    try {
      // 发送 refreshToken 给后端（如果存在）
      if (refreshToken.value) {
        try {
          await logoutApi(refreshToken.value)
          logger.info('[User Store] 退出登录成功，用户:', currentUser, '后端已处理登出请求')
        } catch (error: unknown) {
          logger.error('[User Store] 退出登录失败，用户:', currentUser, '后端登出失败:', toErrorMessage(error))
          // 即使后端登出失败，也继续清理本地状态
        }
      } else {
        logger.warn('[User Store] 退出登录，用户:', currentUser, '没有 refreshToken，跳过后端登出')
      }
    } catch (error: unknown) {
      logger.error('[User Store] 退出登录异常，用户:', currentUser, '错误:', toErrorMessage(error))
      // 即使后端登出失败，也继续清理本地状态
    } finally {
      eventBus.$emit(AuthEvents.DidLogout)
      clearAuth()
      userInfo.value = null
      
      // 重置菜单路由（清除菜单和路由状态）
      const { useMenuStore } = await import('./menu')
      const menuStore = useMenuStore()
      menuStore.reset()
      
      // 重置权限状态
      const { usePermissionStore } = await import('./permission')
      const permissionStore = usePermissionStore()
      permissionStore.reset()
      
      // 断开 SignalR 连接
      try {
        const { useSignalRStore } = await import('./signalr')
        const signalRStore = useSignalRStore()
        if (signalRStore.isConnected) {
          await signalRStore.disconnect()
          logger.info('[User Store] 退出登录：已断开 SignalR 连接')
        }
      } catch (error: unknown) {
        logger.error('[User Store] 断开 SignalR 连接失败:', toErrorMessage(error))
        // SignalR 断开失败不影响退出登录流程
      }
    }
  }

  return {
    token,
    refreshToken,
    userInfo,
    holidayFromToken,
    setToken,
    setAuth,
    clearAuth,
    login: loginAction,
    getUserInfo: getUserInfoAction,
    logout
  }
})