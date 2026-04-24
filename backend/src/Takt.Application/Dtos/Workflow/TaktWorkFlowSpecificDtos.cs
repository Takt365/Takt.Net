// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Workflow
// 文件名称：TaktWorkFlowSpecificDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：工作流业务特定DTO集合，包含流程启动、审批、待办等业务DTO
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Workflow;

/// <summary>
/// Takt流程启动DTO
/// </summary>
public class TaktFlowStartDto
{
    /// <summary>
    /// 流程实例ID（从草稿启动时传入）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? FlowInstanceId { get; set; }

    /// <summary>
    /// 流程Key
    /// </summary>
    public string SchemeKey { get; set; } = string.Empty;

    /// <summary>
    /// 业务键（可选，用于关联业务数据）
    /// </summary>
    public string? BusinessKey { get; set; }

    /// <summary>
    /// 业务类型（可选，用于关联业务数据）
    /// </summary>
    public string? BusinessType { get; set; }

    /// <summary>
    /// 流程表单数据JSON
    /// </summary>
    public string? FrmData { get; set; }

    /// <summary>
    /// 流程标题
    /// </summary>
    public string? ProcessTitle { get; set; }
}

/// <summary>
/// Takt流程启动结果DTO
/// </summary>
public class TaktFlowStartResultDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowStartResultDto()
    {
        InstanceCode = string.Empty;
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
    }

    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FlowInstanceId { get; set; }

    /// <summary>
    /// 流程实例ID（与FlowInstanceId相同，用于兼容）
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 流程实例编码
    /// </summary>
    public string InstanceCode { get; set; } = string.Empty;

    /// <summary>
    /// 流程Key
    /// </summary>
    public string SchemeKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程名称
    /// </summary>
    public string SchemeName { get; set; } = string.Empty;
}

/// <summary>
/// Takt流程完成DTO
/// </summary>
public partial class TaktFlowCompleteDto
{
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FlowInstanceId { get; set; }

    /// <summary>
    /// 审批意见
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// 表单数据JSON
    /// </summary>
    public string? FrmData { get; set; }
}

/// <summary>
/// Takt流程挂起DTO
/// </summary>
public partial class TaktFlowSuspendDto
{
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FlowInstanceId { get; set; }

    /// <summary>
    /// 挂起原因
    /// </summary>
    public string? Reason { get; set; }
}

/// <summary>
/// Takt流程恢复DTO
/// </summary>
public partial class TaktFlowResumeDto
{
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FlowInstanceId { get; set; }
}

/// <summary>
/// Takt流程终止DTO
/// </summary>
public partial class TaktFlowTerminateDto
{
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FlowInstanceId { get; set; }

    /// <summary>
    /// 终止原因
    /// </summary>
    public string? Reason { get; set; }
}

/// <summary>
/// Takt流程转办DTO
/// </summary>
public partial class TaktFlowTransferDto
{
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FlowInstanceId { get; set; }

    /// <summary>
    /// 转办给用户ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long TargetUserId { get; set; }

    /// <summary>
    /// 转办原因
    /// </summary>
    public string? Reason { get; set; }
}

/// <summary>
/// Takt流程加签DTO
/// </summary>
public partial class TaktFlowAddApproversDto
{
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FlowInstanceId { get; set; }

    /// <summary>
    /// 加签用户ID列表
    /// </summary>
    public List<long> UserIds { get; set; } = new();

    /// <summary>
    /// 加签类型（Before=前加签，After=后加签）
    /// </summary>
    public string AddSignType { get; set; } = "After";
}

/// <summary>
/// Takt流程减签DTO
/// </summary>
public partial class TaktFlowReduceApprovalDto
{
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FlowInstanceId { get; set; }

    /// <summary>
    /// 操作记录ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long OperationId { get; set; }
}

/// <summary>
/// Takt流程撤销验证DTO
/// </summary>
public class TaktFlowUndoVerificationDto
{
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FlowInstanceId { get; set; }

    /// <summary>
    /// 操作记录ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long OperationId { get; set; }
}

/// <summary>
/// Takt流程待办项DTO
/// </summary>
public partial class TaktFlowTodoItemDto
{
    /// <summary>
    /// 操作记录ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long OperationId { get; set; }

    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FlowInstanceId { get; set; }

    /// <summary>
    /// 流程实例编码
    /// </summary>
    public string InstanceCode { get; set; } = string.Empty;

    /// <summary>
    /// 流程标题
    /// </summary>
    public string ProcessTitle { get; set; } = string.Empty;

    /// <summary>
    /// 流程Key
    /// </summary>
    public string SchemeKey { get; set; } = string.Empty;

    /// <summary>
    /// 节点名称
    /// </summary>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 发起人姓名
    /// </summary>
    public string? InitiatorName { get; set; }
}

/// <summary>
/// Takt流程待办查询DTO
/// </summary>
public class TaktFlowTodoQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 流程Key（可选）
    /// </summary>
    public string? SchemeKey { get; set; }

    /// <summary>
    /// 流程实例编码（可选）
    /// </summary>
    public string? InstanceCode { get; set; }
}

/// <summary>
/// Takt流程实例详情DTO
/// </summary>
public partial class TaktFlowInstanceDetailDto
{
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long FlowInstanceId { get; set; }

    /// <summary>
    /// 流程实例编码
    /// </summary>
    public string InstanceCode { get; set; } = string.Empty;

    /// <summary>
    /// 流程标题
    /// </summary>
    public string ProcessTitle { get; set; } = string.Empty;

    /// <summary>
    /// 流程Key
    /// </summary>
    public string SchemeKey { get; set; } = string.Empty;

    /// <summary>
    /// 流程名称
    /// </summary>
    public string SchemeName { get; set; } = string.Empty;

    /// <summary>
    /// 流程状态
    /// </summary>
    public int InstanceStatus { get; set; }

    /// <summary>
    /// 是否挂起（0=否，1=是）
    /// </summary>
    public int IsSuspended { get; set; }

    /// <summary>
    /// 当前节点名称
    /// </summary>
    public string? CurrentNodeName { get; set; }

    /// <summary>
    /// 发起人姓名
    /// </summary>
    public string? InitiatorName { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// 表单数据JSON
    /// </summary>
    public string? FrmData { get; set; }
}

/// <summary>
/// Takt流程操作历史项DTO
/// </summary>
public partial class TaktFlowOperationHistoryItemDto
{
    /// <summary>
    /// 操作记录ID
    /// </summary>
    [JsonConverter(typeof(ValueToStringConverter))]
    public long OperationId { get; set; }

    /// <summary>
    /// 节点名称
    /// </summary>
    public string NodeName { get; set; } = string.Empty;

    /// <summary>
    /// 操作类型（Approve=审批，Reject=驳回，Transfer=转办等）
    /// </summary>
    public string OperationType { get; set; } = string.Empty;

    /// <summary>
    /// 操作人姓名
    /// </summary>
    public string? OperatorName { get; set; }

    /// <summary>
    /// 审批意见
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperationTime { get; set; }
}

/// <summary>
/// Takt流程表单可绑定实体DTO
/// </summary>
public class TaktFlowFormBindableEntityDto
{
    /// <summary>
    /// 实体Key
    /// </summary>
    public string EntityKey { get; set; } = string.Empty;

    /// <summary>
    /// 显示名称
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;
}

// ========================================
// 工作流DTO业务扩展字段
// ========================================

/// <summary>
/// Takt流程表单状态DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktFlowFormStatusDto
{
    /// <summary>
    /// 表单ID（非数据库字段，适配字段）
    /// </summary>
    public long? FormId { get; set; }
    
    /// <summary>
    /// 备注（非数据库字段）
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt流程实例查询DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktFlowInstanceQueryDto
{
    /// <summary>
    /// 仅查询我发起的（非数据库字段）
    /// </summary>
    public bool? MyStartedOnly { get; set; }
}

/// <summary>
/// Takt流程实例更新DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktFlowInstanceUpdateDto
{
    /// <summary>
    /// ID（非数据库字段）
    /// </summary>
    public long? Id { get; set; }
}

/// <summary>
/// Takt流程操作历史项DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktFlowOperationHistoryItemDto
{
    /// <summary>
    /// 操作内容（非数据库字段）
    /// </summary>
    public string? Content { get; set; }
    
    /// <summary>
    /// 创建用户ID（非数据库字段）
    /// </summary>
    public long? CreateUserId { get; set; }
    
    /// <summary>
    /// 创建用户名称（非数据库字段）
    /// </summary>
    public string? CreateUserName { get; set; }
    
    /// <summary>
    /// 创建时间（非数据库字段）
    /// </summary>
    public DateTime? CreatedAt { get; set; }
}

/// <summary>
/// Takt流程待办项DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktFlowTodoItemDto
{
    /// <summary>
    /// 流程实例ID（非数据库字段）
    /// </summary>
    public long? InstanceId { get; set; }
    
    /// <summary>
    /// 流程名称（非数据库字段）
    /// </summary>
    public string? SchemeName { get; set; }
    
    /// <summary>
    /// 节点ID（非数据库字段）
    /// </summary>
    public long? NodeId { get; set; }
    
    /// <summary>
    /// 发起人姓名（非数据库字段）
    /// </summary>
    public string? StartUserName { get; set; }
    
    /// <summary>
    /// 发起时间（非数据库字段）
    /// </summary>
    public DateTime? StartTime { get; set; }
}

/// <summary>
/// Takt流程完成DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktFlowCompleteDto
{
    /// <summary>
    /// 流程实例编码（非数据库字段）
    /// </summary>
    public string? InstanceCode { get; set; }
    
    /// <summary>
    /// 是否批准（非数据库字段）
    /// </summary>
    public bool? Approved { get; set; }
    
    /// <summary>
    /// 驳回步数（非数据库字段）
    /// </summary>
    public int? NodeRejectStep { get; set; }
    
    /// <summary>
    /// 选定的受让人ID列表（非数据库字段）
    /// </summary>
    public List<long>? SelectedAssigneeIds { get; set; }
}

/// <summary>
/// Takt流程挂起DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktFlowSuspendDto
{
    /// <summary>
    /// 流程实例编码（非数据库字段）
    /// </summary>
    public string? InstanceCode { get; set; }
}

/// <summary>
/// Takt流程恢复DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktFlowResumeDto
{
    /// <summary>
    /// 流程实例编码（非数据库字段）
    /// </summary>
    public string? InstanceCode { get; set; }
}

/// <summary>
/// Takt流程终止DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktFlowTerminateDto
{
    /// <summary>
    /// 流程实例编码（非数据库字段）
    /// </summary>
    public string? InstanceCode { get; set; }
}

/// <summary>
/// Takt流程转办DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktFlowTransferDto
{
    /// <summary>
    /// 流程实例编码（非数据库字段）
    /// </summary>
    public string? InstanceCode { get; set; }
    
    /// <summary>
    /// 目标用户ID（非数据库字段）
    /// </summary>
    public long? ToUserId { get; set; }
    
    /// <summary>
    /// 目标用户名称（非数据库字段）
    /// </summary>
    public string? ToUserName { get; set; }
    
    /// <summary>
    /// 审批意见（非数据库字段）
    /// </summary>
    public string? Comment { get; set; }
}

/// <summary>
/// Takt流程加签DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktFlowAddApproversDto
{
    /// <summary>
    /// 流程实例编码（非数据库字段）
    /// </summary>
    public string? InstanceCode { get; set; }
    
    /// <summary>
    /// 加签人列表（非数据库字段）
    /// </summary>
    public List<long>? Approvers { get; set; }
    
    /// <summary>
    /// 审批类型（非数据库字段）
    /// </summary>
    public int? ApproveType { get; set; }
    
    /// <summary>
    /// 原因（非数据库字段）
    /// </summary>
    public string? Reason { get; set; }
    
    /// <summary>
    /// 是否返回前加签节点（非数据库字段）
    /// </summary>
    public bool? ReturnToSignNode { get; set; }
}

/// <summary>
/// Takt流程减签DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktFlowReduceApprovalDto
{
    /// <summary>
    /// 流程实例编码（非数据库字段）
    /// </summary>
    public string? InstanceCode { get; set; }
    
    /// <summary>
    /// 加签记录ID（非数据库字段）
    /// </summary>
    public long? AddApproverId { get; set; }
}

/// <summary>
/// Takt流程实例详情DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktFlowInstanceDetailDto
{
    /// <summary>
    /// 当前节点ID（非数据库字段）
    /// </summary>
    public long? CurrentNodeId { get; set; }
    
    /// <summary>
    /// 操作历史（非数据库字段）
    /// </summary>
    public List<TaktFlowOperationHistoryItemDto>? History { get; set; }
    
    /// <summary>
    /// 是否可验证（非数据库字段）
    /// </summary>
    public bool? CanVerify { get; set; }
    
    /// <summary>
    /// 是否可取消验证（非数据库字段）
    /// </summary>
    public bool? CanUndoVerify { get; set; }
    
    /// <summary>
    /// 待加签人列表（非数据库字段）
    /// </summary>
    public List<long>? PendingAddApprovers { get; set; }
}

/// <summary>
/// Takt流程加签记录DTO扩展（用于包含业务字段）
/// </summary>
public partial class TaktFlowAddApproverDto
{
    /// <summary>
    /// 加签记录ID（非数据库字段）
    /// </summary>
    public long? AddApproverId { get; set; }
}

/// <summary>
/// Takt流程历史项DTO（用于包含业务字段）
/// </summary>
public class TaktFlowHistoryItemDto
{
    /// <summary>
    /// 操作记录ID
    /// </summary>
    public long Id { get; set; }
    
    /// <summary>
    /// 来源节点名称
    /// </summary>
    public string? FromNodeName { get; set; }
    
    /// <summary>
    /// 目标节点名称
    /// </summary>
    public string? ToNodeName { get; set; }
    
    /// <summary>
    /// 转换操作人姓名
    /// </summary>
    public string? TransitionUserName { get; set; }
    
    /// <summary>
    /// 转换时间
    /// </summary>
    public DateTime? TransitionTime { get; set; }
    
    /// <summary>
    /// 转换备注
    /// </summary>
    public string? TransitionComment { get; set; }
    
    /// <summary>
    /// 节点名称
    /// </summary>
    public string NodeName { get; set; } = string.Empty;
    
    /// <summary>
    /// 操作类型
    /// </summary>
    public string OperationType { get; set; } = string.Empty;
    
    /// <summary>
    /// 操作人姓名
    /// </summary>
    public string? OperatorName { get; set; }
    
    /// <summary>
    /// 审批意见
    /// </summary>
    public string? Comment { get; set; }
    
    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime OperationTime { get; set; }
}

/// <summary>
/// Takt流程验证CCFLOW报告DTO
/// </summary>
public class TaktFlowVerifyCcflowReportDto
{
    /// <summary>
    /// 流程Key
    /// </summary>
    public string SchemeKey { get; set; } = string.Empty;

    /// <summary>
    /// 验证时间
    /// </summary>
    public DateTime VerifyTime { get; set; }

    /// <summary>
    /// 场景结果列表
    /// </summary>
    public List<TaktFlowVerifyScenarioResultDto> Scenarios { get; set; } = new();
}

/// <summary>
/// Takt流程验证场景结果DTO
/// </summary>
public class TaktFlowVerifyScenarioResultDto
{
    /// <summary>
    /// 场景名称
    /// </summary>
    public string ScenarioName { get; set; } = string.Empty;

    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Ok { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 步骤记录
    /// </summary>
    public List<string> Steps { get; set; } = new();
}
