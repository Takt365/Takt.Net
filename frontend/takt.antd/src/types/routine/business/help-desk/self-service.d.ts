// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/business/help-desk/self-service
// 文件名称：self-service.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：self-service相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * SelfService类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktSelfServiceDto）
 */
export interface SelfService extends TaktEntityBase {
  /** 对应后端字段 selfServiceId */
  selfServiceId: string
  /** 对应后端字段 serviceName */
  serviceName: string
  /** 对应后端字段 serviceType */
  serviceType: number
  /** 对应后端字段 description */
  description?: string
  /** 对应后端字段 linkOrCode */
  linkOrCode?: string
  /** 对应后端字段 iconUrl */
  iconUrl?: string
  /** 对应后端字段 selfServiceStatus */
  selfServiceStatus: number
  /** 对应后端字段 orderNum */
  orderNum: number
}

/**
 * SelfServiceQuery类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktSelfServiceQueryDto）
 */
export interface SelfServiceQuery extends TaktPagedQuery {
  /** 对应后端字段 serviceName */
  serviceName?: string
  /** 对应后端字段 serviceType */
  serviceType?: number
  /** 对应后端字段 selfServiceStatus */
  selfServiceStatus?: number
}

/**
 * SelfServiceCreate类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktSelfServiceCreateDto）
 */
export interface SelfServiceCreate {
  /** 对应后端字段 serviceName */
  serviceName: string
  /** 对应后端字段 serviceType */
  serviceType: number
  /** 对应后端字段 description */
  description?: string
  /** 对应后端字段 linkOrCode */
  linkOrCode?: string
  /** 对应后端字段 iconUrl */
  iconUrl?: string
  /** 对应后端字段 selfServiceStatus */
  selfServiceStatus: number
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * SelfServiceUpdate类型（对应后端 Takt.Application.Dtos.Routine.Business.HelpDesk.TaktSelfServiceUpdateDto）
 */
export interface SelfServiceUpdate extends SelfServiceCreate {
  /** 对应后端字段 selfServiceId */
  selfServiceId: string
}
