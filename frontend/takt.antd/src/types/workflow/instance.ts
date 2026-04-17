import type { WfS } from '@/types/internal/openapi-pick'

export type FlowStartRequest = WfS<'Takt.Application.Dtos.Workflow.TaktFlowStartDto'>
export type FlowStartResult = WfS<'Takt.Application.Dtos.Workflow.TaktFlowStartResultDto'>
export type FlowInstance = WfS<'Takt.Application.Dtos.Workflow.TaktFlowInstanceDto'>
export type FlowInstanceDetail = WfS<'Takt.Application.Dtos.Workflow.TaktFlowInstanceDetailDto'>
export type FlowInstanceQuery = WfS<'Takt.Application.Dtos.Workflow.TaktFlowInstanceQueryDto'>
export type FlowTodoItem = WfS<'Takt.Application.Dtos.Workflow.TaktFlowTodoItemDto'>
/** 待办列表查询参数在 swagger 中为匿名 query，占位。 */
export type FlowTodoQuery = Record<string, unknown>
export type FlowCompleteRequest = WfS<'Takt.Application.Dtos.Workflow.TaktFlowCompleteDto'>
export type FlowOperateBase = Pick<
  WfS<'Takt.WebApi.Controllers.Workflow.TaktFlowInstancesController+RevokeRequest'>,
  'instanceCode' | 'flowInstanceId'
>
export type FlowInstanceUpdate = WfS<'Takt.Application.Dtos.Workflow.TaktFlowInstanceUpdateDto'>
export type FlowUndoVerification = WfS<'Takt.Application.Dtos.Workflow.TaktFlowUndoVerificationDto'>
export type FlowSuspend = WfS<'Takt.Application.Dtos.Workflow.TaktFlowSuspendDto'>
export type FlowResume = WfS<'Takt.Application.Dtos.Workflow.TaktFlowResumeDto'>
export type FlowTerminate = WfS<'Takt.Application.Dtos.Workflow.TaktFlowTerminateDto'>
export type FlowTransfer = WfS<'Takt.Application.Dtos.Workflow.TaktFlowTransferDto'>
export type FlowAddApprovers = WfS<'Takt.Application.Dtos.Workflow.TaktFlowAddApproversDto'>
export type FlowAddApproverItem = WfS<'Takt.Application.Dtos.Workflow.TaktFlowAddApproverItemDto'>
export type FlowReduceApproval = WfS<'Takt.Application.Dtos.Workflow.TaktFlowReduceApprovalDto'>
export type FlowOperationHistoryItem = WfS<'Takt.Application.Dtos.Workflow.TaktFlowOperationHistoryItemDto'>
