// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/controlling/cost-center
// 文件名称：cost-center.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：cost-center相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * CostCenter类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostCenterDto）
 */
export interface CostCenter extends TaktEntityBase {
  /** 对应后端字段 costCenterId */
  costCenterId: string
  /** 对应后端字段 costCenterCode */
  costCenterCode: string
  /** 对应后端字段 costCenterName */
  costCenterName: string
  /** 对应后端字段 parentId */
  parentId: string
  /** 对应后端字段 costCenterType */
  costCenterType: number
  /** 对应后端字段 managerId */
  managerId?: string
  /** 对应后端字段 managerName */
  managerName?: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 costCenterLevel */
  costCenterLevel: number
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 costCenterStatus */
  costCenterStatus: number
  /** 对应后端字段 validFrom */
  validFrom: string
  /** 对应后端字段 validTo */
  validTo: string
}

/**
 * CostCenterQuery类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostCenterQueryDto）
 */
export interface CostCenterQuery extends TaktPagedQuery {
  /** 对应后端字段 costCenterName */
  costCenterName?: string
  /** 对应后端字段 costCenterCode */
  costCenterCode?: string
  /** 对应后端字段 parentId */
  parentId?: string
  /** 对应后端字段 costCenterType */
  costCenterType?: number
  /** 对应后端字段 costCenterStatus */
  costCenterStatus?: number
}

/**
 * CostCenterCreate类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostCenterCreateDto）
 */
export interface CostCenterCreate {
  /** 对应后端字段 costCenterCode */
  costCenterCode: string
  /** 对应后端字段 costCenterName */
  costCenterName: string
  /** 对应后端字段 parentId */
  parentId: string
  /** 对应后端字段 costCenterType */
  costCenterType: number
  /** 对应后端字段 managerId */
  managerId?: string
  /** 对应后端字段 managerName */
  managerName?: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 validFrom */
  validFrom: string
  /** 对应后端字段 validTo */
  validTo: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * CostCenterUpdate类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostCenterUpdateDto）
 */
export interface CostCenterUpdate extends CostCenterCreate {
  /** 对应后端字段 costCenterId */
  costCenterId: string
}

/**
 * CostCenterStatus类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostCenterStatusDto）
 */
export interface CostCenterStatus {
  /** 对应后端字段 costCenterId */
  costCenterId: string
  /** 对应后端字段 costCenterStatus */
  costCenterStatus: number
}
