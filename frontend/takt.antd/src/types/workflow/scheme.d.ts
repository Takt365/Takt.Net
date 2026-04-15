// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/workflow/scheme
// 文件名称：scheme.d.ts
// 创建时间：2025-02-27
// 创建人：Takt365(Cursor AI)
// 功能描述：流程方案类型定义，对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeDto 等
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

/**
 * 流程方案类型定义
 * 对应后端 TaktFlowSchemeDto/TaktFlowSchemeQueryDto/TaktFlowSchemeCreateDto/
 * TaktFlowSchemeUpdateDto/TaktFlowSchemeStatusDto 等
 */

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 流程方案（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeDto）
 */
export interface FlowScheme extends TaktEntityBase {
  /** 方案ID（对应后端 Id/SchemeId，后端 long，前端用 string 以避免精度问题） */
  schemeId: string
  /** 流程Key */
  processKey: string
  /** 流程名称 */
  processName: string
  /** 流程分类 */
  processCategory: number
  /** 流程版本 */
  processVersion: number
  /** 流程描述 */
  processDescription?: string
  /** 表单ID */
  formId?: string
  /** 流程表单编码 */
  formCode?: string
  /** 流程内容（JSON，节点与连线配置） */
  processContent?: string
  /** 排序号 */
  orderNum: number
  /** 流程状态（0=草稿，1=已发布，2=已停用） */
  processStatus: number
}

/**
 * 流程方案查询（对应后端 Takt.Application.Dtos.Workflow.TaktFlowSchemeQueryDto）
 */
export interface FlowSchemeQuery extends TaktPagedQuery {
  /** 流程Key */
  processKey?: string
  /** 流程名称 */
  processName?: string
  /** 流程状态（0=草稿，1=已发布，2=已停用） */
  processStatus?: number
  /** 流程分类 */
  processCategory?: number
  /** 流程表单编码（筛选使用本表单的方案） */
  formCode?: string
}

/**
 * 流程方案创建/更新请求（对应后端 TaktFlowSchemeCreateDto / TaktFlowSchemeUpdateDto）
 */
export interface FlowSchemeCreateOrUpdate {
  /** 方案ID（创建时不传，更新时必传；对应后端 SchemeId） */
  schemeId?: string
  /** 流程Key */
  processKey: string
  /** 流程名称 */
  processName: string
  /** 流程分类 */
  processCategory: number
  /** 流程描述 */
  processDescription?: string
  /** 表单ID */
  formId?: string
  /** 流程表单编码 */
  formCode?: string
  /** 流程内容（JSON，节点与连线配置） */
  processContent?: string
  /** 排序号（越小越靠前） */
  orderNum: number
  /** 流程状态（0=草稿，1=已发布，2=已停用） */
  processStatus: number
}

/**
 * 流程方案状态更新请求（对应后端 TaktFlowSchemeStatusDto）
 */
export interface FlowSchemeStatusUpdate {
  /** 方案ID */
  schemeId: string
  /** 流程状态（0=草稿，1=已发布，2=已停用） */
  processStatus: number
  /** 备注 */
  remark?: string
}

