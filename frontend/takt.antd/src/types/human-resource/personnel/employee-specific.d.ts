// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-specific
// 文件名称：employee-specific.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：employee-specific相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EmployeeCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeCreateDto）
 */
export interface EmployeeCreate {
  /** 对应后端字段 isSystemEmployeeCode */
  isSystemEmployeeCode?: boolean
}

/**
 * EmployeeTransferStatus类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeTransferStatusDto）
 */
export interface EmployeeTransferStatus {
  /** 对应后端字段 transferId */
  transferId?: string
}
