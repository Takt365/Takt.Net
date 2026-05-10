// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/visiting/visit-person
// 文件名称：visit-person.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：visit-person相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * VisitPerson类型（对应后端 Takt.Application.Dtos.Routine.Business.Visiting.TaktVisitPersonDto）
 */
export interface VisitPerson extends TaktEntityBase {
  /** 对应后端字段 visitPersonId */
  visitPersonId: string
  /** 对应后端字段 visitId */
  visitId: string
  /** 对应后端字段 department */
  department: string
  /** 对应后端字段 jobTitle */
  jobTitle: string
  /** 对应后端字段 personName */
  personName: string
  /** 对应后端字段 visit */
  visit?: unknown
}

/**
 * VisitPersonQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.Visiting.TaktVisitPersonQueryDto）
 */
export interface VisitPersonQuery extends TaktPagedQuery {
  /** 对应后端字段 visitId */
  visitId?: string
  /** 对应后端字段 department */
  department?: string
  /** 对应后端字段 jobTitle */
  jobTitle?: string
  /** 对应后端字段 personName */
  personName?: string
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
 * VisitPersonCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.Visiting.TaktVisitPersonCreateDto）
 */
export interface VisitPersonCreate {
  /** 对应后端字段 visitId */
  visitId: string
  /** 对应后端字段 department */
  department: string
  /** 对应后端字段 jobTitle */
  jobTitle: string
  /** 对应后端字段 personName */
  personName: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * VisitPersonUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.Visiting.TaktVisitPersonUpdateDto）
 */
export interface VisitPersonUpdate extends VisitPersonCreate {
  /** 对应后端字段 visitPersonId */
  visitPersonId: string
}
