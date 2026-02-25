// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/routine/announcement
// 文件名称：announcement.d.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：公告相关类型定义，对应后端 Takt.Application.Dtos.Routine.Announcement.TaktAnnouncementDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 公告类型（对应后端 Takt.Application.Dtos.Routine.Announcement.TaktAnnouncementDto）
 */
export interface Announcement extends TaktEntityBase {
  /** 公告ID（对应后端 Id，序列化为string以避免Javascript精度问题） */
  announcementId: string
  /** 公告编码 */
  announcementCode: string
  /** 公告标题 */
  announcementTitle: string
  /** 公告内容 */
  announcementContent: string
  /** 公告类型（0=通知，1=公告，2=新闻，3=活动） */
  announcementType: number
  /** 发布人ID（对应后端 PublisherId，序列化为string以避免Javascript精度问题） */
  publisherId: string
  /** 发布人姓名 */
  publisherName: string
  /** 发布部门ID（对应后端 DeptId，序列化为string以避免Javascript精度问题） */
  deptId?: string
  /** 发布部门名称 */
  deptName?: string
  /** 发布范围（0=全部，1=指定部门，2=指定用户，3=指定角色） */
  publishScope: number
  /** 发布范围配置（JSON格式） */
  publishScopeConfig?: string
  /** 是否置顶（0=否，1=是） */
  isTop: number
  /** 是否紧急（0=一般，1=紧急，2=非常紧急） */
  isUrgent: number
  /** 发布时间 */
  publishTime?: string
  /** 生效时间 */
  effectiveTime?: string
  /** 失效时间 */
  expireTime?: string
  /** 阅读次数 */
  readCount: number
  /** 附件数量 */
  attachmentCount: number
  /** 排序号 */
  orderNum: number
  /** 公告状态（0=草稿，1=已发布，2=已撤回，3=已过期） */
  announcementStatus: number
}

/**
 * 公告查询类型（对应后端 Takt.Application.Dtos.Routine.Announcement.TaktAnnouncementQueryDto）
 */
export interface AnnouncementQuery extends TaktPagedQuery {
  /** 关键词（在公告标题、内容中模糊查询，继承自分页查询） */
  keyWords?: string
  /** 公告类型（0=通知，1=公告，2=新闻，3=活动） */
  announcementType?: number
  /** 公告状态（0=草稿，1=已发布，2=已撤回，3=已过期） */
  announcementStatus?: number
  /** 是否置顶（0=否，1=是） */
  isTop?: number
}

/**
 * 创建公告类型（对应后端 Takt.Application.Dtos.Routine.Announcement.TaktAnnouncementCreateDto）
 */
export interface AnnouncementCreate {
  /** 公告标题 */
  announcementTitle: string
  /** 公告内容 */
  announcementContent: string
  /** 公告类型（0=通知，1=公告，2=新闻，3=活动） */
  announcementType: number
  /** 发布范围（0=全部，1=指定部门，2=指定用户，3=指定角色） */
  publishScope: number
  /** 发布范围配置（JSON格式） */
  publishScopeConfig?: string
  /** 是否置顶（0=否，1=是） */
  isTop: number
  /** 是否紧急（0=一般，1=紧急，2=非常紧急） */
  isUrgent: number
  /** 生效时间 */
  effectiveTime?: string
  /** 失效时间 */
  expireTime?: string
  /** 排序号 */
  orderNum: number
  /** 附件列表（创建时可选上传的附件） */
  attachments?: AnnouncementAttachmentCreate[]
}

/**
 * 更新公告类型（对应后端 Takt.Application.Dtos.Routine.Announcement.TaktAnnouncementUpdateDto）
 */
export interface AnnouncementUpdate extends AnnouncementCreate {
  /** 公告ID（对应后端 AnnouncementId，序列化为string以避免Javascript精度问题） */
  announcementId: string
}

/**
 * 公告状态类型（对应后端 Takt.Application.Dtos.Routine.Announcement.TaktAnnouncementStatusDto）
 */
export interface AnnouncementStatus {
  /** 公告ID（对应后端 AnnouncementId，序列化为string以避免Javascript精度问题） */
  announcementId: string
  /** 公告状态（0=草稿，1=已发布，2=已撤回，3=已过期） */
  announcementStatus: number
}

/**
 * 公告附件类型（对应后端 Takt.Application.Dtos.Routine.Announcement.TaktAnnouncementAttachmentDto）
 */
export interface AnnouncementAttachment {
  /** 附件ID（对应后端 Id，序列化为string以避免Javascript精度问题） */
  attachmentId: string
  /** 公告ID（对应后端 AnnouncementId，序列化为string以避免Javascript精度问题） */
  announcementId: string
  /** 文件ID（对应后端 FileId，序列化为string以避免Javascript精度问题） */
  fileId: string
  /** 文件名称 */
  fileName: string
  /** 文件路径 */
  filePath: string
  /** 文件大小（字节） */
  fileSize: number
  /** 文件类型（MIME类型） */
  fileType?: string
  /** 文件扩展名 */
  fileExtension?: string
  /** 排序号 */
  orderNum: number
}

/**
 * 公告附件创建类型（对应后端 Takt.Application.Dtos.Routine.Announcement.TaktAnnouncementAttachmentCreateDto）
 */
export interface AnnouncementAttachmentCreate {
  /** 文件ID（对应后端 FileId，序列化为string以避免Javascript精度问题） */
  fileId: string
  /** 文件名称 */
  fileName: string
  /** 文件路径 */
  filePath: string
  /** 文件大小（字节） */
  fileSize: number
  /** 文件类型（MIME类型） */
  fileType?: string
  /** 文件扩展名 */
  fileExtension?: string
  /** 排序号 */
  orderNum: number
}

/**
 * 公告阅读记录类型（对应后端 Takt.Application.Dtos.Routine.Announcement.TaktAnnouncementReadDto）
 */
export interface AnnouncementRead {
  /** 阅读记录ID（对应后端 ReadId，序列化为string以避免Javascript精度问题） */
  readId: string
  /** 公告ID（对应后端 AnnouncementId，序列化为string以避免Javascript精度问题） */
  announcementId: string
  /** 用户ID（对应后端 UserId，序列化为string以避免Javascript精度问题） */
  userId: string
  /** 用户姓名 */
  userName: string
  /** 阅读时间 */
  readTime: string
  /** 阅读时长（秒） */
  readDuration: number
}
