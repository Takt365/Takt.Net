// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Workflow
// 文件名称：TaktFlowInstanceDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：流程实例表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Workflow;

/// <summary>
/// 流程实例表Dto
/// </summary>
public partial class TaktFlowInstanceDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowInstanceDto()
    {
        InstanceCode = string.Empty;
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
        StartUserName = string.Empty;
    }

    /// <summary>
    /// 流程实例表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowInstanceId { get; set; }

    /// <summary>
    /// 实例编码
    /// </summary>
    public string InstanceCode { get; set; }
    /// <summary>
    /// 流程Key
    /// </summary>
    public string SchemeKey { get; set; }
    /// <summary>
    /// 流程方案ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SchemeId { get; set; }
    /// <summary>
    /// 流程名称
    /// </summary>
    public string SchemeName { get; set; }
    /// <summary>
    /// 业务主键
    /// </summary>
    public string? BusinessKey { get; set; }
    /// <summary>
    /// 业务类型
    /// </summary>
    public string? BusinessType { get; set; }
    /// <summary>
    /// 启动人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long StartUserId { get; set; }
    /// <summary>
    /// 启动人姓名
    /// </summary>
    public string StartUserName { get; set; }
    /// <summary>
    /// 启动部门ID
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
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CurrentNodeId { get; set; }
    /// <summary>
    /// 当前节点ID
    /// </summary>
    public string? CurrentNodeName { get; set; }
    /// <summary>
    /// 当前节点名称
    /// </summary>
    public string? ActivityName { get; set; }
    /// <summary>
    /// 上一节点ID
    /// </summary>
    public string? PreviousNodeId { get; set; }
    /// <summary>
    /// 当前节点执行人ID列表
    /// </summary>
    public string? MakerList { get; set; }
    /// <summary>
    /// 表单数据
    /// </summary>
    public string? FrmData { get; set; }
    /// <summary>
    /// 实例状态
    /// </summary>
    public int InstanceStatus { get; set; }
    /// <summary>
    /// 是否挂起
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
    /// 优先级
    /// </summary>
    public int Priority { get; set; }
    /// <summary>
    /// 流程标题
    /// </summary>
    public string? ProcessTitle { get; set; }
    /// <summary>
    /// 流程表单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FormId { get; set; }
    /// <summary>
    /// 流程表单编码
    /// </summary>
    public string? FormCode { get; set; }
}

/// <summary>
/// 流程实例表查询DTO
/// </summary>
public partial class TaktFlowInstanceQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowInstanceQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 流程实例表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowInstanceId { get; set; }

    /// <summary>
    /// 实例编码
    /// </summary>
    public string? InstanceCode { get; set; }
    /// <summary>
    /// 流程Key
    /// </summary>
    public string? SchemeKey { get; set; }
    /// <summary>
    /// 流程方案ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? SchemeId { get; set; }
    /// <summary>
    /// 流程名称
    /// </summary>
    public string? SchemeName { get; set; }
    /// <summary>
    /// 业务主键
    /// </summary>
    public string? BusinessKey { get; set; }
    /// <summary>
    /// 业务类型
    /// </summary>
    public string? BusinessType { get; set; }
    /// <summary>
    /// 启动人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? StartUserId { get; set; }
    /// <summary>
    /// 启动人姓名
    /// </summary>
    public string? StartUserName { get; set; }
    /// <summary>
    /// 启动部门ID
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
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 启动时间开始时间
    /// </summary>
    public DateTime? StartTimeStart { get; set; }
    /// <summary>
    /// 启动时间结束时间
    /// </summary>
    public DateTime? StartTimeEnd { get; set; }
    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 结束时间开始时间
    /// </summary>
    public DateTime? EndTimeStart { get; set; }
    /// <summary>
    /// 结束时间结束时间
    /// </summary>
    public DateTime? EndTimeEnd { get; set; }
    /// <summary>
    /// 当前节点ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CurrentNodeId { get; set; }
    /// <summary>
    /// 当前节点ID
    /// </summary>
    public string? CurrentNodeName { get; set; }
    /// <summary>
    /// 当前节点名称
    /// </summary>
    public string? ActivityName { get; set; }
    /// <summary>
    /// 上一节点ID
    /// </summary>
    public string? PreviousNodeId { get; set; }
    /// <summary>
    /// 当前节点执行人ID列表
    /// </summary>
    public string? MakerList { get; set; }
    /// <summary>
    /// 表单数据
    /// </summary>
    public string? FrmData { get; set; }
    /// <summary>
    /// 实例状态
    /// </summary>
    public int? InstanceStatus { get; set; }
    /// <summary>
    /// 是否挂起
    /// </summary>
    public int? IsSuspended { get; set; }
    /// <summary>
    /// 挂起时间
    /// </summary>
    public DateTime? SuspendTime { get; set; }

    /// <summary>
    /// 挂起时间开始时间
    /// </summary>
    public DateTime? SuspendTimeStart { get; set; }
    /// <summary>
    /// 挂起时间结束时间
    /// </summary>
    public DateTime? SuspendTimeEnd { get; set; }
    /// <summary>
    /// 挂起原因
    /// </summary>
    public string? SuspendReason { get; set; }
    /// <summary>
    /// 优先级
    /// </summary>
    public int? Priority { get; set; }
    /// <summary>
    /// 流程标题
    /// </summary>
    public string? ProcessTitle { get; set; }
    /// <summary>
    /// 流程表单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FormId { get; set; }
    /// <summary>
    /// 流程表单编码
    /// </summary>
    public string? FormCode { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreatedById { get; set; }
    /// <summary>
    /// 创建人
    /// </summary>
    public long? CreatedBy { get; set; }
    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 创建时间开始
    /// </summary>
    public DateTime? CreatedAtStart { get; set; }
    /// <summary>
    /// 创建时间结束
    /// </summary>
    public DateTime? CreatedAtEnd { get; set; }
}

/// <summary>
/// Takt创建流程实例表DTO
/// </summary>
public partial class TaktFlowInstanceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowInstanceCreateDto()
    {
        InstanceCode = string.Empty;
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
        StartUserName = string.Empty;
    }

        /// <summary>
    /// 实例编码
    /// </summary>
    public string InstanceCode { get; set; }

        /// <summary>
    /// 流程Key
    /// </summary>
    public string SchemeKey { get; set; }

        /// <summary>
    /// 流程方案ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SchemeId { get; set; }

        /// <summary>
    /// 流程名称
    /// </summary>
    public string SchemeName { get; set; }

        /// <summary>
    /// 业务主键
    /// </summary>
    public string? BusinessKey { get; set; }

        /// <summary>
    /// 业务类型
    /// </summary>
    public string? BusinessType { get; set; }

        /// <summary>
    /// 启动人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long StartUserId { get; set; }

        /// <summary>
    /// 启动人姓名
    /// </summary>
    public string StartUserName { get; set; }

        /// <summary>
    /// 启动部门ID
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
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CurrentNodeId { get; set; }

        /// <summary>
    /// 当前节点ID
    /// </summary>
    public string? CurrentNodeName { get; set; }

        /// <summary>
    /// 当前节点名称
    /// </summary>
    public string? ActivityName { get; set; }

        /// <summary>
    /// 上一节点ID
    /// </summary>
    public string? PreviousNodeId { get; set; }

        /// <summary>
    /// 当前节点执行人ID列表
    /// </summary>
    public string? MakerList { get; set; }

        /// <summary>
    /// 表单数据
    /// </summary>
    public string? FrmData { get; set; }

        /// <summary>
    /// 实例状态
    /// </summary>
    public int InstanceStatus { get; set; }

        /// <summary>
    /// 是否挂起
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
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

        /// <summary>
    /// 流程标题
    /// </summary>
    public string? ProcessTitle { get; set; }

        /// <summary>
    /// 流程表单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? FormId { get; set; }

        /// <summary>
    /// 流程表单编码
    /// </summary>
    public string? FormCode { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// Takt更新流程实例表DTO
/// </summary>
public partial class TaktFlowInstanceUpdateDto : TaktFlowInstanceCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowInstanceUpdateDto()
    {
    }

        /// <summary>
    /// 流程实例表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowInstanceId { get; set; }
}

/// <summary>
/// 流程实例表实例状态DTO
/// </summary>
public partial class TaktFlowInstanceStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowInstanceStatusDto()
    {
    }

        /// <summary>
    /// 流程实例表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowInstanceId { get; set; }

    /// <summary>
    /// 实例状态（0=禁用，1=启用）
    /// </summary>
    public int InstanceStatus { get; set; }
}

/// <summary>
/// 流程实例表导入模板DTO
/// </summary>
public partial class TaktFlowInstanceTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowInstanceTemplateDto()
    {
        InstanceCode = string.Empty;
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
        StartUserName = string.Empty;
    }

        /// <summary>
    /// 实例编码
    /// </summary>
    public string InstanceCode { get; set; }

        /// <summary>
    /// 流程Key
    /// </summary>
    public string SchemeKey { get; set; }

        /// <summary>
    /// 流程方案ID
    /// </summary>
    public long SchemeId { get; set; }

        /// <summary>
    /// 流程名称
    /// </summary>
    public string SchemeName { get; set; }

        /// <summary>
    /// 业务主键
    /// </summary>
    public string? BusinessKey { get; set; }

        /// <summary>
    /// 业务类型
    /// </summary>
    public string? BusinessType { get; set; }

        /// <summary>
    /// 启动人ID
    /// </summary>
    public long StartUserId { get; set; }

        /// <summary>
    /// 启动人姓名
    /// </summary>
    public string StartUserName { get; set; }

        /// <summary>
    /// 启动部门ID
    /// </summary>
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
    public long? CurrentNodeId { get; set; }

        /// <summary>
    /// 当前节点ID
    /// </summary>
    public string? CurrentNodeName { get; set; }

        /// <summary>
    /// 当前节点名称
    /// </summary>
    public string? ActivityName { get; set; }

        /// <summary>
    /// 上一节点ID
    /// </summary>
    public string? PreviousNodeId { get; set; }

        /// <summary>
    /// 当前节点执行人ID列表
    /// </summary>
    public string? MakerList { get; set; }

        /// <summary>
    /// 表单数据
    /// </summary>
    public string? FrmData { get; set; }

        /// <summary>
    /// 实例状态
    /// </summary>
    public int InstanceStatus { get; set; }

        /// <summary>
    /// 是否挂起
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
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

        /// <summary>
    /// 流程标题
    /// </summary>
    public string? ProcessTitle { get; set; }

        /// <summary>
    /// 流程表单ID
    /// </summary>
    public long? FormId { get; set; }

        /// <summary>
    /// 流程表单编码
    /// </summary>
    public string? FormCode { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 流程实例表导入DTO
/// </summary>
public partial class TaktFlowInstanceImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowInstanceImportDto()
    {
        InstanceCode = string.Empty;
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
        StartUserName = string.Empty;
    }

        /// <summary>
    /// 实例编码
    /// </summary>
    public string InstanceCode { get; set; }

        /// <summary>
    /// 流程Key
    /// </summary>
    public string SchemeKey { get; set; }

        /// <summary>
    /// 流程方案ID
    /// </summary>
    public long SchemeId { get; set; }

        /// <summary>
    /// 流程名称
    /// </summary>
    public string SchemeName { get; set; }

        /// <summary>
    /// 业务主键
    /// </summary>
    public string? BusinessKey { get; set; }

        /// <summary>
    /// 业务类型
    /// </summary>
    public string? BusinessType { get; set; }

        /// <summary>
    /// 启动人ID
    /// </summary>
    public long StartUserId { get; set; }

        /// <summary>
    /// 启动人姓名
    /// </summary>
    public string StartUserName { get; set; }

        /// <summary>
    /// 启动部门ID
    /// </summary>
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
    public long? CurrentNodeId { get; set; }

        /// <summary>
    /// 当前节点ID
    /// </summary>
    public string? CurrentNodeName { get; set; }

        /// <summary>
    /// 当前节点名称
    /// </summary>
    public string? ActivityName { get; set; }

        /// <summary>
    /// 上一节点ID
    /// </summary>
    public string? PreviousNodeId { get; set; }

        /// <summary>
    /// 当前节点执行人ID列表
    /// </summary>
    public string? MakerList { get; set; }

        /// <summary>
    /// 表单数据
    /// </summary>
    public string? FrmData { get; set; }

        /// <summary>
    /// 实例状态
    /// </summary>
    public int InstanceStatus { get; set; }

        /// <summary>
    /// 是否挂起
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
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

        /// <summary>
    /// 流程标题
    /// </summary>
    public string? ProcessTitle { get; set; }

        /// <summary>
    /// 流程表单ID
    /// </summary>
    public long? FormId { get; set; }

        /// <summary>
    /// 流程表单编码
    /// </summary>
    public string? FormCode { get; set; }

    /// <summary>
    /// 扩展字段JSON
    /// </summary>
    public string? ExtFieldJson { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 流程实例表导出DTO
/// </summary>
public partial class TaktFlowInstanceExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowInstanceExportDto()
    {
        CreatedAt = DateTime.Now;
        InstanceCode = string.Empty;
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
        StartUserName = string.Empty;
    }

        /// <summary>
    /// 实例编码
    /// </summary>
    public string InstanceCode { get; set; }

        /// <summary>
    /// 流程Key
    /// </summary>
    public string SchemeKey { get; set; }

        /// <summary>
    /// 流程方案ID
    /// </summary>
    public long SchemeId { get; set; }

        /// <summary>
    /// 流程名称
    /// </summary>
    public string SchemeName { get; set; }

        /// <summary>
    /// 业务主键
    /// </summary>
    public string? BusinessKey { get; set; }

        /// <summary>
    /// 业务类型
    /// </summary>
    public string? BusinessType { get; set; }

        /// <summary>
    /// 启动人ID
    /// </summary>
    public long StartUserId { get; set; }

        /// <summary>
    /// 启动人姓名
    /// </summary>
    public string StartUserName { get; set; }

        /// <summary>
    /// 启动部门ID
    /// </summary>
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
    public long? CurrentNodeId { get; set; }

        /// <summary>
    /// 当前节点ID
    /// </summary>
    public string? CurrentNodeName { get; set; }

        /// <summary>
    /// 当前节点名称
    /// </summary>
    public string? ActivityName { get; set; }

        /// <summary>
    /// 上一节点ID
    /// </summary>
    public string? PreviousNodeId { get; set; }

        /// <summary>
    /// 当前节点执行人ID列表
    /// </summary>
    public string? MakerList { get; set; }

        /// <summary>
    /// 表单数据
    /// </summary>
    public string? FrmData { get; set; }

        /// <summary>
    /// 实例状态
    /// </summary>
    public int InstanceStatus { get; set; }

        /// <summary>
    /// 是否挂起
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
    /// 优先级
    /// </summary>
    public int Priority { get; set; }

        /// <summary>
    /// 流程标题
    /// </summary>
    public string? ProcessTitle { get; set; }

        /// <summary>
    /// 流程表单ID
    /// </summary>
    public long? FormId { get; set; }

        /// <summary>
    /// 流程表单编码
    /// </summary>
    public string? FormCode { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}