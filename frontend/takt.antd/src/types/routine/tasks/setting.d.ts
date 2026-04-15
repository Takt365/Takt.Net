// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：@/types/routine/setting
// 文件名称：setting.d.ts
// 功能描述：设置相关类型定义，对应后端 Takt.Application.Dtos.Routine.Setting
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 设置实体（对应后端 TaktSettingsDto）
 */
export interface Setting extends TaktEntityBase {
  /** 设置ID（对应后端 SettingId，序列化为 string 以避免精度问题） */
  settingId: string
  /** 设置键（唯一） */
  settingKey: string
  /** 设置值 */
  settingValue?: string
  /** 设置名称（描述） */
  settingName?: string
  /** 设置分组（backend=后端，frontend=前端） */
  settingGroup?: string
  /** 是否内置（1=是，0=否） */
  isBuiltIn: number
  /** 是否加密（1=是，0=否） */
  isEncrypted: number
  /** 排序号（越小越靠前） */
  orderNum: number
  /** 设置状态（0=启用，1=禁用） */
  settingStatus: number
}

/**
 * 设置查询（对应后端 TaktSettingsQueryDto）
 */
export interface SettingQuery extends TaktPagedQuery {
  /** 关键词（在设置键、设置名称中模糊查询） */
  keyWords?: string
  /** 设置键 */
  settingKey?: string
  /** 设置分组 */
  settingGroup?: string
  /** 设置状态（0=启用，1=禁用） */
  settingStatus?: number
}

/**
 * 创建设置（对应后端 TaktSettingsCreateDto）
 */
export interface SettingCreate {
  settingKey: string
  settingValue?: string
  settingName?: string
  settingGroup?: string
  isBuiltIn?: number
  isEncrypted?: number
  orderNum?: number
  remark?: string
}

/**
 * 更新设置（对应后端 TaktSettingsUpdateDto）
 */
export interface SettingUpdate extends SettingCreate {
  settingId: string
}

/**
 * 设置状态（对应后端 TaktSettingsStatusDto）
 */
export interface SettingStatus {
  settingId: string
  settingStatus: number
}
