// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/controlling/cost-element
// 文件名称：cost-element.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：cost-element相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * CostElement类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementDto）
 */
export interface CostElement extends TaktEntityBase {
  /** 对应后端字段 costElementId */
  costElementId: string
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 costElementCode */
  costElementCode: string
  /** 对应后端字段 costElementName */
  costElementName: string
  /** 对应后端字段 costElementType */
  costElementType: number
  /** 对应后端字段 costElementCategory */
  costElementCategory: number
  /** 对应后端字段 parentId */
  parentId: string
  /** 对应后端字段 costElementLevel */
  costElementLevel: number
  /** 对应后端字段 costElementStatus */
  costElementStatus: number
  /** 对应后端字段 validFrom */
  validFrom: string
  /** 对应后端字段 validTo */
  validTo: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * CostElementTree类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementTreeDto）
 */
export interface CostElementTree extends CostElement {
  /** 对应后端字段 children */
  children: CostElementTree[]
}

/**
 * CostElementQuery类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementQueryDto）
 */
export interface CostElementQuery extends TaktPagedQuery {
  /** 对应后端字段 parentId */
  parentId?: string
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 costElementCode */
  costElementCode?: string
  /** 对应后端字段 costElementName */
  costElementName?: string
  /** 对应后端字段 costElementType */
  costElementType?: number
  /** 对应后端字段 costElementCategory */
  costElementCategory?: number
  /** 对应后端字段 costElementLevel */
  costElementLevel?: number
  /** 对应后端字段 costElementStatus */
  costElementStatus?: number
  /** 对应后端字段 validFrom */
  validFrom?: string
  /** 对应后端字段 validFromStart */
  validFromStart?: string
  /** 对应后端字段 validFromEnd */
  validFromEnd?: string
  /** 对应后端字段 validTo */
  validTo?: string
  /** 对应后端字段 validToStart */
  validToStart?: string
  /** 对应后端字段 validToEnd */
  validToEnd?: string
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
 * CostElementCreate类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementCreateDto）
 */
export interface CostElementCreate {
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 costElementCode */
  costElementCode: string
  /** 对应后端字段 costElementName */
  costElementName: string
  /** 对应后端字段 costElementType */
  costElementType: number
  /** 对应后端字段 costElementCategory */
  costElementCategory: number
  /** 对应后端字段 parentId */
  parentId: string
  /** 对应后端字段 costElementLevel */
  costElementLevel: number
  /** 对应后端字段 costElementStatus */
  costElementStatus: number
  /** 对应后端字段 validFrom */
  validFrom: string
  /** 对应后端字段 validTo */
  validTo: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * CostElementUpdate类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementUpdateDto）
 */
export interface CostElementUpdate extends CostElementCreate {
  /** 对应后端字段 costElementId */
  costElementId: string
}

/**
 * CostElementStatus类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementStatusDto）
 */
export interface CostElementStatus {
  /** 对应后端字段 costElementId */
  costElementId: string
  /** 对应后端字段 costElementStatus */
  costElementStatus: number
}

/**
 * CostElementSort类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementSortDto）
 */
export interface CostElementSort {
  /** 对应后端字段 costElementId */
  costElementId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
