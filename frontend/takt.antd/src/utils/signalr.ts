// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/utils/signalr
// 文件名称：signalr.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：SignalR 连接管理工具类
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

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
  OnlineMessageEvent
} from '@/types/routine/tasks/signalr/signalr'
import { logger } from './logger'

const toErrorMessage = (e: unknown): string => (e instanceof Error ? e.message : String(e))

/**
 * SignalR 自定义日志记录器（将 UTC 时间转换为本地时间）
 */
class TaktSignalRLogger implements signalR.ILogger {
  private readonly logLevel: signalR.LogLevel

  constructor(logLevel: signalR.LogLevel) {
    this.logLevel = logLevel
  }

  /**
   * 格式化日期时间为本地时间
   */
  private formatLocalTime(date: Date = new Date()): string {
    const year = date.getFullYear()
    const month = String(date.getMonth() + 1).padStart(2, '0')
    const day = String(date.getDate()).padStart(2, '0')
    const hours = String(date.getHours()).padStart(2, '0')
    const minutes = String(date.getMinutes()).padStart(2, '0')
    const seconds = String(date.getSeconds()).padStart(2, '0')
    const milliseconds = String(date.getMilliseconds()).padStart(3, '0')
    return `${year}-${month}-${day} ${hours}:${minutes}:${seconds}.${milliseconds}`
  }

  log(logLevel: signalR.LogLevel, message: string): void {
    if (logLevel >= this.logLevel) {
      const localTime = this.formatLocalTime()
      const levelName = signalR.LogLevel[logLevel] || 'Unknown'
      
      // 根据日志级别使用不同的 logger 方法
      switch (logLevel) {
        case signalR.LogLevel.Critical:
        case signalR.LogLevel.Error:
          logger.error(`[SignalR ${levelName}] [${localTime}] ${message}`)
          break
        case signalR.LogLevel.Warning:
          logger.warn(`[SignalR ${levelName}] [${localTime}] ${message}`)
          break
        case signalR.LogLevel.Information:
          logger.info(`[SignalR ${levelName}] [${localTime}] ${message}`)
          break
        case signalR.LogLevel.Debug:
        case signalR.LogLevel.Trace:
          logger.debug(`[SignalR ${levelName}] [${localTime}] ${message}`)
          break
        default:
          logger.debug(`[SignalR ${levelName}] [${localTime}] ${message}`)
      }
    }
  }
}

/**
 * SignalR 连接管理器
 */
export class TaktSignalRManager {
  private connectHub: signalR.HubConnection | null = null
  private notificationHub: signalR.HubConnection | null = null
  private reconnectAttempts = 0
  private maxReconnectAttempts = 5
  private reconnectDelay = 3000
  private heartbeatInterval: number | null = null
  private heartbeatIntervalMs = 30000 // 30秒心跳

  /**
   * 获取 API 基础 URL
   */
  private getBaseUrl(): string {
    // 优先使用环境变量中的 API 目标地址
    const apiTarget = import.meta.env.VITE_API_TARGET
    if (apiTarget) {
      return apiTarget
    }
    
    // 如果没有配置，使用当前页面的协议和主机名，但使用 API 端口
    const protocol = window.location.protocol === 'https:' ? 'https:' : 'http:'
    const hostname = window.location.hostname
    // 默认使用后端 API 端口（不是前端开发服务器端口）
    const port = import.meta.env.VITE_API_PORT || (protocol === 'https:' ? '60071' : '60080')
    return `${protocol}//${hostname}:${port}`
  }

  /**
   * 获取访问令牌（从 localStorage 读取）
   */
  private getAccessToken(): string | null {
    return localStorage.getItem('token')
  }

  /**
   * 创建连接 Hub
   */
  private createConnectHub(): signalR.HubConnection {
    const baseUrl = this.getBaseUrl()
    const hubUrl = `${baseUrl}/hubs/TaktConnectHub`

    return new signalR.HubConnectionBuilder()
      .withUrl(hubUrl, {
        withCredentials: true, // 允许发送 Cookie
        skipNegotiation: false,
        transport: signalR.HttpTransportType.WebSockets | signalR.HttpTransportType.LongPolling,
        accessTokenFactory: () => {
          // 返回访问令牌（Bearer token）
          // 注意：对于 WebSocket，token 会通过 access_token 查询参数传递
          // SignalR 会在每次连接和重连时调用此函数获取最新的 token
          const token = this.getAccessToken()
          if (token) {
            logger.debug('[SignalR] accessTokenFactory 返回 token，长度:', token.length)
          } else {
            logger.warn('[SignalR] accessTokenFactory 返回空 token，连接可能失败（后端 Hub 需要 [Authorize]）')
          }
          return Promise.resolve(token || '')
        }
      })
      .withAutomaticReconnect({
        nextRetryDelayInMilliseconds: (retryContext) => {
          if (retryContext.previousRetryCount < this.maxReconnectAttempts) {
            return this.reconnectDelay * (retryContext.previousRetryCount + 1)
          }
          return null // 停止重连
        }
      })
      .configureLogging(new TaktSignalRLogger(signalR.LogLevel.Information))
      .build()
  }

  /**
   * 创建通知 Hub
   */
  private createNotificationHub(): signalR.HubConnection {
    const baseUrl = this.getBaseUrl()
    const hubUrl = `${baseUrl}/hubs/TaktNotificationHub`

    return new signalR.HubConnectionBuilder()
      .withUrl(hubUrl, {
        withCredentials: true, // 允许发送 Cookie
        skipNegotiation: false,
        transport: signalR.HttpTransportType.WebSockets | signalR.HttpTransportType.LongPolling,
        accessTokenFactory: () => {
          // 返回访问令牌（Bearer token）
          // 注意：对于 WebSocket，token 会通过 access_token 查询参数传递
          // SignalR 会在每次连接和重连时调用此函数获取最新的 token
          const token = this.getAccessToken()
          if (token) {
            logger.debug('[SignalR] accessTokenFactory 返回 token，长度:', token.length)
          } else {
            logger.warn('[SignalR] accessTokenFactory 返回空 token，连接可能失败（后端 Hub 需要 [Authorize]）')
          }
          return Promise.resolve(token || '')
        }
      })
      .withAutomaticReconnect({
        nextRetryDelayInMilliseconds: (retryContext) => {
          if (retryContext.previousRetryCount < this.maxReconnectAttempts) {
            return this.reconnectDelay * (retryContext.previousRetryCount + 1)
          }
          return null // 停止重连
        }
      })
      .configureLogging(new TaktSignalRLogger(signalR.LogLevel.Information))
      .build()
  }

  /**
   * 注册连接 Hub 事件监听器
   */
  private registerConnectHubEvents(
    onUserConnected?: (event: UserConnectedEvent) => void,
    onUserDisconnected?: (event: UserDisconnectedEvent) => void,
    onOnlineMessage?: (event: OnlineMessageEvent) => void
  ): void {
    if (!this.connectHub) return

    // 上线消息事件
    this.connectHub.on('OnlineMessage', (event: OnlineMessageEvent) => {
      logger.info('[SignalR] 收到上线消息:', event)
      onOnlineMessage?.(event)
    })

    // 用户连接事件
    this.connectHub.on('UserConnected', (event: UserConnectedEvent) => {
      logger.debug('[SignalR] 用户已连接:', event)
      onUserConnected?.(event)
    })

    // 用户断开连接事件
    this.connectHub.on('UserDisconnected', (event: UserDisconnectedEvent) => {
      logger.debug('[SignalR] 用户已断开连接:', event)
      onUserDisconnected?.(event)
    })

    // 连接状态变化
    this.connectHub.onreconnecting((error) => {
      logger.warn('[SignalR] 连接 Hub 正在重连，错误:', error?.message || error)
      this.reconnectAttempts++
      
      // 重连时检查 token 是否过期，如果过期则先刷新 token
      const token = this.getAccessToken()
      if (!token) {
        logger.warn('[SignalR] 重连时发现 token 为空，可能需要重新登录')
        return
      }
    })

    this.connectHub.onreconnected((connectionId) => {
      logger.info('[SignalR] 连接 Hub 已重新连接成功，ConnectionId:', connectionId, '重连次数:', this.reconnectAttempts)
      this.reconnectAttempts = 0
    })

    this.connectHub.onclose(async (error) => {
      if (error) {
        logger.error('[SignalR] 连接 Hub 已关闭，错误:', error.message || error)
        
        // 如果是认证错误（401），可能是 token 过期，尝试刷新 token 后重连
        if (error.message?.includes('401') || error.message?.includes('Unauthorized')) {
          logger.info('[SignalR] 检测到认证错误，尝试刷新 token 后重连')
          try {
            const token = this.getAccessToken()
            if (token) {
              // 检查 token 是否过期（简单检查：尝试刷新 token）
              const { useUserStore } = await import('@/stores/identity/user')
              const userStore = useUserStore()
              if (userStore.refreshToken) {
                // 尝试刷新 token
                const { tryRefreshToken } = await import('@/api/request')
                const refreshSuccess = await tryRefreshToken()
                if (refreshSuccess) {
                  logger.info('[SignalR] Token 刷新成功，将重新连接')
                  // 重新连接会在 tryRefreshToken 中处理
                  return
                }
              }
            }
          } catch (refreshError: unknown) {
            logger.error('[SignalR] 刷新 token 失败:', toErrorMessage(refreshError))
          }
        }
      } else {
        logger.info('[SignalR] 连接 Hub 已正常关闭')
      }
      this.stopHeartbeat()
    })
  }

  /**
   * 注册通知 Hub 事件监听器
   */
  private registerNotificationHubEvents(
    onReceiveMessage?: (message: SignalRMessage) => void,
    onReceiveBroadcast?: (message: BroadcastMessage) => void,
    onMessageSent?: (event: MessageSentEvent) => void,
    onMessageRead?: (event: MessageReadEvent) => void,
    onError?: (error: SignalRErrorEvent) => void,
    onOnlineMessage?: (event: OnlineMessageEvent) => void
  ): void {
    if (!this.notificationHub) return

    // 上线消息事件
    this.notificationHub.on('OnlineMessage', (event: OnlineMessageEvent) => {
      logger.info('[SignalR] 收到上线消息:', event)
      onOnlineMessage?.(event)
    })

    // 接收消息
    this.notificationHub.on('ReceiveMessage', (message: SignalRMessage) => {
      logger.debug('[SignalR] 收到消息:', message)
      onReceiveMessage?.(message)
    })

    // 接收广播消息
    this.notificationHub.on('ReceiveBroadcast', (message: BroadcastMessage) => {
      logger.debug('[SignalR] 收到广播消息:', message)
      onReceiveBroadcast?.(message)
    })

    // 消息已发送
    this.notificationHub.on('MessageSent', (event: MessageSentEvent) => {
      logger.debug('[SignalR] 消息已发送:', event)
      onMessageSent?.(event)
    })

    // 消息已读
    this.notificationHub.on('MessageRead', (event: MessageReadEvent) => {
      logger.debug('[SignalR] 消息已读:', event)
      onMessageRead?.(event)
    })

    // 错误事件
    this.notificationHub.on('Error', (error: SignalRErrorEvent) => {
      logger.error('[SignalR] 通知 Hub 错误:', error)
      onError?.(error)
    })

    // 连接状态变化
    this.notificationHub.onreconnecting((error) => {
      logger.warn('[SignalR] 通知 Hub 正在重连，错误:', error?.message || error)
    })

    this.notificationHub.onreconnected((connectionId) => {
      logger.info('[SignalR] 通知 Hub 已重新连接成功，ConnectionId:', connectionId)
    })

    this.notificationHub.onclose(async (error) => {
      if (error) {
        logger.error('[SignalR] 通知 Hub 已关闭，错误:', error.message || error)
        
        // 如果是认证错误（401），可能是 token 过期
        if (error.message?.includes('401') || error.message?.includes('Unauthorized')) {
          logger.info('[SignalR] 通知 Hub 检测到认证错误，token 可能已过期')
          // 重连逻辑已在 connectHub 的 onclose 中处理
        }
      } else {
        logger.info('[SignalR] 通知 Hub 已正常关闭')
      }
    })
  }

  /**
   * 启动心跳
   */
  private startHeartbeat(): void {
    if (this.heartbeatInterval) {
      return
    }

    this.heartbeatInterval = window.setInterval(() => {
      if (this.connectHub && this.connectHub.state === signalR.HubConnectionState.Connected) {
        this.connectHub.invoke('Heartbeat').catch((error) => {
          logger.error('[SignalR] 心跳失败:', error)
        })
      }
    }, this.heartbeatIntervalMs)
  }

  /**
   * 停止心跳
   */
  private stopHeartbeat(): void {
    if (this.heartbeatInterval) {
      clearInterval(this.heartbeatInterval)
      this.heartbeatInterval = null
    }
  }

  /**
   * 连接所有 Hub
   */
  async connect(
    callbacks?: {
      onUserConnected?: (event: UserConnectedEvent) => void
      onUserDisconnected?: (event: UserDisconnectedEvent) => void
      onReceiveMessage?: (message: SignalRMessage) => void
      onReceiveBroadcast?: (message: BroadcastMessage) => void
      onMessageSent?: (event: MessageSentEvent) => void
      onMessageRead?: (event: MessageReadEvent) => void
      onError?: (error: SignalRErrorEvent) => void
      onOnlineMessage?: (event: OnlineMessageEvent) => void
    }
  ): Promise<void> {
    const baseUrl = this.getBaseUrl()
    const connectHubUrl = `${baseUrl}/hubs/TaktConnectHub`
    const notificationHubUrl = `${baseUrl}/hubs/TaktNotificationHub`
    
    logger.info('[SignalR] 开始连接 SignalR Hub')
    logger.debug('[SignalR] 连接 Hub URL:', connectHubUrl)
    logger.debug('[SignalR] 通知 Hub URL:', notificationHubUrl)
    
    try {
      // 创建连接 Hub
      this.connectHub = this.createConnectHub()
      this.registerConnectHubEvents(
        callbacks?.onUserConnected,
        callbacks?.onUserDisconnected,
        callbacks?.onOnlineMessage
      )

      // 创建通知 Hub
      this.notificationHub = this.createNotificationHub()
      this.registerNotificationHubEvents(
        callbacks?.onReceiveMessage,
        callbacks?.onReceiveBroadcast,
        callbacks?.onMessageSent,
        callbacks?.onMessageRead,
        callbacks?.onError,
        callbacks?.onOnlineMessage
      )

      // 连接两个 Hub
      try {
        logger.info('[SignalR] 正在连接连接 Hub...')
        await this.connectHub.start()
        logger.info('[SignalR] 连接 Hub 连接成功，ConnectionId:', this.connectHub.connectionId)
      } catch (connectError: unknown) {
        logger.error('[SignalR] 连接 Hub 连接失败，URL:', connectHubUrl, '错误:', toErrorMessage(connectError))
        const hubErr = new Error(`连接 Hub 连接失败: ${toErrorMessage(connectError)}`)
        Object.assign(hubErr, { cause: connectError })
        throw hubErr
      }

      try {
        logger.info('[SignalR] 正在连接通知 Hub...')
        await this.notificationHub.start()
        logger.info('[SignalR] 通知 Hub 连接成功，ConnectionId:', this.notificationHub.connectionId)
      } catch (notificationError: unknown) {
        logger.error('[SignalR] 通知 Hub 连接失败，URL:', notificationHubUrl, '错误:', toErrorMessage(notificationError))
        // 如果通知 Hub 连接失败，先断开连接 Hub
        if (this.connectHub.state === signalR.HubConnectionState.Connected) {
          await this.connectHub.stop()
        }
        const notifyHubErr = new Error(`通知 Hub 连接失败: ${toErrorMessage(notificationError)}`)
        Object.assign(notifyHubErr, { cause: notificationError })
        throw notifyHubErr
      }

      logger.info('[SignalR] 所有 Hub 已连接成功')
      this.reconnectAttempts = 0

      // 启动心跳
      this.startHeartbeat()
      logger.info('[SignalR] 心跳已启动')
    } catch (error: unknown) {
      logger.error('[SignalR] SignalR 连接失败，错误:', toErrorMessage(error))
      throw error
    }
  }

  /**
   * 断开所有 Hub
   */
  async disconnect(): Promise<void> {
    logger.info('[SignalR] 开始断开 SignalR Hub 连接')
    this.stopHeartbeat()

    const promises: Promise<void>[] = []

    if (this.connectHub) {
      try {
        const connectionId = this.connectHub.connectionId
        promises.push(
          this.connectHub.stop().then(() => {
            logger.info('[SignalR] 连接 Hub 已断开，ConnectionId:', connectionId)
          }).catch((error: unknown) => {
            logger.error('[SignalR] 断开连接 Hub 失败，ConnectionId:', connectionId, '错误:', toErrorMessage(error))
          })
        )
        this.connectHub = null
      } catch (error: unknown) {
        logger.error('[SignalR] 断开连接 Hub 时发生错误:', toErrorMessage(error))
      }
    }

    if (this.notificationHub) {
      try {
        const connectionId = this.notificationHub.connectionId
        promises.push(
          this.notificationHub.stop().then(() => {
            logger.info('[SignalR] 通知 Hub 已断开，ConnectionId:', connectionId)
          }).catch((error: unknown) => {
            logger.error('[SignalR] 断开通知 Hub 失败，ConnectionId:', connectionId, '错误:', toErrorMessage(error))
          })
        )
        this.notificationHub = null
      } catch (error: unknown) {
        logger.error('[SignalR] 断开通知 Hub 时发生错误:', toErrorMessage(error))
      }
    }

    await Promise.all(promises)
    logger.info('[SignalR] 所有 Hub 已断开')
  }

  /**
   * 获取在线用户列表
   */
  async getOnlineUsers(): Promise<OnlineUser[]> {
    if (!this.connectHub || this.connectHub.state !== signalR.HubConnectionState.Connected) {
      throw new Error('连接 Hub 未连接')
    }

    try {
      const users = await this.connectHub.invoke<OnlineUser[]>('GetOnlineUsers')
      return users || []
    } catch (error) {
      logger.error('[SignalR] 获取在线用户列表失败:', error)
      throw error
    }
  }

  /**
   * 发送消息
   */
  async sendMessage(
    toUserName: string,
    messageContent: string,
    messageTitle?: string,
    messageType = 'Text',
    messageGroup?: string,
    messageExtData?: string
  ): Promise<void> {
    if (!this.notificationHub || this.notificationHub.state !== signalR.HubConnectionState.Connected) {
      throw new Error('通知 Hub 未连接')
    }

    try {
      await this.notificationHub.invoke('SendMessage', toUserName, messageContent, messageTitle, messageType, messageGroup, messageExtData)
    } catch (error) {
      logger.error('[SignalR] 发送消息失败:', error)
      throw error
    }
  }

  /**
   * 发送广播消息
   */
  async broadcastMessage(
    messageContent: string,
    messageTitle?: string,
    messageType = 'System',
    messageGroup = 'Notification'
  ): Promise<void> {
    if (!this.notificationHub || this.notificationHub.state !== signalR.HubConnectionState.Connected) {
      throw new Error('通知 Hub 未连接')
    }

    try {
      await this.notificationHub.invoke('BroadcastMessage', messageContent, messageTitle, messageType, messageGroup)
    } catch (error) {
      logger.error('[SignalR] 发送广播消息失败:', error)
      throw error
    }
  }

  /**
   * 标记消息为已读
   */
  async markAsRead(messageId: number): Promise<void> {
    if (!this.notificationHub || this.notificationHub.state !== signalR.HubConnectionState.Connected) {
      throw new Error('通知 Hub 未连接')
    }

    try {
      await this.notificationHub.invoke('MarkAsRead', messageId)
    } catch (error) {
      logger.error('[SignalR] 标记消息为已读失败:', error)
      throw error
    }
  }

  /**
   * 获取未读消息数量
   */
  async getUnreadCount(): Promise<number> {
    if (!this.notificationHub || this.notificationHub.state !== signalR.HubConnectionState.Connected) {
      throw new Error('通知 Hub 未连接')
    }

    try {
      const count = await this.notificationHub.invoke<number>('GetUnreadCount')
      return count || 0
    } catch (error) {
      logger.error('[SignalR] 获取未读消息数量失败:', error)
      throw error
    }
  }

  /**
   * 获取连接状态
   */
  getConnectionState(): {
    connectHub: signalR.HubConnectionState
    notificationHub: signalR.HubConnectionState
  } {
    return {
      connectHub: this.connectHub?.state ?? signalR.HubConnectionState.Disconnected,
      notificationHub: this.notificationHub?.state ?? signalR.HubConnectionState.Disconnected
    }
  }

  /**
   * 检查是否已连接
   */
  isConnected(): boolean {
    const state = this.getConnectionState()
    return (
      state.connectHub === signalR.HubConnectionState.Connected &&
      state.notificationHub === signalR.HubConnectionState.Connected
    )
  }
}

// 导出单例实例
export const signalRManager = new TaktSignalRManager()
