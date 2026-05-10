// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/logistics/manufacturing/engineering-change/ec-attachment
// 文件名称：ec-attachment.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：ec-attachment相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EcAttachment类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcAttachmentDto）
 */
export interface EcAttachment extends TaktEntityBase {
  /** 对应后端字段 ecAttachmentId */
  ecAttachmentId: string
  /** 对应后端字段 ecnId */
  ecnId: string
  /** 对应后端字段 attachmentType */
  attachmentType: string
  /** 对应后端字段 docNo */
  docNo: string
  /** 对应后端字段 fileName */
  fileName: string
  /** 对应后端字段 accessUrl */
  accessUrl: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 ecn */
  ecn?: unknown
}

/**
 * EcAttachmentQuery类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcAttachmentQueryDto）
 */
export interface EcAttachmentQuery extends TaktPagedQuery {
  /** 对应后端字段 ecnId */
  ecnId?: string
  /** 对应后端字段 attachmentType */
  attachmentType?: string
  /** 对应后端字段 docNo */
  docNo?: string
  /** 对应后端字段 fileName */
  fileName?: string
  /** 对应后端字段 accessUrl */
  accessUrl?: string
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
 * EcAttachmentCreate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcAttachmentCreateDto）
 */
export interface EcAttachmentCreate {
  /** 对应后端字段 ecnId */
  ecnId: string
  /** 对应后端字段 attachmentType */
  attachmentType: string
  /** 对应后端字段 docNo */
  docNo: string
  /** 对应后端字段 fileName */
  fileName: string
  /** 对应后端字段 accessUrl */
  accessUrl: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * EcAttachmentUpdate类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcAttachmentUpdateDto）
 */
export interface EcAttachmentUpdate extends EcAttachmentCreate {
  /** 对应后端字段 ecAttachmentId */
  ecAttachmentId: string
}

/**
 * EcAttachmentSort类型（对应后端 Takt.Application.Dtos.Logistics.Manufacturing.EngineeringChange.TaktEcAttachmentSortDto）
 */
export interface EcAttachmentSort {
  /** 对应后端字段 ecAttachmentId */
  ecAttachmentId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
