// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/accounting/financial/bank
// 文件名称：bank.d.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：银行相关类型定义，对应后端 Takt.Application.Dtos.Accounting.Financial.TaktBankDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktPagedQuery } from '@/types/common'

/**
 * 银行类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktBankDto）
 */
export interface Bank {
  /** 银行ID（对应后端 BankId，序列化为string以避免Javascript精度问题） */
  bankId: string
  /** 公司代码 */
  companyCode: string
  /** 银行编码 */
  bankCode: string
  /** 银行名称 */
  bankName: string
  /** 简称 */
  shortName?: string
  /** Swift代码/联行号 */
  swiftCode?: string
  /** 地址 */
  address?: string
  /** 联系电话 */
  contactPhone?: string
  /** 排序号（越小越靠前） */
  orderNum: number
  /** 银行状态（0=启用，1=禁用） */
  bankStatus: number
  /** 租户配置ID（ConfigId） */
  configId?: string
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
 * 银行查询类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktBankQueryDto）
 */
export interface BankQuery extends TaktPagedQuery {
  /** 公司代码 */
  companyCode?: string
  /** 银行编码 */
  bankCode?: string
  /** 银行名称 */
  bankName?: string
  /** 银行状态（0=启用，1=禁用） */
  bankStatus?: number
}

/**
 * 创建银行类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktBankCreateDto）
 */
export interface BankCreate {
  /** 公司代码 */
  companyCode: string
  /** 银行编码 */
  bankCode: string
  /** 银行名称 */
  bankName: string
  /** 简称 */
  shortName?: string
  /** Swift代码/联行号 */
  swiftCode?: string
  /** 地址 */
  address?: string
  /** 联系电话 */
  contactPhone?: string
  /** 排序号 */
  orderNum?: number
}

/**
 * 更新银行类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktBankUpdateDto）
 */
export interface BankUpdate extends BankCreate {
  /** 银行ID（序列化为string） */
  bankId: string
}

/**
 * 银行状态类型（对应后端 Takt.Application.Dtos.Accounting.Financial.TaktBankStatusDto）
 */
export interface BankStatus {
  /** 银行ID（序列化为string） */
  bankId: string
  /** 银行状态（0=启用，1=禁用） */
  bankStatus: number
}
