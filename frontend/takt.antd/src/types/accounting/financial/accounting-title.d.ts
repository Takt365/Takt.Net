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
  /** 对应后端字段 titleId */
  titleId: string
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
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 titleStatus */
  titleStatus: number
  /** 对应后端字段 validFrom */
  validFrom: string
  /** 对应后端字段 validTo */
  validTo: string
}

/**
 * AccountingTitleQuery类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountingTitleQueryDto）
 */
export interface AccountingTitleQuery extends TaktPagedQuery {
  /** 对应后端字段 titleName */
  titleName?: string
  /** 对应后端字段 titleCode */
  titleCode?: string
  /** 对应后端字段 parentId */
  parentId?: string
  /** 对应后端字段 titleType */
  titleType?: number
  /** 对应后端字段 titleStatus */
  titleStatus?: number
}

/**
 * AccountingTitleCreate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountingTitleCreateDto）
 */
export interface AccountingTitleCreate {
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
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 validFrom */
  validFrom: string
  /** 对应后端字段 validTo */
  validTo: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * AccountingTitleUpdate类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountingTitleUpdateDto）
 */
export interface AccountingTitleUpdate extends AccountingTitleCreate {
  /** 对应后端字段 titleId */
  titleId: string
}

/**
 * AccountingTitleStatus类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountingTitleStatusDto）
 */
export interface AccountingTitleStatus {
  /** 对应后端字段 titleId */
  titleId: string
  /** 对应后端字段 titleStatus */
  titleStatus: number
}

/**
 * AccountingTitleTree类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountingTitleTreeDto）
 */
export interface AccountingTitleTree extends AccountingTitle {
  /** 对应后端字段 children */
  children: unknown[]
}
