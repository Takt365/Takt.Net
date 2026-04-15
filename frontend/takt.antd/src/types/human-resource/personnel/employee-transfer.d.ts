// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-transfer
// 文件名称：employee-transfer.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工调动相关类型定义，对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeTransferDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 员工调动类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeTransferDto）
 */
export interface EmployeeTransfer extends TaktEntityBase {
  /** 调动ID（对应后端 TransferId，序列化为string以避免Javascript精度问题） */
  transferId: string
  /** 员工ID（对应后端 EmployeeId，序列化为string以避免Javascript精度问题） */
  employeeId: string
  /** 调动类型（0=转岗 1=调岗） */
  transferType: number
  /** 原部门ID（对应后端 FromDeptId，序列化为string以避免Javascript精度问题） */
  fromDeptId: string
  /** 原部门名称 */
  fromDeptName: string
  /** 原岗位ID（对应后端 FromPostId，序列化为string以避免Javascript精度问题） */
  fromPostId?: string
  /** 原岗位名称 */
  fromPostName?: string
  /** 目标部门ID（对应后端 ToDeptId，序列化为string以避免Javascript精度问题） */
  toDeptId: string
  /** 目标部门名称 */
  toDeptName: string
  /** 目标岗位ID（对应后端 ToPostId，序列化为string以避免Javascript精度问题） */
  toPostId?: string
  /** 目标岗位名称 */
  toPostName?: string
  /** 生效日期 */
  effectiveDate?: string
  /** 申请事由 */
  reason?: string
  /** 流程实例ID（对应后端 FlowInstanceId，序列化为string以避免Javascript精度问题） */
  flowInstanceId?: string
  /** 调动状态：0=草稿 1=审批中 2=已通过 3=已驳回 4=已撤回 */
  transferStatus: number
}

/**
 * 员工调动查询类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeTransferQueryDto）
 */
export interface EmployeeTransferQuery extends TaktPagedQuery {
  /** 员工ID（精确） */
  employeeId?: string
  /** 调动类型（0=转岗 1=调岗；null 表示全部） */
  transferType?: number
  /** 调动状态（0=草稿 1=审批中 2=已通过 3=已驳回 4=已撤回；null 表示全部） */
  transferStatus?: number
  /** 生效日期起（闭区间） */
  effectiveDateFrom?: string
  /** 生效日期止（闭区间） */
  effectiveDateTo?: string
}

/**
 * 创建员工调动类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeTransferCreateDto）
 */
export interface EmployeeTransferCreate {
  /** 员工ID */
  employeeId: string
  /** 调动类型（0=转岗 1=调岗） */
  transferType: number
  /** 原部门ID */
  fromDeptId: string
  /** 原部门名称 */
  fromDeptName: string
  /** 原岗位ID */
  fromPostId?: string
  /** 原岗位名称 */
  fromPostName?: string
  /** 目标部门ID */
  toDeptId: string
  /** 目标部门名称 */
  toDeptName: string
  /** 目标岗位ID */
  toPostId?: string
  /** 目标岗位名称 */
  toPostName?: string
  /** 生效日期 */
  effectiveDate?: string
  /** 申请事由 */
  reason?: string
}

/**
 * 更新员工调动类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeTransferUpdateDto）
 */
export interface EmployeeTransferUpdate extends EmployeeTransferCreate {
  /** 调动ID（对应后端 TransferId，序列化为string以避免Javascript精度问题） */
  transferId: string
}

/**
 * 员工调动状态类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeTransferStatusDto，流程回调更新用）
 */
export interface EmployeeTransferStatus {
  /** 调动ID（对应后端 TransferId，序列化为string以避免Javascript精度问题） */
  transferId: string
  /** 调动状态：0=草稿 1=审批中 2=已通过 3=已驳回 4=已撤回 */
  transferStatus: number
}
