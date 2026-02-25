// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/common
// 文件名称：common.d.ts
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：通用类型定义，对应后端 Takt.Shared.Models
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktResultCode } from '@/utils/enum'

/**
 * Takt 实体基类（对应后端 Takt.Domain.Entities.TaktEntityBase，不包含 ID）
 * 审计字段顺序与后端一致：configId → extFieldJson → remark → createId → createBy → createTime → updateId → updateBy → updateTime → isDeleted → deleteId → deletedBy → deletedTime
 */
export interface TaktEntityBase {
  /** 租户配置ID（ConfigId，用于多租户数据隔离和数据库切换） */
  configId: string
  /** 扩展字段JSON */
  extFieldJson?: string
  /** 备注 */
  remark?: string
  /** 创建人ID（后端 long 序列化为 string） */
  createId?: string
  /** 创建人（用户名） */
  createBy?: string
  /** 创建时间 */
  createTime: string
  /** 更新人ID（后端 long 序列化为 string） */
  updateId?: string
  /** 更新人（用户名） */
  updateBy?: string
  /** 更新时间 */
  updateTime?: string
  /** 是否删除（软删除标记，0=未删除，1=已删除） */
  isDeleted: number
  /** 删除人ID（后端 long 序列化为 string） */
  deleteId?: string
  /** 删除人（用户名） */
  deletedBy?: string
  /** 删除时间 */
  deletedTime?: string
}

/**
 * Takt 通用分页查询（对应后端 Takt.Shared.Models.TaktPagedQuery）
 */
export interface TaktPagedQuery {
  /** 当前页码（从1开始，默认值：1） */
  pageIndex: number
  /** 每页大小（默认值：10） */
  pageSize: number
}

/**
 * Takt 通用分页结果（对应后端 Takt.Shared.Models.TaktPagedResult<T>）
 */
export interface TaktPagedResult<T> {
  /** 数据列表（对应后端 Data） */
  data: T[]
  /** 总记录数（对应后端 Total） */
  total: number
  /** 当前页码（从1开始，对应后端 PageIndex，默认值：1） */
  pageIndex: number
  /** 每页大小（对应后端 PageSize，默认值：10） */
  pageSize: number
  /** 总页数（计算属性，对应后端 TotalPages） */
  totalPages: number
  /** 是否有上一页（计算属性，对应后端 HasPreviousPage） */
  hasPreviousPage: boolean
  /** 是否有下一页（计算属性，对应后端 HasNextPage） */
  hasNextPage: boolean
}

/**
 * Takt 下拉选择框选项（对应后端 TaktSelectOption，与 TaktDictData 一致）
 */
export interface TaktSelectOption {
  /** 字典标签 */
  dictLabel: string
  /** 字典键值 */
  dictValue: string | number
  /** 字典本地化键（用于多语言翻译） */
  dictL10nKey?: string
  /** 字典类型编码（用于批量加载时前端分组，单个查询时通常为空） */
  dictTypeCode?: string
  /** 扩展标签 */
  extLabel?: string
  /** 扩展键值 */
  extValue?: string | number
  /** CSS类名 */
  cssClass?: number
  /** 列表类名 */
  listClass?: number
  /** 排序号 */
  orderNum: number
}

/**
 * Takt 树形下拉选择框选项（对应后端 TaktTreeSelectOption，通用树形结构，适用于部门、会计科目、菜单等）
 */
export interface TaktTreeSelectOption extends TaktSelectOption {
  /** 子节点列表 */
  children?: TaktTreeSelectOption[]
}

/**
 * Takt API统一返回结果（对应后端 Takt.Shared.Models.TaktApiResult<T>）
 */
export interface TaktApiResult<T = any> {
  /** 结果代码（对应后端 Code） */
  code: TaktResultCode | number
  /** 消息（对应后端 Message） */
  message: string
  /** 数据（对应后端 Data，可为 null） */
  data: T | null
  /** 是否成功（计算属性，对应后端 Success，Code == TaktResultCode.Success） */
  success: boolean
}

// 重新导出 enum 类型，方便使用
export type { TaktResultCode } from '@/utils/enum'
