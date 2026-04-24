// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Workflow
// 文件名称：TaktFlowExecutionDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：流程执行记录表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Workflow;

/// <summary>
/// 流程执行记录表Dto
/// </summary>
public partial class TaktFlowExecutionDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowExecutionDto()
    {
        InstanceCode = string.Empty;
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
        ToNodeId = string.Empty;
        ToNodeName = string.Empty;
        TransitionUserName = string.Empty;
    }

    /// <summary>
    /// 流程执行记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowExecutionId { get; set; }

    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }
    /// <summary>
    /// 流程方案ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SchemeId { get; set; }
    /// <summary>
    /// 触发任务ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TaskId { get; set; }
    /// <summary>
    /// 流程实例编码
    /// </summary>
    public string InstanceCode { get; set; }
    /// <summary>
    /// 流程Key
    /// </summary>
    public string SchemeKey { get; set; }
    /// <summary>
    /// 流程名称
    /// </summary>
    public string SchemeName { get; set; }
    /// <summary>
    /// 源节点ID
    /// </summary>
    public string? FromNodeId { get; set; }
    /// <summary>
    /// 源节点名称
    /// </summary>
    public string? FromNodeName { get; set; }
    /// <summary>
    /// 目标节点ID
    /// </summary>
    public string ToNodeId { get; set; }
    /// <summary>
    /// 目标节点名称
    /// </summary>
    public string ToNodeName { get; set; }
    /// <summary>
    /// 流转类型
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
    public string TransitionUserName { get; set; }
    /// <summary>
    /// 流转部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TransitionDeptId { get; set; }
    /// <summary>
    /// 流转部门名称
    /// </summary>
    public string? TransitionDeptName { get; set; }
    /// <summary>
    /// 流转意见
    /// </summary>
    public string? TransitionComment { get; set; }
    /// <summary>
    /// 流转附件
    /// </summary>
    public string? TransitionAttachments { get; set; }
    /// <summary>
    /// 流转耗时（毫秒）
    /// </summary>
    public int ElapsedMilliseconds { get; set; }
}

/// <summary>
/// 流程执行记录表查询DTO
/// </summary>
public partial class TaktFlowExecutionQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowExecutionQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 流程执行记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowExecutionId { get; set; }

    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? InstanceId { get; set; }
    /// <summary>
    /// 流程方案ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? SchemeId { get; set; }
    /// <summary>
    /// 触发任务ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TaskId { get; set; }
    /// <summary>
    /// 流程实例编码
    /// </summary>
    public string? InstanceCode { get; set; }
    /// <summary>
    /// 流程Key
    /// </summary>
    public string? SchemeKey { get; set; }
    /// <summary>
    /// 流程名称
    /// </summary>
    public string? SchemeName { get; set; }
    /// <summary>
    /// 源节点ID
    /// </summary>
    public string? FromNodeId { get; set; }
    /// <summary>
    /// 源节点名称
    /// </summary>
    public string? FromNodeName { get; set; }
    /// <summary>
    /// 目标节点ID
    /// </summary>
    public string? ToNodeId { get; set; }
    /// <summary>
    /// 目标节点名称
    /// </summary>
    public string? ToNodeName { get; set; }
    /// <summary>
    /// 流转类型
    /// </summary>
    public int? TransitionType { get; set; }
    /// <summary>
    /// 流转时间
    /// </summary>
    public DateTime? TransitionTime { get; set; }

    /// <summary>
    /// 流转时间开始时间
    /// </summary>
    public DateTime? TransitionTimeStart { get; set; }
    /// <summary>
    /// 流转时间结束时间
    /// </summary>
    public DateTime? TransitionTimeEnd { get; set; }
    /// <summary>
    /// 流转人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TransitionUserId { get; set; }
    /// <summary>
    /// 流转人姓名
    /// </summary>
    public string? TransitionUserName { get; set; }
    /// <summary>
    /// 流转部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TransitionDeptId { get; set; }
    /// <summary>
    /// 流转部门名称
    /// </summary>
    public string? TransitionDeptName { get; set; }
    /// <summary>
    /// 流转意见
    /// </summary>
    public string? TransitionComment { get; set; }
    /// <summary>
    /// 流转附件
    /// </summary>
    public string? TransitionAttachments { get; set; }
    /// <summary>
    /// 流转耗时（毫秒）
    /// </summary>
    public int? ElapsedMilliseconds { get; set; }

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
/// Takt创建流程执行记录表DTO
/// </summary>
public partial class TaktFlowExecutionCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowExecutionCreateDto()
    {
        InstanceCode = string.Empty;
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
        ToNodeId = string.Empty;
        ToNodeName = string.Empty;
        TransitionUserName = string.Empty;
    }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }

        /// <summary>
    /// 流程方案ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long SchemeId { get; set; }

        /// <summary>
    /// 触发任务ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TaskId { get; set; }

        /// <summary>
    /// 流程实例编码
    /// </summary>
    public string InstanceCode { get; set; }

        /// <summary>
    /// 流程Key
    /// </summary>
    public string SchemeKey { get; set; }

        /// <summary>
    /// 流程名称
    /// </summary>
    public string SchemeName { get; set; }

        /// <summary>
    /// 源节点ID
    /// </summary>
    public string? FromNodeId { get; set; }

        /// <summary>
    /// 源节点名称
    /// </summary>
    public string? FromNodeName { get; set; }

        /// <summary>
    /// 目标节点ID
    /// </summary>
    public string ToNodeId { get; set; }

        /// <summary>
    /// 目标节点名称
    /// </summary>
    public string ToNodeName { get; set; }

        /// <summary>
    /// 流转类型
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
    public string TransitionUserName { get; set; }

        /// <summary>
    /// 流转部门ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TransitionDeptId { get; set; }

        /// <summary>
    /// 流转部门名称
    /// </summary>
    public string? TransitionDeptName { get; set; }

        /// <summary>
    /// 流转意见
    /// </summary>
    public string? TransitionComment { get; set; }

        /// <summary>
    /// 流转附件
    /// </summary>
    public string? TransitionAttachments { get; set; }

        /// <summary>
    /// 流转耗时（毫秒）
    /// </summary>
    public int ElapsedMilliseconds { get; set; }

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
/// Takt更新流程执行记录表DTO
/// </summary>
public partial class TaktFlowExecutionUpdateDto : TaktFlowExecutionCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowExecutionUpdateDto()
    {
    }

        /// <summary>
    /// 流程执行记录表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowExecutionId { get; set; }
}

/// <summary>
/// 流程执行记录表导入模板DTO
/// </summary>
public partial class TaktFlowExecutionTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowExecutionTemplateDto()
    {
        InstanceCode = string.Empty;
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
        ToNodeId = string.Empty;
        ToNodeName = string.Empty;
        TransitionUserName = string.Empty;
    }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long InstanceId { get; set; }

        /// <summary>
    /// 流程方案ID
    /// </summary>
    public long SchemeId { get; set; }

        /// <summary>
    /// 触发任务ID
    /// </summary>
    public long? TaskId { get; set; }

        /// <summary>
    /// 流程实例编码
    /// </summary>
    public string InstanceCode { get; set; }

        /// <summary>
    /// 流程Key
    /// </summary>
    public string SchemeKey { get; set; }

        /// <summary>
    /// 流程名称
    /// </summary>
    public string SchemeName { get; set; }

        /// <summary>
    /// 源节点ID
    /// </summary>
    public string? FromNodeId { get; set; }

        /// <summary>
    /// 源节点名称
    /// </summary>
    public string? FromNodeName { get; set; }

        /// <summary>
    /// 目标节点ID
    /// </summary>
    public string ToNodeId { get; set; }

        /// <summary>
    /// 目标节点名称
    /// </summary>
    public string ToNodeName { get; set; }

        /// <summary>
    /// 流转类型
    /// </summary>
    public int TransitionType { get; set; }

        /// <summary>
    /// 流转时间
    /// </summary>
    public DateTime TransitionTime { get; set; }

        /// <summary>
    /// 流转人ID
    /// </summary>
    public long TransitionUserId { get; set; }

        /// <summary>
    /// 流转人姓名
    /// </summary>
    public string TransitionUserName { get; set; }

        /// <summary>
    /// 流转部门ID
    /// </summary>
    public long? TransitionDeptId { get; set; }

        /// <summary>
    /// 流转部门名称
    /// </summary>
    public string? TransitionDeptName { get; set; }

        /// <summary>
    /// 流转意见
    /// </summary>
    public string? TransitionComment { get; set; }

        /// <summary>
    /// 流转附件
    /// </summary>
    public string? TransitionAttachments { get; set; }

        /// <summary>
    /// 流转耗时（毫秒）
    /// </summary>
    public int ElapsedMilliseconds { get; set; }

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
/// 流程执行记录表导入DTO
/// </summary>
public partial class TaktFlowExecutionImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowExecutionImportDto()
    {
        InstanceCode = string.Empty;
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
        ToNodeId = string.Empty;
        ToNodeName = string.Empty;
        TransitionUserName = string.Empty;
    }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long InstanceId { get; set; }

        /// <summary>
    /// 流程方案ID
    /// </summary>
    public long SchemeId { get; set; }

        /// <summary>
    /// 触发任务ID
    /// </summary>
    public long? TaskId { get; set; }

        /// <summary>
    /// 流程实例编码
    /// </summary>
    public string InstanceCode { get; set; }

        /// <summary>
    /// 流程Key
    /// </summary>
    public string SchemeKey { get; set; }

        /// <summary>
    /// 流程名称
    /// </summary>
    public string SchemeName { get; set; }

        /// <summary>
    /// 源节点ID
    /// </summary>
    public string? FromNodeId { get; set; }

        /// <summary>
    /// 源节点名称
    /// </summary>
    public string? FromNodeName { get; set; }

        /// <summary>
    /// 目标节点ID
    /// </summary>
    public string ToNodeId { get; set; }

        /// <summary>
    /// 目标节点名称
    /// </summary>
    public string ToNodeName { get; set; }

        /// <summary>
    /// 流转类型
    /// </summary>
    public int TransitionType { get; set; }

        /// <summary>
    /// 流转时间
    /// </summary>
    public DateTime TransitionTime { get; set; }

        /// <summary>
    /// 流转人ID
    /// </summary>
    public long TransitionUserId { get; set; }

        /// <summary>
    /// 流转人姓名
    /// </summary>
    public string TransitionUserName { get; set; }

        /// <summary>
    /// 流转部门ID
    /// </summary>
    public long? TransitionDeptId { get; set; }

        /// <summary>
    /// 流转部门名称
    /// </summary>
    public string? TransitionDeptName { get; set; }

        /// <summary>
    /// 流转意见
    /// </summary>
    public string? TransitionComment { get; set; }

        /// <summary>
    /// 流转附件
    /// </summary>
    public string? TransitionAttachments { get; set; }

        /// <summary>
    /// 流转耗时（毫秒）
    /// </summary>
    public int ElapsedMilliseconds { get; set; }

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
/// 流程执行记录表导出DTO
/// </summary>
public partial class TaktFlowExecutionExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowExecutionExportDto()
    {
        CreatedAt = DateTime.Now;
        InstanceCode = string.Empty;
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
        ToNodeId = string.Empty;
        ToNodeName = string.Empty;
        TransitionUserName = string.Empty;
    }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long InstanceId { get; set; }

        /// <summary>
    /// 流程方案ID
    /// </summary>
    public long SchemeId { get; set; }

        /// <summary>
    /// 触发任务ID
    /// </summary>
    public long? TaskId { get; set; }

        /// <summary>
    /// 流程实例编码
    /// </summary>
    public string InstanceCode { get; set; }

        /// <summary>
    /// 流程Key
    /// </summary>
    public string SchemeKey { get; set; }

        /// <summary>
    /// 流程名称
    /// </summary>
    public string SchemeName { get; set; }

        /// <summary>
    /// 源节点ID
    /// </summary>
    public string? FromNodeId { get; set; }

        /// <summary>
    /// 源节点名称
    /// </summary>
    public string? FromNodeName { get; set; }

        /// <summary>
    /// 目标节点ID
    /// </summary>
    public string ToNodeId { get; set; }

        /// <summary>
    /// 目标节点名称
    /// </summary>
    public string ToNodeName { get; set; }

        /// <summary>
    /// 流转类型
    /// </summary>
    public int TransitionType { get; set; }

        /// <summary>
    /// 流转时间
    /// </summary>
    public DateTime TransitionTime { get; set; }

        /// <summary>
    /// 流转人ID
    /// </summary>
    public long TransitionUserId { get; set; }

        /// <summary>
    /// 流转人姓名
    /// </summary>
    public string TransitionUserName { get; set; }

        /// <summary>
    /// 流转部门ID
    /// </summary>
    public long? TransitionDeptId { get; set; }

        /// <summary>
    /// 流转部门名称
    /// </summary>
    public string? TransitionDeptName { get; set; }

        /// <summary>
    /// 流转意见
    /// </summary>
    public string? TransitionComment { get; set; }

        /// <summary>
    /// 流转附件
    /// </summary>
    public string? TransitionAttachments { get; set; }

        /// <summary>
    /// 流转耗时（毫秒）
    /// </summary>
    public int ElapsedMilliseconds { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}