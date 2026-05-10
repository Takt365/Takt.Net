// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/quality/cost/quality-scrap
// 文件名称：quality-scrap.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：quality-scrap相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * QualityScrap类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityScrapDto）
 */
export interface QualityScrap extends TaktEntityBase {
  /** 对应后端字段 qualityScrapId */
  qualityScrapId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 scrapNo */
  scrapNo: string
  /** 对应后端字段 scrapDate */
  scrapDate: string
  /** 对应后端字段 indirectManpowerCostPerMinute */
  indirectManpowerCostPerMinute: number
  /** 对应后端字段 model */
  model: string
  /** 对应后端字段 scrapReason */
  scrapReason?: string
  /** 对应后端字段 totalScrapQuantity */
  totalScrapQuantity: number
  /** 对应后端字段 totalScrapCost */
  totalScrapCost: number
  /** 对应后端字段 costCurrency */
  costCurrency: string
  /** 对应后端字段 scrapItems */
  scrapItems?: unknown[]
}

/**
 * QualityScrapQuery类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityScrapQueryDto）
 */
export interface QualityScrapQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 scrapNo */
  scrapNo?: string
  /** 对应后端字段 scrapDate */
  scrapDate?: string
  /** 对应后端字段 scrapDateStart */
  scrapDateStart?: string
  /** 对应后端字段 scrapDateEnd */
  scrapDateEnd?: string
  /** 对应后端字段 indirectManpowerCostPerMinute */
  indirectManpowerCostPerMinute?: number
  /** 对应后端字段 model */
  model?: string
  /** 对应后端字段 scrapReason */
  scrapReason?: string
  /** 对应后端字段 totalScrapQuantity */
  totalScrapQuantity?: number
  /** 对应后端字段 totalScrapCost */
  totalScrapCost?: number
  /** 对应后端字段 costCurrency */
  costCurrency?: string
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
 * QualityScrapCreate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityScrapCreateDto）
 */
export interface QualityScrapCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 scrapNo */
  scrapNo: string
  /** 对应后端字段 scrapDate */
  scrapDate: string
  /** 对应后端字段 indirectManpowerCostPerMinute */
  indirectManpowerCostPerMinute: number
  /** 对应后端字段 model */
  model: string
  /** 对应后端字段 scrapReason */
  scrapReason?: string
  /** 对应后端字段 totalScrapQuantity */
  totalScrapQuantity: number
  /** 对应后端字段 totalScrapCost */
  totalScrapCost: number
  /** 对应后端字段 costCurrency */
  costCurrency: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 scrapItems */
  scrapItems?: unknown[]
}

/**
 * QualityScrapUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Quality.Cost.TaktQualityScrapUpdateDto）
 */
export interface QualityScrapUpdate extends QualityScrapCreate {
  /** 对应后端字段 qualityScrapId */
  qualityScrapId: string
}
