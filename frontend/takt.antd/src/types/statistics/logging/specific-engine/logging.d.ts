// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/statistics/logging/specific-engine/logging
// 文件名称：logging.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：logging相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * AopLogCreate类型（对应后端 Takt.Application.Dtos.Statistics.Logging.SpecificEngine.TaktAopLogCreateDto）
 */
export interface AopLogCreate {
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 operType */
  operType: string
  /** 对应后端字段 tableName */
  tableName: string
  /** 对应后端字段 primaryKeyId */
  primaryKeyId?: string
  /** 对应后端字段 beforeData */
  beforeData?: string
  /** 对应后端字段 afterData */
  afterData?: string
  /** 对应后端字段 diffData */
  diffData?: string
  /** 对应后端字段 sqlStatement */
  sqlStatement?: string
  /** 对应后端字段 operTime */
  operTime: string
  /** 对应后端字段 costTime */
  costTime: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * OperLogCreate类型（对应后端 Takt.Application.Dtos.Statistics.Logging.SpecificEngine.TaktOperLogCreateDto）
 */
export interface OperLogCreate {
  /** 对应后端字段 userName */
  userName: string
  /** 对应后端字段 operModule */
  operModule?: string
  /** 对应后端字段 operType */
  operType?: string
  /** 对应后端字段 operMethod */
  operMethod?: string
  /** 对应后端字段 requestMethod */
  requestMethod?: string
  /** 对应后端字段 operUrl */
  operUrl?: string
  /** 对应后端字段 requestParam */
  requestParam?: string
  /** 对应后端字段 jsonResult */
  jsonResult?: string
  /** 对应后端字段 operStatus */
  operStatus: number
  /** 对应后端字段 errorMsg */
  errorMsg?: string
  /** 对应后端字段 operIp */
  operIp?: string
  /** 对应后端字段 operLocation */
  operLocation?: string
  /** 对应后端字段 operCountry */
  operCountry?: string
  /** 对应后端字段 operProvince */
  operProvince?: string
  /** 对应后端字段 operCity */
  operCity?: string
  /** 对应后端字段 operIsp */
  operIsp?: string
  /** 对应后端字段 operTime */
  operTime: string
  /** 对应后端字段 costTime */
  costTime: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}
