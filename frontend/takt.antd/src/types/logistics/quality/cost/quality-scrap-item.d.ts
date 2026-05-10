// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/cost/quality-scrap-item
// 文件名称：quality-scrap-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：quality-scrap-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * QualityScrapItem类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityScrapItemDto）
 */
export interface QualityScrapItem extends TaktEntityBase {
  /** 对应后端字段 qualityScrapItemId */
  qualityScrapItemId: string
  /** 对应后端字段 qualityScrapId */
  qualityScrapId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 scrapCost */
  scrapCost: number
  /** 对应后端字段 scrapSize */
  scrapSize: number
  /** 对应后端字段 partPrice */
  partPrice: number
  /** 对应后端字段 scrapReasonCost */
  scrapReasonCost: number
  /** 对应后端字段 freightCharges */
  freightCharges: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 reasonWorkTimeMinutes */
  reasonWorkTimeMinutes: number
  /** 对应后端字段 tax */
  tax: number
  /** 对应后端字段 reasonOtherExpenses */
  reasonOtherExpenses: number
  /** 对应后端字段 scrapNote */
  scrapNote?: string
  /** 对应后端字段 scrap */
  scrap?: unknown
}

/**
 * QualityScrapItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityScrapItemQueryDto）
 */
export interface QualityScrapItemQuery extends TaktPagedQuery {
  /** 对应后端字段 qualityScrapId */
  qualityScrapId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 materialName */
  materialName?: string
  /** 对应后端字段 scrapCost */
  scrapCost?: number
  /** 对应后端字段 scrapSize */
  scrapSize?: number
  /** 对应后端字段 partPrice */
  partPrice?: number
  /** 对应后端字段 scrapReasonCost */
  scrapReasonCost?: number
  /** 对应后端字段 freightCharges */
  freightCharges?: number
  /** 对应后端字段 otherExpenses */
  otherExpenses?: number
  /** 对应后端字段 reasonWorkTimeMinutes */
  reasonWorkTimeMinutes?: number
  /** 对应后端字段 tax */
  tax?: number
  /** 对应后端字段 reasonOtherExpenses */
  reasonOtherExpenses?: number
  /** 对应后端字段 scrapNote */
  scrapNote?: string
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
 * QualityScrapItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityScrapItemCreateDto）
 */
export interface QualityScrapItemCreate {
  /** 对应后端字段 qualityScrapId */
  qualityScrapId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 scrapCost */
  scrapCost: number
  /** 对应后端字段 scrapSize */
  scrapSize: number
  /** 对应后端字段 partPrice */
  partPrice: number
  /** 对应后端字段 scrapReasonCost */
  scrapReasonCost: number
  /** 对应后端字段 freightCharges */
  freightCharges: number
  /** 对应后端字段 otherExpenses */
  otherExpenses: number
  /** 对应后端字段 reasonWorkTimeMinutes */
  reasonWorkTimeMinutes: number
  /** 对应后端字段 tax */
  tax: number
  /** 对应后端字段 reasonOtherExpenses */
  reasonOtherExpenses: number
  /** 对应后端字段 scrapNote */
  scrapNote?: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * QualityScrapItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityScrapItemUpdateDto）
 */
export interface QualityScrapItemUpdate extends QualityScrapItemCreate {
  /** 对应后端字段 qualityScrapItemId */
  qualityScrapItemId: string
}
