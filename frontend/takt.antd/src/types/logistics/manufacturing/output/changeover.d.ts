// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/output/changeover
// 文件名称：changeover.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：changeover相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Changeover类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktChangeoverDto）
 */
export interface Changeover extends TaktEntityBase {
  /** 对应后端字段 changeoverId */
  changeoverId: string
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 productionCategory */
  productionCategory?: string
  /** 对应后端字段 productionDate */
  productionDate: string
  /** 对应后端字段 productionLine */
  productionLine?: string
  /** 对应后端字段 readSopTime */
  readSopTime: number
  /** 对应后端字段 personCount */
  personCount: number
  /** 对应后端字段 totalSopTime */
  totalSopTime: number
  /** 对应后端字段 changeoverCount */
  changeoverCount: number
  /** 对应后端字段 changeoverTime */
  changeoverTime: number
  /** 对应后端字段 totalChangeoverTime */
  totalChangeoverTime: number
}

/**
 * ChangeoverQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktChangeoverQueryDto）
 */
export interface ChangeoverQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 productionCategory */
  productionCategory?: string
  /** 对应后端字段 productionDate */
  productionDate?: string
  /** 对应后端字段 productionDateStart */
  productionDateStart?: string
  /** 对应后端字段 productionDateEnd */
  productionDateEnd?: string
  /** 对应后端字段 productionLine */
  productionLine?: string
  /** 对应后端字段 readSopTime */
  readSopTime?: number
  /** 对应后端字段 personCount */
  personCount?: number
  /** 对应后端字段 totalSopTime */
  totalSopTime?: number
  /** 对应后端字段 changeoverCount */
  changeoverCount?: number
  /** 对应后端字段 changeoverTime */
  changeoverTime?: number
  /** 对应后端字段 totalChangeoverTime */
  totalChangeoverTime?: number
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
 * ChangeoverCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktChangeoverCreateDto）
 */
export interface ChangeoverCreate {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 productionCategory */
  productionCategory?: string
  /** 对应后端字段 productionDate */
  productionDate: string
  /** 对应后端字段 productionLine */
  productionLine?: string
  /** 对应后端字段 readSopTime */
  readSopTime: number
  /** 对应后端字段 personCount */
  personCount: number
  /** 对应后端字段 totalSopTime */
  totalSopTime: number
  /** 对应后端字段 changeoverCount */
  changeoverCount: number
  /** 对应后端字段 changeoverTime */
  changeoverTime: number
  /** 对应后端字段 totalChangeoverTime */
  totalChangeoverTime: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * ChangeoverUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktChangeoverUpdateDto）
 */
export interface ChangeoverUpdate extends ChangeoverCreate {
  /** 对应后端字段 changeoverId */
  changeoverId: string
}
