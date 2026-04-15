// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/generator
// 文件名称：table-column.d.ts
// 功能描述：代码生成字段配置类型，对应后端 TaktGenTableColumnDto 等
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'
import type { GenTable } from './table'

/** 代码生成字段配置（对应后端 TaktGenTableColumnDto，继承 TaktEntityBase） */
export interface GenTableColumn extends TaktEntityBase {
  /** 字段ID（对应后端 Id，序列化为 string 以避免精度问题） */
  columnId: string
  /** 表ID（关联代码生成表配置） */
  tableId: string
  /** 所属表配置（主表导航，按需返回） */
  table?: GenTable
  databaseColumnName?: string
  columnComment?: string
  databaseDataType?: string
  csharpDataType?: string
  csharpColumnName?: string
  length?: number
  decimalDigits?: number
  isPk?: number
  isIncrement?: number
  isRequired?: number
  isCreate?: number
  isUpdate?: number
  isUnique?: number
  isList?: number
  isExport?: number
  isSort?: number
  isQuery?: number
  queryType?: string
  htmlType?: string
  dictType?: string
  orderNum?: number
  [key: string]: any
}

/** 代码生成字段配置查询（对应后端 TaktGenTableColumnQueryDto） */
export interface GenTableColumnQuery extends TaktPagedQuery {
  tableId?: string
  databaseColumnName?: string
  isPk?: number
  isQuery?: number
}

/** 创建代码生成字段配置（对应后端 TaktGenTableColumnCreateDto） */
export interface GenTableColumnCreate {
  tableId: string
  databaseColumnName?: string
  columnComment?: string
  databaseDataType?: string
  csharpDataType?: string
  csharpColumnName?: string
  length?: number
  decimalDigits?: number
  isPk?: number
  isIncrement?: number
  isRequired?: number
  isCreate?: number
  isUpdate?: number
  isUnique?: number
  isList?: number
  isExport?: number
  isSort?: number
  isQuery?: number
  queryType?: string
  htmlType?: string
  dictType?: string
  orderNum?: number
  remark?: string
  [key: string]: any
}

/** 更新代码生成字段配置（对应后端 TaktGenTableColumnUpdateDto） */
export interface GenTableColumnUpdate extends GenTableColumnCreate {
  columnId: string
}
