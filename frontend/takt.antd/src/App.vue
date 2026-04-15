<template>
  <a-config-provider :theme="themeConfig" :direction="direction" :locale="antdVueLocale">
    <RouterView />
  </a-config-provider>
</template>

<script setup lang="ts">
import { computed, onMounted, onUnmounted, ref, h, watch } from 'vue'
import { useThemeStore } from '@/stores/theme'
import { useLocaleStore } from '@/stores/routine/localization/locale'
import { theme, notification } from 'ant-design-vue'
import zhCN from 'ant-design-vue/es/locale/zh_CN'
import enUS from 'ant-design-vue/es/locale/en_US'
import arEG from 'ant-design-vue/es/locale/ar_EG'
import { useRouter } from 'vue-router'
import { check as healthCheck } from '@/api/identity/health'
import { applySettings, watchSettings } from '@/utils/apply-settings'
import { storeToRefs } from 'pinia'
import { defaultSetting, getThemeColorValue, useSettingStore } from '@/stores/setting'
import { logger } from '@/utils/logger'
import { useUserStore } from '@/stores/identity/user'
import { useUserActivity } from '@/composables/use-user-activity'

const router = useRouter()
const themeStore = useThemeStore()
const localeStore = useLocaleStore()
const { setting } = storeToRefs(useSettingStore())
const userStore = useUserStore()

/** 判断当前语言是否为 RTL（从右到左），如阿拉伯语、波斯语、希伯来语、乌尔都语等 */
function isRtlLocale(localeCode: string): boolean {
  const code = (localeCode || '').toLowerCase()
  return code.startsWith('ar') || code.startsWith('fa') || code.startsWith('he') || code.startsWith('ur')
}

/** 根据当前语言动态设置布局方向 */
const direction = computed(() => (isRtlLocale(localeStore.locale) ? 'rtl' : 'ltr'))

/** Ant Design Vue 组件语言包：与当前应用语言一致 */
const antdVueLocale = computed(() => {
  const code = (localeStore.locale || '').toLowerCase()
  if (code.startsWith('zh')) return zhCN
  if (code.startsWith('ar')) return arEG
  return enUS
})

// 根据主题模式和颜色配置 Ant Design Vue 主题
const themeConfig = computed(() => {
  const s = setting.value ?? defaultSetting
  const themeColorValue = getThemeColorValue(s.themeColor ?? { type: 'blue' })
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

// 健康检查定时器
let healthCheckTimer: ReturnType<typeof setInterval> | null = null
// 当前错误通知的 key
let errorNotificationKey: string | null = null
// 是否已显示错误通知
const hasErrorNotification = ref(false)

/**
 * 执行健康检查
 */
const performHealthCheck = async () => {
  try {
    await healthCheck()
    
    // 健康检查成功，如果之前有错误提示，则关闭它
    if (hasErrorNotification.value && errorNotificationKey) {
      notification.close(errorNotificationKey)
      errorNotificationKey = null
      hasErrorNotification.value = false
    }
  } catch (error: any) {
    // 健康检查失败，显示连接失败提示并跳转登录页（避免重复提示）
    if (!hasErrorNotification.value) {
      notification.error({
        key: 'health-check-error', // 使用固定的 key，便于后续关闭
        message: '后端连接失败',
        description: h('div', { style: { whiteSpace: 'pre-line' } }, '无法连接到后端服务器，请检查：\n1. 后端服务是否已启动\n2. 网络连接是否正常\n3. API 地址配置是否正确'),
        duration: 10, // 10秒后自动关闭
        placement: 'topRight',
        onClose: () => {
          errorNotificationKey = null
          hasErrorNotification.value = false
        }
      })
      errorNotificationKey = 'health-check-error'
      hasErrorNotification.value = true
      // 后端不可用时跳转登录页并清除登录态，避免停留在需鉴权页却无法请求
      if (router.currentRoute.value.path !== '/login') {
        userStore.logout().then(() => {
          router.replace('/login')
        }).catch((e) => {
          logger.error('[Health Check] 退出登录失败:', e)
          router.replace('/login')
        })
      }
    }
    logger.error('[Health Check] 后端健康检查失败:', error)
  }
}

// 应用启动时立即执行一次健康检查，然后每 30 秒执行一次
onMounted(() => {
  // 应用设置到界面
  applySettings()
  // 监听设置变化
  watchSettings()
  
  // 立即执行一次
  performHealthCheck()
  
  // 设置定时器，每 30 秒执行一次
  healthCheckTimer = setInterval(() => {
    performHealthCheck()
  }, 30000) // 30秒 = 30000毫秒
})

// 组件卸载时清理定时器
onUnmounted(() => {
  if (healthCheckTimer) {
    clearInterval(healthCheckTimer)
    healthCheckTimer = null
  }
  
  // 清理错误通知
  if (errorNotificationKey) {
    notification.close(errorNotificationKey)
    errorNotificationKey = null
    hasErrorNotification.value = false
  }
})
</script>
