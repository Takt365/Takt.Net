// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/workflow/form
// 文件名称：form.d.ts
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：流程表单类型定义，对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormDto 等
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

/**
 * 流程表单类型定义
 * 对应后端 TaktFlowFormDto/TaktFlowFormQueryDto/TaktFlowFormCreateDto/
 * TaktFlowFormUpdateDto/TaktFlowFormStatusDto 等
 */

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 流程表单（对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormDto）
 */
export interface FlowForm extends TaktEntityBase {
  /** 表单ID（对应后端 Id/FormId，后端 long，前端用 string 以避免精度问题） */
  formId: string
  /** 表单编码 */
  formCode: string
  /** 表单名称 */
  formName: string
  /** 表单分类（0=通用表单，1=业务表单，2=系统表单） */
  formCategory: number
  /** 表单类型（0=动态表单，1=静态表单，2=自定义表单） */
  formType: number
  /** 表单配置（JSON） */
  formConfig?: string
  /** 表单模板 */
  formTemplate?: string
  /** 表单版本号 */
  formVersion: string
  /** 是否启用数据源（0=否，1=是）。启用时由 relatedDataBaseName 指定关联表。 */
  isDatasource: number
  /** 关联数据库名（从 appsettings.dbConfigs 的 Conn 解析，如 Takt_Identity_Dev），开发/生产环境不同 */
  relatedDataBaseName?: string
  /** 关联表名：通过 relatedDataBaseName 选中的表名 */
  relatedTableName?: string
  /** 关联表单字段（JSON 数组）：通过 relatedTableName 选中的、要显示在表单中的列名 */
  relatedFormField?: string
  /** 排序号 */
  orderNum: number
  /** 表单状态（0=草稿，1=已发布，2=已停用） */
  formStatus: number
}

/**
 * 流程表单查询（对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormQueryDto）
 */
export interface FlowFormQuery extends TaktPagedQuery {
  /** 表单编码 */
  formCode?: string
  /** 表单名称 */
  formName?: string
  /** 表单分类（0=通用表单，1=业务表单，2=系统表单） */
  formCategory?: number
  /** 表单状态（0=草稿，1=已发布，2=已停用） */
  formStatus?: number
}

/**
 * 创建流程表单请求（对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormCreateDto）
 */
export interface FlowFormCreate {
  /** 表单编码 */
  formCode: string
  /** 表单名称 */
  formName: string
  /** 表单分类（0=通用表单，1=业务表单，2=系统表单） */
  formCategory: number
  /** 表单类型（0=动态表单，1=静态表单，2=自定义表单） */
  formType: number
  /** 表单配置（JSON） */
  formConfig?: string
  /** 表单模板 */
  formTemplate?: string
  /** 表单版本号 */
  formVersion: string
  /** 是否启用数据源（0=否，1=是） */
  isDatasource: number
  /** 关联数据库名（从 appsettings.dbConfigs 的 Conn 解析，如 Takt_Identity_Dev），开发/生产环境不同 */
  relatedDataBaseName?: string
  /** 关联表名：通过 relatedDataBaseName 选中的表名 */
  relatedTableName?: string
  /** 关联表单字段（JSON 数组）：通过 relatedTableName 选中的、要显示在表单中的列名 */
  relatedFormField?: string
  /** 排序号 */
  orderNum: number
  /** 表单状态（0=草稿，1=已发布，2=已停用） */
  formStatus: number
}

/**
 * 更新流程表单请求（对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormUpdateDto）
 */
export interface FlowFormUpdate extends FlowFormCreate {
  /** 表单ID（对应后端 FormId，后端 long，前端 string） */
  formId: string
}

/**
 * 更新流程表单状态请求（对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormStatusDto）
 */
export interface FlowFormStatusUpdate {
  /** 表单ID（对应后端 FormId，后端 long，前端 string） */
  formId: string
  /** 表单状态（0=草稿，1=已发布，2=已停用） */
  formStatus: number
  /** 备注 */
  remark?: string
}

