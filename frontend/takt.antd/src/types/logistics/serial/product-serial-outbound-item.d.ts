// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/serial/product-serial-outbound-item
// 文件名称：product-serial-outbound-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：product-serial-outbound-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * ProductSerialOutboundItem类型（对应后端 Takt.Application.Dtos.Logistics.Serial.TaktProductSerialOutboundItemDto）
 */
export interface ProductSerialOutboundItem extends TaktEntityBase {
  /** 对应后端字段 productSerialOutboundItemId */
  productSerialOutboundItemId: string
  /** 对应后端字段 outboundId */
  outboundId: string
  /** 对应后端字段 outboundNo */
  outboundNo: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 outboundSerialNo */
  outboundSerialNo: string
  /** 对应后端字段 referenceInboundId */
  referenceInboundId: string
  /** 对应后端字段 referenceInboundNo */
  referenceInboundNo: string
  /** 对应后端字段 referenceInboundLineNumber */
  referenceInboundLineNumber: number
  /** 对应后端字段 outboundTime */
  outboundTime: string
  /** 对应后端字段 outbound */
  outbound?: unknown
}

/**
 * ProductSerialOutboundItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Serial.TaktProductSerialOutboundItemQueryDto）
 */
export interface ProductSerialOutboundItemQuery extends TaktPagedQuery {
  /** 对应后端字段 outboundId */
  outboundId?: string
  /** 对应后端字段 outboundNo */
  outboundNo?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 outboundSerialNo */
  outboundSerialNo?: string
  /** 对应后端字段 referenceInboundId */
  referenceInboundId?: string
  /** 对应后端字段 referenceInboundNo */
  referenceInboundNo?: string
  /** 对应后端字段 referenceInboundLineNumber */
  referenceInboundLineNumber?: number
  /** 对应后端字段 outboundTime */
  outboundTime?: string
  /** 对应后端字段 outboundTimeStart */
  outboundTimeStart?: string
  /** 对应后端字段 outboundTimeEnd */
  outboundTimeEnd?: string
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
 * ProductSerialOutboundItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Serial.TaktProductSerialOutboundItemCreateDto）
 */
export interface ProductSerialOutboundItemCreate {
  /** 对应后端字段 outboundId */
  outboundId: string
  /** 对应后端字段 outboundNo */
  outboundNo: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 outboundSerialNo */
  outboundSerialNo: string
  /** 对应后端字段 referenceInboundId */
  referenceInboundId: string
  /** 对应后端字段 referenceInboundNo */
  referenceInboundNo: string
  /** 对应后端字段 referenceInboundLineNumber */
  referenceInboundLineNumber: number
  /** 对应后端字段 outboundTime */
  outboundTime: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * ProductSerialOutboundItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Serial.TaktProductSerialOutboundItemUpdateDto）
 */
export interface ProductSerialOutboundItemUpdate extends ProductSerialOutboundItemCreate {
  /** 对应后端字段 productSerialOutboundItemId */
  productSerialOutboundItemId: string
}
