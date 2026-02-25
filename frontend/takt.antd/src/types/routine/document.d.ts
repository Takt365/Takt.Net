// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/routine/document
// 文件名称：document.d.ts
// 功能描述：文控中心文档类型定义，对应后端 Takt.Application.Dtos.Routine.DocsCenter
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 文控中心文档（对应后端 TaktDocumentDto）
 */
export interface Document extends TaktEntityBase {
  /** 文档ID（对应后端 DocumentId，序列化为 string） */
  documentId: string
  /** 文档编码（唯一） */
  documentCode: string
  /** 文档标题 */
  documentTitle: string
  /** 文档类型（0=发布，1=变更，2=废止，3=其他） */
  documentType: number
  /** 文档版本号 */
  documentVersion?: string
  /** 工作流实例ID */
  instanceId: string
  /** 文档状态（0=草稿，1=审批中，2=已批准，3=已驳回，4=已发布，5=已废止） */
  documentStatus: number
  /** 申请人ID */
  applicantId: string
  /** 申请人姓名 */
  applicantName: string
  /** 申请部门ID */
  applicantDeptId?: string
  /** 申请部门名称 */
  applicantDeptName?: string
  /** 申请时间 */
  applyTime?: string
  /** 批准时间 */
  approvedTime?: string
  /** 关联文件ID */
  fileId?: string
  /** 收发文方向（0=发文，1=收文） */
  direction: number
  /** 文种（0=通知，1=报告，2=制度，3=规定，4=其他） */
  documentCategory: number
  /** 生命周期阶段（0~6） */
  lifecycleStage: number
  /** 保管期限（年） */
  retentionYears?: number
  /** 生效时间 */
  effectiveTime?: string
  /** 归档时间 */
  archiveTime?: string
  /** 作废时间 */
  obsoleteTime?: string
}

/**
 * 文档查询（对应后端 TaktDocumentQueryDto）
 */
export interface DocumentQuery extends TaktPagedQuery {
  keyWords?: string
  documentCode?: string
  documentTitle?: string
  documentType?: number
  documentStatus?: number
  direction?: number
  lifecycleStage?: number
}

/**
 * 创建文档（对应后端 TaktDocumentCreateDto）
 */
export interface DocumentCreate {
  documentCode: string
  documentTitle: string
  documentType?: number
  documentVersion?: string
  applicantId: string
  applicantName: string
  applicantDeptId?: string
  applicantDeptName?: string
  fileId?: string
  direction?: number
  documentCategory?: number
  lifecycleStage?: number
  retentionYears?: number
  remark?: string
}

/**
 * 更新文档（对应后端 TaktDocumentUpdateDto）
 */
export interface DocumentUpdate extends DocumentCreate {
  documentId: string
}

/**
 * 文档状态（对应后端 TaktDocumentStatusDto）
 */
export interface DocumentStatus {
  documentId: string
  documentStatus: number
}
