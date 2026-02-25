// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：@/types/workflow/instance
// 文件名称：instance.d.ts
// 创建时间：2025-02-17
// 创建人：Takt365(Cursor AI)
// 功能描述：流程实例相关类型定义，对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceDtos
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

import type { TaktEntityBase, TaktPagedQuery } from '@/types/common'

/**
 * 流程实例类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceDto）
 */
export interface FlowInstance extends TaktEntityBase {
  /** 实例ID（对应后端 InstanceId，序列化为string以避免Javascript精度问题） */
  instanceId: string
  /** 实例编码 */
  instanceCode: string
  /** 流程Key */
  processKey: string
  /** 流程方案ID（对应后端 SchemeId，序列化为string以避免Javascript精度问题） */
  schemeId: string
  /** 流程名称（来自方案，展示用） */
  processName: string
  /** 业务主键 */
  businessKey?: string
  /** 部门ID（对应后端 DeptId，序列化为string以避免Javascript精度问题） */
  deptId: string
  /** 当前节点ID（对应后端 CurrentNodeId，序列化为string以避免Javascript精度问题） */
  currentNodeId?: string
  /** 当前节点名称 */
  currentNodeName?: string
  /** 上一节点ID（对应后端 PreviousNodeId，序列化为string以避免Javascript精度问题） */
  previousNodeId?: string
  /** 实例状态（0=运行中，1=已完成，2=已终止，3=已挂起，4=已撤回） */
  instanceStatus: number
  /** 优先级（0=低，1=中，2=高，3=紧急） */
  priority: number
  /** 流程标题 */
  processTitle?: string
}

/**
 * 流程实例查询类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceQueryDto）
 */
export interface FlowInstanceQuery extends TaktPagedQuery {
  /** 流程Key */
  processKey?: string
  /** 业务主键 */
  businessKey?: string
  /** 实例状态（0=运行中，1=已完成，2=已终止，3=已挂起，4=已撤回） */
  instanceStatus?: number
  /** 发起人ID（对应后端 CreateId，序列化为string以避免Javascript精度问题） */
  createId?: string
}

/**
 * 创建流程实例类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceCreateDto）
 */
export interface FlowInstanceCreate {
  /** 流程Key（对应 TaktFlowScheme.ProcessKey） */
  processKey: string
  /** 业务主键 */
  businessKey?: string
  /** 业务类型（如 Announcement、Document） */
  businessType?: string
  /** 流程标题 */
  processTitle?: string
  /** 备注 */
  remark?: string
}

/**
 * 更新流程实例类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceUpdateDto）
 */
export interface FlowInstanceUpdate extends FlowInstanceCreate {
  /** 实例ID（对应后端 InstanceId，序列化为string以避免Javascript精度问题） */
  instanceId: string
  /** 优先级（0=低，1=中，2=高，3=紧急） */
  priority?: number
}

/**
 * 流程实例状态类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceStatusDto）
 */
export interface FlowInstanceStatus {
  /** 实例ID（对应后端 InstanceId，序列化为string以避免Javascript精度问题） */
  instanceId: string
  /** 实例状态（0=运行中，1=已完成，2=已终止，3=已挂起，4=已撤回） */
  instanceStatus: number
}

/**
 * 审批流程类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceApproveDto）
 */
export interface FlowInstanceApprove {
  /** 实例ID（对应后端 InstanceId，序列化为string以避免Javascript精度问题） */
  instanceId: string
  /** 是否通过（true=通过，false=驳回） */
  pass: boolean
  /** 审批意见 */
  comment?: string
}

/**
 * 撤回流程类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceRecallDto）
 */
export interface FlowInstanceRecall {
  /** 实例ID（对应后端 InstanceId，序列化为string以避免Javascript精度问题） */
  instanceId: string
  /** 撤回说明 */
  comment?: string
}

/**
 * 流转历史类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceHistoryDto）
 */
export interface FlowInstanceHistory {
  /** 历史记录ID（对应后端 Id，序列化为string以避免Javascript精度问题） */
  id: string
  /** 流程实例ID（对应后端 InstanceId，序列化为string以避免Javascript精度问题） */
  instanceId: string
  /** 流程实例编码 */
  instanceCode: string
  /** 流程Key */
  processKey: string
  /** 流程名称 */
  processName: string
  /** 源节点ID */
  fromNodeId?: string
  /** 源节点名称 */
  fromNodeName?: string
  /** 源节点类型（如 2=审批节点，3=开始节点） */
  fromNodeType?: string
  /** 目标节点ID */
  toNodeId: string
  /** 目标节点名称 */
  toNodeName: string
  /** 目标节点类型（如 2=审批节点，4=结束节点） */
  toNodeType?: string
  /** 该次流转后是否已结束（0=否，1=是） */
  isFinish: number
  /** 流转类型（0=正常流转，1=退回，2=转办，3=加签，4=减签，5=撤回） */
  transitionType: number
  /** 流转时间 */
  transitionTime: string
  /** 流转人ID（对应后端 TransitionUserId，序列化为string以避免Javascript精度问题） */
  transitionUserId: string
  /** 流转人姓名 */
  transitionUserName: string
  /** 流转意见 */
  transitionComment?: string
}

/**
 * 流转历史查询类型（对应后端 Takt.Application.Dtos.Workflow.TaktFlowInstanceHistoryQueryDto）
 */
export interface FlowInstanceHistoryQuery extends TaktPagedQuery {
  /** 流程实例ID（对应后端 InstanceId，序列化为string以避免Javascript精度问题） */
  instanceId: string
}
