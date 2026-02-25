// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/financial/account-title
// 文件名称：account-title.d.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：会计科目（AccountTitle）相关类型定义，对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountTitleDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktPagedQuery, TaktTreeSelectOption } from '@/types/common'

/**
 * 会计科目类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountTitleDto）
 */
export interface AccountTitle {
  titleId: string
  titleCode: string
  titleName: string
  parentId: string
  titleType: number
  balanceDirection: number
  orderNum: number
  effectiveDate?: string
  expiryDate?: string
  isReconciliationAccount: number
  titleStatus: number
  configId?: string
  extFieldJson?: string
  remark?: string
  createBy?: string
  createTime: string
  updateBy?: string
  updateTime?: string
  isDeleted?: number
  deletedBy?: string
  deletedTime?: string
}

/**
 * 会计科目树节点类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountTitleTreeDto）
 */
export interface AccountTitleTree extends AccountTitle {
  children?: AccountTitleTree[]
}

/**
 * 会计科目查询类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountTitleQueryDto）
 */
export interface AccountTitleQuery extends TaktPagedQuery {
  keyWords?: string
  titleName?: string
  titleCode?: string
  parentId?: string
  titleType?: number
  titleStatus?: number
}

/**
 * 创建会计科目类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountTitleCreateDto）
 */
export interface AccountTitleCreate {
  titleCode: string
  titleName: string
  parentId: string
  titleType: number
  balanceDirection: number
  orderNum: number
  effectiveDate?: string
  expiryDate?: string
  isReconciliationAccount: number
  remark?: string
}

/**
 * 更新会计科目类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountTitleUpdateDto）
 */
export interface AccountTitleUpdate extends AccountTitleCreate {
  titleId: string
}

/**
 * 会计科目状态类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktAccountTitleStatusDto）
 */
export interface AccountTitleStatus {
  titleId: string
  titleStatus: number
}

/**
 * 会计科目树形选项类型（对应后端 GetTreeOptionsAsync 返回的 TaktTreeSelectOption）
 */
export type AccountTitleTreeOption = TaktTreeSelectOption
