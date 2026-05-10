// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/visiting/visit
// 文件名称：visit.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：visit相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Visit类型（对应后端 Takt.Application.Dtos.Routine.Business.Visiting.TaktVisitDto）
 */
export interface Visit extends TaktEntityBase {
  /** 对应后端字段 visitId */
  visitId: string
  /** 对应后端字段 companyName */
  companyName: string
  /** 对应后端字段 visitStartTime */
  visitStartTime: string
  /** 对应后端字段 visitEndTime */
  visitEndTime: string
  /** 对应后端字段 persons */
  persons?: unknown[]
}

/**
 * VisitQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.Visiting.TaktVisitQueryDto）
 */
export interface VisitQuery extends TaktPagedQuery {
  /** 对应后端字段 companyName */
  companyName?: string
  /** 对应后端字段 visitStartTime */
  visitStartTime?: string
  /** 对应后端字段 visitStartTimeStart */
  visitStartTimeStart?: string
  /** 对应后端字段 visitStartTimeEnd */
  visitStartTimeEnd?: string
  /** 对应后端字段 visitEndTime */
  visitEndTime?: string
  /** 对应后端字段 visitEndTimeStart */
  visitEndTimeStart?: string
  /** 对应后端字段 visitEndTimeEnd */
  visitEndTimeEnd?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 createdById */
  createdById?: string
  /** 对应后端字段 createdBy */
  createdBy?: string
  /** 对应后端字段 createdAt */
  createdAt?: string
  /** 对应后端字段 createdAtStart */
  createdAtStart?: string
  /** 对应后端字段 createdAtEnd */
  createdAtEnd?: string
}

/**
 * VisitCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.Visiting.TaktVisitCreateDto）
 */
export interface VisitCreate {
  /** 对应后端字段 companyName */
  companyName: string
  /** 对应后端字段 visitStartTime */
  visitStartTime: string
  /** 对应后端字段 visitEndTime */
  visitEndTime: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 persons */
  persons?: unknown[]
}

/**
 * VisitUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.Visiting.TaktVisitUpdateDto）
 */
export interface VisitUpdate extends VisitCreate {
  /** 对应后端字段 visitId */
  visitId: string
}
