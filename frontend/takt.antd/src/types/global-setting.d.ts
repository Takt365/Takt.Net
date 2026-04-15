/**
 * 应用全局配置类型（UI 设置，非后端 Routine 设置）
 * 命名空间：@/types/setting
 */
export type ThemeColor =
  | 'blue' | 'green' | 'red' | 'orange' | 'purple' | 'cyan' | 'pink' | 'yellow' | 'indigo' | 'brown' | 'gray' | 'custom'

export interface ThemeColorConfig {
  type: ThemeColor
  customColor?: string
}

export interface AppSetting {
  layout: 'side' | 'top' | 'mix' | 'content'
  theme: 'light' | 'dark'
  themeColor: ThemeColorConfig
  borderRadius: 0 | 5 | 10 | 15 | 20
  fontSize: number
  colorWeak: boolean
  grayscale: boolean
  fixedHeader: boolean
  fixedSider: boolean
  showLogo: boolean
  siderWidth: number
  siderCollapsedWidth: number
  showBreadcrumb: boolean
  breadcrumbIcon: boolean
  showTabs: boolean
  tabStyle: 'google' | 'card'
  persistTabs: boolean
  maxTabs: number
  showFooter: boolean
  copyright: string
  contentWidth: 'fluid' | 'fixed'
  multiTab: boolean
  watermark: boolean
  watermarkContent: string
  demo: boolean
  menuAccordion: boolean
  menuStyle: 'rounded' | 'plain'
  defaultLocale: string
  logo: string
  logoText: string
  logoCollapsedText: string
  showForgotPassword: boolean
  showRegister: boolean
}
