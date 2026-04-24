// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-contract
// 文件名称：employee-contract.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：employee-contract相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EmployeeContract类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeContractDto）
 */
export interface EmployeeContract extends TaktEntityBase {
  /** 对应后端字段 employeeContractId */
  employeeContractId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 contractNo */
  contractNo: string
  /** 对应后端字段 contractType */
  contractType: number
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 endDate */
  endDate?: string
  /** 对应后端字段 probationEndDate */
  probationEndDate?: string
  /** 对应后端字段 signDate */
  signDate?: string
  /** 对应后端字段 contractStatus */
  contractStatus: number
  /** 对应后端字段 signCompany */
  signCompany?: string
}

/**
 * EmployeeContractQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeContractQueryDto）
 */
export interface EmployeeContractQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 contractStatus */
  contractStatus?: number
  /** 对应后端字段 contractNo */
  contractNo?: string
}

/**
 * EmployeeContractCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeContractCreateDto）
 */
export interface EmployeeContractCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 contractNo */
  contractNo: string
  /** 对应后端字段 contractType */
  contractType: number
  /** 对应后端字段 startDate */
  startDate?: string
  /** 对应后端字段 endDate */
  endDate?: string
  /** 对应后端字段 probationEndDate */
  probationEndDate?: string
  /** 对应后端字段 signDate */
  signDate?: string
  /** 对应后端字段 contractStatus */
  contractStatus: number
  /** 对应后端字段 signCompany */
  signCompany?: string
}

/**
 * EmployeeContractUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeContractUpdateDto）
 */
export interface EmployeeContractUpdate extends EmployeeContractCreate {
  /** 对应后端字段 employeeContractId */
  employeeContractId: string
}
