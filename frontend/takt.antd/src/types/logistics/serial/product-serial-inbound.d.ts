// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/serial/product-serial-inbound
// 文件名称：product-serial-inbound.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：product-serial-inbound相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * ProductSerialInbound类型（对应后端 Takt.Application.Dtos.Logistics.Serial.TaktProductSerialInboundDto）
 */
export interface ProductSerialInbound extends TaktEntityBase {
  /** 对应后端字段 productSerialInboundId */
  productSerialInboundId: string
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 inboundNo */
  inboundNo: string
  /** 对应后端字段 inboundDate */
  inboundDate: string
  /** 对应后端字段 inboundType */
  inboundType: number
  /** 对应后端字段 warehouseCode */
  warehouseCode: string
  /** 对应后端字段 locationCode */
  locationCode: string
  /** 对应后端字段 totalQuantity */
  totalQuantity: number
  /** 对应后端字段 relatedCompany */
  relatedCompany: string
  /** 对应后端字段 items */
  items?: unknown[]
}

/**
 * ProductSerialInboundQuery类型（对应后端 Takt.Application.Dtos.Logistics.Serial.TaktProductSerialInboundQueryDto）
 */
export interface ProductSerialInboundQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 inboundNo */
  inboundNo?: string
  /** 对应后端字段 inboundDate */
  inboundDate?: string
  /** 对应后端字段 inboundDateStart */
  inboundDateStart?: string
  /** 对应后端字段 inboundDateEnd */
  inboundDateEnd?: string
  /** 对应后端字段 inboundType */
  inboundType?: number
  /** 对应后端字段 warehouseCode */
  warehouseCode?: string
  /** 对应后端字段 locationCode */
  locationCode?: string
  /** 对应后端字段 totalQuantity */
  totalQuantity?: number
  /** 对应后端字段 relatedCompany */
  relatedCompany?: string
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
 * ProductSerialInboundCreate类型（对应后端 Takt.Application.Dtos.Logistics.Serial.TaktProductSerialInboundCreateDto）
 */
export interface ProductSerialInboundCreate {
  /** 对应后端字段 plantCode */
  plantCode: string
  /** 对应后端字段 inboundNo */
  inboundNo: string
  /** 对应后端字段 inboundDate */
  inboundDate: string
  /** 对应后端字段 inboundType */
  inboundType: number
  /** 对应后端字段 warehouseCode */
  warehouseCode: string
  /** 对应后端字段 locationCode */
  locationCode: string
  /** 对应后端字段 totalQuantity */
  totalQuantity: number
  /** 对应后端字段 relatedCompany */
  relatedCompany: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 items */
  items?: unknown[]
}

/**
 * ProductSerialInboundUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Serial.TaktProductSerialInboundUpdateDto）
 */
export interface ProductSerialInboundUpdate extends ProductSerialInboundCreate {
  /** 对应后端字段 productSerialInboundId */
  productSerialInboundId: string
}
