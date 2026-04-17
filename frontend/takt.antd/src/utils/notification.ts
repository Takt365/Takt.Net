// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/utils/notification
// 文件名称：notification.ts
// 功能描述：通用 Notification，按官方原生用法统一使用静态 notification（notification.error / notification.success 等）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import { notification } from 'ant-design-vue'
import type { NotificationPlacement } from 'ant-design-vue'
import type { VNode } from 'vue'
import i18n from '@/locales'
import { useLocaleStore } from '@/stores/routine/localization/locale'

notification.config({ placement: 'topRight' })

function t(key: string): string {
  const locale = useLocaleStore().locale
  type I18nT = (key: string, values?: object, options?: { locale?: string }) => string
  const translate = i18n.global.t as I18nT
  return String(translate(key, {}, { locale }))
}

export type NotifyType = 'success' | 'info' | 'error' | 'warning'

export interface NotifyOptions {
  message: string
  description?: string | VNode
  type?: NotifyType
  duration?: number | null
  placement?: NotificationPlacement
  key?: string
  onClose?: () => void
}

/** 通用通知 */
export function notify(options: NotifyOptions): void {
  const { type = 'info', message, description, duration, placement, key, onClose, ...rest } = options
  notification[type]({ message, description, duration, placement, key, onClose, ...rest })
}

export const API_CONNECT_FAIL_KEY = 'api-connect-fail'

export function closeApiConnectFailNotification(): void {
  notification.close(API_CONNECT_FAIL_KEY)
}

export function showApiConnectFail(options?: Partial<NotifyOptions>): void {
  notification.error({
    message: t('common.api.connectFail'),
    description: t('common.api.connectFailDescription'),
    placement: 'topRight',
    duration: 4.5,
    key: API_CONNECT_FAIL_KEY,
    ...options
  })
}

/** API 错误提示，支持 message 中含 \\n 换行（首行作 message，余下作 description） */
export function showApiError(message: string, description?: string): void {
  const lines = message.split('\n')
  const msg = lines[0]
  const desc = description ?? (lines.length > 1 ? lines.slice(1).join('\n') : undefined)
  notification.error({
    message: msg,
    description: desc,
    placement: 'topRight',
    duration: 4.5
  })
}

export function showSignalrConnectFail(options?: Partial<NotifyOptions>): void {
  notify({
    type: 'error',
    message: t('stores.signalr.connectFail'),
    placement: 'topRight',
    ...options
  })
}

export function showOnlineNotify(options: Partial<NotifyOptions> & { description: string }): void {
  notify({
    type: 'success',
    message: t('stores.signalr.onlineNotify'),
    placement: 'topRight',
    duration: 4.5,
    ...options
  })
}

export function showNewMessage(options: Omit<NotifyOptions, 'type'> & { message: string; description: string }): void {
  notify({ type: 'info', placement: 'topRight', duration: 5, ...options })
}