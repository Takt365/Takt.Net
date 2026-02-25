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
import { ref, computed } from 'vue'
import { signalRManager } from '@/utils/signalr'
import * as signalR from '@microsoft/signalr'
import type {
  OnlineUser,
  UserConnectedEvent,
  UserDisconnectedEvent,
  SignalRMessage,
  BroadcastMessage,
  MessageSentEvent,
  MessageReadEvent,
  SignalRErrorEvent,
  OnlineMessageEvent,
  LoginRequestElsewhereEvent
} from '@/types/routine/signalr/signalr'
import dayjs from 'dayjs'
import { message, Modal } from 'ant-design-vue'
import router from '@/router'
import i18n from '@/locales'
import { logger } from '@/utils/logger'
import { showSignalrConnectFail, showOnlineNotify, showNewMessage } from '@/utils/notification'
import { useUserStore } from '@/stores/identity/user'

const t = (key: string, params?: Record<string, unknown>) =>
  String(params ? (i18n.global.t as (k: string, p?: object) => unknown)(key, params) : (i18n.global.t as (k: string) => unknown)(key))

export const useSignalRStore = defineStore('signalr', () => {
  // 状态
  const connectHubState = ref<signalR.HubConnectionState>(signalR.HubConnectionState.Disconnected)
  const notificationHubState = ref<signalR.HubConnectionState>(signalR.HubConnectionState.Disconnected)
  const onlineUsers = ref<OnlineUser[]>([])
  const unreadCount = ref<number>(0)
  const messages = ref<SignalRMessage[]>([])
  const broadcastMessages = ref<BroadcastMessage[]>([])

  // 计算属性
  const isConnected = computed(() => {
    return (
      connectHubState.value === signalR.HubConnectionState.Connected &&
      notificationHubState.value === signalR.HubConnectionState.Connected
    )
  })

  // 事件回调
  const handleUserConnected = (event: UserConnectedEvent) => {
    // 可以在这里更新在线用户列表
    logger.debug('[SignalR Store] 用户已连接:', event)
  }

  const handleUserDisconnected = (event: UserDisconnectedEvent) => {
    // 可以在这里更新在线用户列表
    logger.debug('[SignalR Store] 用户已断开连接:', event)
  }

  const handleReceiveMessage = (msg: SignalRMessage) => {
    messages.value.push(msg)
    if (msg.readStatus === 0) {
      unreadCount.value++
    }
    showNewMessage({
      message: t('stores.signalr.newMessage'),
      description: `${msg.fromUserName}: ${msg.messageContent}`,
      duration: 3
    })
  }

  const handleReceiveBroadcast = (msg: BroadcastMessage) => {
    broadcastMessages.value.push(msg)
    showNewMessage({
      message: t('stores.signalr.newMessage'),
      description: `[${t('stores.signalr.broadcastLabel')}] ${msg.messageContent}`,
      duration: 5
    })
  }

  const handleMessageSent = (event: MessageSentEvent) => {
    logger.debug('[SignalR Store] 消息已发送:', event)
  }

  const handleMessageRead = (event: MessageReadEvent) => {
    // 更新消息状态
    const message = messages.value.find((m) => (m as any).messageId === event.messageId)
    if (message) {
      message.readStatus = 1
      if (unreadCount.value > 0) {
        unreadCount.value--
      }
    }
  }

  const handleError = (error: SignalRErrorEvent) => {
    logger.error('[SignalR Store] SignalR 错误:', error)
    message.error(error.message || t('stores.signalr.error'))
  }

  const handleOnlineMessage = (event: OnlineMessageEvent) => {
    const name = event.realName || event.userName || ''
    const time = event.connectTime ? dayjs(event.connectTime).format('YYYY-MM-DD HH:mm:ss') : ''
    const description = t('stores.signalr.onlineWelcome', { name, time })
    logger.info('[SignalR Store] 收到上线消息:', description)
    showOnlineNotify({ description })
  }

  const handleLoginRequestElsewhere = (event: LoginRequestElsewhereEvent) => {
    const location = event.requestLocation || '其他位置'
    const title = t('stores.signalr.loginRequestElsewhereTitle')
    const content = t('stores.signalr.loginRequestElsewhereContent', { location })
    Modal.confirm({
      title,
      content,
      okText: t('stores.signalr.exitCurrentLogin'),
      cancelText: t('common.button.cancel'),
      onOk: async () => {
        try {
          const userStore = useUserStore()
          await userStore.logout()
          try {
            await router.push('/login')
          } catch (pushErr) {
            logger.error('[SignalR Store] 跳转登录页失败，使用 location 兜底:', pushErr)
            window.location.href = '/login'
          }
        } catch (err) {
          const msg = (err as Error)?.message
          logger.error('[SignalR Store] 退出当前登录失败:', msg ?? err)
          message.error(t('stores.signalr.exitCurrentLoginFail'))
          throw err
        }
      }
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
        onOnlineMessage: handleOnlineMessage,
        onLoginRequestElsewhere: handleLoginRequestElsewhere
      })
      updateConnectionState()
      logger.info('[SignalR Store] SignalR 连接成功')
      
      // 获取在线用户列表
      try {
        await refreshOnlineUsers()
        logger.debug('[SignalR Store] 已获取在线用户列表')
      } catch (error: any) {
        logger.error('[SignalR Store] 获取在线用户列表失败:', error.message || error)
      }
      
      // 获取未读消息数量
      try {
        await refreshUnreadCount()
        logger.debug('[SignalR Store] 已获取未读消息数量')
      } catch (error: any) {
        logger.error('[SignalR Store] 获取未读消息数量失败:', error.message || error)
      }
    } catch (error: any) {
      logger.error('[SignalR Store] SignalR 连接失败，错误:', error.message || error)
      showSignalrConnectFail()
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
    } catch (error: any) {
      logger.error('[SignalR Store] SignalR 断开连接失败，错误:', error.message || error)
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
      message.error(t('stores.signalr.sendFail'))
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
      message.error(t('stores.signalr.broadcastFail'))
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
