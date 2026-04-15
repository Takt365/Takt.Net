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
 * Takt 实体基类（对应后端 Takt.Domain.Entities.TaktEntityBase，JSON 为 camelCase）
 * 后端 Id/CreatedById/UpdatedById/DeletedById 使用 ValueToStringConverter 序列化为 string，避免前端精度问题
 */
export interface TaktEntityBase {

  /** 租户配置ID（ConfigId，用于多租户数据隔离和数据库切换） */
  configId: string
  /** 扩展字段JSON */
  extFieldJson?: string
  /** 备注 */
  remark?: string
  /** 创建人ID（后端序列化为 string） */
  createdById: string
  /** 创建人（用户名） */
  createdBy: string
  /** 创建时间（ISO 8601 字符串） */
  createdAt: string
  /** 更新人ID（后端序列化为 string） */
  updatedById?: string
  /** 更新人（用户名） */
  updatedBy?: string
  /** 更新时间（ISO 8601 字符串） */
  updatedAt?: string
  /** 是否删除（软删除标记，0=未删除，1=已删除） */
  isDeleted: number
  /** 删除人ID（后端序列化为 string） */
  deletedById?: string
  /** 删除人（用户名） */
  deletedBy?: string
  /** 删除时间（ISO 8601 字符串） */
  deletedAt?: string
}

/**
 * Takt 通用分页查询（对应后端 Takt.Shared.Models.TaktPagedQuery）
 */
export interface TaktPagedQuery {
  /** 当前页码（从1开始，默认值：1） */
  pageIndex: number
  /** 每页大小（默认值：10） */
  pageSize: number
  /** 关键词（与后端 TaktPagedQuery.KeyWords 一致；列表请求 params 常用 PascalCase，见 holiday loadData） */
  KeyWords?: string
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

/**
 * 证明附件 JSON 数组单项（与 routine 文件上传返回、实体 `proof_attachments_json` 序列化项一致，camelCase）。
 * 人事请假与其它需存附件 JSON 的模块可共用此类型。
 */
export interface LeaveProofAttachment {
  /** 文件主键（可选） */
  fileId?: string
  /** 文件业务编码 */
  fileCode: string
  /** 存储文件名（可选） */
  fileName?: string
  /** 原始文件名 */
  fileOriginalName: string
  /** 存储路径 */
  filePath: string
  /** 字节大小 */
  fileSize: number
  /** MIME 类型（可选） */
  fileType?: string
  /** 扩展名（可选） */
  fileExtension?: string
  /** 哈希（可选） */
  fileHash?: string
  /** 文件分类（可选） */
  fileCategory?: number
  /** 访问 URL（可选） */
  accessUrl?: string
  /** 排序号 */
  orderNum: number
}

// 重新导出 enum 类型，方便使用
export type { TaktResultCode } from '@/utils/enum'
