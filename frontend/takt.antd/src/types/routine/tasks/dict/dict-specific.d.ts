// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/dict/dict-specific
// 文件名称：dict-specific.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：dict-specific相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * DictType类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Dict.TaktDictTypeDto）
 */
export interface DictType {
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
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 dictTypeStatus */
  dictTypeStatus: number
  /** 对应后端字段 dictDataList */
  dictDataList?: number[]
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
  /** 对应后端字段 sortOrder */
  sortOrder: number
  /** 对应后端字段 dictTypeStatus */
  dictTypeStatus: number
  /** 对应后端字段 extFieldJson */
  extFieldJson?: string
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * DictTypeUpdate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Dict.TaktDictTypeUpdateDto）
 */
export interface DictTypeUpdate extends DictTypeCreate {
  /** 对应后端字段 dictTypeId */
  dictTypeId: string
}
