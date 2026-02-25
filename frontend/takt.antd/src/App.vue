<template>
  <a-config-provider :theme="themeConfig" :direction="direction" :locale="antdVueLocale">
    <RouterView />
    <TaktPwaReloadPrompt />
  </a-config-provider>
</template>

<script setup lang="ts">
import { computed, onMounted, watch, provide } from 'vue'
import { useThemeStore } from '@/stores/theme'
import { useLocaleStore } from '@/stores/routine/localization/locale'
import { theme } from 'ant-design-vue'
import arEG from 'ant-design-vue/es/locale/ar_EG'
import enUS from 'ant-design-vue/es/locale/en_US'
import esES from 'ant-design-vue/es/locale/es_ES'
import frFR from 'ant-design-vue/es/locale/fr_FR'
import jaJP from 'ant-design-vue/es/locale/ja_JP'
import koKR from 'ant-design-vue/es/locale/ko_KR'
import ruRU from 'ant-design-vue/es/locale/ru_RU'
import zhCN from 'ant-design-vue/es/locale/zh_CN'
import zhTW from 'ant-design-vue/es/locale/zh_TW'
import fcDesignerZhCn from '@form-create/antd-designer/locale/zh-cn'
import fcDesignerEn from '@form-create/antd-designer/locale/en'
import dayjs from 'dayjs'
import 'dayjs/locale/ar'
import 'dayjs/locale/en'
import 'dayjs/locale/es'
import 'dayjs/locale/fr'
import 'dayjs/locale/ja'
import 'dayjs/locale/ko'
import 'dayjs/locale/ru'
import 'dayjs/locale/zh-cn'
import 'dayjs/locale/zh-tw'
import { applySettings, watchSettings } from '@/utils/apply-settings'
import { storeToRefs } from 'pinia'
import { defaultSetting, getEffectiveThemeColorValue, themeColorMap, useSettingStore } from '@/stores/setting'
import { logger } from '@/utils/logger'
const holidayLogger = logger.withTag('Holiday')
import { useUserStore } from '@/stores/identity/user'
import { useUserActivity } from '@/composables/use-user-activity'
import TaktPwaReloadPrompt from '@/components/common/takt-pwa-reload-prompt/index.vue'

const themeStore = useThemeStore()
const localeStore = useLocaleStore()
const { locale: currentLocale } = storeToRefs(localeStore)
const { setting } = storeToRefs(useSettingStore())
const userStore = useUserStore()

/** 判断当前语言是否为 RTL */
function isRtlLocale(code: string): boolean {
  const c = (code || '').toLowerCase()
  return c.startsWith('ar') || c.startsWith('fa') || c.startsWith('he') || c.startsWith('ur')
}

/** 布局方向：与 locale 一致 */
const direction = computed(() => (isRtlLocale(currentLocale.value) ? 'rtl' : 'ltr'))

/** Ant Design Vue 语言包：与 localeStore 一致，9 种语言均有对应包，未匹配回退 enUS */
const antdVueLocaleMap: Record<string, typeof zhCN> = {
  'ar-SA': arEG, 'en-US': enUS, 'es-ES': esES, 'fr-FR': frFR,
  'ja-JP': jaJP, 'ko-KR': koKR, 'ru-RU': ruRU, 'zh-CN': zhCN, 'zh-TW': zhTW
}
const antdVueLocale = computed(() => antdVueLocaleMap[currentLocale.value] ?? enUS)

/** FcDesigner 表单设计器语言包：与 localeStore 一致，仅 zh-CN/zh-TW 用中文，其余用英文（与 antdVueLocale 同源） */
const fcDesignerLocaleMap: Record<string, typeof fcDesignerZhCn> = {
  'zh-CN': fcDesignerZhCn, 'zh-TW': fcDesignerZhCn,
  'ar-SA': fcDesignerEn, 'en-US': fcDesignerEn, 'es-ES': fcDesignerEn, 'fr-FR': fcDesignerEn,
  'ja-JP': fcDesignerEn, 'ko-KR': fcDesignerEn, 'ru-RU': fcDesignerEn
}
const fcDesignerLocale = computed(() => fcDesignerLocaleMap[currentLocale.value] ?? fcDesignerEn)
provide('taktFcDesignerLocale', fcDesignerLocale)

/** WangEditor 富文本：语言与暗色与 themeStore/localeStore 一致（仅内置 zh-CN / en） */
const taktWangEditorLocale = computed(() =>
  (currentLocale.value === 'zh-CN' || currentLocale.value === 'zh-TW') ? 'zh-CN' : 'en'
)
const taktWangEditorDark = computed(() => themeStore.themeMode === 'dark')
provide('taktWangEditorLocale', taktWangEditorLocale)
provide('taktWangEditorDark', taktWangEditorDark)

/** dayjs 全局 locale：与 localeStore 一致，顺序 ar-SA, en-US, es-ES, fr-FR, ja-JP, ko-KR, ru-RU, zh-CN, zh-TW */
const dayjsLocaleMap: Record<string, string> = {
  'ar-SA': 'ar', 'en-US': 'en', 'es-ES': 'es', 'fr-FR': 'fr',
  'ja-JP': 'ja', 'ko-KR': 'ko', 'ru-RU': 'ru', 'zh-CN': 'zh-cn', 'zh-TW': 'zh-tw'
}
watch(currentLocale, (loc) => {
  dayjs.locale(dayjsLocaleMap[loc] || loc?.toLowerCase() || 'zh-cn')
}, { immediate: true })

// 根据主题模式和颜色配置 Ant Design Vue 主题（今日假期主色优先于系统/默认）
const themeConfig = computed(() => {
  const s = setting.value ?? defaultSetting
  const rawKey = userStore.holidayFromToken?.holidayTheme ?? ''
  const holidayKey = rawKey.replace(/\s+/g, ' ').replace(/[\u3000-\u303f\uff00-\uffef]/g, '').trim().toLowerCase()
  const fromHoliday = Boolean(holidayKey && holidayKey in themeColorMap)
  const themeColorValue = fromHoliday
    ? themeColorMap[holidayKey as keyof typeof themeColorMap]
    : getEffectiveThemeColorValue(s.themeColor ?? { type: 'blue' })
  holidayLogger.info('[主题色]', fromHoliday ? `假日主题 key=${holidayKey} -> ${themeColorValue}` : `系统默认 -> ${themeColorValue}`)
  return {
    algorithm:
      themeStore.themeMode === 'dark'
        ? theme.darkAlgorithm
        : theme.defaultAlgorithm,
    token: {
      colorPrimary: themeColorValue,
      borderRadius: s.borderRadius ?? 5
    }
  }
})

// 用户活动检测（30分钟不活动自动登出）
const { startActivityDetection, stopActivityDetection } = useUserActivity({
  inactivityTimeout: 30 * 60 * 1000, // 30 分钟
  warningTime: 5 * 60 * 1000, // 5 分钟前警告
  enabled: true
})

// 监听 token 变化，启动/停止活动检测
watch(
  () => userStore.token,
  (newToken) => {
    if (newToken) {
      // 用户登录，启动活动检测
      startActivityDetection()
    } else {
      // 用户登出，停止活动检测
      stopActivityDetection()
    }
  },
  { immediate: true }
)

// 监听布局方向变化，设置 document.dir 使整页（含非 Ant Design 内容）遵循 RTL/LTR
watch(
  direction,
  (dir) => {
    if (document.documentElement) {
      document.documentElement.setAttribute('dir', dir)
    }
  },
  { immediate: true }
)

// 连接验证统一在 request 拦截器：连接失败时提示并跳转登录，任意请求成功时关闭连接失败通知
onMounted(() => {
  applySettings()
  watchSettings()
})
</script>
