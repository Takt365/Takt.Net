/**
 * 应用设置到界面
 * 将设置中的值实时应用到 DOM 和 CSS
 */
import type { Ref } from 'vue'
import { getSetting, useSettingStore } from '@/stores/setting'
import type { AppSetting } from '@/types/global-setting'
import { useThemeStore } from '@/stores/theme'

/**
 * 应用所有设置到界面
 * 优先使用 store 中的 setting（与 setSetting 同源），确保主题等变更立即生效
 */
export function applySettings() {
  const store = useSettingStore()
  const themeStore = useThemeStore()
  const settingRef = store.setting as Ref<AppSetting> | undefined
  const s = settingRef?.value ?? getSetting()
  if (s.theme !== themeStore.themeMode) {
    themeStore.setTheme(s.theme)
  }
  const root = document.documentElement
  if (root) {
    root.style.fontSize = `${s.fontSize}px`
    s.colorWeak ? root.classList.add('color-weak') : root.classList.remove('color-weak')
    s.grayscale ? root.classList.add('grayscale') : root.classList.remove('grayscale')
    root.style.setProperty('--border-radius-base', `${s.borderRadius}px`)
  }
}

export function watchSettings() {
  if (typeof window !== 'undefined') {
    window.addEventListener('storage', (e) => {
      if (e.key === 'app-setting') applySettings()
    })
  }
}

export const notifySettingsChanged = applySettings

