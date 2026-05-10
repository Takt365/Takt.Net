// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/controlling/profit-center
// 文件名称：profit-center.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：profit-center相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * ProfitCenter类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktProfitCenterDto）
 */
export interface ProfitCenter extends TaktEntityBase {
  /** 对应后端字段 profitCenterId */
  profitCenterId: string
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 profitCenterCode */
  profitCenterCode: string
  /** 对应后端字段 profitCenterName */
  profitCenterName: string
  /** 对应后端字段 parentId */
  parentId: string
  /** 对应后端字段 managerId */
  managerId?: string
  /** 对应后端字段 managerName */
  managerName?: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 profitCenterLevel */
  profitCenterLevel: number
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
  /** 对应后端字段 profitCenterStatus */
  profitCenterStatus: number
  /** 对应后端字段 validFrom */
  validFrom: string
  /** 对应后端字段 validTo */
  validTo: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * ProfitCenterTree类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktProfitCenterTreeDto）
 */
export interface ProfitCenterTree extends ProfitCenter {
  /** 对应后端字段 children */
  children: ProfitCenterTree[]
}

/**
 * ProfitCenterQuery类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktProfitCenterQueryDto）
 */
export interface ProfitCenterQuery extends TaktPagedQuery {
  /** 对应后端字段 parentId */
  parentId?: string
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 profitCenterCode */
  profitCenterCode?: string
  /** 对应后端字段 profitCenterName */
  profitCenterName?: string
  /** 对应后端字段 managerId */
  managerId?: string
  /** 对应后端字段 managerName */
  managerName?: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 profitCenterLevel */
  profitCenterLevel?: number
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
  /** 对应后端字段 profitCenterStatus */
  profitCenterStatus?: number
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
 * ProfitCenterCreate类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktProfitCenterCreateDto）
 */
export interface ProfitCenterCreate {
  /** 对应后端字段 companyCode */
  companyCode?: string
  /** 对应后端字段 profitCenterCode */
  profitCenterCode: string
  /** 对应后端字段 profitCenterName */
  profitCenterName: string
  /** 对应后端字段 parentId */
  parentId: string
  /** 对应后端字段 managerId */
  managerId?: string
  /** 对应后端字段 managerName */
  managerName?: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 profitCenterLevel */
  profitCenterLevel: number
  /** 对应后端字段 relatedPlant */
  relatedPlant?: string
  /** 对应后端字段 profitCenterStatus */
  profitCenterStatus: number
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
 * ProfitCenterUpdate类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktProfitCenterUpdateDto）
 */
export interface ProfitCenterUpdate extends ProfitCenterCreate {
  /** 对应后端字段 profitCenterId */
  profitCenterId: string
}

/**
 * ProfitCenterStatus类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktProfitCenterStatusDto）
 */
export interface ProfitCenterStatus {
  /** 对应后端字段 profitCenterId */
  profitCenterId: string
  /** 对应后端字段 profitCenterStatus */
  profitCenterStatus: number
}

/**
 * ProfitCenterSort类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktProfitCenterSortDto）
 */
export interface ProfitCenterSort {
  /** 对应后端字段 profitCenterId */
  profitCenterId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
