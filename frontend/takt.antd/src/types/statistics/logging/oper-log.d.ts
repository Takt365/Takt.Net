// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/logging/oper-log
// 文件名称：oper-log.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：操作日志相关类型定义，对应后端 Takt.Application.Dtos.Logging.TaktOperLogDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 操作日志类型（对应后端 Takt.Application.Dtos.Logging.TaktOperLogDto）
 */
export interface OperLog extends TaktEntityBase {
  /** 操作日志ID（对应后端 OperLogId，序列化为 string 以避免精度问题） */
  operLogId: string
  /** 用户名 */
  userName?: string
  /** 操作模块 */
  operModule?: string
  /** 操作类型（如：新增、删除、修改、查询） */
  operType?: string
  /** 操作方法 */
  operMethod?: string
  /** 请求方式（GET、POST、PUT、DELETE） */
  requestMethod?: string
  /** 操作URL */
  operUrl?: string
  /** 请求参数（JSON） */
  requestParam?: string
  /** 返回结果（JSON） */
  jsonResult?: string
  /** 操作状态（0=成功，1=失败） */
  operStatus: number
  /** 错误消息 */
  errorMsg?: string
  /** 操作IP */
  operIp?: string
  /** 操作地点 */
  operLocation?: string
  /** 操作时间 */
  operTime?: string
  /** 执行耗时（毫秒） */
  costTime?: number
}

/**
 * 操作日志查询类型（对应后端 Takt.Application.Dtos.Logging.TaktOperLogQueryDto）
 * 请求时需转换为 PascalCase 与后端一致
 */
export interface OperLogQuery extends TaktPagedQuery {
  /** 关键词（在用户名、操作模块、操作方法中模糊查询） */
  keyWords?: string
  /** 用户名 */
  userName?: string
  /** 操作模块 */
  operModule?: string
  /** 操作类型 */
  operType?: string
  /** 操作状态（0=成功，1=失败） */
  operStatus?: number
  /** 操作时间开始 */
  operTimeStart?: string
  /** 操作时间结束 */
  operTimeEnd?: string
}
