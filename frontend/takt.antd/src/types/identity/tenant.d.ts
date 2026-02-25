// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/tenant/tenant
// 文件名称：tenant.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：租户类型定义，对应后端 Takt.Application.Dtos.Tenant
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery, TaktPagedResult, TaktSelectOption } from '@/types/common'

/**
 * 租户（对应后端 Takt.Application.Dtos.Tenant.TaktTenantDto）
 */
export interface Tenant extends TaktEntityBase {
  /** 租户ID（对应后端 TenantId，序列化为string以避免Javascript精度问题） */
  tenantId: string
  /** 租户名称（对应后端 TenantName） */
  tenantName: string
  /** 租户编码（对应后端 TenantCode） */
  tenantCode: string
  /** 订阅开始时间（对应后端 StartTime） */
  startTime: string
  /** 订阅结束时间（对应后端 EndTime） */
  endTime: string
  /** 租户状态（0=启用，1=禁用，对应后端 TenantStatus） */
  tenantStatus: number
}

/**
 * 租户查询（对应后端 Takt.Application.Dtos.Tenant.TaktTenantQueryDto）
 */
export interface TenantQuery extends TaktPagedQuery {
  /** 关键词（在租户名称、租户编码中模糊查询，对应后端 KeyWords） */
  keyWords?: string
  /** 租户名称（对应后端 TenantName） */
  tenantName?: string
  /** 租户编码（对应后端 TenantCode） */
  tenantCode?: string
  /** 租户配置ID（ConfigId，对应后端 ConfigId） */
  configId?: string
  /** 租户状态（0=启用，1=禁用，对应后端 TenantStatus） */
  tenantStatus?: number
}

/**
 * 租户创建（对应后端 Takt.Application.Dtos.Tenant.TaktTenantCreateDto）
 */
export interface TenantCreate {
  /** 租户名称（对应后端 TenantName） */
  tenantName: string
  /** 租户编码（对应后端 TenantCode） */
  tenantCode: string
  /** 租户配置ID（ConfigId，用于多租户数据隔离和数据库切换，对应后端 ConfigId） */
  configId: string
  /** 订阅开始时间（对应后端 StartTime） */
  startTime?: string
  /** 订阅结束时间（对应后端 EndTime） */
  endTime?: string
  /** 备注（对应后端 Remark） */
  remark?: string
}

/**
 * 租户更新（对应后端 Takt.Application.Dtos.Tenant.TaktTenantUpdateDto）
 */
export interface TenantUpdate extends TenantCreate {
  /** 租户ID（对应后端 TenantId，序列化为string以避免Javascript精度问题） */
  tenantId: string
}

/**
 * 租户状态（对应后端 Takt.Application.Dtos.Tenant.TaktTenantStatusDto）
 */
export interface TenantStatus {
  /** 租户ID（对应后端 TenantId，序列化为string以避免Javascript精度问题） */
  tenantId: string
  /** 租户状态（0=启用，1=禁用，对应后端 TenantStatus） */
  tenantStatus: number
}

/**
 * 租户导入模板（对应后端 Takt.Application.Dtos.Tenant.TaktTenantTemplateDto）
 */
export interface TenantTemplate {
  /** 租户名称（对应后端 TenantName） */
  tenantName: string
  /** 租户编码（对应后端 TenantCode） */
  tenantCode: string
  /** 租户配置ID（ConfigId，对应后端 ConfigId） */
  configId: string
  /** 订阅开始时间（对应后端 StartTime） */
  startTime: string
  /** 订阅结束时间（对应后端 EndTime） */
  endTime: string
  /** 租户状态（0=启用，1=禁用，对应后端 TenantStatus） */
  tenantStatus: number
  /** 创建时间（对应后端 CreateTime） */
  createTime: string
}

/**
 * 租户导入（对应后端 Takt.Application.Dtos.Tenant.TaktTenantImportDto）
 */
export interface TenantImport {
  /** 租户名称（对应后端 TenantName） */
  tenantName: string
  /** 租户编码（对应后端 TenantCode） */
  tenantCode: string
  /** 租户配置ID（ConfigId，对应后端 ConfigId） */
  configId: string
  /** 订阅开始时间（对应后端 StartTime） */
  startTime: string
  /** 订阅结束时间（对应后端 EndTime） */
  endTime: string
  /** 租户状态（0=启用，1=禁用，对应后端 TenantStatus） */
  tenantStatus: number
  /** 创建时间（对应后端 CreateTime） */
  createTime: string
}

/**
 * 租户导出（对应后端 Takt.Application.Dtos.Tenant.TaktTenantExportDto）
 */
export interface TenantExport {
  /** 租户名称（对应后端 TenantName） */
  tenantName: string
  /** 租户编码（对应后端 TenantCode） */
  tenantCode: string
  /** 租户配置ID（ConfigId，对应后端 ConfigId） */
  configId: string
  /** 订阅开始时间（对应后端 StartTime） */
  startTime: string
  /** 订阅结束时间（对应后端 EndTime） */
  endTime: string
  /** 租户状态（0=启用，1=禁用，对应后端 TenantStatus） */
  tenantStatus: number
  /** 创建时间（对应后端 CreateTime） */
  createTime: string
}
