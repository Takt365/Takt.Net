// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/output/production-team
// 文件名称：production-team.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：production-team相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * ProductionTeam类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktProductionTeamDto）
 */
export interface ProductionTeam extends TaktEntityBase {
  /** 对应后端字段 productionTeamId */
  productionTeamId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 teamCode */
  teamCode: string
  /** 对应后端字段 teamName */
  teamName: string
  /** 对应后端字段 teamCategory */
  teamCategory?: string
  /** 对应后端字段 teamCategoryName */
  teamCategoryName?: string
  /** 对应后端字段 productionLine */
  productionLine?: string
  /** 对应后端字段 teamLeaderId */
  teamLeaderId?: string
  /** 对应后端字段 teamLeaderName */
  teamLeaderName?: string
  /** 对应后端字段 shiftNo */
  shiftNo?: number
  /** 对应后端字段 status */
  status: number
}

/**
 * ProductionTeamQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktProductionTeamQueryDto）
 */
export interface ProductionTeamQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 teamCode */
  teamCode?: string
  /** 对应后端字段 teamName */
  teamName?: string
  /** 对应后端字段 teamCategory */
  teamCategory?: string
  /** 对应后端字段 teamCategoryName */
  teamCategoryName?: string
  /** 对应后端字段 productionLine */
  productionLine?: string
  /** 对应后端字段 teamLeaderId */
  teamLeaderId?: string
  /** 对应后端字段 teamLeaderName */
  teamLeaderName?: string
  /** 对应后端字段 shiftNo */
  shiftNo?: number
  /** 对应后端字段 status */
  status?: number
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
 * ProductionTeamCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktProductionTeamCreateDto）
 */
export interface ProductionTeamCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 teamCode */
  teamCode: string
  /** 对应后端字段 teamName */
  teamName: string
  /** 对应后端字段 teamCategory */
  teamCategory?: string
  /** 对应后端字段 teamCategoryName */
  teamCategoryName?: string
  /** 对应后端字段 productionLine */
  productionLine?: string
  /** 对应后端字段 teamLeaderId */
  teamLeaderId?: string
  /** 对应后端字段 teamLeaderName */
  teamLeaderName?: string
  /** 对应后端字段 shiftNo */
  shiftNo?: number
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * ProductionTeamUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktProductionTeamUpdateDto）
 */
export interface ProductionTeamUpdate extends ProductionTeamCreate {
  /** 对应后端字段 productionTeamId */
  productionTeamId: string
}

/**
 * ProductionTeamStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktProductionTeamStatusDto）
 */
export interface ProductionTeamStatus {
  /** 对应后端字段 productionTeamId */
  productionTeamId: string
  /** 对应后端字段 status */
  status: number
}
