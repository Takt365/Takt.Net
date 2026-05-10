// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/stores/identity/signalr
// 文件名称：signalr.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：SignalR 状态管理 Store
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import { defineStore } from 'pinia'
import type { VNodeChild } from 'vue'
import { signalRManager } from '@/utils/signalr'
import * as signalR from '@microsoft/signalr'
import type {
  OnlineUserDto,
  UserConnectedEventDto,
  UserDisconnectedEventDto,
  SignalRMessageDto,
  BroadcastMessageDto,
  MessageSentEventDto,
  MessageReadEventDto,
  SignalRErrorEventDto,
  OnlineMessageEventDto
} from '@/types/routine/tasks/signal-r/specific-engine/signal-r'
import { message, notification } from 'ant-design-vue'
import i18n from '@/locales'
import { logger } from '@/utils/logger'

/** 非 setup 场景下 `i18n.global.t` 为 Composer / Legacy 联合类型，TS 无法直接调用；收窄为可调用签名 */
function tGlobal(key: string): string {
  return String((i18n.global as { t: (k: string) => unknown }).t(key))
}

type MessageWithId = SignalRMessageDto & { messageId?: string | number }
const toErrorMessage = (error: unknown): string =>
  error instanceof Error ? error.message : String(error)

export const useSignalRStore = defineStore('signalr', () => {
  // 状态
  const connectHubState = ref<signalR.HubConnectionState>(signalR.HubConnectionState.Disconnected)
  const notificationHubState = ref<signalR.HubConnectionState>(signalR.HubConnectionState.Disconnected)
  const onlineUsers = ref<OnlineUserDto[]>([])
  const unreadCount = ref<number>(0)
  const messages = ref<SignalRMessageDto[]>([])
  const broadcastMessages = ref<BroadcastMessageDto[]>([])

  // 计算属性
  const isConnected = computed(() => {
    return (
      connectHubState.value === signalR.HubConnectionState.Connected &&
      notificationHubState.value === signalR.HubConnectionState.Connected
    )
  })

  // 事件回调
  const handleUserConnected = (event: UserConnectedEventDto) => {
    // 可以在这里更新在线用户列表
    logger.debug('[SignalR Store] 用户已连接:', event)
  }

  const handleUserDisconnected = (event: UserDisconnectedEventDto) => {
    // 可以在这里更新在线用户列表
    logger.debug('[SignalR Store] 用户已断开连接:', event)
  }

  const handleReceiveMessage = (msg: SignalRMessageDto) => {
    messages.value.push(msg)
    if (msg.readStatus === 0) {
      unreadCount.value++
    }
    // 显示通知
    message.info({
      content: `${msg.fromUserName}: ${msg.messageContent}`,
      duration: 3
    })
  }

  const handleReceiveBroadcast = (msg: BroadcastMessageDto) => {
    broadcastMessages.value.push(msg)
    // 显示通知
    message.info({
      content: `[${tGlobal('stores.page.signalr.broadcastlabel')}] ${msg.messageContent}`,
      duration: 5
    })
  }

  const handleMessageSent = (event: MessageSentEventDto) => {
    logger.debug('[SignalR Store] 消息已发送:', event)
  }

  const handleMessageRead = (event: MessageReadEventDto) => {
    // 更新消息状态
    const msg = messages.value.find((m: MessageWithId) => String(m.messageId) === String(event.messageId))
    if (msg) {
      msg.readStatus = 1
      if (unreadCount.value > 0) {
        unreadCount.value--
      }
    }
  }

  const handleError = (error: SignalRErrorEventDto) => {
    logger.error('[SignalR Store] SignalR 错误:', error)
    const errText = String(error.message ?? '').trim()
    message.error(errText || tGlobal('stores.page.signalr.error'))
  }

  const handleOnlineMessage = (event: OnlineMessageEventDto) => {
    logger.info('[SignalR Store] 收到上线消息:', event.message)
    // OpenAPI 推断的 message 为 unknown，需转为 VNodeChild 才能满足 Notification 的 props
    const description: VNodeChild = String(event.message ?? '')
    // 显示上线通知（使用 Notification，位置在右上角）
    notification.success({
      message: tGlobal('stores.page.signalr.onlinenotify'),
      description,
      placement: 'topRight',
      duration: 10
    })
  }

  // 更新连接状态
  const updateConnectionState = () => {
    const state = signalRManager.getConnectionState()
    connectHubState.value = state.connectHub
    notificationHubState.value = state.notificationHub
  }

  // 连接
  const connect = async () => {
    logger.info('[SignalR Store] 开始连接 SignalR')
    try {
      await signalRManager.connect({
        onUserConnected: handleUserConnected,
        onUserDisconnected: handleUserDisconnected,
        onReceiveMessage: handleReceiveMessage,
        onReceiveBroadcast: handleReceiveBroadcast,
        onMessageSent: handleMessageSent,
        onMessageRead: handleMessageRead,
        onError: handleError,
        onOnlineMessage: handleOnlineMessage
      })
      updateConnectionState()
      logger.info('[SignalR Store] SignalR 连接成功')
      
      // 优化：将获取在线用户列表和未读消息数量放到后台执行，不阻塞连接
      // 使用 setTimeout 延迟执行，让页面先渲染
      setTimeout(() => {
        // 获取在线用户列表
        refreshOnlineUsers().then(() => {
          logger.debug('[SignalR Store] 已获取在线用户列表')
        }).catch((error: unknown) => {
          logger.error('[SignalR Store] 获取在线用户列表失败:', toErrorMessage(error))
        })
        
        // 获取未读消息数量
        refreshUnreadCount().then(() => {
          logger.debug('[SignalR Store] 已获取未读消息数量')
        }).catch((error: unknown) => {
          logger.error('[SignalR Store] 获取未读消息数量失败:', toErrorMessage(error))
        })
      }, 0)
    } catch (error: unknown) {
      logger.error('[SignalR Store] SignalR 连接失败，错误:', toErrorMessage(error))
      message.error(tGlobal('stores.page.signalr.connectfail'))
      throw error
    }
  }

  // 断开连接
  const disconnect = async () => {
    logger.info('[SignalR Store] 开始断开 SignalR 连接')
    try {
      await signalRManager.disconnect()
      updateConnectionState()
      onlineUsers.value = []
      messages.value = []
      broadcastMessages.value = []
      unreadCount.value = 0
      logger.info('[SignalR Store] SignalR 断开连接成功')
    } catch (error: unknown) {
      logger.error('[SignalR Store] SignalR 断开连接失败，错误:', toErrorMessage(error))
      throw error
    }
  }

  // 刷新在线用户列表
  const refreshOnlineUsers = async () => {
    try {
      const users = await signalRManager.getOnlineUsers()
      onlineUsers.value = users
    } catch (error) {
      logger.error('[SignalR Store] 获取在线用户列表失败:', error)
    }
  }

  // 刷新未读消息数量
  const refreshUnreadCount = async () => {
    try {
      const count = await signalRManager.getUnreadCount()
      unreadCount.value = count
    } catch (error) {
      logger.error('[SignalR Store] 获取未读消息数量失败:', error)
    }
  }

  // 发送消息
  const sendMessage = async (
    toUserName: string,
    messageContent: string,
    messageTitle?: string,
    messageType = 'Text',
    messageGroup?: string,
    messageExtData?: string
  ) => {
    try {
      await signalRManager.sendMessage(toUserName, messageContent, messageTitle, messageType, messageGroup, messageExtData)
    } catch (error) {
      logger.error('[SignalR Store] 发送消息失败:', error)
      message.error(tGlobal('stores.page.signalr.sendfail'))
      throw error
    }
  }

  // 发送广播消息
  const broadcastMessage = async (
    messageContent: string,
    messageTitle?: string,
    messageType = 'System',
    messageGroup = 'Notification'
  ) => {
    try {
      await signalRManager.broadcastMessage(messageContent, messageTitle, messageType, messageGroup)
    } catch (error) {
      logger.error('[SignalR Store] 发送广播消息失败:', error)
      message.error(tGlobal('stores.page.signalr.broadcastfail'))
      throw error
    }
  }

  // 标记消息为已读
  const markAsRead = async (messageId: number) => {
    try {
      await signalRManager.markAsRead(messageId)
    } catch (error) {
      logger.error('[SignalR Store] 标记消息为已读失败:', error)
      throw error
    }
  }

  return {
    // 状态
    connectHubState,
    notificationHubState,
    onlineUsers,
    unreadCount,
    messages,
    broadcastMessages,
    // 计算属性
    isConnected,
    // 方法
    connect,
    disconnect,
    refreshOnlineUsers,
    refreshUnreadCount,
    sendMessage,
    broadcastMessage,
    markAsRead,
    updateConnectionState
  }
})
