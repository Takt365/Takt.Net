// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-work
// 文件名称：employee-work.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工工作经历相关类型定义，对应后端 TaktEmployeeWorkDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 员工工作经历类型（对应后端 TaktEmployeeWorkDto）
 */
export interface EmployeeWork extends TaktEntityBase {
  /** 员工工作经历ID（对应后端 EmployeeWorkId） */
  employeeWorkId: string
  /** 员工ID（对应后端 EmployeeId） */
  employeeId: string
  /** 单位名称 */
  companyName: string
  /** 岗位名称 */
  positionName?: string
  /** 工作内容 */
  jobContent?: string
  /** 开始日期 */
  startDate?: string
  /** 结束日期 */
  endDate?: string
  /** 证明人 */
  witnessName?: string
  /** 证明人电话 */
  witnessPhone?: string
}

/**
 * 员工工作经历查询类型（对应后端 TaktEmployeeWorkQueryDto）
 */
export interface EmployeeWorkQuery extends TaktPagedQuery {
  /** 员工ID（精确） */
  employeeId?: string
  /** 单位名称（模糊） */
  companyName?: string
}

/**
 * 创建员工工作经历类型（对应后端 TaktEmployeeWorkCreateDto）
 */
export interface EmployeeWorkCreate {
  /** 员工ID */
  employeeId: string
  /** 单位名称 */
  companyName: string
  /** 岗位名称 */
  positionName?: string
  /** 工作内容 */
  jobContent?: string
  /** 开始日期 */
  startDate?: string
  /** 结束日期 */
  endDate?: string
  /** 证明人 */
  witnessName?: string
  /** 证明人电话 */
  witnessPhone?: string
}

/**
 * 更新员工工作经历类型（对应后端 TaktEmployeeWorkUpdateDto）
 */
export interface EmployeeWorkUpdate extends EmployeeWorkCreate {
  /** 员工工作经历ID */
  employeeWorkId: string
}
