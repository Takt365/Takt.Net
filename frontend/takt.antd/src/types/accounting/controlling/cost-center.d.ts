// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/controlling/cost-center
// 文件名称：cost-center.d.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：成本中心相关类型定义，对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostCenterDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktPagedQuery } from '@/types/common'

/**
 * 成本中心类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostCenterDto）
 */
export interface CostCenter {
  /** 成本中心ID（对应后端 CostCenterId，序列化为string以避免Javascript精度问题） */
  costCenterId: string
  /** 成本中心编码 */
  costCenterCode: string
  /** 成本中心名称 */
  costCenterName: string
  /** 父级ID（树形结构，0表示根节点，序列化为string） */
  parentId: string
  /** 成本中心类型（0=成本中心，1=利润中心，2=投资中心） */
  costCenterType: number
  /** 负责人ID（序列化为string） */
  managerId?: string
  /** 负责人姓名 */
  managerName?: string
  /** 所属部门ID（序列化为string） */
  deptId?: string
  /** 所属部门名称 */
  deptName?: string
  /** 排序号（越小越靠前） */
  orderNum: number
  /** 成本中心状态（0=启用，1=禁用） */
  costCenterStatus: number
  /** 租户配置ID（ConfigId） */
  configId?: string
  /** 备注 */
  remark?: string
  /** 创建人（用户名） */
  createBy?: string
  /** 创建时间 */
  createTime?: string
  /** 更新人（用户名） */
  updateBy?: string
  /** 更新时间 */
  updateTime?: string
}

/**
 * 成本中心查询类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostCenterQueryDto）
 */
export interface CostCenterQuery extends TaktPagedQuery {
  /** 关键词 */
  keyWords?: string
  /** 成本中心编码 */
  costCenterCode?: string
  /** 成本中心名称 */
  costCenterName?: string
  /** 成本中心状态（0=启用，1=禁用） */
  costCenterStatus?: number
}

/**
 * 创建成本中心类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostCenterCreateDto）
 */
export interface CostCenterCreate {
  /** 成本中心编码 */
  costCenterCode: string
  /** 成本中心名称 */
  costCenterName: string
  /** 父级ID（0表示根节点） */
  parentId?: string
  /** 成本中心类型（0=成本中心，1=利润中心，2=投资中心） */
  costCenterType?: number
  /** 负责人ID */
  managerId?: string
  /** 所属部门ID */
  deptId?: string
  /** 排序号 */
  orderNum?: number
  /** 备注 */
  remark?: string
}

/**
 * 更新成本中心类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostCenterUpdateDto）
 */
export interface CostCenterUpdate extends CostCenterCreate {
  /** 成本中心ID（序列化为string） */
  costCenterId: string
}

/**
 * 成本中心状态类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostCenterStatusDto）
 */
export interface CostCenterStatus {
  /** 成本中心ID（序列化为string） */
  costCenterId: string
  /** 成本中心状态（0=启用，1=禁用） */
  costCenterStatus: number
}
