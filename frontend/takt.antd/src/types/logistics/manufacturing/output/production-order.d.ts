// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/output/production-order
// 文件名称：production-order.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：production-order相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * ProductionOrder类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktProductionOrderDto）
 */
export interface ProductionOrder extends TaktEntityBase {
  /** 对应后端字段 productionOrderId */
  productionOrderId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 prodOrderType */
  prodOrderType: string
  /** 对应后端字段 prodOrderCode */
  prodOrderCode: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 prodOrderQty */
  prodOrderQty: number
  /** 对应后端字段 producedQty */
  producedQty: number
  /** 对应后端字段 unitOfMeasure */
  unitOfMeasure: string
  /** 对应后端字段 actualStartDate */
  actualStartDate?: string
  /** 对应后端字段 actualEndDate */
  actualEndDate?: string
  /** 对应后端字段 priority */
  priority: number
  /** 对应后端字段 workCenter */
  workCenter?: string
  /** 对应后端字段 prodLine */
  prodLine?: string
  /** 对应后端字段 prodBatch */
  prodBatch?: string
  /** 对应后端字段 serialNo */
  serialNo?: string
  /** 对应后端字段 routingCode */
  routingCode?: string
  /** 对应后端字段 status */
  status: number
}

/**
 * ProductionOrderQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktProductionOrderQueryDto）
 */
export interface ProductionOrderQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 prodOrderType */
  prodOrderType?: string
  /** 对应后端字段 prodOrderCode */
  prodOrderCode?: string
  /** 对应后端字段 materialCode */
  materialCode?: string
  /** 对应后端字段 prodOrderQty */
  prodOrderQty?: number
  /** 对应后端字段 producedQty */
  producedQty?: number
  /** 对应后端字段 unitOfMeasure */
  unitOfMeasure?: string
  /** 对应后端字段 actualStartDate */
  actualStartDate?: string
  /** 对应后端字段 actualStartDateStart */
  actualStartDateStart?: string
  /** 对应后端字段 actualStartDateEnd */
  actualStartDateEnd?: string
  /** 对应后端字段 actualEndDate */
  actualEndDate?: string
  /** 对应后端字段 actualEndDateStart */
  actualEndDateStart?: string
  /** 对应后端字段 actualEndDateEnd */
  actualEndDateEnd?: string
  /** 对应后端字段 priority */
  priority?: number
  /** 对应后端字段 workCenter */
  workCenter?: string
  /** 对应后端字段 prodLine */
  prodLine?: string
  /** 对应后端字段 prodBatch */
  prodBatch?: string
  /** 对应后端字段 serialNo */
  serialNo?: string
  /** 对应后端字段 routingCode */
  routingCode?: string
  /** 对应后端字段 status */
  status?: number
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
 * ProductionOrderCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktProductionOrderCreateDto）
 */
export interface ProductionOrderCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 prodOrderType */
  prodOrderType: string
  /** 对应后端字段 prodOrderCode */
  prodOrderCode: string
  /** 对应后端字段 materialCode */
  materialCode: string
  /** 对应后端字段 prodOrderQty */
  prodOrderQty: number
  /** 对应后端字段 producedQty */
  producedQty: number
  /** 对应后端字段 unitOfMeasure */
  unitOfMeasure: string
  /** 对应后端字段 actualStartDate */
  actualStartDate?: string
  /** 对应后端字段 actualEndDate */
  actualEndDate?: string
  /** 对应后端字段 priority */
  priority: number
  /** 对应后端字段 workCenter */
  workCenter?: string
  /** 对应后端字段 prodLine */
  prodLine?: string
  /** 对应后端字段 prodBatch */
  prodBatch?: string
  /** 对应后端字段 serialNo */
  serialNo?: string
  /** 对应后端字段 routingCode */
  routingCode?: string
  /** 对应后端字段 status */
  status: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * ProductionOrderUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktProductionOrderUpdateDto）
 */
export interface ProductionOrderUpdate extends ProductionOrderCreate {
  /** 对应后端字段 productionOrderId */
  productionOrderId: string
}

/**
 * ProductionOrderStatus类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Output.TaktProductionOrderStatusDto）
 */
export interface ProductionOrderStatus {
  /** 对应后端字段 productionOrderId */
  productionOrderId: string
  /** 对应后端字段 status */
  status: number
}
