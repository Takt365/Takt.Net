// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/attendance-leave/attendance-correction
// 文件名称：attendance-correction.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：补卡管理相关类型定义，对应后端 TaktAttendanceCorrectionDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 补卡记录类型（对应后端 TaktAttendanceCorrectionDto）
 */
export interface AttendanceCorrection extends TaktEntityBase {
  /** 补卡记录 ID（对应后端 CorrectionId，序列化为 string） */
  correctionId: string
  /** 员工 ID（对应后端 EmployeeId） */
  employeeId: string
  /** 补卡归属日期（日期部分有效，对应后端 TargetDate） */
  targetDate: string
  /** 补卡类型（1=上班 2=下班，对应后端 CorrectionKind） */
  correctionKind: number
  /** 申请补录的打卡时间（对应后端 RequestPunchTime） */
  requestPunchTime: string
  /** 申请原因（对应后端 Reason） */
  reason: string
  /** 审批状态（0=草稿 1=待审 2=通过 3=驳回，对应后端 ApprovalStatus） */
  approvalStatus: number
}

/**
 * 补卡记录查询类型（对应后端 TaktAttendanceCorrectionQueryDto；KeyWords 在后端用于匹配申请原因、备注）
 */
export interface AttendanceCorrectionQuery extends TaktPagedQuery {
  /** 员工 ID（精确） */
  employeeId?: string
  /** 归属日期起（含，对应后端 TargetDateFrom） */
  targetDateFrom?: string
  /** 归属日期止（含，对应后端 TargetDateTo） */
  targetDateTo?: string
  /** 审批状态（精确） */
  approvalStatus?: number
}

/**
 * 创建补卡记录类型（对应后端 TaktAttendanceCorrectionCreateDto）
 */
export interface AttendanceCorrectionCreate {
  /** 员工 ID */
  employeeId: string
  /** 补卡归属日期 */
  targetDate: string
  /** 补卡类型（1=上班 2=下班） */
  correctionKind: number
  /** 申请补录的打卡时间 */
  requestPunchTime: string
  /** 申请原因 */
  reason: string
  /** 审批状态 */
  approvalStatus: number
  /** 备注（写入 DTO 基类 Remark） */
  remark?: string
}

/**
 * 更新补卡记录类型（对应后端 TaktAttendanceCorrectionUpdateDto）
 */
export interface AttendanceCorrectionUpdate extends AttendanceCorrectionCreate {
  /** 补卡记录 ID */
  correctionId: string
}
