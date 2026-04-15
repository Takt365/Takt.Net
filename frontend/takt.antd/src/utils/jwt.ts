/**
 * JWT 解析工具：从 access_token 中安全解析 payload（用于读取假期等自定义 claims）
 */
import { logger } from '@/utils/logger'

export interface HolidayFromToken {
  isHolidayToday: boolean
  holidayName: string
  holidayGreeting: string
  /** 假日引用/诗句（用于引用区，优先于 holidayGreeting） */
  holidayQuote?: string
  /** 假日主题（对应 themeColorMap 的 key，命名 HolidayTheme 避免与前端的 ThemeColorKey 冲突） */
  holidayTheme: string
}

/**
 * 从 JWT 中解析 payload（不校验签名，仅用于读取前端展示用 claims）
 * @param token access_token 字符串
 * @returns payload 或 null（解析失败时）
 */
function decodeJwtPayload(token: string | null): Record<string, unknown> | null {
  if (!token || typeof token !== 'string') return null
  const parts = token.trim().split('.')
  if (parts.length !== 3) return null
  try {
    const base64 = parts[1].replace(/-/g, '+').replace(/_/g, '/')
    const pad = base64.length % 4
    const padded = pad ? base64 + '='.repeat(4 - pad) : base64
    const json = typeof atob !== 'undefined' ? atob(padded) : ''
    return JSON.parse(json) as Record<string, unknown>
  } catch {
    return null
  }
}

/** 从 payload 中取字符串 claim（支持短名或带 URI 的 key） */
function getClaimString(payload: Record<string, unknown>, shortKey: string): string {
  const v = payload[shortKey]
  if (typeof v === 'string') return v.trim()
  for (const key of Object.keys(payload)) {
    if (key === shortKey || key.endsWith('/' + shortKey) || key.endsWith('#' + shortKey)) {
      const val = payload[key]
      if (typeof val === 'string') return val.trim()
    }
  }
  return ''
}

/** 规范化主题色 key：去除首尾空白与全角等字符，便于匹配 themeColorMap */
function normalizeThemeColorKey(key: string): string {
  return key.replace(/\s+/g, ' ').replace(/[\u3000-\u303f\uff00-\uffef]/g, '').trim().toLowerCase()
}

/**
 * 从 token 中解析今日假期信息（holiday_today, holiday_name, holiday_greeting, holiday_quote, holiday_theme）
 */
export function decodeHolidayFromToken(token: string | null): HolidayFromToken | null {
  const hasToken = Boolean(token && typeof token === 'string')
  logger.info('[Holiday Token] 解析 token 假日信息, 有 token:', hasToken, hasToken ? ', 长度: ' + (token!.length) : '')
  const payload = decodeJwtPayload(token)
  if (!payload) {
    logger.info('[Holiday Token] 解析完成: 无 token 或 payload 解析失败')
    return null
  }
  const holidayTodayKey = Object.keys(payload).find(k => k === 'holiday_today' || k.endsWith('/holiday_today') || k.endsWith('#holiday_today'))
  const holidayTodayRaw = holidayTodayKey != null ? payload[holidayTodayKey] : undefined
  const isHoliday = holidayTodayRaw === '1' || holidayTodayRaw === true
  if (!isHoliday) {
    logger.info('[Holiday Token] 解析完成: 今日非假日, holiday_today=', holidayTodayRaw)
    return null
  }
  const holidayName = getClaimString(payload, 'holiday_name')
  const holidayGreeting = getClaimString(payload, 'holiday_greeting')
  const holidayQuoteRaw = getClaimString(payload, 'holiday_quote')
  const holidayQuote = holidayQuoteRaw ? holidayQuoteRaw : undefined
  const rawKey = getClaimString(payload, 'holiday_theme')
  const holidayTheme = rawKey ? normalizeThemeColorKey(rawKey) : ''
  logger.info('[Holiday Token] 解析完成: 今日假日, HolidayName=', holidayName, ', HolidayGreeting=', holidayGreeting, ', HolidayQuote=', holidayQuoteRaw || '(无)', ', HolidayTheme(normalized)=', holidayTheme)
  return {
    isHolidayToday: true,
    holidayName,
    holidayGreeting,
    holidayQuote,
    holidayTheme
  }
}