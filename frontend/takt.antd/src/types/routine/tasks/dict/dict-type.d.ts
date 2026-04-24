// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/dict/dict-type
// 文件名称：dict-type.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：dict-type相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * DictType类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Dict.TaktDictTypeDto）
 */
export interface DictType extends TaktEntityBase {
  /** 对应后端字段 dictTypeId */
  dictTypeId: string
  /** 对应后端字段 dictTypeCode */
  dictTypeCode: string
  /** 对应后端字段 dictTypeName */
  dictTypeName: string
  /** 对应后端字段 dataSource */
  dataSource: number
  /** 对应后端字段 dataConfigId */
  dataConfigId: string
  /** 对应后端字段 sqlScript */
  sqlScript?: string
  /** 对应后端字段 isBuiltIn */
  isBuiltIn: number
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 dictTypeStatus */
  dictTypeStatus: number
  /** 对应后端字段 dictDataList */
  dictDataList?: unknown[]
}

/**
 * DictTypeQuery类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Dict.TaktDictTypeQueryDto）
 */
export interface DictTypeQuery extends TaktPagedQuery {
  /** 对应后端字段 dictTypeName */
  dictTypeName?: string
  /** 对应后端字段 dictTypeCode */
  dictTypeCode?: string
  /** 对应后端字段 dictTypeStatus */
  dictTypeStatus?: number
}

/**
 * DictTypeCreate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Dict.TaktDictTypeCreateDto）
 */
export interface DictTypeCreate {
  /** 对应后端字段 dictTypeCode */
  dictTypeCode: string
  /** 对应后端字段 dictTypeName */
  dictTypeName: string
  /** 对应后端字段 dataSource */
  dataSource: number
  /** 对应后端字段 dataConfigId */
  dataConfigId: string
  /** 对应后端字段 sqlScript */
  sqlScript?: string
  /** 对应后端字段 isBuiltIn */
  isBuiltIn: number
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 remark */
  remark?: string
  /** 对应后端字段 dictDataList */
  dictDataList?: unknown[]
}

/**
 * DictTypeUpdate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Dict.TaktDictTypeUpdateDto）
 */
export interface DictTypeUpdate extends DictTypeCreate {
  /** 对应后端字段 dictTypeId */
  dictTypeId: string
}

/**
 * DictTypeStatus类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Dict.TaktDictTypeStatusDto）
 */
export interface DictTypeStatus {
  /** 对应后端字段 dictTypeId */
  dictTypeId: string
  /** 对应后端字段 dictTypeStatus */
  dictTypeStatus: number
}
