// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/financial/accounting-title
// 文件名称：accounting-title.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：accounting-title相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AccountingTitle类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountingTitleDto）
 */
export interface AccountingTitle extends TaktEntityBase {
  /** 对应后端字段 accountingTitleId */
  accountingTitleId: string
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 titleCode */
  titleCode: string
  /** 对应后端字段 titleName */
  titleName: string
  /** 对应后端字段 parentId */
  parentId: string
  /** 对应后端字段 titleType */
  titleType: number
  /** 对应后端字段 balanceDirection */
  balanceDirection: number
  /** 对应后端字段 titleLevel */
  titleLevel: number
  /** 对应后端字段 isLeaf */
  isLeaf: number
  /** 对应后端字段 isAuxiliary */
  isAuxiliary: number
  /** 对应后端字段 auxiliaryType */
  auxiliaryType: number
  /** 对应后端字段 isQuantity */
  isQuantity: number
  /** 对应后端字段 isCurrency */
  isCurrency: number
  /** 对应后端字段 isCash */
  isCash: number
  /** 对应后端字段 isBank */
  isBank: number
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
  /** 对应后端字段 titleStatus */
  titleStatus: number
  /** 对应后端字段 validFrom */
  validFrom: string
  /** 对应后端字段 validTo */
  validTo: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * AccountingTitleTree类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountingTitleTreeDto）
 */
export interface AccountingTitleTree extends AccountingTitle {
  /** 对应后端字段 children */
  children: AccountingTitleTree[]
}

/**
 * AccountingTitleQuery类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountingTitleQueryDto）
 */
export interface AccountingTitleQuery extends TaktPagedQuery {
  /** 对应后端字段 parentId */
  parentId?: string
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 titleCode */
  titleCode?: string
  /** 对应后端字段 titleName */
  titleName?: string
  /** 对应后端字段 titleType */
  titleType?: number
  /** 对应后端字段 balanceDirection */
  balanceDirection?: number
  /** 对应后端字段 titleLevel */
  titleLevel?: number
  /** 对应后端字段 isLeaf */
  isLeaf?: number
  /** 对应后端字段 isAuxiliary */
  isAuxiliary?: number
  /** 对应后端字段 auxiliaryType */
  auxiliaryType?: number
  /** 对应后端字段 isQuantity */
  isQuantity?: number
  /** 对应后端字段 isCurrency */
  isCurrency?: number
  /** 对应后端字段 isCash */
  isCash?: number
  /** 对应后端字段 isBank */
  isBank?: number
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
  /** 对应后端字段 titleStatus */
  titleStatus?: number
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
 * AccountingTitleCreate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountingTitleCreateDto）
 */
export interface AccountingTitleCreate {
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 titleCode */
  titleCode: string
  /** 对应后端字段 titleName */
  titleName: string
  /** 对应后端字段 parentId */
  parentId: string
  /** 对应后端字段 titleType */
  titleType: number
  /** 对应后端字段 balanceDirection */
  balanceDirection: number
  /** 对应后端字段 titleLevel */
  titleLevel: number
  /** 对应后端字段 isLeaf */
  isLeaf: number
  /** 对应后端字段 isAuxiliary */
  isAuxiliary: number
  /** 对应后端字段 auxiliaryType */
  auxiliaryType: number
  /** 对应后端字段 isQuantity */
  isQuantity: number
  /** 对应后端字段 isCurrency */
  isCurrency: number
  /** 对应后端字段 isCash */
  isCash: number
  /** 对应后端字段 isBank */
  isBank: number
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
  /** 对应后端字段 titleStatus */
  titleStatus: number
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
 * AccountingTitleUpdate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountingTitleUpdateDto）
 */
export interface AccountingTitleUpdate extends AccountingTitleCreate {
  /** 对应后端字段 accountingTitleId */
  accountingTitleId: string
}

/**
 * AccountingTitleSort类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountingTitleSortDto）
 */
export interface AccountingTitleSort {
  /** 对应后端字段 accountingTitleId */
  accountingTitleId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * AccountingTitleTitleStatus类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountingTitleTitleStatusDto）
 */
export interface AccountingTitleTitleStatus {
  /** 对应后端字段 accountingTitleId */
  accountingTitleId: string
  /** 对应后端字段 titleStatus */
  titleStatus: number
}
