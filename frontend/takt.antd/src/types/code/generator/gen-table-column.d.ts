// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：@/types/code/generator/gen-table-column
// 文件名称：gen-table-column.d.ts
// 创建时间：2025-02-02
// 创建人：Takt365
// 功能描述：gen-table-column相关类型定义（自动生成）
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * GenTableColumn类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktGenTableColumnDto）
 */
export interface GenTableColumn extends TaktEntityBase {
  /** 对应后端字段 columnId */
  columnId: string
  /** 对应后端字段 tableId */
  tableId: string
  /** 对应后端字段 databaseColumnName */
  databaseColumnName: string
  /** 对应后端字段 columnComment */
  columnComment?: string
  /** 对应后端字段 databaseDataType */
  databaseDataType: string
  /** 对应后端字段 csharpDataType */
  csharpDataType: string
  /** 对应后端字段 csharpColumnName */
  csharpColumnName: string
  /** 对应后端字段 length */
  length: number
  /** 对应后端字段 decimalDigits */
  decimalDigits: number
  /** 对应后端字段 isPk */
  isPk: number
  /** 对应后端字段 isIncrement */
  isIncrement: number
  /** 对应后端字段 isRequired */
  isRequired: number
  /** 对应后端字段 isCreate */
  isCreate: number
  /** 对应后端字段 isUpdate */
  isUpdate: number
  /** 对应后端字段 isUnique */
  isUnique: number
  /** 对应后端字段 isList */
  isList: number
  /** 对应后端字段 isExport */
  isExport: number
  /** 对应后端字段 isSort */
  isSort: number
  /** 对应后端字段 isQuery */
  isQuery: number
  /** 对应后端字段 queryType */
  queryType: string
  /** 对应后端字段 htmlType */
  htmlType: string
  /** 对应后端字段 dictType */
  dictType?: string
  /** 对应后端字段 orderNum */
  orderNum: number
}

/**
 * GenTableColumnQuery类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktGenTableColumnQueryDto）
 */
export interface GenTableColumnQuery extends TaktPagedQuery {
  /** 对应后端字段 tableId */
  tableId?: string
  /** 对应后端字段 databaseColumnName */
  databaseColumnName?: string
  /** 对应后端字段 isPk */
  isPk?: number
  /** 对应后端字段 isQuery */
  isQuery?: number
  /** 对应后端字段 isUnique */
  isUnique?: number
}

/**
 * GenTableColumnCreate类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktGenTableColumnCreateDto）
 */
export interface GenTableColumnCreate {
  /** 对应后端字段 tableId */
  tableId: string
  /** 对应后端字段 databaseColumnName */
  databaseColumnName: string
  /** 对应后端字段 columnComment */
  columnComment?: string
  /** 对应后端字段 databaseDataType */
  databaseDataType: string
  /** 对应后端字段 csharpDataType */
  csharpDataType: string
  /** 对应后端字段 csharpColumnName */
  csharpColumnName: string
  /** 对应后端字段 length */
  length: number
  /** 对应后端字段 decimalDigits */
  decimalDigits: number
  /** 对应后端字段 isPk */
  isPk: number
  /** 对应后端字段 isIncrement */
  isIncrement: number
  /** 对应后端字段 isRequired */
  isRequired: number
  /** 对应后端字段 isCreate */
  isCreate: number
  /** 对应后端字段 isUpdate */
  isUpdate: number
  /** 对应后端字段 isUnique */
  isUnique: number
  /** 对应后端字段 isList */
  isList: number
  /** 对应后端字段 isExport */
  isExport: number
  /** 对应后端字段 isSort */
  isSort: number
  /** 对应后端字段 isQuery */
  isQuery: number
  /** 对应后端字段 queryType */
  queryType: string
  /** 对应后端字段 htmlType */
  htmlType: string
  /** 对应后端字段 dictType */
  dictType?: string
  /** 对应后端字段 orderNum */
  orderNum: number
  /** 对应后端字段 remark */
  remark?: string
}

/**
 * GenTableColumnUpdate类型（对应后端 Takt.Application.Dtos.Code.Generator.TaktGenTableColumnUpdateDto）
 */
export interface GenTableColumnUpdate extends GenTableColumnCreate {
  /** 对应后端字段 columnId */
  columnId: string
}
