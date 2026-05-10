// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/stores/identity/user
// 文件名称：user.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：用户认证状态管理 Store，管理登录、登出、Token、用户信息等
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import { defineStore } from 'pinia'
import { login, getUserInfo, logout as logoutApi } from '@/api/identity/security-engine/auth'
import type { HolidayFromToken, LoginParams, UserInfoWithHoliday } from '@/types/common'
import type { Holiday } from '@/types/human-resource/attendance-leave/holiday'
import { useSettingStore, themeColorMap, type ThemeColor } from '@/stores/setting'
import { applySettings } from '@/utils/apply-settings'
import { logger } from '@/utils/logger'
import { eventBus, AuthEvents } from '@/utils/eventBus'
import i18n, { loadTranslationsFromBackend } from '@/locales'
import { getHolidayTheme } from '@/api/human-resource/attendance-leave/specific-engine/attendance-leave'
import { useLocaleStore } from '@/stores/routine/localization/locale'
import { useDictDataStore } from '@/stores/routine/dict/dict-data'
const toErrorMessage = (error: unknown): string =>
  error instanceof Error ? error.message : String(error)
type ThemePreset = Exclude<ThemeColor, 'custom'>

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
  const userInfo = ref<UserInfoWithHoliday | null>(null)
  /** 今日假日展示信息（优先 `/api/TaktHolidays/theme`；userinfo 若显式返回假日则覆盖） */
  const holidayFromToken = ref<HolidayFromToken | null>(null)
  const themeBeforeHoliday = ref<ThemePreset | null>(null)

  function normalizeHolidayThemeKey(raw: unknown): string {
    return (typeof raw === 'string' ? raw : '')
      .replace(/\s+/g, ' ')
      .replace(/[\u3000-\u303f\uff00-\uffef]/g, '')
      .trim()
      .toLowerCase()
  }

  /** 按当前前端语言解析假日地区码（未匹配时回退 CN，与后端默认一致） */
  function resolveHolidayRegionByLocale(localeCode: string): string {
    const normalized = localeCode.trim().toLowerCase()
    const region = normalized.split('-')[1]?.toUpperCase()
    if (region && region.length === 2) {
      return region
    }
    return 'CN'
  }

  /** 将假日 DTO 写入 `holidayFromToken`；`dto` 为空则清空 */
  function applyHolidayDto(dto: Holiday | null) {
    if (
      dto == null ||
      ((dto.holidayName == null || String(dto.holidayName).trim() === '') &&
        (dto.holidayGreeting == null || String(dto.holidayGreeting).trim() === '') &&
        (dto.holidayQuote == null || String(dto.holidayQuote).trim() === '') &&
        (dto.holidayTheme == null || String(dto.holidayTheme).trim() === ''))
    ) {
      holidayFromToken.value = null
      return
    }
    const holidayName = typeof dto.holidayName === 'string' ? dto.holidayName : ''
    const holidayGreeting = typeof dto.holidayGreeting === 'string' ? dto.holidayGreeting : ''
    const next: HolidayFromToken = {
      isHolidayToday: true,
      holidayName,
      holidayGreeting,
      holidayTheme: normalizeHolidayThemeKey(dto.holidayTheme)
    }
    const quoteRaw = dto.holidayQuote
    if (typeof quoteRaw === 'string' && quoteRaw.trim() !== '') {
      next.holidayQuote = quoteRaw.trim()
    }
    holidayFromToken.value = next
  }

  /** 严格按假日主题色切换；非假日时恢复入节前主题 */
  function syncAppThemeWithHoliday() {
    const settingStore = useSettingStore()
    const currentThemeTypeRaw = String(settingStore.setting?.themeColor?.type ?? 'blue')
    const currentThemeType: ThemePreset =
      currentThemeTypeRaw in themeColorMap ? (currentThemeTypeRaw as ThemePreset) : 'blue'
    const holidayTheme = holidayFromToken.value?.holidayTheme
    const isHoliday = holidayFromToken.value?.isHolidayToday === true
    const isHolidayThemeValid =
      typeof holidayTheme === 'string' &&
      holidayTheme in themeColorMap

    if (isHoliday && isHolidayThemeValid) {
      if (themeBeforeHoliday.value == null) {
        themeBeforeHoliday.value = currentThemeType
      }
      if (currentThemeType !== holidayTheme) {
        settingStore.setSettingTransient({ themeColor: { type: holidayTheme as ThemePreset } })
        applySettings()
        logger.info('[User Store] 假日主题已生效(临时覆盖，不持久化): ', holidayTheme)
      }
      return
    }

    if (themeBeforeHoliday.value != null && currentThemeType !== themeBeforeHoliday.value) {
      settingStore.setSettingTransient({ themeColor: { type: themeBeforeHoliday.value } })
      applySettings()
      logger.info('[User Store] 非假日，已恢复原主题(不改持久化): ', themeBeforeHoliday.value)
    }
    themeBeforeHoliday.value = null
  }

  /** 未登录可调用：按当前日期拉取公开假日主题并写入 store */
  const syncHolidayThemeFromPublicApi = async () => {
    try {
      const locale = String(unref(i18n.global.locale) ?? '')
      const region = resolveHolidayRegionByLocale(locale)
      const dto = await getHolidayTheme({ region })
      applyHolidayDto(dto)
      syncAppThemeWithHoliday()
      
      // 更清晰的日志输出
      const hasHoliday = dto && (
        (dto.holidayName && String(dto.holidayName).trim() !== '') ||
        (dto.holidayGreeting && String(dto.holidayGreeting).trim() !== '')
      )
      if (hasHoliday) {
        logger.info('[User Store] 假日主题接口: 已同步「', dto.holidayName, '」')
      } else {
        logger.debug('[User Store] 假日主题接口: 今日无假日记录（region=', region, '）')
      }
    } catch (error: unknown) {
      logger.warn('[User Store] 拉取假日主题失败:', toErrorMessage(error))
    }
  }

  /** 仅更新内存中的 token（内部用）；登出清空假日；登录换 token 时保留已预热的假日主题 */
  function setToken(newToken: string | null) {
    token.value = newToken
    if (newToken == null) {
      holidayFromToken.value = null
    }
    logger.info('[User Store] Token 已更新:', newToken == null ? '已清除会话' : '已写入访问令牌')
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

  // 登录：语言列表 → 当前语言翻译 → 公开假日主题 → 调登录接口 → 成功后全量字典 → userinfo
  const loginAction = async (params: LoginParams) => {
    const loginStart = performance.now()
    logger.info('[Perf] ===== 登录流程开始 =====')
    
    try {
      logger.info('[User Store] 开始登录，用户名:', params.username)
      
      // Step 1: 加载语言列表
      const step1Start = performance.now()
      logger.debug('[Perf] [Login Step 1] 开始加载语言列表...')
      await useLocaleStore().loadLanguages()
      const step1Duration = performance.now() - step1Start
      logger.info(`[Perf] [Login Step 1] 语言列表加载完成，耗时: ${step1Duration.toFixed(2)}ms`)
      
      // Step 2: 加载翻译数据
      const step2Start = performance.now()
      const culture = unref(i18n.global.locale) as string
      logger.debug('[Perf] [Login Step 2] 开始加载翻译数据...', culture)
      await loadTranslationsFromBackend(culture, 'Frontend')
      const step2Duration = performance.now() - step2Start
      logger.info(`[Perf] [Login Step 2] 翻译数据加载完成，耗时: ${step2Duration.toFixed(2)}ms`)

      // Step 3: 调用登录接口
      const step3Start = performance.now()
      logger.debug('[Perf] [Login Step 3] 开始调用登录API...')
      const data = await login(params)
      const step3Duration = performance.now() - step3Start
      logger.info(`[Perf] [Login Step 3] 登录API请求完成，耗时: ${step3Duration.toFixed(2)}ms`)
      
      const authExtra: { refreshToken?: string; expiresIn?: number } = {}
      if (data.refreshToken != null) authExtra.refreshToken = data.refreshToken
      if (data.expiresIn != null) authExtra.expiresIn = data.expiresIn
      setAuth(data.token, Object.keys(authExtra).length > 0 ? authExtra : undefined)

      // Step 4: 加载字典数据
      const step4Start = performance.now()
      logger.debug('[Perf] [Login Step 4] 开始加载字典数据...')
      await useDictDataStore().loadAllDictData()
      const step4Duration = performance.now() - step4Start
      logger.info(`[Perf] [Login Step 4] 字典数据加载完成，耗时: ${step4Duration.toFixed(2)}ms`)

      // Step 5: 获取用户信息
      const step5Start = performance.now()
      logger.debug('[Perf] [Login Step 5] 开始获取用户信息...')
      const info = await getUserInfoAction()
      const step5Duration = performance.now() - step5Start
      logger.info(`[Perf] [Login Step 5] 用户信息获取完成，耗时: ${step5Duration.toFixed(2)}ms`)
      
      eventBus.$emit(AuthEvents.LoginSuccess, { 
        userId: info.userId, 
        username: params.username 
      })
      
      const totalDuration = performance.now() - loginStart
      logger.info('[Perf] ===== 登录流程完成 =====')
      logger.info('[Perf] [Login] 总耗时:', totalDuration.toFixed(2), 'ms')
      logger.info('[Perf] [Login] 各步骤耗时:', {
        'Step1_语言列表': step1Duration.toFixed(2) + 'ms',
        'Step2_翻译数据': step2Duration.toFixed(2) + 'ms',
        'Step3_登录API': step3Duration.toFixed(2) + 'ms',
        'Step4_字典数据': step4Duration.toFixed(2) + 'ms',
        'Step5_用户信息': step5Duration.toFixed(2) + 'ms'
      })
      const stepDurations = {
        'Step1_语言列表': step1Duration,
        'Step2_翻译数据': step2Duration,
        'Step3_登录API': step3Duration,
        'Step4_字典数据': step4Duration,
        'Step5_用户信息': step5Duration
      }
      const slowestStep = Object.entries(stepDurations).sort(([,a], [,b]) => b - a)[0]
      logger.info('[Perf] [Login] 最慢的步骤:', slowestStep ? slowestStep[0] : '未知')
      
      logger.info('[User Store] 登录成功，用户名:', params.username, '用户ID:', info.userId)
      return { ...data, userInfo: info }
    } catch (error: unknown) {
      const totalDuration = performance.now() - loginStart
      logger.error('[Perf] [Login] 登录失败，总耗时:', totalDuration.toFixed(2), 'ms')
      logger.error('[User Store] 登录失败，用户名:', params.username, '错误:', toErrorMessage(error))
      throw error
    }
  }

  // 获取用户信息（userinfo 返回假日信息时同步到 holidayFromToken，因 access_token 可能为 JWE 无法前端解析）
  const getUserInfoAction = async () => {
    if (!token.value) {
      throw new Error('未登录')
    }
    const userInfoStart = performance.now()
    logger.debug('[Perf] [UserInfo] 开始获取用户信息...')
    const info = await getUserInfo()
    const apiDuration = performance.now() - userInfoStart
    logger.info(`[Perf] [UserInfo] API 请求完成，耗时: ${apiDuration.toFixed(2)}ms`)
    
    const processStart = performance.now()
    userInfo.value = info
    if (info.holidayToday === true && (info.holidayName != null || info.holidayGreeting != null || info.holidayQuote != null || info.holidayTheme != null)) {
      const holidayThemeRaw = info.holidayTheme
      const key = (typeof holidayThemeRaw === 'string' ? holidayThemeRaw : '')
        .replace(/\s+/g, ' ')
        .replace(/[\u3000-\u303f\uff00-\uffef]/g, '')
        .trim()
        .toLowerCase()
      const holidayName = typeof info.holidayName === 'string' ? info.holidayName : ''
      const holidayGreeting = typeof info.holidayGreeting === 'string' ? info.holidayGreeting : ''
      const fromUserInfo: HolidayFromToken = {
        isHolidayToday: true,
        holidayName,
        holidayGreeting,
        holidayTheme: key
      }
      const quoteRaw = info.holidayQuote
      if (typeof quoteRaw === 'string' && quoteRaw.trim() !== '') {
        fromUserInfo.holidayQuote = quoteRaw.trim()
      }
      holidayFromToken.value = fromUserInfo
      syncAppThemeWithHoliday()
      logger.info('[User Store] 假日信息已从 userinfo 同步: ', holidayName, ', 主题 key=', key)
    } else {
      // 未返回假日或今日非假日时清空，避免切换语言/地区后仍显示旧假日（如 zh-CN 有假日、en-US 无假日）
      holidayFromToken.value = null
      syncAppThemeWithHoliday()
      logger.info('[User Store] userinfo 无假日信息，假日状态: 非假日/无')
    }
    if (import.meta.env.DEV) {
      logger.debug('[User Store] 获取用户信息成功:', {
        userId: info.userId,
        userName: info.userName,
        roles: info.roles,
        permissions: info.permissions,
        permissionsCount: Array.isArray(info.permissions) ? info.permissions.length : 0
      })
    }
    const processDuration = performance.now() - processStart
    logger.debug(`[Perf] [UserInfo] 数据处理完成，耗时: ${processDuration.toFixed(2)}ms`)
    const totalDuration = performance.now() - userInfoStart
    logger.info(`[Perf] [UserInfo] 总耗时: ${totalDuration.toFixed(2)}ms`)
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
    syncHolidayThemeFromPublicApi,
    logout
  }
})