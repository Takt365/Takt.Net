/**
 * 全局配置（默认值、常量、持久化读写与工具）
 * 类型定义见 @/types/setting
 */
import type { ThemeColor, ThemeColorConfig, AppSetting } from '@/types/global-setting'

export type { ThemeColor, ThemeColorConfig, AppSetting } from '@/types/global-setting'

/** 默认设置 */
export const defaultSetting: AppSetting = {
  layout: 'side',
  theme: 'dark',
  themeColor: { type: 'blue' },
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
 * 主题色预设映射（与 @/assets/styles/color-base.less 十大著名色彩 保持一致）
 * Less: mars-green, tiffany-blue, chinese-red, titian-red, burgundy, bordeaux, klein-blue, van-dyke-brown, prussian-blue, sennelier-yellow, memorial-gray
 */
export const themeColorMap: Record<Exclude<ThemeColor, 'custom'>, string> = {
  green: '#2e8b57',  // 马尔斯绿 @mars-green
  cyan: '#00a0b0',   // 蒂芙尼蓝 @tiffany-blue
  red: '#FF0000',    // 中国红 @chinese-red
  orange: '#FF6347', // 提香红 @titian-red
  purple: '#990033', // 勃艮第红 @burgundy
  pink: '#8c1515',   // 波尔多红 @bordeaux
  blue: '#002FA7',   // 克莱因蓝 @klein-blue
  brown: '#4c2b18',  // 凡戴克棕 @van-dyke-brown
  indigo: '#003153', // 普鲁士蓝 @prussian-blue
  yellow: '#F9DC24', // 申布伦黄 @sennelier-yellow
  gray: '#808080'    // 纪念灰 @memorial-gray
}

export function getThemeColorValue(config: ThemeColorConfig): string {
  if (config.type === 'custom' && config.customColor) return config.customColor
  return themeColorMap[config.type as Exclude<ThemeColor, 'custom'>] || themeColorMap.blue
}

export function validateFontSize(size: number): number {
  if (size < 15) return 15
  if (size > 22) return 22
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
  if (typeof window === 'undefined' || !localStorage) return defaultSetting
  try {
    const stored = localStorage.getItem(STORAGE_KEY)
    if (!stored) return defaultSetting
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
