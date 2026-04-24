// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/routine/tasks/dict/dict-data
// 文件名称：dict-data.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：dict-data相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * DictData类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Dict.TaktDictDataDto）
 */
export interface DictData extends TaktEntityBase {
  /** 对应后端字段 dictDataId */
  dictDataId: string
  /** 对应后端字段 dictTypeId */
  dictTypeId: string
  /** 对应后端字段 dictTypeCode */
  dictTypeCode: string
  /** 对应后端字段 dictLabel */
  dictLabel: string
  /** 对应后端字段 dictL10nKey */
  dictL10nKey?: string
  /** 对应后端字段 dictValue */
  dictValue: string
  /** 对应后端字段 cssClass */
  cssClass: number
  /** 对应后端字段 listClass */
  listClass: number
  /** 对应后端字段 extLabel */
  extLabel?: string
  /** 对应后端字段 extValue */
  extValue?: string
  /** 对应后端字段 orderNum */
  orderNum: number
}

/**
 * DictDataQuery类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Dict.TaktDictDataQueryDto）
 */
export interface DictDataQuery extends TaktPagedQuery {
  /** 对应后端字段 dictTypeId */
  dictTypeId?: string
  /** 对应后端字段 dictTypeCode */
  dictTypeCode?: string
  /** 对应后端字段 dictLabel */
  dictLabel?: string
  /** 对应后端字段 dictValue */
  dictValue?: string
}

/**
 * DictDataCreate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Dict.TaktDictDataCreateDto）
 */
export interface DictDataCreate {
  /** 对应后端字段 dictTypeId */
  dictTypeId: string
  /** 对应后端字段 dictTypeCode */
  dictTypeCode: string
  /** 对应后端字段 dictLabel */
  dictLabel: string
  /** 对应后端字段 dictL10nKey */
  dictL10nKey?: string
  /** 对应后端字段 dictValue */
  dictValue: string
  /** 对应后端字段 cssClass */
  cssClass: number
  /** 对应后端字段 listClass */
  listClass: number
  /** 对应后端字段 extLabel */
  extLabel?: string
  /** 对应后端字段 extValue */
  extValue?: string
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * DictDataUpdate类型（对应后端 Takt.Application.Dtos.Routine.Tasks.Dict.TaktDictDataUpdateDto）
 */
export interface DictDataUpdate extends DictDataCreate {
  /** 对应后端字段 dictDataId */
  dictDataId: string
}
