/**
 * 用户活动检测 Composable
 * 用于检测用户活动并在不活动一定时间后自动登出
 */

import { ref, onMounted, onUnmounted } from 'vue'
import { useRouter } from 'vue-router'
import { Modal, message } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { useUserStore } from '@/stores/identity/user'
import { logger } from '@/utils/logger'

/**
 * 用户活动检测配置
 */
interface UserActivityConfig {
  /** 不活动超时时间（毫秒），默认 30 分钟 */
  inactivityTimeout?: number
  /** 警告提示时间（毫秒），在超时前多少时间提示，默认 5 分钟 */
  warningTime?: number
  /** 是否启用活动检测，默认 true */
  enabled?: boolean
}

/**
 * 用户活动检测 Composable
 * @param config 配置选项
 */
export function useUserActivity(config: UserActivityConfig = {}) {
  const {
    inactivityTimeout = 30 * 60 * 1000, // 默认 30 分钟
    warningTime = 5 * 60 * 1000, // 默认 5 分钟前警告
    enabled = true
  } = config

  const { t } = useI18n()
  const userStore = useUserStore()
  const router = useRouter()

  // 最后活动时间
  const lastActivityTime = ref<number>(Date.now())
  // 活动检测定时器
  let activityCheckTimer: ReturnType<typeof setInterval> | null = null
  // 警告 Modal 实例
  let warningModal: any = null
  // 是否已显示警告
  const hasWarningShown = ref(false)
  // 是否已启动检测
  let isDetectionStarted = false

  /**
   * 更新最后活动时间
   */
  const updateActivityTime = () => {
    if (!enabled || !userStore.token) {
      return
    }
    lastActivityTime.value = Date.now()
    
    // 如果已显示警告，关闭警告
    if (hasWarningShown.value && warningModal) {
      warningModal.destroy()
      warningModal = null
      hasWarningShown.value = false
    }
  }

  /**
   * 检查用户活动状态
   */
  const checkUserActivity = () => {
    if (!enabled || !userStore.token) {
      return
    }

    const now = Date.now()
    const timeSinceLastActivity = now - lastActivityTime.value
    const timeUntilTimeout = inactivityTimeout - timeSinceLastActivity

    // 如果已超时，自动登出
    if (timeSinceLastActivity >= inactivityTimeout) {
      logger.info('[User Activity] 用户不活动超时，自动登出')
      handleAutoLogout()
      return
    }

    // 如果接近超时且未显示警告，显示警告
    if (timeUntilTimeout <= warningTime && !hasWarningShown.value) {
      showWarningModal(timeUntilTimeout)
    }
  }

  /**
   * 显示警告 Modal
   */
  const showWarningModal = (timeRemaining: number) => {
    if (hasWarningShown.value) {
      return
    }

    const minutesRemaining = Math.ceil(timeRemaining / 60000)
    
    warningModal = Modal.warning({
      title: t('layouts.session.title'),
      content: t('layouts.session.content', { minutes: minutesRemaining }),
      okText: t('layouts.session.okText'),
      cancelText: t('layouts.session.cancelText'),
      onOk: () => {
        updateActivityTime()
        warningModal = null
        hasWarningShown.value = false
      },
      onCancel: () => {
        handleAutoLogout()
      }
    })
    
    hasWarningShown.value = true
  }

  /**
   * 自动登出
   */
  const handleAutoLogout = async () => {
    if (warningModal) {
      warningModal.destroy()
      warningModal = null
    }
    
    hasWarningShown.value = false
    
    try {
      message.warning(t('layouts.session.autoLogout'))
      await userStore.logout()
      router.push('/login')
    } catch (error: any) {
      logger.error('[User Activity] 自动登出失败:', error)
    }
  }

  /**
   * 绑定活动事件监听器
   */
  const bindActivityEvents = () => {
    if (!enabled || !userStore.token) {
      return
    }

    // 监听用户活动事件
    const events = ['mousedown', 'mousemove', 'keypress', 'scroll', 'touchstart', 'click']
    
    events.forEach(event => {
      document.addEventListener(event, updateActivityTime, { passive: true })
    })

    // 监听页面可见性变化
    document.addEventListener('visibilitychange', () => {
      if (document.visibilityState === 'visible') {
        updateActivityTime()
      }
    })

    // 监听窗口焦点变化
    window.addEventListener('focus', updateActivityTime)
  }

  /**
   * 解绑活动事件监听器
   */
  const unbindActivityEvents = () => {
    const events = ['mousedown', 'mousemove', 'keypress', 'scroll', 'touchstart', 'click']
    
    events.forEach(event => {
      document.removeEventListener(event, updateActivityTime)
    })

    document.removeEventListener('visibilitychange', updateActivityTime)
    window.removeEventListener('focus', updateActivityTime)
  }

  /**
   * 启动活动检测
   */
  const startActivityDetection = () => {
    if (!enabled || !userStore.token) {
      return
    }

    // 如果已经启动，先停止再启动（避免重复绑定事件）
    if (isDetectionStarted) {
      stopActivityDetection()
    }

    // 初始化最后活动时间
    lastActivityTime.value = Date.now()

    // 绑定活动事件
    bindActivityEvents()

    // 启动定时检查（每 1 分钟检查一次）
    activityCheckTimer = setInterval(() => {
      checkUserActivity()
    }, 60000) // 1 分钟

    isDetectionStarted = true
    logger.debug('[User Activity] 用户活动检测已启动')
  }

  /**
   * 停止活动检测
   */
  const stopActivityDetection = () => {
    if (activityCheckTimer) {
      clearInterval(activityCheckTimer)
      activityCheckTimer = null
    }

    unbindActivityEvents()

    if (warningModal) {
      warningModal.destroy()
      warningModal = null
    }

    hasWarningShown.value = false
    isDetectionStarted = false
    logger.debug('[User Activity] 用户活动检测已停止')
  }

  // 组件挂载时，如果已有 token 则启动检测
  onMounted(() => {
    if (userStore.token) {
      startActivityDetection()
    }
  })

  // 组件卸载时停止检测
  onUnmounted(() => {
    stopActivityDetection()
  })

  return {
    lastActivityTime,
    updateActivityTime,
    startActivityDetection,
    stopActivityDetection
  }
}
