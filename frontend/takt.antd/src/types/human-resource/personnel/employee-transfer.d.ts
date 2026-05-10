// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-transfer
// 文件名称：employee-transfer.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：employee-transfer相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EmployeeTransfer类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeTransferDto）
 */
export interface EmployeeTransfer extends TaktEntityBase {
  /** 对应后端字段 employeeTransferId */
  employeeTransferId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 applicantId */
  applicantId?: string
  /** 对应后端字段 applicantBy */
  applicantBy?: string
  /** 对应后端字段 applicationDate */
  applicationDate: string
  /** 对应后端字段 transferType */
  transferType: number
  /** 对应后端字段 fromDeptId */
  fromDeptId: string
  /** 对应后端字段 fromDeptName */
  fromDeptName: string
  /** 对应后端字段 fromPostId */
  fromPostId?: string
  /** 对应后端字段 fromPostName */
  fromPostName?: string
  /** 对应后端字段 toDeptId */
  toDeptId: string
  /** 对应后端字段 toDeptName */
  toDeptName: string
  /** 对应后端字段 toPostId */
  toPostId?: string
  /** 对应后端字段 toPostName */
  toPostName?: string
  /** 对应后端字段 effectiveDate */
  effectiveDate?: string
  /** 对应后端字段 reason */
  reason?: string
  /** 对应后端字段 approverId */
  approverId?: string
  /** 对应后端字段 approverBy */
  approverBy?: string
  /** 对应后端字段 approveTime */
  approveTime?: string
  /** 对应后端字段 approveComment */
  approveComment?: string
  /** 对应后端字段 handlingId */
  handlingId?: string
  /** 对应后端字段 handlingBy */
  handlingBy?: string
  /** 对应后端字段 handlingTime */
  handlingTime?: string
  /** 对应后端字段 handlingComment */
  handlingComment?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 transferStatus */
  transferStatus: number
}

/**
 * EmployeeTransferQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeTransferQueryDto）
 */
export interface EmployeeTransferQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 applicantId */
  applicantId?: string
  /** 对应后端字段 applicantBy */
  applicantBy?: string
  /** 对应后端字段 applicationDate */
  applicationDate?: string
  /** 对应后端字段 applicationDateStart */
  applicationDateStart?: string
  /** 对应后端字段 applicationDateEnd */
  applicationDateEnd?: string
  /** 对应后端字段 transferType */
  transferType?: number
  /** 对应后端字段 fromDeptId */
  fromDeptId?: string
  /** 对应后端字段 fromDeptName */
  fromDeptName?: string
  /** 对应后端字段 fromPostId */
  fromPostId?: string
  /** 对应后端字段 fromPostName */
  fromPostName?: string
  /** 对应后端字段 toDeptId */
  toDeptId?: string
  /** 对应后端字段 toDeptName */
  toDeptName?: string
  /** 对应后端字段 toPostId */
  toPostId?: string
  /** 对应后端字段 toPostName */
  toPostName?: string
  /** 对应后端字段 effectiveDate */
  effectiveDate?: string
  /** 对应后端字段 effectiveDateStart */
  effectiveDateStart?: string
  /** 对应后端字段 effectiveDateEnd */
  effectiveDateEnd?: string
  /** 对应后端字段 reason */
  reason?: string
  /** 对应后端字段 approverId */
  approverId?: string
  /** 对应后端字段 approverBy */
  approverBy?: string
  /** 对应后端字段 approveTime */
  approveTime?: string
  /** 对应后端字段 approveTimeStart */
  approveTimeStart?: string
  /** 对应后端字段 approveTimeEnd */
  approveTimeEnd?: string
  /** 对应后端字段 approveComment */
  approveComment?: string
  /** 对应后端字段 handlingId */
  handlingId?: string
  /** 对应后端字段 handlingBy */
  handlingBy?: string
  /** 对应后端字段 handlingTime */
  handlingTime?: string
  /** 对应后端字段 handlingTimeStart */
  handlingTimeStart?: string
  /** 对应后端字段 handlingTimeEnd */
  handlingTimeEnd?: string
  /** 对应后端字段 handlingComment */
  handlingComment?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 transferStatus */
  transferStatus?: number
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
 * EmployeeTransferCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeTransferCreateDto）
 */
export interface EmployeeTransferCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 applicantId */
  applicantId?: string
  /** 对应后端字段 applicantBy */
  applicantBy?: string
  /** 对应后端字段 applicationDate */
  applicationDate: string
  /** 对应后端字段 transferType */
  transferType: number
  /** 对应后端字段 fromDeptId */
  fromDeptId: string
  /** 对应后端字段 fromDeptName */
  fromDeptName: string
  /** 对应后端字段 fromPostId */
  fromPostId?: string
  /** 对应后端字段 fromPostName */
  fromPostName?: string
  /** 对应后端字段 toDeptId */
  toDeptId: string
  /** 对应后端字段 toDeptName */
  toDeptName: string
  /** 对应后端字段 toPostId */
  toPostId?: string
  /** 对应后端字段 toPostName */
  toPostName?: string
  /** 对应后端字段 effectiveDate */
  effectiveDate?: string
  /** 对应后端字段 reason */
  reason?: string
  /** 对应后端字段 approverId */
  approverId?: string
  /** 对应后端字段 approverBy */
  approverBy?: string
  /** 对应后端字段 approveTime */
  approveTime?: string
  /** 对应后端字段 approveComment */
  approveComment?: string
  /** 对应后端字段 handlingId */
  handlingId?: string
  /** 对应后端字段 handlingBy */
  handlingBy?: string
  /** 对应后端字段 handlingTime */
  handlingTime?: string
  /** 对应后端字段 handlingComment */
  handlingComment?: string
  /** 对应后端字段 flowInstanceId */
  flowInstanceId?: string
  /** 对应后端字段 transferStatus */
  transferStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * EmployeeTransferUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeTransferUpdateDto）
 */
export interface EmployeeTransferUpdate extends EmployeeTransferCreate {
  /** 对应后端字段 employeeTransferId */
  employeeTransferId: string
}

/**
 * EmployeeTransferTransferStatus类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeTransferTransferStatusDto）
 */
export interface EmployeeTransferTransferStatus {
  /** 对应后端字段 employeeTransferId */
  employeeTransferId: string
  /** 对应后端字段 transferStatus */
  transferStatus: number
}
