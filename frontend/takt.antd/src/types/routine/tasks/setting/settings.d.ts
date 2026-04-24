// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/setting/settings
// 文件名称：settings.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：settings相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Settings类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Setting.TaktSettingsDto）
 */
export interface Settings extends TaktEntityBase {
  /** 对应后端字段 settingId */
  settingId: string
  /** 对应后端字段 settingKey */
  settingKey: string
  /** 对应后端字段 settingValue */
  settingValue?: string
  /** 对应后端字段 settingName */
  settingName?: string
  /** 对应后端字段 settingGroup */
  settingGroup?: string
  /** 对应后端字段 isBuiltIn */
  isBuiltIn: number
  /** 对应后端字段 isEncrypted */
  isEncrypted: number
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 settingStatus */
  settingStatus: number
}

/**
 * SettingsQuery类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Setting.TaktSettingsQueryDto）
 */
export interface SettingsQuery extends TaktPagedQuery {
  /** 对应后端字段 settingKey */
  settingKey?: string
  /** 对应后端字段 settingGroup */
  settingGroup?: string
  /** 对应后端字段 settingStatus */
  settingStatus?: number
}

/**
 * SettingsCreate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Setting.TaktSettingsCreateDto）
 */
export interface SettingsCreate {
  /** 对应后端字段 settingKey */
  settingKey: string
  /** 对应后端字段 settingValue */
  settingValue?: string
  /** 对应后端字段 settingName */
  settingName?: string
  /** 对应后端字段 settingGroup */
  settingGroup?: string
  /** 对应后端字段 isBuiltIn */
  isBuiltIn: number
  /** 对应后端字段 isEncrypted */
  isEncrypted: number
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * SettingsUpdate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Setting.TaktSettingsUpdateDto）
 */
export interface SettingsUpdate extends SettingsCreate {
  /** 对应后端字段 settingId */
  settingId: string
}

/**
 * SettingsStatus类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Setting.TaktSettingsStatusDto）
 */
export interface SettingsStatus {
  /** 对应后端字段 settingId */
  settingId: string
  /** 对应后端字段 settingStatus */
  settingStatus: number
}
