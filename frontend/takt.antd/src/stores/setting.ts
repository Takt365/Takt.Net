// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/stores/setting
// 文件名称：setting.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：应用配置 Store，管理主题、语言、布局等全局配置
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

/**
 * 配置 Store：仅状态与读写，全局配置见 @/setting，类型见 @/types/setting
 */
import { defineStore } from 'pinia'
import { readSettingFromStorage, normalizeSetting, defaultSetting, STORAGE_KEY } from '@/setting'
import type { AppSetting } from '@/types/global-setting'

export type { ThemeColor, ThemeColorConfig, AppSetting } from '@/types/global-setting'
export { defaultSetting, getSetting, themeColorMap, getThemeColorValue, validateFontSize } from '@/setting'

export const useSettingStore = defineStore('setting', () => {
  const initial = readSettingFromStorage()
  const setting = ref<AppSetting>(initial ?? defaultSetting)

  function setSetting(partial: Partial<AppSetting>) {
    const current = setting.value ?? defaultSetting
    const next = { ...current, ...partial }
    if (partial.themeColor && typeof partial.themeColor === 'object') {
      next.themeColor = { ...current.themeColor, ...partial.themeColor }
    }
    setting.value = normalizeSetting(next)
    if (typeof window !== 'undefined' && localStorage) {
      localStorage.setItem(STORAGE_KEY, JSON.stringify(setting.value))
    }
  }

  /** 仅更新内存配置（不写入 localStorage），用于假日等临时覆盖场景 */
  function setSettingTransient(partial: Partial<AppSetting>) {
    const current = setting.value ?? defaultSetting
    const next = { ...current, ...partial }
    if (partial.themeColor && typeof partial.themeColor === 'object') {
      next.themeColor = { ...current.themeColor, ...partial.themeColor }
    }
    setting.value = normalizeSetting(next)
  }

  function resetSetting() {
    setting.value = { ...defaultSetting }
    if (typeof window !== 'undefined' && localStorage) {
      localStorage.removeItem(STORAGE_KEY)
    }
  }

  function syncFromStorage() {
    const next = readSettingFromStorage()
    setting.value = next ?? defaultSetting
  }

  if (typeof window !== 'undefined') {
    window.addEventListener('storage', (e) => {
      if (e.key === STORAGE_KEY) syncFromStorage()
    })
  }

  return { setting, setSetting, setSettingTransient, resetSetting, syncFromStorage }
})
