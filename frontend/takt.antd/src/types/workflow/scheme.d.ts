// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/workflow/scheme
// 文件名称：scheme.d.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：流程方案相关类型定义，对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 流程方案类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeDto）
 */
export interface FlowScheme extends TaktEntityBase {
  /** 方案ID（对应后端 SchemeId，序列化为string以避免Javascript精度问题） */
  schemeId: string
  /** 流程Key */
  processKey: string
  /** 流程名称 */
  processName: string
  /** 流程分类（0=通用流程，1=业务流程，2=系统流程） */
  processCategory: number
  /** 流程版本号 */
  processVersion: number
  /** 流程描述 */
  processDescription?: string
  /** 流程表单ID（对应后端 FormId，序列化为string以避免Javascript精度问题） */
  formId?: string
  /** 流程表单编码 */
  formCode?: string
  /** BPMN流程定义（XML） */
  bpmnXml?: string
  /** 流程JSON定义 */
  processJson?: string
  /** 流程图标 */
  processIcon?: string
  /** 是否支持挂起（0=否，1=是） */
  isSuspendable: number
  /** 是否支持撤回（0=否，1=是） */
  isRevocable: number
  /** 是否支持转办（0=否，1=是） */
  isTransferable: number
  /** 是否支持加签（0=否，1=是） */
  isAddsignable: number
  /** 是否支持减签（0=否，1=是） */
  isReduceSignable: number
  /** 是否支持退回（0=否，1=是） */
  isReturnable: number
  /** 是否自动完成（0=否，1=是） */
  isAutoComplete: number
  /** 超时配置 */
  timeoutConfig?: string
  /** 通知配置 */
  notificationConfig?: string
  /** 排序号 */
  orderNum: number
  /** 流程状态（0=草稿，1=已发布，2=已停用） */
  processStatus: number
}

/**
 * 流程方案查询类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeQueryDto）
 */
export interface FlowSchemeQuery extends TaktPagedQuery {
  /** 关键词（在流程Key、流程名称中模糊查询） */
  key?: string
  /** 流程Key */
  processKey?: string
  /** 流程状态（0=草稿，1=已发布，2=已停用） */
  processStatus?: number
  /** 流程分类（0=通用，1=业务，2=系统） */
  processCategory?: number
}

/**
 * 创建流程方案类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeCreateDto）
 */
export interface FlowSchemeCreate {
  /** 流程Key */
  processKey: string
  /** 流程名称 */
  processName: string
  /** 流程分类（0=通用流程，1=业务流程，2=系统流程） */
  processCategory?: number
  /** 流程版本号 */
  processVersion?: number
  /** 流程描述 */
  processDescription?: string
  /** 流程表单ID（对应后端 FormId，序列化为string以避免Javascript精度问题） */
  formId?: string
  /** 流程表单编码 */
  formCode?: string
  /** BPMN流程定义（XML） */
  bpmnXml?: string
  /** 流程JSON定义 */
  processJson?: string
  /** 流程图标 */
  processIcon?: string
  /** 是否支持挂起（0=否，1=是） */
  isSuspendable?: number
  /** 是否支持撤回（0=否，1=是） */
  isRevocable?: number
  /** 是否支持转办（0=否，1=是） */
  isTransferable?: number
  /** 是否支持加签（0=否，1=是） */
  isAddsignable?: number
  /** 是否支持减签（0=否，1=是） */
  isReduceSignable?: number
  /** 是否支持退回（0=否，1=是） */
  isReturnable?: number
  /** 是否自动完成（0=否，1=是） */
  isAutoComplete?: number
  /** 超时配置 */
  timeoutConfig?: string
  /** 通知配置 */
  notificationConfig?: string
  /** 排序号 */
  orderNum?: number
  /** 流程状态（0=草稿，1=已发布，2=已停用） */
  processStatus?: number
  /** 备注 */
  remark?: string
}

/**
 * 更新流程方案类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeUpdateDto）
 */
export interface FlowSchemeUpdate extends FlowSchemeCreate {
  /** 方案ID（对应后端 SchemeId，序列化为string以避免Javascript精度问题） */
  schemeId: string
}

/**
 * 流程方案状态类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeStatusDto）
 */
export interface FlowSchemeStatus {
  /** 方案ID（对应后端 SchemeId，序列化为string以避免Javascript精度问题） */
  schemeId: string
  /** 流程状态（0=草稿，1=已发布，2=已停用） */
  processStatus: number
}
