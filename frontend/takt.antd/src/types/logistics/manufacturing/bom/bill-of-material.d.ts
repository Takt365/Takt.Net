// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/bom/bill-of-material
// 文件名称：bill-of-material.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：bill-of-material相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * BillOfMaterial类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktBillOfMaterialDto）
 */
export interface BillOfMaterial extends TaktEntityBase {
  /** 对应后端字段 billOfMaterialId */
  billOfMaterialId: string
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 bomCode */
  bomCode: string
  /** 对应后端字段 bomName */
  bomName: string
  /** 对应后端字段 parentMaterialId */
  parentMaterialId: string
  /** 对应后端字段 parentMaterialCode */
  parentMaterialCode: string
  /** 对应后端字段 parentMaterialName */
  parentMaterialName: string
  /** 对应后端字段 bomVersion */
  bomVersion: string
  /** 对应后端字段 bomType */
  bomType: number
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 expiryDate */
  expiryDate?: string
  /** 对应后端字段 parentMaterialUnit */
  parentMaterialUnit: string
  /** 对应后端字段 parentMaterialQuantity */
  parentMaterialQuantity: number
  /** 对应后端字段 isEnabled */
  isEnabled: number
  /** 对应后端字段 bomStatus */
  bomStatus: number
  /** 对应后端字段 bomDescription */
  bomDescription?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 items */
  items?: unknown[]
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * BillOfMaterialQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktBillOfMaterialQueryDto）
 */
export interface BillOfMaterialQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 bomCode */
  bomCode?: string
  /** 对应后端字段 bomName */
  bomName?: string
  /** 对应后端字段 parentMaterialId */
  parentMaterialId?: string
  /** 对应后端字段 parentMaterialCode */
  parentMaterialCode?: string
  /** 对应后端字段 parentMaterialName */
  parentMaterialName?: string
  /** 对应后端字段 bomVersion */
  bomVersion?: string
  /** 对应后端字段 bomType */
  bomType?: number
  /** 对应后端字段 effectiveDate */
  effectiveDate?: string
  /** 对应后端字段 effectiveDateStart */
  effectiveDateStart?: string
  /** 对应后端字段 effectiveDateEnd */
  effectiveDateEnd?: string
  /** 对应后端字段 expiryDate */
  expiryDate?: string
  /** 对应后端字段 expiryDateStart */
  expiryDateStart?: string
  /** 对应后端字段 expiryDateEnd */
  expiryDateEnd?: string
  /** 对应后端字段 parentMaterialUnit */
  parentMaterialUnit?: string
  /** 对应后端字段 parentMaterialQuantity */
  parentMaterialQuantity?: number
  /** 对应后端字段 isEnabled */
  isEnabled?: number
  /** 对应后端字段 bomStatus */
  bomStatus?: number
  /** 对应后端字段 bomDescription */
  bomDescription?: string
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
 * BillOfMaterialCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktBillOfMaterialCreateDto）
 */
export interface BillOfMaterialCreate {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 bomCode */
  bomCode: string
  /** 对应后端字段 bomName */
  bomName: string
  /** 对应后端字段 parentMaterialId */
  parentMaterialId: string
  /** 对应后端字段 parentMaterialCode */
  parentMaterialCode: string
  /** 对应后端字段 parentMaterialName */
  parentMaterialName: string
  /** 对应后端字段 bomVersion */
  bomVersion: string
  /** 对应后端字段 bomType */
  bomType: number
  /** 对应后端字段 effectiveDate */
  effectiveDate: string
  /** 对应后端字段 expiryDate */
  expiryDate?: string
  /** 对应后端字段 parentMaterialUnit */
  parentMaterialUnit: string
  /** 对应后端字段 parentMaterialQuantity */
  parentMaterialQuantity: number
  /** 对应后端字段 isEnabled */
  isEnabled: number
  /** 对应后端字段 bomStatus */
  bomStatus: number
  /** 对应后端字段 bomDescription */
  bomDescription?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 items */
  items?: unknown[]
  /** 对应后端字段 changeLogs */
  changeLogs?: unknown[]
}

/**
 * BillOfMaterialUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktBillOfMaterialUpdateDto）
 */
export interface BillOfMaterialUpdate extends BillOfMaterialCreate {
  /** 对应后端字段 billOfMaterialId */
  billOfMaterialId: string
}

/**
 * BillOfMaterialSort类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktBillOfMaterialSortDto）
 */
export interface BillOfMaterialSort {
  /** 对应后端字段 billOfMaterialId */
  billOfMaterialId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * BillOfMaterialBomStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktBillOfMaterialBomStatusDto）
 */
export interface BillOfMaterialBomStatus {
  /** 对应后端字段 billOfMaterialId */
  billOfMaterialId: string
  /** 对应后端字段 bomStatus */
  bomStatus: number
}
