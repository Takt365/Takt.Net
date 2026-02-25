/**
 * 全局配置（默认值、常量、持久化读写与工具）
 * 类型定义见 @/types/setting
 */
import type { ThemeColor, ThemeColorConfig, AppSetting } from '@/types/setting'

export type { ThemeColor, ThemeColorConfig, AppSetting } from '@/types/setting'

/** 默认设置 */
export const defaultSetting: AppSetting = {
  layout: 'side',
  theme: 'dark',
  themeColor: { type: 'blue', customColor: undefined },
  borderRadius: 5,
  fontSize: 15,
  colorWeak: false,
  grayscale: false,
  fixedHeader: true,
  fixedSider: true,
  showLogo: true,
  siderWidth: 200,
  siderCollapsedWidth: 64,
  showBreadcrumb: true,
  breadcrumbIcon: true,
  showTabs: true,
  tabStyle: 'google',
  persistTabs: false,
  maxTabs: 10,
  showFooter: true,
  copyright: '© 2025 Takt Digital Factory (TDF) . All rights reserved.',
  contentWidth: 'fluid',
  multiTab: true,
  watermark: false,
  watermarkContent: 'Takt Digital Factory (TDF) ',
  demo: false,
  menuAccordion: true,
  menuStyle: 'plain',
  defaultLocale: 'zh-CN',
  logo: '@/assets/images/takt.svg',
  logoText: 'Takt',
  logoCollapsedText: 'T',
  showForgotPassword: false,
  showRegister: false
}

export const STORAGE_KEY = 'app-setting'

/**
 * 主题色预设映射（与 @/assets/styles/color-base.less 十大著名色彩 完全一致，顺序同步）
 * Less 顺序: mars-green, tiffany-blue, chinese-red, titian-red, burgundy, bordeaux, klein-blue, van-dyke-brown, prussian-blue, sennelier-yellow, memorial-gray
 */
export const themeColorMap: Record<Exclude<ThemeColor, 'custom'>, string> = {
  green: '#2e8b57',  // @mars-green 马尔斯绿
  cyan: '#00a0b0',   // @tiffany-blue 蒂芙尼蓝
  red: '#FF0000',    // @chinese-red 中国红
  orange: '#FF6347', // @titian-red 提香红
  purple: '#990033', // @burgundy 勃艮第红
  pink: '#8c1515',   // @bordeaux 波尔多红
  blue: '#002FA7',   // @klein-blue 克莱因蓝
  brown: '#4c2b18',  // @van-dyke-brown 凡戴克棕
  indigo: '#003153', // @prussian-blue 普鲁士蓝
  yellow: '#F9DC24', // @sennelier-yellow 申布伦黄
  gray: '#808080'    // @memorial-gray 纪念灰
}

export function getThemeColorValue(config: ThemeColorConfig): string {
  if (config.type === 'custom' && config.customColor) {
    return config.customColor
  }
  return themeColorMap[config.type as Exclude<ThemeColor, 'custom'>] || themeColorMap.blue
}

/**
 * 特定日期固定主色（如 10-01 国庆固定中国红，不可变更）
 * 格式：MM-DD -> ThemeColor
 */
export const specialDateThemeMap: Record<string, Exclude<ThemeColor, 'custom'>> = {
  '10-01': 'red' // 国庆节：中国红 @chinese-red
}

/** 获取今日是否为特定日期，若是则返回固定的 ThemeColor */
export function getFixedThemeForDate(): Exclude<ThemeColor, 'custom'> | null {
  if (typeof window === 'undefined') return null
  const now = new Date()
  const key = `${String(now.getMonth() + 1).padStart(2, '0')}-${String(now.getDate()).padStart(2, '0')}`
  return specialDateThemeMap[key] ?? null
}

/** 是否今日为特定日期（主题色被锁定） */
export function isThemeColorLocked(): boolean {
  return getFixedThemeForDate() != null
}

/**
 * 获取实际生效的主题色（特定日期优先于用户设置）
 */
export function getEffectiveThemeColorValue(config: ThemeColorConfig): string {
  const fixed = getFixedThemeForDate()
  if (fixed != null) {
    return themeColorMap[fixed]
  }
  return getThemeColorValue(config)
}

export function validateFontSize(size: number): number {
  if (size < 15) {
    return 15
  }
  if (size > 22) {
    return 22
  }
  return size
}

function normalizeSetting(raw: Partial<AppSetting>): AppSetting {
  const base: AppSetting = { ...defaultSetting, ...raw }
  if (raw?.themeColor && typeof raw.themeColor === 'object') {
    base.themeColor = { ...defaultSetting.themeColor, ...raw.themeColor }
  }
  if (typeof base.fontSize === 'number') {
    base.fontSize = validateFontSize(base.fontSize)
  } else {
    const sizeMap: Record<string, number> = { small: 15, medium: 16, large: 18 }
    base.fontSize = sizeMap[String(base.fontSize)] ?? 15
  }
  if (!base.themeColor || typeof base.themeColor !== 'object') {
    base.themeColor = { ...defaultSetting.themeColor }
  } else if (base.themeColor.type !== 'custom' && !(base.themeColor.type in themeColorMap)) {
    base.themeColor = { ...defaultSetting.themeColor }
  }
  return base
}

/** 从 localStorage 读取并合并为有效 AppSetting */
export function readSettingFromStorage(): AppSetting {
  if (typeof window === 'undefined' || !localStorage) {
    return defaultSetting
  }
  try {
    const stored = localStorage.getItem(STORAGE_KEY)
    if (!stored) {
      return defaultSetting
    }
    const parsed = JSON.parse(stored) as Record<string, unknown>
    const merged = { ...defaultSetting, ...parsed }
    if (parsed.themeColor && typeof parsed.themeColor === 'object') {
      merged.themeColor = { ...defaultSetting.themeColor, ...parsed.themeColor } as ThemeColorConfig
    }
    return normalizeSetting(merged)
  } catch (error) {
    console.warn('[setting] 读取/解析失败，使用默认值:', error)
    return defaultSetting
  }
}

/** 非响应式读取当前设置 */
export const getSetting = readSettingFromStorage

/** 供 store 使用 */
export { normalizeSetting }
