// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/bom/model-destination
// 文件名称：model-destination.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：model-destination相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * ModelDestination类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktModelDestinationDto）
 */
export interface ModelDestination extends TaktEntityBase {
  /** 对应后端字段 modelDestinationId */
  modelDestinationId: string
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 modelName */
  modelName: string
  /** 对应后端字段 destinationName */
  destinationName: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * ModelDestinationQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktModelDestinationQueryDto）
 */
export interface ModelDestinationQuery extends TaktPagedQuery {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 materialName */
  materialName?: string
  /** 对应后端字段 modelName */
  modelName?: string
  /** 对应后端字段 destinationName */
  destinationName?: string
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
 * ModelDestinationCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktModelDestinationCreateDto）
 */
export interface ModelDestinationCreate {
  /** 对应后端字段 plantCode */
  plantCode?: string
  /** 对应后端字段 materialName */
  materialName: string
  /** 对应后端字段 modelName */
  modelName: string
  /** 对应后端字段 destinationName */
  destinationName: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * ModelDestinationUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktModelDestinationUpdateDto）
 */
export interface ModelDestinationUpdate extends ModelDestinationCreate {
  /** 对应后端字段 modelDestinationId */
  modelDestinationId: string
}

/**
 * ModelDestinationSort类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.Bom.TaktModelDestinationSortDto）
 */
export interface ModelDestinationSort {
  /** 对应后端字段 modelDestinationId */
  modelDestinationId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
