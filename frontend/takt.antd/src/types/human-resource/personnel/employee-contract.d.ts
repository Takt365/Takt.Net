// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-contract
// 文件名称：employee-contract.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工合同相关类型定义，对应后端 TaktEmployeeContractDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 员工合同类型（对应后端 TaktEmployeeContractDto）
 */
export interface EmployeeContract extends TaktEntityBase {
  /** 员工合同ID（对应后端 EmployeeContractId） */
  employeeContractId: string
  /** 员工ID（对应后端 EmployeeId） */
  employeeId: string
  /** 合同编号 */
  contractNo: string
  /** 合同类型 */
  contractType: number
  /** 开始日期 */
  startDate?: string
  /** 结束日期 */
  endDate?: string
  /** 试用期结束日期 */
  probationEndDate?: string
  /** 签订日期 */
  signDate?: string
  /** 合同状态 */
  contractStatus: number
  /** 签约主体 */
  signCompany?: string
}

/**
 * 员工合同查询类型（对应后端 TaktEmployeeContractQueryDto）
 */
export interface EmployeeContractQuery extends TaktPagedQuery {
  /** 员工ID（精确） */
  employeeId?: string
  /** 合同状态（精确） */
  contractStatus?: number
  /** 合同编号（模糊） */
  contractNo?: string
}

/**
 * 创建员工合同类型（对应后端 TaktEmployeeContractCreateDto）
 */
export interface EmployeeContractCreate {
  /** 员工ID */
  employeeId: string
  /** 合同编号 */
  contractNo: string
  /** 合同类型 */
  contractType: number
  /** 开始日期 */
  startDate?: string
  /** 结束日期 */
  endDate?: string
  /** 试用期结束日期 */
  probationEndDate?: string
  /** 签订日期 */
  signDate?: string
  /** 合同状态 */
  contractStatus: number
  /** 签约主体 */
  signCompany?: string
}

/**
 * 更新员工合同类型（对应后端 TaktEmployeeContractUpdateDto）
 */
export interface EmployeeContractUpdate extends EmployeeContractCreate {
  /** 员工合同ID */
  employeeContractId: string
}
