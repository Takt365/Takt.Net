// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/engineering-change/ec-dept
// 文件名称：ec-dept.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：ec-dept相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EcDept类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcDeptDto）
 */
export interface EcDept extends TaktEntityBase {
  /** 对应后端字段 ecDeptId */
  ecDeptId: string
  /** 对应后端字段 ecnDetailId */
  ecnDetailId: string
  /** 对应后端字段 ecnNo */
  ecnNo: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 deptCode */
  deptCode: string
  /** 对应后端字段 isImplemented */
  isImplemented: number
  /** 对应后端字段 content */
  content?: string
  /** 对应后端字段 scheduledProductionDate */
  scheduledProductionDate?: string
  /** 对应后端字段 scheduledBatch */
  scheduledBatch?: string
  /** 对应后端字段 poRemainder */
  poRemainder?: string
  /** 对应后端字段 balance */
  balance?: string
  /** 对应后端字段 oldProductHandling */
  oldProductHandling?: string
  /** 对应后端字段 purchaseOrderIssueDate */
  purchaseOrderIssueDate?: string
  /** 对应后端字段 supplier */
  supplier?: string
  /** 对应后端字段 purchaseOrderNo */
  purchaseOrderNo?: string
  /** 对应后端字段 iqcOrderNo */
  iqcOrderNo?: string
  /** 对应后端字段 inspectionDate */
  inspectionDate?: string
  /** 对应后端字段 outboundBatch */
  outboundBatch?: string
  /** 对应后端字段 outboundDate */
  outboundDate?: string
  /** 对应后端字段 productionDate */
  productionDate?: string
  /** 对应后端字段 productionBatch */
  productionBatch?: string
  /** 对应后端字段 outboundOrderNo */
  outboundOrderNo?: string
  /** 对应后端字段 productionTeam */
  productionTeam?: string
  /** 对应后端字段 implementationDate */
  implementationDate?: string
  /** 对应后端字段 inspectionBatch */
  inspectionBatch?: string
  /** 对应后端字段 samplingNo */
  samplingNo?: string
  /** 对应后端字段 isSopUpdated */
  isSopUpdated: number
  /** 对应后端字段 ecnDetail */
  ecnDetail?: unknown
}

/**
 * EcDeptQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcDeptQueryDto）
 */
export interface EcDeptQuery extends TaktPagedQuery {
  /** 对应后端字段 ecnDetailId */
  ecnDetailId?: string
  /** 对应后端字段 ecnNo */
  ecnNo?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 deptCode */
  deptCode?: string
  /** 对应后端字段 isImplemented */
  isImplemented?: number
  /** 对应后端字段 content */
  content?: string
  /** 对应后端字段 scheduledProductionDate */
  scheduledProductionDate?: string
  /** 对应后端字段 scheduledProductionDateStart */
  scheduledProductionDateStart?: string
  /** 对应后端字段 scheduledProductionDateEnd */
  scheduledProductionDateEnd?: string
  /** 对应后端字段 scheduledBatch */
  scheduledBatch?: string
  /** 对应后端字段 poRemainder */
  poRemainder?: string
  /** 对应后端字段 balance */
  balance?: string
  /** 对应后端字段 oldProductHandling */
  oldProductHandling?: string
  /** 对应后端字段 purchaseOrderIssueDate */
  purchaseOrderIssueDate?: string
  /** 对应后端字段 purchaseOrderIssueDateStart */
  purchaseOrderIssueDateStart?: string
  /** 对应后端字段 purchaseOrderIssueDateEnd */
  purchaseOrderIssueDateEnd?: string
  /** 对应后端字段 supplier */
  supplier?: string
  /** 对应后端字段 purchaseOrderNo */
  purchaseOrderNo?: string
  /** 对应后端字段 iqcOrderNo */
  iqcOrderNo?: string
  /** 对应后端字段 inspectionDate */
  inspectionDate?: string
  /** 对应后端字段 inspectionDateStart */
  inspectionDateStart?: string
  /** 对应后端字段 inspectionDateEnd */
  inspectionDateEnd?: string
  /** 对应后端字段 outboundBatch */
  outboundBatch?: string
  /** 对应后端字段 outboundDate */
  outboundDate?: string
  /** 对应后端字段 outboundDateStart */
  outboundDateStart?: string
  /** 对应后端字段 outboundDateEnd */
  outboundDateEnd?: string
  /** 对应后端字段 productionDate */
  productionDate?: string
  /** 对应后端字段 productionDateStart */
  productionDateStart?: string
  /** 对应后端字段 productionDateEnd */
  productionDateEnd?: string
  /** 对应后端字段 productionBatch */
  productionBatch?: string
  /** 对应后端字段 outboundOrderNo */
  outboundOrderNo?: string
  /** 对应后端字段 productionTeam */
  productionTeam?: string
  /** 对应后端字段 implementationDate */
  implementationDate?: string
  /** 对应后端字段 implementationDateStart */
  implementationDateStart?: string
  /** 对应后端字段 implementationDateEnd */
  implementationDateEnd?: string
  /** 对应后端字段 inspectionBatch */
  inspectionBatch?: string
  /** 对应后端字段 samplingNo */
  samplingNo?: string
  /** 对应后端字段 isSopUpdated */
  isSopUpdated?: number
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
 * EcDeptCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcDeptCreateDto）
 */
export interface EcDeptCreate {
  /** 对应后端字段 ecnDetailId */
  ecnDetailId: string
  /** 对应后端字段 ecnNo */
  ecnNo: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 deptCode */
  deptCode: string
  /** 对应后端字段 isImplemented */
  isImplemented: number
  /** 对应后端字段 content */
  content?: string
  /** 对应后端字段 scheduledProductionDate */
  scheduledProductionDate?: string
  /** 对应后端字段 scheduledBatch */
  scheduledBatch?: string
  /** 对应后端字段 poRemainder */
  poRemainder?: string
  /** 对应后端字段 balance */
  balance?: string
  /** 对应后端字段 oldProductHandling */
  oldProductHandling?: string
  /** 对应后端字段 purchaseOrderIssueDate */
  purchaseOrderIssueDate?: string
  /** 对应后端字段 supplier */
  supplier?: string
  /** 对应后端字段 purchaseOrderNo */
  purchaseOrderNo?: string
  /** 对应后端字段 iqcOrderNo */
  iqcOrderNo?: string
  /** 对应后端字段 inspectionDate */
  inspectionDate?: string
  /** 对应后端字段 outboundBatch */
  outboundBatch?: string
  /** 对应后端字段 outboundDate */
  outboundDate?: string
  /** 对应后端字段 productionDate */
  productionDate?: string
  /** 对应后端字段 productionBatch */
  productionBatch?: string
  /** 对应后端字段 outboundOrderNo */
  outboundOrderNo?: string
  /** 对应后端字段 productionTeam */
  productionTeam?: string
  /** 对应后端字段 implementationDate */
  implementationDate?: string
  /** 对应后端字段 inspectionBatch */
  inspectionBatch?: string
  /** 对应后端字段 samplingNo */
  samplingNo?: string
  /** 对应后端字段 isSopUpdated */
  isSopUpdated: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * EcDeptUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcDeptUpdateDto）
 */
export interface EcDeptUpdate extends EcDeptCreate {
  /** 对应后端字段 ecDeptId */
  ecDeptId: string
}
