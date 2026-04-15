// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/routine/dict/dictdata
// 文件名称：dictdata.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：字典数据类型定义，对应后端 Takt.Application.Dtos.Routine.DataDict.TaktDictDataDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 字典数据（对应后端 Takt.Application.Dtos.Routine.DataDict.TaktDictDataDto）
 */
export interface DictData extends TaktEntityBase {
  /** 字典数据ID（对应后端 DictDataId，序列化为string以避免Javascript精度问题） */
  dictDataId: string
  /** 字典类型ID（外键，关联字典类型，对应后端 DictTypeId，序列化为string以避免Javascript精度问题） */
  dictTypeId: string
  /** 字典类型编码（对应后端 DictTypeCode） */
  dictTypeCode: string
  /** 字典标签（在同一个字典类型下唯一，对应后端 DictLabel） */
  dictLabel: string
  /** 字典本地化键（用于多语言翻译，对应后端 DictL10nKey） */
  dictL10nKey?: string
  /** 字典值（显示值，对应后端 DictValue） */
  dictValue: string
  /** CSS类名（对应后端 CssClass） */
  cssClass: number
  /** 列表类名（对应后端 ListClass） */
  listClass: number
  /** 扩展标签（对应后端 ExtLabel） */
  extLabel?: string
  /** 扩展值（对应后端 ExtValue） */
  extValue?: string
  /** 排序号（越小越靠前，对应后端 OrderNum） */
  orderNum: number
}

/**
 * 字典数据查询（对应后端 Takt.Application.Dtos.Routine.DataDict.TaktDictDataQueryDto）
 */
export interface DictDataQuery extends TaktPagedQuery {
  /** 关键词（在字典标签、字典值中模糊查询，对应后端 KeyWords） */
  keyWords?: string
  /** 字典类型ID（外键，关联字典类型，对应后端 DictTypeId，序列化为string以避免Javascript精度问题） */
  dictTypeId?: string
  /** 字典类型编码（对应后端 DictTypeCode） */
  dictTypeCode?: string
  /** 字典标签（对应后端 DictLabel） */
  dictLabel?: string
  /** 字典值（对应后端 DictValue） */
  dictValue?: string
}

/**
 * 字典数据创建（对应后端 Takt.Application.Dtos.Routine.DataDict.TaktDictDataCreateDto）
 */
export interface DictDataCreate {
  /** 字典类型ID（外键，关联字典类型，对应后端 DictTypeId，序列化为string以避免Javascript精度问题） */
  dictTypeId: string
  /** 字典类型编码（对应后端 DictTypeCode） */
  dictTypeCode: string
  /** 字典标签（在同一个字典类型下唯一，对应后端 DictLabel） */
  dictLabel: string
  /** 字典本地化键（用于多语言翻译，对应后端 DictL10nKey） */
  dictL10nKey?: string
  /** 字典值（显示值，对应后端 DictValue） */
  dictValue: string
  /** CSS类名（对应后端 CssClass） */
  cssClass: number
  /** 列表类名（对应后端 ListClass） */
  listClass: number
  /** 扩展标签（对应后端 ExtLabel） */
  extLabel?: string
  /** 扩展值（对应后端 ExtValue） */
  extValue?: string
  /** 排序号（越小越靠前，对应后端 OrderNum） */
  orderNum: number
  /** 备注（对应后端 Remark） */
  remark?: string
}

/**
 * 字典数据更新（对应后端 Takt.Application.Dtos.Routine.DataDict.TaktDictDataUpdateDto）
 */
export interface DictDataUpdate extends DictDataCreate {
  /** 字典数据ID（对应后端 DictDataId，序列化为string以避免Javascript精度问题） */
  dictDataId: string
}
