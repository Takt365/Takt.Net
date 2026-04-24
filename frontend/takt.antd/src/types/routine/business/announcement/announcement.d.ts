// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/announcement/announcement
// 文件名称：announcement.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：announcement相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Announcement类型（对应后端 Takt.Application.Dtos.Routine.Business.Announcement.TaktAnnouncementDto）
 */
export interface Announcement extends TaktEntityBase {
  /** 对应后端字段 announcementId */
  announcementId: string
  /** 对应后端字段 announcementCode */
  announcementCode: string
  /** 对应后端字段 announcementTitle */
  announcementTitle: string
  /** 对应后端字段 announcementContent */
  announcementContent: string
  /** 对应后端字段 announcementType */
  announcementType: number
  /** 对应后端字段 publisherId */
  publisherId: string
  /** 对应后端字段 publisherName */
  publisherName: string
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 publishScope */
  publishScope: number
  /** 对应后端字段 publishScopeConfig */
  publishScopeConfig?: string
  /** 对应后端字段 isTop */
  isTop: number
  /** 对应后端字段 isUrgent */
  isUrgent: number
  /** 对应后端字段 publishTime */
  publishTime?: string
  /** 对应后端字段 effectiveTime */
  effectiveTime?: string
  /** 对应后端字段 expireTime */
  expireTime?: string
  /** 对应后端字段 readCount */
  readCount: number
  /** 对应后端字段 attachmentCount */
  attachmentCount: number
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 announcementStatus */
  announcementStatus: number
}

/**
 * AnnouncementQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.Announcement.TaktAnnouncementQueryDto）
 */
export interface AnnouncementQuery extends TaktPagedQuery {
  /** 对应后端字段 announcementCode */
  announcementCode?: string
  /** 对应后端字段 announcementTitle */
  announcementTitle?: string
  /** 对应后端字段 announcementType */
  announcementType?: number
  /** 对应后端字段 announcementStatus */
  announcementStatus?: number
}

/**
 * AnnouncementCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.Announcement.TaktAnnouncementCreateDto）
 */
export interface AnnouncementCreate {
  /** 对应后端字段 announcementCode */
  announcementCode: string
  /** 对应后端字段 announcementTitle */
  announcementTitle: string
  /** 对应后端字段 announcementContent */
  announcementContent: string
  /** 对应后端字段 announcementType */
  announcementType: number
  /** 对应后端字段 deptId */
  deptId?: string
  /** 对应后端字段 deptName */
  deptName?: string
  /** 对应后端字段 publishScope */
  publishScope: number
  /** 对应后端字段 publishScopeConfig */
  publishScopeConfig?: string
  /** 对应后端字段 isTop */
  isTop: number
  /** 对应后端字段 isUrgent */
  isUrgent: number
  /** 对应后端字段 effectiveTime */
  effectiveTime?: string
  /** 对应后端字段 expireTime */
  expireTime?: string
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * AnnouncementUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.Announcement.TaktAnnouncementUpdateDto）
 */
export interface AnnouncementUpdate extends AnnouncementCreate {
  /** 对应后端字段 announcementId */
  announcementId: string
}

/**
 * AnnouncementStatus类型（对应后端 Takt.Application.Dtos.Routine.Business.Announcement.TaktAnnouncementStatusDto）
 */
export interface AnnouncementStatus {
  /** 对应后端字段 announcementId */
  announcementId: string
  /** 对应后端字段 announcementStatus */
  announcementStatus: number
}
