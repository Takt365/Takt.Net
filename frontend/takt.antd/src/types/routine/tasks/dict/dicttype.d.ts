// 命名空间：@/types/routine/tasks/dict/dicttype

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'
import type { DictData, DictDataCreate } from './dictdata'

export interface DictType extends TaktEntityBase {
  dictTypeId: string
  dictTypeCode: string
  dictTypeName: string
  dataSource: number
  dataConfigId?: string
  sqlScript?: string
  isBuiltIn: number
  orderNum: number
  dictTypeStatus: number
  dictDataList?: DictData[]
}

export interface DictTypeQuery extends TaktPagedQuery {
  keyWords?: string
  dictTypeName?: string
  dictTypeCode?: string
  dictTypeStatus?: number
}

export interface DictTypeCreate {
  dictTypeCode: string
  dictTypeName: string
  dataSource: number
  dataConfigId?: string
  sqlScript?: string
  isBuiltIn: number
  orderNum: number
  remark?: string
  dictDataList?: DictDataCreate[]
}

export interface DictTypeUpdate extends DictTypeCreate {
  dictTypeId: string
}

export interface DictTypeStatus {
  dictTypeId: string
  dictTypeStatus: number
}
