// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/logging/aop-log
// 文件名称：aop-log.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：差异日志相关类型定义，对应后端 Takt.Application.Dtos.Logging.TaktAopLogDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 差异日志类型（对应后端 Takt.Application.Dtos.Logging.TaktAopLogDto）
 */
export interface AopLog extends TaktEntityBase {
  /** 差异日志ID（对应后端 AopLogId，序列化为 string 以避免精度问题） */
  aopLogId: string
  /** 用户名 */
  userName?: string
  /** 操作类型（如：INSERT、UPDATE、DELETE） */
  operType?: string
  /** 表名 */
  tableName?: string
  /** 主键ID */
  primaryKeyId?: string
  /** 修改前数据（JSON） */
  beforeData?: string
  /** 修改后数据（JSON） */
  afterData?: string
  /** 差异内容（JSON） */
  diffData?: string
  /** SQL语句 */
  sqlStatement?: string
  /** 操作时间 */
  operTime?: string
  /** 执行耗时（毫秒） */
  costTime?: number
}

/**
 * 差异日志查询类型（对应后端 Takt.Application.Dtos.Logging.TaktAopLogQueryDto）
 * 请求时需转换为 PascalCase 与后端一致
 */
export interface AopLogQuery extends TaktPagedQuery {
  /** 关键词（在用户名、表名中模糊查询） */
  keyWords?: string
  /** 用户名 */
  userName?: string
  /** 操作类型 */
  operType?: string
  /** 表名 */
  tableName?: string
  /** 主键ID */
  primaryKeyId?: string
  /** 操作时间开始 */
  operTimeStart?: string
  /** 操作时间结束 */
  operTimeEnd?: string
}
