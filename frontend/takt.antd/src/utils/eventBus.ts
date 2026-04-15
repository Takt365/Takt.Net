// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/utils/eventBus
// 文件名称：eventBus.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：基于 mitt 的全局事件中心，替代 Vue 2 的 $on/$off/$emit，用于跨层级/模块通信
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import mitt, { type Emitter } from 'mitt'

type Events = Record<string, unknown>

const emitter: Emitter<Events> = mitt<Events>()

/**
 * 全局事件总线（Vue 3 推荐用 mitt 替代已移除的实例 $on/$off/$emit）
 * 使用约定：在 onUnmounted 中 $off 移除监听，避免内存泄漏；事件名使用下方常量避免拼写错误。
 */
export const eventBus = {
  $on: emitter.on.bind(emitter),
  $emit: emitter.emit.bind(emitter),
  $off: emitter.off.bind(emitter),
  all: emitter.all
}

/** 认证相关事件名（命名规范，避免拼写错误） */
export const AuthEvents = {
  /** 需要跳转登录页（如 401、网络错误时由 request 发布） */
  RedirectToLogin: 'auth:redirectToLogin',
  /** 已登出（由 user store 发布，订阅方执行清除动态路由等） */
  DidLogout: 'auth:didLogout',
  /** 登录成功（由 user store 发布，订阅方启动 token 刷新定时器等） */
  LoginSuccess: 'auth:loginSuccess',
  /** Token 已刷新（由 request 发布，订阅方可重连 SignalR） */
  TokenRefreshed: 'auth:tokenRefreshed'
} as const

export default eventBus