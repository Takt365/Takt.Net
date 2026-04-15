// 命名空间：@/types/routine/business/announcement

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

export interface Announcement extends TaktEntityBase {
  announcementId: string
  announcementCode: string
  announcementTitle: string
  announcementContent: string
  announcementType: number
  publisherId: string
  publisherName: string
  deptId?: string
  deptName?: string
  publishScope: number
  publishScopeConfig?: string
  isTop: number
  isUrgent: number
  publishTime?: string
  effectiveTime?: string
  expireTime?: string
  readCount: number
  attachmentCount: number
  orderNum: number
  announcementStatus: number
}

export interface AnnouncementQuery extends TaktPagedQuery {
  keyWords?: string
  announcementCode?: string
  announcementTitle?: string
  announcementType?: number
  announcementStatus?: number
}

export interface AnnouncementCreate {
  announcementCode: string
  announcementTitle: string
  announcementContent: string
  announcementType: number
  deptId?: string
  deptName?: string
  publishScope?: number
  publishScopeConfig?: string
  isTop?: number
  isUrgent?: number
  effectiveTime?: string
  expireTime?: string
  orderNum?: number
  remark?: string
}

export interface AnnouncementUpdate extends AnnouncementCreate {
  announcementId: string
}

export interface AnnouncementStatus {
  announcementId: string
  announcementStatus: number
}
