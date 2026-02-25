// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Workflow
// 文件名称：TaktFlowInstanceDtos.cs
// 创建时间：2025-02-18
// 创建人：Takt365(Cursor AI)
// 功能描述：工作流流程实例 DTO，与实体字段一致，含 Dto/QueryDto/CreateDto/UpdateDto/StatusDto/TemplateDto/ImportDto/ExportDto 及工作流操作 DTO
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Workflow;

/// <summary>
/// 流程实例 DTO（与 TaktFlowInstance 实体字段一致，列表/详情）
/// </summary>
public class TaktFlowInstanceDto
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
    /// 流程方案ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SchemeId { get; set; }

    /// <summary>
    /// 流程名称（来自方案，仅展示）
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 业务主键
    /// </summary>
    public string? BusinessKey { get; set; }

    /// <summary>
    /// 部门ID（0 表示未指定）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 当前节点ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CurrentNodeId { get; set; }

    /// <summary>
    /// 当前节点名称
    /// </summary>
    public string? CurrentNodeName { get; set; }

    /// <summary>
    /// 上一节点ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PreviousNodeId { get; set; }

    /// <summary>
    /// 实例状态（0=运行中，1=已完成，2=已终止，3=已挂起，4=已撤回）
    /// </summary>
    public int InstanceStatus { get; set; }

    /// <summary>
    /// 当前节点处理人ID（认领后写入；空表示未认领）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? AssigneeId { get; set; }

    /// <summary>
    /// 优先级（0=低，1=中，2=高，3=紧急）
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// 流程标题
    /// </summary>
    public string? ProcessTitle { get; set; }

    // ----- 审计字段（与 TaktEntityBase 一致，统一放在最后） -----

    /// <summary>
    /// 租户配置ID
    /// </summary>
    public string ConfigId { get; set; } = "0";

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CreateId { get; set; }

    /// <summary>
    /// 创建人（用户名）
    /// </summary>
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UpdateId { get; set; }

    /// <summary>
    /// 更新人（用户名）
    /// </summary>
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（软删除标记，0=未删除，1=已删除）
    /// </summary>
    public int IsDeleted { get; set; } = 0;

    /// <summary>
    /// 删除人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DeleteId { get; set; }

    /// <summary>
    /// 删除人（用户名）
    /// </summary>
    public string? DeletedBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    public DateTime? DeletedTime { get; set; }
}

/// <summary>
/// 流程实例分页查询 DTO
/// </summary>
public class TaktFlowInstanceQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 流程Key
    /// </summary>
    public string? ProcessKey { get; set; }

    /// <summary>
    /// 业务主键
    /// </summary>
    public string? BusinessKey { get; set; }

    /// <summary>
    /// 实例状态（0=运行中，1=已完成，2=已终止，3=已挂起，4=已撤回）
    /// </summary>
    public int? InstanceStatus { get; set; }

    /// <summary>
    /// 创建人ID（流程发起人）
    /// </summary>
    public long? CreateId { get; set; }

    /// <summary>
    /// 是否仅待办（true=仅查待我审批：AssigneeId 为空或为当前用户且运行中）
    /// </summary>
    public bool? TodoOnly { get; set; }
}

/// <summary>
/// 创建流程实例 DTO（创建后即处于运行中）
/// </summary>
public class TaktFlowInstanceCreateDto
{
    /// <summary>
    /// 流程Key（对应 TaktFlowScheme.ProcessKey，需在流程方案中已配置）
    /// </summary>
    public string ProcessKey { get; set; } = string.Empty;

    /// <summary>
    /// 业务主键（如公告ID、文档ID）
    /// </summary>
    public string? BusinessKey { get; set; }

    /// <summary>
    /// 业务类型（如 Announcement、Document，与 TaktFlowScheme.ProcessKey 对应）
    /// </summary>
    public string? BusinessType { get; set; }

    /// <summary>
    /// 流程标题（可选，用于显示）
    /// </summary>
    public string? ProcessTitle { get; set; }
}

/// <summary>
/// 更新流程实例 DTO（仅允许更新流程标题、优先级等有限字段，且仅运行中可更新）
/// </summary>
public class TaktFlowInstanceUpdateDto : TaktFlowInstanceCreateDto
{
    /// <summary>
    /// 实例ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 优先级（0=低，1=中，2=高，3=紧急）
    /// </summary>
    public int? Priority { get; set; }
}

/// <summary>
/// 启动流程 DTO（当前实现为创建即启动，此 DTO 保留兼容）
/// </summary>
public class TaktFlowInstanceStartDto
{
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }
}

/// <summary>
/// 流程实例审批 DTO（通过则完成，不通过则终止）
/// </summary>
public class TaktFlowInstanceApproveDto
{
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 当前节点ID（可选，简单流程可空）
    /// </summary>
    public string? NodeId { get; set; }

    /// <summary>
    /// 当前节点名称（可选）
    /// </summary>
    public string? NodeName { get; set; }

    /// <summary>
    /// 是否通过（true=通过/完成，false=驳回/终止）
    /// </summary>
    public bool Pass { get; set; }

    /// <summary>
    /// 审批意见
    /// </summary>
    public string? Comment { get; set; }
}

/// <summary>
/// 流程实例认领 DTO（将运行中且未认领的实例认领为当前用户处理）
/// </summary>
public class TaktFlowInstanceClaimDto
{
    /// <summary>流程实例ID</summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }
    /// <summary>说明</summary>
    public string? Comment { get; set; }
}

/// <summary>
/// 流程实例撤回 DTO
/// </summary>
public class TaktFlowInstanceRecallDto
{
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 撤回原因
    /// </summary>
    public string? Comment { get; set; }
}

/// <summary>
/// 流程实例节点指定/转办 DTO
/// </summary>
public class TaktFlowInstanceNodeDesignateDto
{
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 目标节点ID（指定流转到的节点）
    /// </summary>
    public string? ToNodeId { get; set; }

    /// <summary>
    /// 目标节点名称
    /// </summary>
    public string? ToNodeName { get; set; }

    /// <summary>
    /// 转办给用户ID（转办时必填，序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? DesignateUserId { get; set; }

    /// <summary>
    /// 转办说明/意见
    /// </summary>
    public string? Comment { get; set; }
}

/// <summary>
/// 流程实例历史查询 DTO（按实例ID查流转历史，支持分页）
/// </summary>
public class TaktFlowInstanceHistoryQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 流程实例ID（必填）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }
}

/// <summary>
/// 流程实例流转历史项 DTO（与 TaktFlowExecution 展示字段一致）
/// </summary>
public class TaktFlowInstanceHistoryDto
{
    /// <summary>
    /// 历史记录ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long Id { get; set; }

    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 流程实例编码
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
    /// 源节点ID
    /// </summary>
    public string? FromNodeId { get; set; }

    /// <summary>
    /// 源节点名称
    /// </summary>
    public string? FromNodeName { get; set; }

    /// <summary>
    /// 源节点类型（如 2=审批节点，3=开始节点）
    /// </summary>
    public string? FromNodeType { get; set; }

    /// <summary>
    /// 目标节点ID
    /// </summary>
    public string ToNodeId { get; set; } = string.Empty;

    /// <summary>
    /// 目标节点名称
    /// </summary>
    public string ToNodeName { get; set; } = string.Empty;

    /// <summary>
    /// 目标节点类型（如 2=审批节点，4=结束节点）
    /// </summary>
    public string? ToNodeType { get; set; }

    /// <summary>
    /// 是否已结束（0=否，1=是）
    /// </summary>
    public int IsFinish { get; set; }

    /// <summary>
    /// 流转类型（0=正常流转，1=退回，2=转办，3=加签，4=减签，5=撤回）
    /// </summary>
    public int TransitionType { get; set; }

    /// <summary>
    /// 流转时间
    /// </summary>
    public DateTime TransitionTime { get; set; }

    /// <summary>
    /// 流转人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TransitionUserId { get; set; }

    /// <summary>
    /// 流转人姓名
    /// </summary>
    public string TransitionUserName { get; set; } = string.Empty;

    /// <summary>
    /// 流转意见
    /// </summary>
    public string? TransitionComment { get; set; }
}

/// <summary>
/// 流程实例加签 DTO（在当前节点增加审批人/分支，需任务与候选人模型，暂未实现）
/// </summary>
public class TaktFlowInstanceAddSignDto
{
    /// <summary>流程实例ID</summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }
    /// <summary>加签用户ID列表</summary>
    public List<long>? UserIds { get; set; }
    /// <summary>说明</summary>
    public string? Comment { get; set; }
}

/// <summary>
/// 流程实例减签 DTO（在当前节点减少审批人/分支，需任务与候选人模型，暂未实现）
/// </summary>
public class TaktFlowInstanceReduceSignDto
{
    /// <summary>流程实例ID</summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }
    /// <summary>减签用户ID</summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? UserId { get; set; }
    /// <summary>说明</summary>
    public string? Comment { get; set; }
}

/// <summary>
/// 流程实例退回 DTO（退回到上一节点或指定节点）
/// </summary>
public class TaktFlowInstanceReturnDto
{
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 目标节点ID（不传则退回到上一节点）
    /// </summary>
    public string? ToNodeId { get; set; }

    /// <summary>
    /// 目标节点名称（指定节点时建议传入）
    /// </summary>
    public string? ToNodeName { get; set; }

    /// <summary>
    /// 退回说明
    /// </summary>
    public string? Comment { get; set; }
}

/// <summary>
/// 流程实例撤销审批 DTO
/// </summary>
public class TaktFlowInstanceUndoVerificationDto
{
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 要撤销的流转历史ID（不传则撤销最后一次审批）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? HistoryId { get; set; }

    /// <summary>
    /// 撤销原因/说明
    /// </summary>
    public string? Comment { get; set; }
}

/// <summary>
/// 流程实例状态 DTO（更新实例状态：挂起=3、恢复=0，受方案 IsSuspendable 控制）
/// </summary>
public class TaktFlowInstanceStatusDto
{
    /// <summary>
    /// 实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 实例状态（0=运行中，3=挂起；仅支持此二者互转）
    /// </summary>
    public int InstanceStatus { get; set; }

    /// <summary>
    /// 挂起/恢复说明
    /// </summary>
    public string? Comment { get; set; }
}

/// <summary>
/// 流程实例导入/导出模板 DTO（Excel 表头与示例行，与 TaktFlowInstance 实体字段一致）
/// </summary>
public class TaktFlowInstanceTemplateDto
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
    /// 流程方案ID（与实体一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SchemeId { get; set; }

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 业务主键
    /// </summary>
    public string? BusinessKey { get; set; }

    /// <summary>
    /// 创建人ID（与实体基类 CreateId 一致，流程发起人）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CreateId { get; set; }

    /// <summary>
    /// 创建人姓名
    /// </summary>
    public string CreateBy { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间（Excel 用字符串）
    /// </summary>
    public string CreateTime { get; set; } = string.Empty;

    /// <summary>
    /// 部门ID（0 表示未指定，与实体一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 当前节点ID（与实体一致，可空）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CurrentNodeId { get; set; }

    /// <summary>
    /// 当前节点名称
    /// </summary>
    public string? CurrentNodeName { get; set; }

    /// <summary>
    /// 上一节点ID（与实体一致，可空）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PreviousNodeId { get; set; }

    /// <summary>
    /// 优先级（0=低，1=中，2=高，3=紧急，与实体一致）
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// 实例状态（0=运行中，1=已完成，2=已终止，3=已挂起，4=已撤回）
    /// </summary>
    public int InstanceStatus { get; set; }

    /// <summary>
    /// 流程标题
    /// </summary>
    public string? ProcessTitle { get; set; }
}

/// <summary>
/// 流程实例导入 DTO（Excel 行，与 TaktFlowInstance 实体字段一致）
/// </summary>
public class TaktFlowInstanceImportDto
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
    /// 流程方案ID（与实体一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SchemeId { get; set; }

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 业务主键
    /// </summary>
    public string? BusinessKey { get; set; }

    /// <summary>
    /// 创建人ID（与实体基类 CreateId 一致，流程发起人）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CreateId { get; set; }

    /// <summary>
    /// 创建人姓名
    /// </summary>
    public string CreateBy { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间（Excel 用字符串）
    /// </summary>
    public string CreateTime { get; set; } = string.Empty;

    /// <summary>
    /// 部门ID（0 表示未指定，与实体一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 当前节点ID（与实体一致，可空）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CurrentNodeId { get; set; }

    /// <summary>
    /// 当前节点名称
    /// </summary>
    public string? CurrentNodeName { get; set; }

    /// <summary>
    /// 上一节点ID（与实体一致，可空）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PreviousNodeId { get; set; }

    /// <summary>
    /// 优先级（与实体一致）
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// 实例状态（0=运行中，1=已完成，2=已终止，3=已挂起，4=已撤回）
    /// </summary>
    public int InstanceStatus { get; set; }

    /// <summary>
    /// 流程标题
    /// </summary>
    public string? ProcessTitle { get; set; }
}

/// <summary>
/// 流程实例导出 DTO（Excel 行，与 TaktFlowInstance 实体字段一致）
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
    /// 流程方案ID（与实体一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SchemeId { get; set; }

    /// <summary>
    /// 流程名称
    /// </summary>
    public string ProcessName { get; set; } = string.Empty;

    /// <summary>
    /// 业务主键
    /// </summary>
    public string? BusinessKey { get; set; }

    /// <summary>
    /// 创建人ID（与实体基类 CreateId 一致，流程发起人）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long CreateId { get; set; }

    /// <summary>
    /// 创建人姓名
    /// </summary>
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 部门ID（0 表示未指定，与实体一致）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 当前节点ID（与实体一致，可空）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CurrentNodeId { get; set; }

    /// <summary>
    /// 当前节点名称
    /// </summary>
    public string? CurrentNodeName { get; set; }

    /// <summary>
    /// 上一节点ID（与实体一致，可空）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? PreviousNodeId { get; set; }

    /// <summary>
    /// 优先级（与实体一致）
    /// </summary>
    public int Priority { get; set; }

    /// <summary>
    /// 实例状态（0=运行中，1=已完成，2=已终止，3=已挂起，4=已撤回）
    /// </summary>
    public int InstanceStatus { get; set; }

    /// <summary>
    /// 流程标题
    /// </summary>
    public string? ProcessTitle { get; set; }
}
