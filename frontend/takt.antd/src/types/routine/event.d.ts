// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/event
// 文件名称：event.d.ts
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：活动组织（Event）类型定义，对应后端 Takt.Application.Dtos.Routine.Event.TaktEventDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktPagedQuery } from '@/types/common'

/**
 * 活动组织类型（对应后端 TaktEventDto）
 */
export interface Event {
  /** 活动ID（对应后端 EventId/Id，序列化为 string） */
  eventId: string
  companyCode?: string
  plantCode?: string
  /** 活动编码 */
  eventCode: string
  /** 活动名称 */
  eventName: string
  /** 活动类型（0=培训，1=团建，2=会议活动，3=庆典，4=其他） */
  eventType: number
  /** 活动开始时间 */
  startTime: string
  /** 活动结束时间 */
  endTime: string
  /** 活动地点 */
  location?: string
  /** 组织人ID */
  organizerId: string
  /** 组织人姓名 */
  organizerName: string
  /** 组织部门ID */
  deptId?: string
  /** 组织部门名称 */
  deptName?: string
  /** 活动状态（0=草稿，1=已发布，2=进行中，3=已结束，4=已取消） */
  eventStatus: number
  /** 活动内容/描述 */
  eventContent?: string
  /** 参与人摘要 */
  participantSummary?: string
  /** 排序号 */
  orderNum: number
  configId?: string
  remark?: string
  createId?: string
  createBy?: string
  createTime: string
  updateTime?: string
}

/**
 * 活动组织查询类型（对应后端 TaktEventQueryDto，继承 TaktPagedQuery 含 keyWords）
 */
export interface EventQuery extends TaktPagedQuery {
  keyWords?: string
  companyCode?: string
  plantCode?: string
  /** 活动类型（0=培训，1=团建，2=会议活动，3=庆典，4=其他） */
  eventType?: number
  /** 活动状态（0=草稿，1=已发布，2=进行中，3=已结束，4=已取消） */
  eventStatus?: number
}

/**
 * 活动组织创建类型（对应后端 TaktEventCreateDto）
 */
export interface EventCreate {
  companyCode?: string
  plantCode?: string
  eventName: string
  eventType: number
  startTime: string
  endTime: string
  location?: string
  deptId?: string
  deptName?: string
  eventStatus: number
  eventContent?: string
  participantSummary?: string
  orderNum: number
  remark?: string
}

/**
 * 活动组织更新类型（对应后端 TaktEventUpdateDto）
 */
export interface EventUpdate extends EventCreate {
  eventId: string
}
