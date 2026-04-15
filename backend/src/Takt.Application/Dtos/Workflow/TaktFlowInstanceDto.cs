// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Workflow
// 文件名称：TaktFlowInstanceDto.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：流程实例、待办、办结、轨迹 DTO；类顺序与接口一致：列表/查询→详情→创建(Start)→更新→删除→导出，其它随后
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos;
using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Workflow;

/// <summary>
/// 流程实例 DTO
/// </summary>
public class TaktFlowInstanceDto : TaktDtoBase
{
    /// <summary>
    /// 实例ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 实例编码
    /// </summary>
    public string InstanceCode { get; set; } = string.Empty;

    /// <summary>
    /// 流程Key
    /// </summary>
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 流程方案ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SchemeId { get; set; }

    /// <summary>
    /// 业务主键
    /// </summary>
    public string? BusinessKey { get; set; }

    /// <summary>
    /// 业务类型
    /// </summary>
    public string? BusinessType { get; set; }

    /// <summary>
    /// 启动人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long StartUserId { get; set; }

    /// <summary>
    /// 启动人姓名
    /// </summary>
    public string StartUserName { get; set; } = string.Empty;

    /// <summary>
    /// 启动部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? StartDeptId { get; set; }

    /// <summary>
    /// 启动部门名称
    /// </summary>
    public string? StartDeptName { get; set; }

    /// <summary>
    /// 启动时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 当前节点ID
    /// </summary>
    public string? CurrentNodeId { get; set; }

    /// <summary>
    /// 当前节点名称
    /// </summary>
    public string? CurrentNodeName { get; set; }

    /// <summary>
    /// 当前节点展示名称（活动名称）
    /// </summary>
    public string? ActivityName { get; set; }

    /// <summary>
    /// 上一节点ID
    /// </summary>
    public string? PreviousNodeId { get; set; }

    /// <summary>
    /// 当前节点执行人ID列表（逗号分隔）
    /// </summary>
    public string? MakerList { get; set; }

    /// <summary>
    /// 表单数据（JSON）
    /// </summary>
    public string? FrmData { get; set; }

    /// <summary>
    /// 实例状态（0=运行中，1=已完成，2=已终止，3=已挂起，4=已撤回，5=草稿）
    /// </summary>
    public int InstanceStatus { get; set; }

    /// <summary>
    /// 是否挂起（0=否，1=是）
    /// </summary>
    public int IsSuspended { get; set; }

    /// <summary>
    /// 挂起时间
    /// </summary>
    public DateTime? SuspendTime { get; set; }

    /// <summary>
    /// 挂起原因
    /// </summary>
    public string? SuspendReason { get; set; }

    /// <summary>
    /// 优先级（0=低，1=中，2=高，3=紧急）
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// 流程标题
    /// </summary>
    public string? ProcessTitle { get; set; }

    /// <summary>
    /// 流程表单ID（启动时从方案快照，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FormId { get; set; }

    /// <summary>
    /// 流程表单编码（启动时从方案快照）
    /// </summary>
    public string? FormCode { get; set; }
}

/// <summary>
/// 流程实例查询 DTO
/// </summary>
public class TaktFlowInstanceQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 流程Key
    /// </summary>
    public string? ProcessKey { get; set; }

    /// <summary>
    /// 实例编码
    /// </summary>
    public string? InstanceCode { get; set; }

    /// <summary>
    /// 实例状态
    /// </summary>
    public int? InstanceStatus { get; set; }

    /// <summary>
    /// 仅查我发起的
    /// </summary>
    public bool? MyStartedOnly { get; set; }
}

/// <summary>
/// 流程实例详情（含轨迹）
/// </summary>
public class TaktFlowInstanceDetailDto : TaktFlowInstanceDto
{
    /// <summary>
    /// 流转历史
    /// </summary>
    public List<TaktFlowHistoryItemDto> History { get; set; } = new();

    /// <summary>
    /// 当前用户是否可审批
    /// </summary>
    public bool CanVerify { get; set; }

    /// <summary>
    /// 当前用户是否可撤回
    /// </summary>
    public bool CanUndoVerify { get; set; }

    /// <summary>
    /// 本实例下尚未处理（Status=0）的加签记录，供前端展示并调用减签接口；已审批的加签不在此列表。
    /// </summary>
    public List<TaktFlowAddApproverDto> PendingAddApprovers { get; set; } = new();
}

/// <summary>
/// 启动流程 DTO
/// </summary>
public class TaktFlowStartDto
{
    /// <summary>
    /// 流程Key
    /// </summary>
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 业务主键
    /// </summary>
    public string? BusinessKey { get; set; }

    /// <summary>
    /// 业务类型
    /// </summary>
    public string? BusinessType { get; set; }

    /// <summary>
    /// 流程标题
    /// </summary>
    public string? ProcessTitle { get; set; }

    /// <summary>
    /// 表单数据（JSON）
    /// </summary>
    public string? FrmData { get; set; }

    /// <summary>
    /// 草稿实例ID（若传入且该实例为草稿，则从草稿启动而非新建）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
}

/// <summary>
/// 启动流程结果 DTO
/// </summary>
public class TaktFlowStartResultDto
{
    /// <summary>
    /// 实例编码
    /// </summary>
    public string InstanceCode { get; set; } = string.Empty;

    /// <summary>
    /// 实例ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 流程Key
    /// </summary>
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;
}

/// <summary>
/// 流程实例更新 DTO（仅运行中且发起人可更新流程标题与表单数据）
/// </summary>
public class TaktFlowInstanceUpdateDto
{
    /// <summary>
    /// 实例ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long Id { get; set; }

    /// <summary>
    /// 流程标题
    /// </summary>
    public string? ProcessTitle { get; set; }

    /// <summary>
    /// 表单数据（JSON）
    /// </summary>
    public string? FrmData { get; set; }
}

/// <summary>
/// 流程实例批量删除请求 DTO
/// </summary>
public class TaktFlowInstanceDeleteDto
{
    /// <summary>
    /// 要删除的实例ID列表
    /// </summary>
    public List<long> Ids { get; set; } = new();
}

/// <summary>
/// 流程实例导出 DTO
/// </summary>
public class TaktFlowInstanceExportDto
{
    /// <summary>
    /// 实例编码
    /// </summary>
    public string InstanceCode { get; set; } = string.Empty;

    /// <summary>
    /// 流程Key
    /// </summary>
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 业务主键
    /// </summary>
    public string? BusinessKey { get; set; }

    /// <summary>
    /// 业务类型
    /// </summary>
    public string? BusinessType { get; set; }

    /// <summary>
    /// 启动人姓名
    /// </summary>
    public string StartUserName { get; set; } = string.Empty;

    /// <summary>
    /// 启动时间
    /// </summary>
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 实例状态（0=运行中，1=已完成，2=已终止，3=已挂起，4=已撤回）
    /// </summary>
    public int InstanceStatus { get; set; }

    /// <summary>
    /// 流程标题
    /// </summary>
    public string? ProcessTitle { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

// ========== 其它（操作历史、撤销、待办、办结、流程操作等） ==========

/// <summary>
/// 流程实例操作历史项
/// </summary>
public class TaktFlowOperationHistoryItemDto
{
    /// <summary>
    /// 操作内容
    /// </summary>
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 操作人ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CreateUserId { get; set; }

    /// <summary>
    /// 操作人姓名
    /// </summary>
    public string CreateUserName { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}

/// <summary>
/// 撤销审批请求 DTO
/// </summary>
public class TaktFlowUndoVerificationDto
{
    /// <summary>
    /// 流程实例ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowInstanceId { get; set; }
}

/// <summary>
/// 待办项 DTO（按实例维度）
/// </summary>
public class TaktFlowTodoItemDto
{
    /// <summary>
    /// 实例ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 实例编码
    /// </summary>
    public string InstanceCode { get; set; } = string.Empty;

    /// <summary>
    /// 流程Key
    /// </summary>
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 节点ID
    /// </summary>
    public string NodeId { get; set; } = string.Empty;

    /// <summary>
    /// 节点名称
    /// </summary>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// 流程标题
    /// </summary>
    public string? ProcessTitle { get; set; }

    /// <summary>
    /// 启动人姓名
    /// </summary>
    public string StartUserName { get; set; } = string.Empty;

    /// <summary>
    /// 启动时间
    /// </summary>
    public DateTime StartTime { get; set; }
}

/// <summary>
/// 待办查询 DTO
/// </summary>
public class TaktFlowTodoQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 流程Key
    /// </summary>
    public string? ProcessKey { get; set; }
}

/// <summary>
/// 办结 DTO（按实例标识）
/// </summary>
public class TaktFlowCompleteDto
{
    /// <summary>
    /// 流程实例ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

    /// <summary>
    /// 实例编码
    /// </summary>
    public string? InstanceCode { get; set; }

    /// <summary>
    /// 审批意见
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// 是否通过
    /// </summary>
    public bool Approved { get; set; } = true;

    /// <summary>
    /// 表单数据（JSON）
    /// </summary>
    public string? FrmData { get; set; }

    /// <summary>
    /// 驳回时指定退回节点
    /// </summary>
    public string? NodeRejectStep { get; set; }

    /// <summary>
    /// 下一节点为“发起人自选”时，由发起人/审批人选择的审批人 ID 列表（逗号分隔）。
    /// </summary>
    public string? SelectedAssigneeIds { get; set; }
}

/// <summary>
/// 流程实例操作 DTO 基类（实例标识二选一）
/// </summary>
public class TaktFlowInstanceOperateDto
{
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }
    public string? InstanceCode { get; set; }
}

/// <summary>
/// 挂起流程 DTO
/// </summary>
public class TaktFlowSuspendDto : TaktFlowInstanceOperateDto
{
    /// <summary>挂起原因</summary>
    public string? Reason { get; set; }
}

/// <summary>
/// 恢复流程 DTO
/// </summary>
public class TaktFlowResumeDto : TaktFlowInstanceOperateDto { }

/// <summary>
/// 终止流程 DTO
/// </summary>
public class TaktFlowTerminateDto : TaktFlowInstanceOperateDto
{
    /// <summary>终止原因</summary>
    public string? Reason { get; set; }
}

/// <summary>
/// 转办 DTO（将当前待办转给他人）
/// </summary>
public class TaktFlowTransferDto : TaktFlowInstanceOperateDto
{
    /// <summary>转办目标用户ID</summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ToUserId { get; set; }
    /// <summary>转办目标用户姓名</summary>
    public string ToUserName { get; set; } = string.Empty;
    /// <summary>转办意见</summary>
    public string? Comment { get; set; }
}

/// <summary>
/// 加签项（单个审批人）
/// </summary>
public class TaktFlowAddApproverItemDto
{
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApproverUserId { get; set; }
    public string ApproverUserName { get; set; } = string.Empty;
    /// <summary>顺序号（顺序审批时有效）</summary>
    public int? OrderNo { get; set; }
}

/// <summary>
/// 加签 DTO（当前节点临时增加审批人）
/// </summary>
public class TaktFlowAddApproversDto : TaktFlowInstanceOperateDto
{
    /// <summary>加签审批人列表</summary>
    public List<TaktFlowAddApproverItemDto> Approvers { get; set; } = new();
    /// <summary>审批类型：sequential=顺序，all=并行且，one=并行或</summary>
    public string ApproveType { get; set; } = "sequential";
    /// <summary>加签原因</summary>
    public string? Reason { get; set; }
    /// <summary>加签完成后是否回到原节点再审批</summary>
    public bool ReturnToSignNode { get; set; }
}

/// <summary>
/// 减签（Reduce Approval）DTO，用于移除已加的某条加签记录。
/// </summary>
public class TaktFlowReduceApprovalDto : TaktFlowInstanceOperateDto
{
    /// <summary>加签记录 ID（指定要移除的加签）。</summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AddApproverId { get; set; }
}

/// <summary>
/// 流程流转历史项 DTO
/// </summary>
public class TaktFlowHistoryItemDto
{
    /// <summary>
    /// 源节点名称
    /// </summary>
    public string FromNodeName { get; set; } = string.Empty;

    /// <summary>
    /// 目标节点名称
    /// </summary>
    public string ToNodeName { get; set; } = string.Empty;

    /// <summary>
    /// 流转人姓名
    /// </summary>
    public string TransitionUserName { get; set; } = string.Empty;

    /// <summary>
    /// 流转时间
    /// </summary>
    public DateTime TransitionTime { get; set; }

    /// <summary>
    /// 流转意见
    /// </summary>
    public string? TransitionComment { get; set; }
}

/// <summary>
/// 流程实例列表查询 DTO（my=我发起的，todo=待办，processed=已办）
/// </summary>
public class TaktFlowInstanceListQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 流程Key
    /// </summary>
    public string? ProcessKey { get; set; }

    /// <summary>
    /// 实例编码
    /// </summary>
    public string? InstanceCode { get; set; }

    /// <summary>
    /// 实例状态
    /// </summary>
    public int? InstanceStatus { get; set; }

    /// <summary>
    /// 列表类型：my/todo/processed
    /// </summary>
    public string? Type { get; set; }
}

// ========== 工作流与 CCFLOW 对照验证（多场景报告） ==========

/// <summary>
/// 单条验证场景结果（当前工作流与 CCFLOW 能力对照的单项验证）
/// </summary>
public class TaktFlowVerifyScenarioResultDto
{
    /// <summary>场景名称（与 CCFLOW 能力项对应）</summary>
    public string ScenarioName { get; set; } = string.Empty;

    /// <summary>是否通过</summary>
    public bool Ok { get; set; }

    /// <summary>失败或说明信息</summary>
    public string? Message { get; set; }

    /// <summary>执行步骤记录</summary>
    public List<string> Steps { get; set; } = new();
}

/// <summary>
/// 工作流与 CCFLOW 对照验证报告（多场景汇总）
/// </summary>
public class TaktFlowVerifyCcflowReportDto
{
    /// <summary>被验证的流程 Key</summary>
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>验证时间（本地）</summary>
    public DateTime VerifyTime { get; set; } = DateTime.Now;

    /// <summary>各场景验证结果</summary>
    public List<TaktFlowVerifyScenarioResultDto> Scenarios { get; set; } = new();

    /// <summary>是否全部通过</summary>
    public bool AllPassed => Scenarios.Count > 0 && Scenarios.All(s => s.Ok);
}
