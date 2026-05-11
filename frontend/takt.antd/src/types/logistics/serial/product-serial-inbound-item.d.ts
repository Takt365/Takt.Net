// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/serial/product-serial-inbound-item
// 文件名称：product-serial-inbound-item.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：product-serial-inbound-item相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * ProductSerialInboundItem类型（对应后端 Takt.Application.Dtos.Logistics.Serial.TaktProductSerialInboundItemDto）
 */
export interface ProductSerialInboundItem extends TaktEntityBase {
  /** 对应后端字段 productSerialInboundItemId */
  productSerialInboundItemId: string
  /** 对应后端字段 inboundId */
  inboundId: string
  /** 对应后端字段 inboundNo */
  inboundNo: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 inboundSerialNo */
  inboundSerialNo: string
  /** 对应后端字段 inboundTime */
  inboundTime: string
  /** 对应后端字段 inbound */
  inbound?: unknown
}

/**
 * ProductSerialInboundItemQuery类型（对应后端 Takt.Application.Dtos.Logistics.Serial.TaktProductSerialInboundItemQueryDto）
 */
export interface ProductSerialInboundItemQuery extends TaktPagedQuery {
  /** 对应后端字段 inboundId */
  inboundId?: string
  /** 对应后端字段 inboundNo */
  inboundNo?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 inboundSerialNo */
  inboundSerialNo?: string
  /** 对应后端字段 inboundTime */
  inboundTime?: string
  /** 对应后端字段 inboundTimeStart */
  inboundTimeStart?: string
  /** 对应后端字段 inboundTimeEnd */
  inboundTimeEnd?: string
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
 * ProductSerialInboundItemCreate类型（对应后端 Takt.Application.Dtos.Logistics.Serial.TaktProductSerialInboundItemCreateDto）
 */
export interface ProductSerialInboundItemCreate {
  /** 对应后端字段 inboundId */
  inboundId: string
  /** 对应后端字段 inboundNo */
  inboundNo: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 inboundSerialNo */
  inboundSerialNo: string
  /** 对应后端字段 inboundTime */
  inboundTime: string
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * ProductSerialInboundItemUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Serial.TaktProductSerialInboundItemUpdateDto）
 */
export interface ProductSerialInboundItemUpdate extends ProductSerialInboundItemCreate {
  /** 对应后端字段 productSerialInboundItemId */
  productSerialInboundItemId: string
}
