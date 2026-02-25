// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/workflow/form
// 文件名称：form.d.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：流程表单相关类型定义，对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 流程表单类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormDto）
 */
export interface FlowForm extends TaktEntityBase {
  /** 表单ID（对应后端 FormId，序列化为string以避免Javascript精度问题） */
  formId: string
  /** 表单编码 */
  formCode: string
  /** 表单名称 */
  formName: string
  /** 表单分类（0=通用表单，1=业务表单，2=系统表单） */
  formCategory: number
  /** 表单类型（0=动态表单，1=静态表单） */
  formType: number
  /** 表单模板（HTML/JSON） */
  formTemplate?: string
  /** 字段个数 */
  fields?: number
  /** 表单控件位置模板 */
  contentParse?: string
  /** 表单版本号 */
  formVersion: string
  /** 数据源 */
  dataSource?: string
  /** 部门ID（对应后端 DeptId，序列化为string以避免Javascript精度问题） */
  deptId: string
  /** 排序号 */
  orderNum: number
  /** 表单状态（0=草稿，1=已发布，2=已停用） */
  formStatus: number
}

/**
 * 流程表单查询类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormQueryDto）
 */
export interface FlowFormQuery extends TaktPagedQuery {
  /** 关键词（在表单编码、表单名称中模糊查询） */
  key?: string
  /** 表单编码 */
  formCode?: string
  /** 表单状态（0=草稿，1=已发布，2=已停用） */
  formStatus?: number
  /** 表单分类（0=通用，1=业务，2=系统） */
  formCategory?: number
}

/**
 * 创建流程表单类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormCreateDto）
 */
export interface FlowFormCreate {
  /** 表单编码 */
  formCode: string
  /** 表单名称 */
  formName: string
  /** 表单分类（0=通用表单，1=业务表单，2=系统表单） */
  formCategory?: number
  /** 表单类型（0=动态表单，1=静态表单） */
  formType?: number
  /** 表单模板（HTML/JSON） */
  formTemplate?: string
  /** 字段个数 */
  fields?: number
  /** 表单控件位置模板 */
  contentParse?: string
  /** 表单版本号 */
  formVersion?: string
  /** 数据源 */
  dataSource?: string
  /** 部门ID（对应后端 DeptId，序列化为string以避免Javascript精度问题） */
  deptId?: string
  /** 排序号 */
  orderNum?: number
  /** 表单状态（0=草稿，1=已发布，2=已停用） */
  formStatus?: number
  /** 备注 */
  remark?: string
}

/**
 * 更新流程表单类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormUpdateDto）
 */
export interface FlowFormUpdate extends FlowFormCreate {
  /** 表单ID（对应后端 FormId，序列化为string以避免Javascript精度问题） */
  formId: string
}

/**
 * 流程表单状态类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowFormStatusDto）
 */
export interface FlowFormStatus {
  /** 表单ID（对应后端 FormId，序列化为string以避免Javascript精度问题） */
  formId: string
  /** 表单状态（0=草稿，1=已发布，2=已停用） */
  formStatus: number
}
