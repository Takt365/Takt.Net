// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/controlling/specific-engine/controlling
// 文件名称：controlling.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：controlling相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * CostCenterValidity类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.SpecificEngine.TaktCostCenterValidityDto）
 */
export interface CostCenterValidity {
  /** 对应后端字段 costCenterId */
  costCenterId: string
  /** 对应后端字段 validFrom */
  validFrom: string
  /** 对应后端字段 validTo */
  validTo: string
}

/**
 * CostElementValidity类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.SpecificEngine.TaktCostElementValidityDto）
 */
export interface CostElementValidity {
  /** 对应后端字段 costElementId */
  costElementId: string
  /** 对应后端字段 validFrom */
  validFrom: string
  /** 对应后端字段 validTo */
  validTo: string
}

/**
 * ProfitCenterValidity类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.SpecificEngine.TaktProfitCenterValidityDto）
 */
export interface ProfitCenterValidity {
  /** 对应后端字段 profitCenterId */
  profitCenterId: string
  /** 对应后端字段 validFrom */
  validFrom: string
  /** 对应后端字段 validTo */
  validTo: string
}
