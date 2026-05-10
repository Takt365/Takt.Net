// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/setting/setting
// 文件名称：setting.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：setting相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * Setting类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Setting.TaktSettingDto）
 */
export interface Setting extends TaktEntityBase {
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
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 settingStatus */
  settingStatus: number
}

/**
 * SettingQuery类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Setting.TaktSettingQueryDto）
 */
export interface SettingQuery extends TaktPagedQuery {
  /** 对应后端字段 settingKey */
  settingKey?: string
  /** 对应后端字段 settingValue */
  settingValue?: string
  /** 对应后端字段 settingName */
  settingName?: string
  /** 对应后端字段 settingGroup */
  settingGroup?: string
  /** 对应后端字段 isBuiltIn */
  isBuiltIn?: number
  /** 对应后端字段 isEncrypted */
  isEncrypted?: number
  /** 对应后端字段 settingStatus */
  settingStatus?: number
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
 * SettingCreate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Setting.TaktSettingCreateDto）
 */
export interface SettingCreate {
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
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 settingStatus */
  settingStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * SettingUpdate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Setting.TaktSettingUpdateDto）
 */
export interface SettingUpdate extends SettingCreate {
  /** 对应后端字段 settingId */
  settingId: string
}

/**
 * SettingStatus类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Setting.TaktSettingStatusDto）
 */
export interface SettingStatus {
  /** 对应后端字段 settingId */
  settingId: string
  /** 对应后端字段 settingStatus */
  settingStatus: number
}

/**
 * SettingSort类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Setting.TaktSettingSortDto）
 */
export interface SettingSort {
  /** 对应后端字段 settingId */
  settingId: string
  /** 对应后端字段 sortOrder */
  sortOrder: number
}
