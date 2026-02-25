// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/controlling/cost-element
// 文件名称：cost-element.d.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：成本要素相关类型定义，对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktPagedQuery, TaktTreeSelectOption } from '@/types/common'

/**
 * 成本要素类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementDto）
 */
export interface CostElement {
  /** 成本要素ID（对应后端 CostElementId，序列化为string以避免Javascript精度问题） */
  costElementId: string
  /** 成本要素编码 */
  costElementCode: string
  /** 成本要素名称 */
  costElementName: string
  /** 父级ID（树形结构，0表示根节点，序列化为string） */
  parentId: string
  /** 成本要素类型（0=初级成本要素，1=次级成本要素） */
  costElementType: number
  /** 成本要素类别（0=材料，1=人工，2=制造费用，3=其他） */
  costElementCategory: number
  /** 排序号（越小越靠前） */
  orderNum: number
  /** 成本要素状态（0=启用，1=禁用） */
  costElementStatus: number
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
 * 成本要素树节点类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementTreeDto）
 */
export interface CostElementTree extends CostElement {
  /** 子节点列表 */
  children?: CostElementTree[]
}

/**
 * 成本要素树形选项类型（对应后端 GetTreeOptionsAsync 返回的 TaktTreeSelectOption）
 */
export type CostElementTreeOption = TaktTreeSelectOption

/**
 * 成本要素查询类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementQueryDto）
 */
export interface CostElementQuery extends TaktPagedQuery {
  /** 关键词 */
  keyWords?: string
  /** 成本要素编码 */
  costElementCode?: string
  /** 成本要素名称 */
  costElementName?: string
  /** 父级ID */
  parentId?: string
  /** 成本要素状态（0=启用，1=禁用） */
  costElementStatus?: number
}

/**
 * 创建成本要素类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementCreateDto）
 */
export interface CostElementCreate {
  /** 成本要素编码 */
  costElementCode: string
  /** 成本要素名称 */
  costElementName: string
  /** 父级ID（0表示根节点） */
  parentId?: string
  /** 成本要素类型（0=初级，1=次级） */
  costElementType?: number
  /** 成本要素类别（0=材料，1=人工，2=制造费用，3=其他） */
  costElementCategory?: number
  /** 排序号 */
  orderNum?: number
  /** 备注 */
  remark?: string
}

/**
 * 更新成本要素类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementUpdateDto）
 */
export interface CostElementUpdate extends CostElementCreate {
  /** 成本要素ID（序列化为string） */
  costElementId: string
}

/**
 * 成本要素状态类型（对应后端 Takt.Application.Dtos.Accounting.Controlling.TaktCostElementStatusDto）
 */
export interface CostElementStatus {
  /** 成本要素ID（序列化为string） */
  costElementId: string
  /** 成本要素状态（0=启用，1=禁用） */
  costElementStatus: number
}
