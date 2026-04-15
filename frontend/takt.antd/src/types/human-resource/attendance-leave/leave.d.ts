// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/leave
// 文件名称：leave.d.ts
// 功能描述：请假相关类型定义，对应后端 Takt.Application.Dtos.HumanResource.AttendanceLeave.TaktLeaveDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 请假类型（对应后端 TaktLeaveDto）
 */
export interface Leave extends TaktEntityBase {
  /** 请假ID（对应后端 LeaveId，序列化为 string 以避免前端精度问题） */
  leaveId: string
  /** 员工ID */
  employeeId: string
  /** 请假类型（affair/sick/annual 等） */
  leaveType: string
  /** 开始日期 */
  startDate: string
  /** 结束日期 */
  endDate: string
  /** 请假事由 */
  reason?: string
  /** 证明附件 JSON */
  proofAttachmentsJson?: string
  /** 流程实例ID */
  flowInstanceId?: string
  /** 请假状态：0=草稿 1=审批中 2=已通过 3=已驳回 4=已撤回 */
  leaveStatus: number
}

/**
 * 请假查询类型（对应后端 TaktLeaveQueryDto）
 */
export interface LeaveQuery extends TaktPagedQuery {
  /** 关键词（在事由、请假类型中模糊查询） */
  keyWords?: string
  /** 员工ID（精确） */
  employeeId?: string
  /** 请假类型（精确） */
  leaveType?: string
  /** 请假状态（精确） */
  leaveStatus?: number
  /** 开始日期起（闭区间） */
  startDateFrom?: string
  /** 开始日期止（闭区间） */
  startDateTo?: string
}

/**
 * 创建请假类型（对应后端 TaktLeaveCreateDto）
 */
export interface LeaveCreate {
  /** 员工ID */
  employeeId: string
  /** 请假类型（affair/sick/annual 等） */
  leaveType: string
  /** 开始日期 */
  startDate: string
  /** 结束日期 */
  endDate: string
  /** 请假事由 */
  reason?: string
  /** 证明附件 JSON */
  proofAttachmentsJson?: string
}

/**
 * 更新请假类型（对应后端 TaktLeaveUpdateDto）
 */
export interface LeaveUpdate extends LeaveCreate {
  /** 请假ID（路由用，校验一致性） */
  leaveId: string
}

/**
 * 请假状态类型（对应后端 TaktLeaveStatusDto，与流程实例同步）
 */
export interface LeaveStatus {
  /** 请假ID */
  leaveId: string
  /** 请假状态：0=草稿 1=审批中 2=已通过 3=已驳回 4=已撤回 */
  leaveStatus: number
  /** 流程实例ID（可选，同步时校验） */
  flowInstanceId?: string
}

/**
 * 请假导入模板类型（对应后端 TaktLeaveTemplateDto，用于生成 Excel 表头/模板）
 */
export interface LeaveTemplate {
  /** 员工ID */
  employeeId: string
  /** 请假类型（affair/sick/annual 等） */
  leaveType: string
  /** 开始日期 */
  startDate: string
  /** 结束日期 */
  endDate: string
  /** 请假事由 */
  reason?: string
  /** 证明附件 JSON（可选） */
  proofAttachmentsJson?: string
}

/**
 * 请假导入类型（对应后端 TaktLeaveImportDto，Excel 导入行映射）
 */
export interface LeaveImport {
  /** 员工ID */
  employeeId: string
  /** 请假类型（affair/sick/annual 等） */
  leaveType: string
  /** 开始日期 */
  startDate: string
  /** 结束日期 */
  endDate: string
  /** 请假事由 */
  reason?: string
  /** 证明附件 JSON（可选） */
  proofAttachmentsJson?: string
}

/**
 * 请假导出类型（对应后端 TaktLeaveExportDto，Excel 导出行映射）
 */
export interface LeaveExport {
  /** 请假ID */
  leaveId: string
  /** 员工ID */
  employeeId: string
  /** 请假类型 */
  leaveType: string
  /** 开始日期 */
  startDate: string
  /** 结束日期 */
  endDate: string
  /** 请假事由 */
  reason?: string
  /** 证明附件 JSON */
  proofAttachmentsJson?: string
  /** 流程实例ID */
  flowInstanceId?: string
  /** 请假状态：0=草稿 1=审批中 2=已通过 3=已驳回 4=已撤回 */
  leaveStatus: number
  /** 创建时间 */
  createdAt: string
}
