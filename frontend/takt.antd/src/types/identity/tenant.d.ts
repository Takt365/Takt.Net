// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/identity/tenant
// 文件名称：tenant.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：tenant相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Tenant类型（对应后端 Takt.Application.Dtos.Identity.TaktTenantDto）
 */
export interface Tenant extends TaktEntityBase {
  /** 对应后端字段 tenantId */
  tenantId: string
  /** 对应后端字段 tenantName */
  tenantName: string
  /** 对应后端字段 tenantCode */
  tenantCode: string
  /** 对应后端字段 allowedConfigIds */
  allowedConfigIds: string
  /** 对应后端字段 subscriptionStartTime */
  subscriptionStartTime: string
  /** 对应后端字段 subscriptionEndTime */
  subscriptionEndTime: string
  /** 对应后端字段 tenantStatus */
  tenantStatus: number
}

/**
 * TenantQuery类型（对应后端 Takt.Application.Dtos.Identity.TaktTenantQueryDto）
 */
export interface TenantQuery extends TaktPagedQuery {
  /** 对应后端字段 tenantName */
  tenantName?: string
  /** 对应后端字段 tenantCode */
  tenantCode?: string
  /** 对应后端字段 allowedConfigIds */
  allowedConfigIds?: string
  /** 对应后端字段 subscriptionStartTime */
  subscriptionStartTime?: string
  /** 对应后端字段 subscriptionStartTimeStart */
  subscriptionStartTimeStart?: string
  /** 对应后端字段 subscriptionStartTimeEnd */
  subscriptionStartTimeEnd?: string
  /** 对应后端字段 subscriptionEndTime */
  subscriptionEndTime?: string
  /** 对应后端字段 subscriptionEndTimeStart */
  subscriptionEndTimeStart?: string
  /** 对应后端字段 subscriptionEndTimeEnd */
  subscriptionEndTimeEnd?: string
  /** 对应后端字段 tenantStatus */
  tenantStatus?: number
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
 * TenantCreate类型（对应后端 Takt.Application.Dtos.Identity.TaktTenantCreateDto）
 */
export interface TenantCreate {
  /** 对应后端字段 tenantName */
  tenantName: string
  /** 对应后端字段 tenantCode */
  tenantCode: string
  /** 对应后端字段 allowedConfigIds */
  allowedConfigIds: string
  /** 对应后端字段 subscriptionStartTime */
  subscriptionStartTime: string
  /** 对应后端字段 subscriptionEndTime */
  subscriptionEndTime: string
  /** 对应后端字段 tenantStatus */
  tenantStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * TenantUpdate类型（对应后端 Takt.Application.Dtos.Identity.TaktTenantUpdateDto）
 */
export interface TenantUpdate extends TenantCreate {
  /** 对应后端字段 tenantId */
  tenantId: string
}

/**
 * TenantStatus类型（对应后端 Takt.Application.Dtos.Identity.TaktTenantStatusDto）
 */
export interface TenantStatus {
  /** 对应后端字段 tenantId */
  tenantId: string
  /** 对应后端字段 tenantStatus */
  tenantStatus: number
}
