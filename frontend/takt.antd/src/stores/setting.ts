/**
 * 配置 Store：仅状态与读写，全局配置见 @/setting，类型见 @/types/setting
 */
import { defineStore } from 'pinia'
import { ref } from 'vue'
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
