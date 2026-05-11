// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/serial/product-serial-outbound
// 文件名称：product-serial-outbound.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：product-serial-outbound相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * ProductSerialOutbound类型（对应后端 Takt.Application.Dtos.Logistics.Serial.TaktProductSerialOutboundDto）
 */
export interface ProductSerialOutbound extends TaktEntityBase {
  /** 对应后端字段 productSerialOutboundId */
  productSerialOutboundId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 outboundNo */
  outboundNo: string
  /** 对应后端字段 shippingInvoiceNo */
  shippingInvoiceNo: string
  /** 对应后端字段 outboundDate */
  outboundDate: string
  /** 对应后端字段 destination */
  destination: string
  /** 对应后端字段 shippingMethod */
  shippingMethod: string
  /** 对应后端字段 destinationPort */
  destinationPort: string
  /** 对应后端字段 outboundType */
  outboundType: number
  /** 对应后端字段 warehouseCode */
  warehouseCode: string
  /** 对应后端字段 locationCode */
  locationCode: string
  /** 对应后端字段 relatedCompany */
  relatedCompany: string
  /** 对应后端字段 totalQuantity */
  totalQuantity: number
  /** 对应后端字段 items */
  items?: unknown[]
}

/**
 * ProductSerialOutboundQuery类型（对应后端 Takt.Application.Dtos.Logistics.Serial.TaktProductSerialOutboundQueryDto）
 */
export interface ProductSerialOutboundQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 outboundNo */
  outboundNo?: string
  /** 对应后端字段 shippingInvoiceNo */
  shippingInvoiceNo?: string
  /** 对应后端字段 outboundDate */
  outboundDate?: string
  /** 对应后端字段 outboundDateStart */
  outboundDateStart?: string
  /** 对应后端字段 outboundDateEnd */
  outboundDateEnd?: string
  /** 对应后端字段 destination */
  destination?: string
  /** 对应后端字段 shippingMethod */
  shippingMethod?: string
  /** 对应后端字段 destinationPort */
  destinationPort?: string
  /** 对应后端字段 outboundType */
  outboundType?: number
  /** 对应后端字段 warehouseCode */
  warehouseCode?: string
  /** 对应后端字段 locationCode */
  locationCode?: string
  /** 对应后端字段 relatedCompany */
  relatedCompany?: string
  /** 对应后端字段 totalQuantity */
  totalQuantity?: number
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
 * ProductSerialOutboundCreate类型（对应后端 Takt.Application.Dtos.Logistics.Serial.TaktProductSerialOutboundCreateDto）
 */
export interface ProductSerialOutboundCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 outboundNo */
  outboundNo: string
  /** 对应后端字段 shippingInvoiceNo */
  shippingInvoiceNo: string
  /** 对应后端字段 outboundDate */
  outboundDate: string
  /** 对应后端字段 destination */
  destination: string
  /** 对应后端字段 shippingMethod */
  shippingMethod: string
  /** 对应后端字段 destinationPort */
  destinationPort: string
  /** 对应后端字段 outboundType */
  outboundType: number
  /** 对应后端字段 warehouseCode */
  warehouseCode: string
  /** 对应后端字段 locationCode */
  locationCode: string
  /** 对应后端字段 relatedCompany */
  relatedCompany: string
  /** 对应后端字段 totalQuantity */
  totalQuantity: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 items */
  items?: unknown[]
}

/**
 * ProductSerialOutboundUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Serial.TaktProductSerialOutboundUpdateDto）
 */
export interface ProductSerialOutboundUpdate extends ProductSerialOutboundCreate {
  /** 对应后端字段 productSerialOutboundId */
  productSerialOutboundId: string
}
