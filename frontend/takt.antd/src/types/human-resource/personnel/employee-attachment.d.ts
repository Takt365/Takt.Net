// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/human-resource/personnel/employee-attachment
// 文件名称：employee-attachment.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：员工附件相关类型定义，对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeAttachmentDtos
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 员工附件类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeAttachmentDto）
 */
export interface EmployeeAttachment extends TaktEntityBase {
  /** 附件ID（对应后端 AttachmentId，序列化为string以避免Javascript精度问题） */
  attachmentId: string
  /** 员工ID（对应后端 EmployeeId，序列化为string以避免Javascript精度问题） */
  employeeId: string
  /** 文件ID（关联TaktFile，序列化为string以避免Javascript精度问题） */
  fileId: string
  /** 文件编码 */
  fileCode: string
  /** 文件名称 */
  fileName: string
  /** 文件路径 */
  filePath: string
  /** 文件大小（字节） */
  fileSize: number
  /** 文件类型（MIME类型） */
  fileType?: string
  /** 附件类型（0=身份证，1=学历证书，2=学位证书，3=资格证书，4=劳动合同，5=其他） */
  attachmentType: number
  /** 附件描述 */
  attachmentDescription?: string
  /** 排序号（越小越靠前） */
  orderNum: number
}

/**
 * 员工附件查询类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeAttachmentQueryDto）
 */
export interface EmployeeAttachmentQuery extends TaktPagedQuery {
  /** 员工ID（精确） */
  employeeId?: string
  /** 文件ID（精确） */
  fileId?: string
  /** 附件类型（0=身份证，1=学历证书，2=学位证书，3=资格证书，4=劳动合同，5=其他；null 表示全部） */
  attachmentType?: number
}

/**
 * 创建员工附件类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeAttachmentCreateDto）
 */
export interface EmployeeAttachmentCreate {
  /** 员工ID */
  employeeId: string
  /** 文件ID（关联TaktFile） */
  fileId: string
  /** 文件编码 */
  fileCode: string
  /** 文件名称 */
  fileName: string
  /** 文件路径 */
  filePath: string
  /** 文件大小（字节） */
  fileSize: number
  /** 文件类型（MIME类型） */
  fileType?: string
  /** 附件类型（0=身份证，1=学历证书，2=学位证书，3=资格证书，4=劳动合同，5=其他） */
  attachmentType: number
  /** 附件描述 */
  attachmentDescription?: string
  /** 排序号（越小越靠前） */
  orderNum: number
}

/**
 * 更新员工附件类型（对应后端 Takt.Application.Dtos.HumanResource.Personnel.TaktEmployeeAttachmentUpdateDto）
 */
export interface EmployeeAttachmentUpdate extends EmployeeAttachmentCreate {
  /** 附件ID（对应后端 AttachmentId，序列化为string以避免Javascript精度问题） */
  attachmentId: string
}
