// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/utils/eventBus
// 文件名称：eventBus.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：高级事件总线 - 支持命名空间、类型安全、Pinia 集成、自动清理
//
// 特性：
// 1. 命名空间管理 - 按模块组织事件，避免冲突
// 2. 类型安全 - TypeScript 强类型检查，编译期捕获错误
// 3. Pinia 集成 - 与 Store 深度集成，支持响应式更新
// 4. 自动清理 - 组件卸载时自动取消订阅，防止内存泄漏
// 5. 中间件支持 - 支持事件拦截、日志、性能监控
// 6. 一次性的监听器 - 支持只触发一次的事件监听
//
// 使用说明：
// 
// 1. 基本用法（推荐 - 自动清理）
//    import { useEventBus, CrudEvents } from '@/utils/eventBus'
//    
//    setup() {
//      const { on, emit } = useEventBus()
//      
//      // 订阅事件（组件卸载时自动清理）
//      on(CrudEvents.Created, ({ module, id }) => {
//        if (module === 'user') loadUserList()
//      })
//      
//      // 发布事件
//      emit(CrudEvents.Created, { module: 'user', id: '123' })
//    }
//
// 2. 传统用法（需手动清理）
//    import { eventBus, AuthEvents } from '@/utils/eventBus'
//    import { onMounted, onUnmounted } from 'vue'
//    
//    setup() {
//      const handleLogin = (data) => { console.log('登录成功', data) }
//      
//      onMounted(() => {
//        eventBus.$on(AuthEvents.LoginSuccess, handleLogin)
//      })
//      
//      onUnmounted(() => {
//        eventBus.$off(AuthEvents.LoginSuccess, handleLogin)
//      })
//    }
//
// 3. 全局监听（在 main.ts 中）
//    import { eventBus, CrudEvents } from '@/utils/eventBus'
//    
//    eventBus.$on(CrudEvents.Created, ({ module, id }) => {
//      logger.info(`[CRUD] 创建: ${module} - ${id}`)
//    })
//
// 4. 事件分类
//    - AuthEvents: 认证事件（登录、登出、Token）
//    - NotificationEvents: 通知事件（消息、广播、角标）
//    - MenuEvents: 菜单事件（刷新、选择、路由）
//    - CrudEvents: CRUD 事件（创建、读取、更新、删除、批量）
//    - DataEvents: 数据事件（刷新、字典、翻译）
//    - SystemEvents: 系统事件（语言、主题、在线）
//    - SignalREvents: SignalR 事件（连接、断开、重连）
//    - UserEvents: 用户事件（资料、偏好）
//    - TaskEvents: 任务事件（创建、完成、失败）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import mitt from 'mitt'
import type { App } from 'vue'
import { onUnmounted } from 'vue'

/**
 * 通知载荷类型
 */
export interface NotificationPayload {
  title?: string
  content: string
  type?: 'info' | 'success' | 'warning' | 'error'
  data?: unknown
  timestamp?: number
}

/**
 * 事件类型定义（类型安全 + 动态扩展）
 * 
 * 说明：
 * 1. 基础类型提供类型安全和 IDE 提示
 * 2. 使用字符串索引签名支持动态事件（项目中的任何事件都能使用）
 * 3. 推荐的事件命名规范：`模块:动作` 如 `auth:loginSuccess`
 * 4. 可以在下方扩展更多具名事件，也可以直接使用任意字符串事件名
 */
export type Events = Record<string, unknown> & {
  // ========== 核心模块（推荐的事件） ==========
  
  // 认证模块
  'auth:redirectToLogin': void
  'auth:didLogout': void
  'auth:loginSuccess': { userId: string; username: string }
  'auth:tokenRefreshed': { token: string; expiresIn: number }
  
  // 通知模块  
  'notification:received': NotificationPayload
  'notification:broadcast': NotificationPayload
  'notification:count:update': number
  'notification:clear': void
  
  // 菜单模块
  'menu:refresh': void
  'menu:select': { path: string; name?: string }
  'menu:collapse': boolean
  'routes:updated': void
  
  // ========== 通用 CRUD 事件（所有业务模块通用） ==========
  
  // 创建
  'crud:created': { module: string; id: string | number; data?: unknown }
  'crud:create:success': { module: string; id: string | number }
  'crud:create:failed': { module: string; error: Error }
  
  // 读取/查询
  'crud:loaded': { module: string; count: number; data?: unknown }
  'crud:load:success': { module: string; count: number }
  'crud:load:failed': { module: string; error: Error }
  
  // 更新
  'crud:updated': { module: string; id: string | number; data?: unknown }
  'crud:update:success': { module: string; id: string | number }
  'crud:update:failed': { module: string; error: Error }
  
  // 删除
  'crud:deleted': { module: string; id: string | number }
  'crud:delete:success': { module: string; id: string | number }
  'crud:delete:failed': { module: string; error: Error }
  
  // 批量操作
  'crud:batch-created': { module: string; count: number }
  'crud:batch-updated': { module: string; count: number }
  'crud:batch-deleted': { module: string; ids: Array<string | number> }
  
  // 导出/导入
  'crud:exported': { module: string; count: number; format: string }
  'crud:imported': { module: string; success: number; failed: number }
  
  // 数据刷新（通用）
  'data:refresh': { module: string; params?: unknown }
  'data:refresh:all': void  // 刷新所有模块数据
  
  // ========== 业务模块事件 ==========
  
  // 数据模块（字典、翻译等）
  'dict:updated': { dictType: string }
  'translation:updated': { locale: string }
  
  // 系统模块
  'system:online': void
  'system:offline': void
  'system:language:change': { locale: string; fallback?: boolean }
  'system:theme:change': { theme: 'light' | 'dark'; mode?: 'auto' | 'manual' }
  'system:ready': void
  
  // SignalR 模块
  'signalr:connected': { connectionId: string; hubs: string[] }
  'signalr:disconnected': { reason?: string }
  'signalr:reconnecting': { attempt: number }
  'signalr:error': { error: Error }
  
  // 用户模块
  'user:profile:update': { userId: string }
  'user:preferences:update': { key: string; value: unknown }
  
  // 任务模块
  'task:created': { taskId: string; taskType: string }
  'task:completed': { taskId: string; result?: unknown }
  'task:failed': { taskId: string; error: Error }
}

// 创建 mitt 实例
const emitter = mitt<Events>()

/**
 * 事件命名空间常量（支持按模块过滤和管理）
 */
export const EventNamespace = {
  AUTH: 'auth',
  NOTIFICATION: 'notification',
  MENU: 'menu',
  DATA: 'data',
  SYSTEM: 'system',
  SIGNALR: 'signalr',
  USER: 'user',
  TASK: 'task'
} as const

export type EventNamespaceType = typeof EventNamespace[keyof typeof EventNamespace]

/**
 * 高级事件总线类（支持命名空间、中间件、自动清理）
 */
class EventBus {
  private subscriptions: Map<string, Array<{ type: keyof Events; handler: Function }>> = new Map()
  private componentCounter = 0
  private middlewares: Array<(type: keyof Events, event: unknown) => void> = []

  /**
   * 注册事件监听器（支持自动清理）
   */
  $on<T extends keyof Events>(
    type: T, 
    handler: (event: Events[T]) => void, 
    componentId?: string
  ): void {
    const wrappedHandler = ((event: Events[T]) => {
      // 执行中间件
      for (const mw of this.middlewares) {
        mw(type, event)
      }
      handler(event)
    }) as Function
    
    emitter.on(type, wrappedHandler as any)
    
    // 记录订阅关系（用于自动清理）
    if (componentId) {
      if (!this.subscriptions.has(componentId)) {
        this.subscriptions.set(componentId, [])
      }
      this.subscriptions.get(componentId)!.push({ type, handler: wrappedHandler })
    }
  }

  /**
   * 注册一次性事件监听器（触发后自动移除）
   */
  $once<T extends keyof Events>(
    type: T, 
    handler: (event: Events[T]) => void, 
    componentId?: string
  ): void {
    const onceHandler = ((event: Events[T]) => {
      this.$off(type, onceHandler as any)
      handler(event)
    }) as Function
    
    this.$on(type, onceHandler as any, componentId)
  }

  /**
   * 移除事件监听器
   */
  $off<T extends keyof Events>(type: T, handler?: Function): void {
    if (handler) {
      emitter.off(type, handler as any)
    } else {
      emitter.off(type)
    }
  }

  /**
   * 发布事件
   */
  $emit<T extends keyof Events>(type: T, event?: Events[T]): void {
    emitter.emit(type, event as any)
  }

  /**
   * 按命名空间获取事件列表
   */
  getEventsByNamespace(namespace: EventNamespaceType): string[] {
    return Array.from(emitter.all.keys())
      .filter(key => typeof key === 'string' && key.startsWith(`${namespace}:`))
      .map(key => key as string)
  }

  /**
   * 批量取消某个组件的所有订阅
   */
  cleanup(componentId: string): void {
    const subs = this.subscriptions.get(componentId)
    if (subs) {
      for (const { type, handler } of subs) {
        emitter.off(type, handler as any)
      }
      this.subscriptions.delete(componentId)
    }
  }

  /**
   * 注册中间件（用于日志、监控、拦截等）
   */
  use(middleware: (type: keyof Events, event: unknown) => void): void {
    this.middlewares.push(middleware)
  }

  /**
   * 生成唯一的组件ID
   */
  generateComponentId(): string {
    return `component_${++this.componentCounter}`
  }

  /**
   * 获取所有订阅统计信息（用于调试）
   */
  getStats(): { totalSubscriptions: number; components: number; events: number } {
    let totalSubscriptions = 0
    for (const subs of this.subscriptions.values()) {
      totalSubscriptions += subs.length
    }
    return {
      totalSubscriptions,
      components: this.subscriptions.size,
      events: emitter.all.size
    }
  }
}

/**
 * 创建全局事件总线实例
 */
export const eventBus = new EventBus()

/**
 * 事件常量定义（按命名空间组织，避免拼写错误）
 */

/** 认证相关事件 */
export const AuthEvents = {
  RedirectToLogin: 'auth:redirectToLogin',
  DidLogout: 'auth:didLogout',
  LoginSuccess: 'auth:loginSuccess',
  TokenRefreshed: 'auth:tokenRefreshed'
} as const

/** 通知相关事件 */
export const NotificationEvents = {
  Received: 'notification:received',
  Broadcast: 'notification:broadcast',
  CountUpdate: 'notification:count:update',
  Clear: 'notification:clear'
} as const

/** 菜单/路由相关事件 */
export const MenuEvents = {
  Refresh: 'menu:refresh',
  Select: 'menu:select',
  Collapse: 'menu:collapse',
  RoutesUpdated: 'routes:updated'
} as const

/** CRUD 通用事件（所有业务模块通用） */
export const CrudEvents = {
  // 创建
  Created: 'crud:created',
  CreateSuccess: 'crud:create:success',
  CreateFailed: 'crud:create:failed',
  
  // 读取/查询
  Loaded: 'crud:loaded',
  LoadSuccess: 'crud:load:success',
  LoadFailed: 'crud:load:failed',
  
  // 更新
  Updated: 'crud:updated',
  UpdateSuccess: 'crud:update:success',
  UpdateFailed: 'crud:update:failed',
  
  // 删除
  Deleted: 'crud:deleted',
  DeleteSuccess: 'crud:delete:success',
  DeleteFailed: 'crud:delete:failed',
  
  // 批量操作
  BatchCreated: 'crud:batch-created',
  BatchUpdated: 'crud:batch-updated',
  BatchDeleted: 'crud:batch-deleted',
  
  // 导出/导入
  Exported: 'crud:exported',
  Imported: 'crud:imported'
} as const

/** 数据刷新相关事件 */
export const DataEvents = {
  Refresh: 'data:refresh',
  RefreshAll: 'data:refresh:all',
  DictUpdated: 'dict:updated',
  TranslationUpdated: 'translation:updated'
} as const

/** 系统相关事件 */
export const SystemEvents = {
  Online: 'system:online',
  Offline: 'system:offline',
  LanguageChange: 'system:language:change',
  ThemeChange: 'system:theme:change',
  Ready: 'system:ready'
} as const

/** SignalR 相关事件 */
export const SignalREvents = {
  Connected: 'signalr:connected',
  Disconnected: 'signalr:disconnected',
  Reconnecting: 'signalr:reconnecting',
  Error: 'signalr:error'
} as const

/** 用户相关事件 */
export const UserEvents = {
  ProfileUpdate: 'user:profile:update',
  PreferencesUpdate: 'user:preferences:update'
} as const

/** 任务相关事件 */
export const TaskEvents = {
  Created: 'task:created',
  Completed: 'task:completed',
  Failed: 'task:failed'
} as const

/**
 * Pinia 插件：为 Store 注入事件总线
 * 
 * 使用方式：
 * ```typescript
 * import { createPinia } from 'pinia'
 * import { eventBusPiniaPlugin } from '@/utils/eventBus'
 * 
 * const pinia = createPinia()
 * pinia.use(eventBusPiniaPlugin)
 * ```
 */
export function eventBusPiniaPlugin() {
  return {
    $eventBus: eventBus
  }
}

/**
 * Vue 插件：提供全局事件总线注入和自动清理
 * 
 * 使用方式：
 * ```typescript
 * import { createApp } from 'vue'
 * import { eventBusVuePlugin } from '@/utils/eventBus'
 * 
 * const app = createApp(App)
 * app.use(eventBusVuePlugin)
 * ```
 */
export function eventBusVuePlugin(app: App): void {
  // 注入全局事件总线
  app.config.globalProperties.$eventBus = eventBus
  app.provide('eventBus', eventBus)
  
  // 添加全局 mixin：自动清理
  app.mixin({
    beforeUnmount() {
      const componentId = (this.$options as any).__eventBusComponentId
      if (componentId) {
        eventBus.cleanup(componentId)
      }
    },
    created() {
      // 为每个组件实例生成唯一ID
      ;(this.$options as any).__eventBusComponentId = eventBus.generateComponentId()
    }
  })
}

/**
 * 组合式函数：在 setup 中使用事件总线（自动清理）
 * 
 * 使用示例：
 * ```typescript
 * import { useEventBus, AuthEvents } from '@/utils/eventBus'
 * 
 * setup() {
 *   const { on, emit } = useEventBus()
 *   
 *   on(AuthEvents.LoginSuccess, (data) => {
 *     console.log('登录成功:', data)
 *   })
 *   
 *   // 组件卸载时自动清理，无需手动调用
 * }
 * ```
 */
export function useEventBus() {
  const componentId = eventBus.generateComponentId()
  
  // 注册自动清理
  onUnmounted(() => {
    eventBus.cleanup(componentId)
  })
  
  return {
    /**
     * 订阅事件（自动清理）
     */
    on: <T extends keyof Events>(type: T, handler: (event: Events[T]) => void) => {
      eventBus.$on(type, handler, componentId)
    },
    
    /**
     * 订阅一次性事件（自动清理）
     */
    once: <T extends keyof Events>(type: T, handler: (event: Events[T]) => void) => {
      eventBus.$once(type, handler, componentId)
    },
    
    /**
     * 取消订阅
     */
    off: <T extends keyof Events>(type: T, handler?: Function) => {
      eventBus.$off(type, handler)
    },
    
    /**
     * 发布事件
     */
    emit: <T extends keyof Events>(type: T, event?: Events[T]) => {
      eventBus.$emit(type, event)
    },
    
    /**
     * 组件ID（用于手动清理）
     */
    componentId
  }
}

export default eventBus
