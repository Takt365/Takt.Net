// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/bom/bill-of-material-item
// 文件名称：bill-of-material-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：bill-of-material-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * BillOfMaterialItem类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktBillOfMaterialItemDto）
 */
export interface BillOfMaterialItem extends TaktEntityBase {
  /** 对应后端字段 billOfMaterialItemId */
  billOfMaterialItemId: string
  /** 对应后端字段 billOfMaterialId */
  billOfMaterialId: string
  /** 对应后端字段 bomCode */
  bomCode: string
  /** 对应后端字段 childMaterialId */
  childMaterialId: string
  /** 对应后端字段 childMaterialCode */
  childMaterialCode: string
  /** 对应后端字段 childMaterialName */
  childMaterialName: string
  /** 对应后端字段 childMaterialSpecification */
  childMaterialSpecification?: string
  /** 对应后端字段 usageQuantity */
  usageQuantity: number
  /** 对应后端字段 childMaterialUnit */
  childMaterialUnit: string
  /** 对应后端字段 scrapRate */
  scrapRate: number
  /** 对应后端字段 actualUsageQuantity */
  actualUsageQuantity: number
  /** 对应后端字段 isSubstitute */
  isSubstitute: number
  /** 对应后端字段 substitutePriority */
  substitutePriority: number
  /** 对应后端字段 isRequired */
  isRequired: number
  /** 对应后端字段 isPhantom */
  isPhantom: number
  /** 对应后端字段 isCritical */
  isCritical: number
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 bom */
  bom?: unknown
}

/**
 * BillOfMaterialItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktBillOfMaterialItemQueryDto）
 */
export interface BillOfMaterialItemQuery extends TaktPagedQuery {
  /** 对应后端字段 billOfMaterialId */
  billOfMaterialId?: string
  /** 对应后端字段 bomCode */
  bomCode?: string
  /** 对应后端字段 childMaterialId */
  childMaterialId?: string
  /** 对应后端字段 childMaterialCode */
  childMaterialCode?: string
  /** 对应后端字段 childMaterialName */
  childMaterialName?: string
  /** 对应后端字段 childMaterialSpecification */
  childMaterialSpecification?: string
  /** 对应后端字段 usageQuantity */
  usageQuantity?: number
  /** 对应后端字段 childMaterialUnit */
  childMaterialUnit?: string
  /** 对应后端字段 scrapRate */
  scrapRate?: number
  /** 对应后端字段 actualUsageQuantity */
  actualUsageQuantity?: number
  /** 对应后端字段 isSubstitute */
  isSubstitute?: number
  /** 对应后端字段 substitutePriority */
  substitutePriority?: number
  /** 对应后端字段 isRequired */
  isRequired?: number
  /** 对应后端字段 isPhantom */
  isPhantom?: number
  /** 对应后端字段 isCritical */
  isCritical?: number
  /** 对应后端字段 lineNumber */
  lineNumber?: number
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
 * BillOfMaterialItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktBillOfMaterialItemCreateDto）
 */
export interface BillOfMaterialItemCreate {
  /** 对应后端字段 billOfMaterialId */
  billOfMaterialId: string
  /** 对应后端字段 bomCode */
  bomCode: string
  /** 对应后端字段 childMaterialId */
  childMaterialId: string
  /** 对应后端字段 childMaterialCode */
  childMaterialCode: string
  /** 对应后端字段 childMaterialName */
  childMaterialName: string
  /** 对应后端字段 childMaterialSpecification */
  childMaterialSpecification?: string
  /** 对应后端字段 usageQuantity */
  usageQuantity: number
  /** 对应后端字段 childMaterialUnit */
  childMaterialUnit: string
  /** 对应后端字段 scrapRate */
  scrapRate: number
  /** 对应后端字段 actualUsageQuantity */
  actualUsageQuantity: number
  /** 对应后端字段 isSubstitute */
  isSubstitute: number
  /** 对应后端字段 substitutePriority */
  substitutePriority: number
  /** 对应后端字段 isRequired */
  isRequired: number
  /** 对应后端字段 isPhantom */
  isPhantom: number
  /** 对应后端字段 isCritical */
  isCritical: number
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * BillOfMaterialItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktBillOfMaterialItemUpdateDto）
 */
export interface BillOfMaterialItemUpdate extends BillOfMaterialItemCreate {
  /** 对应后端字段 billOfMaterialItemId */
  billOfMaterialItemId: string
}
