// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-attachment
// 文件名称：employee-attachment.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：employee-attachment相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * EmployeeAttachment类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeAttachmentDto）
 */
export interface EmployeeAttachment extends TaktEntityBase {
  /** 对应后端字段 employeeAttachmentId */
  employeeAttachmentId: string
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 fileId */
  fileId: string
  /** 对应后端字段 fileCode */
  fileCode: string
  /** 对应后端字段 fileName */
  fileName: string
  /** 对应后端字段 filePath */
  filePath: string
  /** 对应后端字段 fileSize */
  fileSize: number
  /** 对应后端字段 fileType */
  fileType?: string
  /** 对应后端字段 attachmentType */
  attachmentType: number
  /** 对应后端字段 attachmentDescription */
  attachmentDescription?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}

/**
 * EmployeeAttachmentQuery类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeAttachmentQueryDto）
 */
export interface EmployeeAttachmentQuery extends TaktPagedQuery {
  /** 对应后端字段 employeeId */
  employeeId?: string
  /** 对应后端字段 lineNumber */
  lineNumber?: number
  /** 对应后端字段 fileId */
  fileId?: string
  /** 对应后端字段 fileCode */
  fileCode?: string
  /** 对应后端字段 fileName */
  fileName?: string
  /** 对应后端字段 filePath */
  filePath?: string
  /** 对应后端字段 fileSize */
  fileSize?: number
  /** 对应后端字段 fileType */
  fileType?: string
  /** 对应后端字段 attachmentType */
  attachmentType?: number
  /** 对应后端字段 attachmentDescription */
  attachmentDescription?: string
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
 * EmployeeAttachmentCreate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeAttachmentCreateDto）
 */
export interface EmployeeAttachmentCreate {
  /** 对应后端字段 employeeId */
  employeeId: string
  /** 对应后端字段 lineNumber */
  lineNumber: number
  /** 对应后端字段 fileId */
  fileId: string
  /** 对应后端字段 fileCode */
  fileCode: string
  /** 对应后端字段 fileName */
  fileName: string
  /** 对应后端字段 filePath */
  filePath: string
  /** 对应后端字段 fileSize */
  fileSize: number
  /** 对应后端字段 fileType */
  fileType?: string
  /** 对应后端字段 attachmentType */
  attachmentType: number
  /** 对应后端字段 attachmentDescription */
  attachmentDescription?: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * EmployeeAttachmentUpdate类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeAttachmentUpdateDto）
 */
export interface EmployeeAttachmentUpdate extends EmployeeAttachmentCreate {
  /** 对应后端字段 employeeAttachmentId */
  employeeAttachmentId: string
}

/**
 * EmployeeAttachmentSort类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeAttachmentSortDto）
 */
export interface EmployeeAttachmentSort {
  /** 对应后端字段 employeeAttachmentId */
  employeeAttachmentId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
