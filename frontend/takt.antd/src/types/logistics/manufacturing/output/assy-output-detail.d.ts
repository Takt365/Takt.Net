// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/output/assy-output-detail
// 文件名称：assy-output-detail.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：assy-output-detail相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AssyOutputDetail类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktAssyOutputDetailDto）
 */
export interface AssyOutputDetail extends TaktEntityBase {
  /** 对应后端字段 assyOutputDetailId */
  assyOutputDetailId: string
  /** 对应后端字段 assyOutputId */
  assyOutputId: string
  /** 对应后端字段 prodOrderCode */
  prodOrderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 timePeriod */
  timePeriod: string
  /** 对应后端字段 prodActualQty */
  prodActualQty: number
  /** 对应后端字段 downtimeMinutes */
  downtimeMinutes: number
  /** 对应后端字段 downtimeReason */
  downtimeReason?: string
  /** 对应后端字段 downtimeDescription */
  downtimeDescription?: string
  /** 对应后端字段 unachievedReason */
  unachievedReason?: string
  /** 对应后端字段 unachievedDescription */
  unachievedDescription?: string
  /** 对应后端字段 inputMinutes */
  inputMinutes: number
  /** 对应后端字段 prodMinutes */
  prodMinutes: number
  /** 对应后端字段 actualMinutes */
  actualMinutes: number
  /** 对应后端字段 achievementRate */
  achievementRate: number
  /** 对应后端字段 assyOutput */
  assyOutput?: unknown
}

/**
 * AssyOutputDetailQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktAssyOutputDetailQueryDto）
 */
export interface AssyOutputDetailQuery extends TaktPagedQuery {
  /** 对应后端字段 assyOutputId */
  assyOutputId?: string
  /** 对应后端字段 prodOrderCode */
  prodOrderCode?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 timePeriod */
  timePeriod?: string
  /** 对应后端字段 prodActualQty */
  prodActualQty?: number
  /** 对应后端字段 downtimeMinutes */
  downtimeMinutes?: number
  /** 对应后端字段 downtimeReason */
  downtimeReason?: string
  /** 对应后端字段 downtimeDescription */
  downtimeDescription?: string
  /** 对应后端字段 unachievedReason */
  unachievedReason?: string
  /** 对应后端字段 unachievedDescription */
  unachievedDescription?: string
  /** 对应后端字段 inputMinutes */
  inputMinutes?: number
  /** 对应后端字段 prodMinutes */
  prodMinutes?: number
  /** 对应后端字段 actualMinutes */
  actualMinutes?: number
  /** 对应后端字段 achievementRate */
  achievementRate?: number
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
 * AssyOutputDetailCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktAssyOutputDetailCreateDto）
 */
export interface AssyOutputDetailCreate {
  /** 对应后端字段 assyOutputId */
  assyOutputId: string
  /** 对应后端字段 prodOrderCode */
  prodOrderCode: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 timePeriod */
  timePeriod: string
  /** 对应后端字段 prodActualQty */
  prodActualQty: number
  /** 对应后端字段 downtimeMinutes */
  downtimeMinutes: number
  /** 对应后端字段 downtimeReason */
  downtimeReason?: string
  /** 对应后端字段 downtimeDescription */
  downtimeDescription?: string
  /** 对应后端字段 unachievedReason */
  unachievedReason?: string
  /** 对应后端字段 unachievedDescription */
  unachievedDescription?: string
  /** 对应后端字段 inputMinutes */
  inputMinutes: number
  /** 对应后端字段 prodMinutes */
  prodMinutes: number
  /** 对应后端字段 actualMinutes */
  actualMinutes: number
  /** 对应后端字段 achievementRate */
  achievementRate: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * AssyOutputDetailUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktAssyOutputDetailUpdateDto）
 */
export interface AssyOutputDetailUpdate extends AssyOutputDetailCreate {
  /** 对应后端字段 assyOutputDetailId */
  assyOutputDetailId: string
}
