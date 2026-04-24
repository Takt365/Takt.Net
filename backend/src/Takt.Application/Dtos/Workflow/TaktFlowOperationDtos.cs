// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Workflow
// 文件名称：TaktFlowOperationDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：流程操作历史表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Workflow;

/// <summary>
/// 流程操作历史表Dto
/// </summary>
public partial class TaktFlowOperationDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowOperationDto()
    {
        InstanceCode = string.Empty;
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
    }

    /// <summary>
    /// 流程操作历史表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowOperationId { get; set; }

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
    /// 关联任务ID
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
    /// 操作类型
    /// </summary>
    public int OperationType { get; set; }
    /// <summary>
    /// 操作节点ID
    /// </summary>
    public string? NodeId { get; set; }
    /// <summary>
    /// 操作节点名称
    /// </summary>
    public string? NodeName { get; set; }
    /// <summary>
    /// 操作内容
    /// </summary>
    public string? OperationContent { get; set; }
    /// <summary>
    /// 操作意见
    /// </summary>
    public string? OperationComment { get; set; }
    /// <summary>
    /// 操作结果
    /// </summary>
    public int OperationResult { get; set; }
    /// <summary>
    /// 错误信息
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// 流程操作历史表查询DTO
/// </summary>
public partial class TaktFlowOperationQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowOperationQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 流程操作历史表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowOperationId { get; set; }

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
    /// 关联任务ID
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
    /// 操作类型
    /// </summary>
    public int? OperationType { get; set; }
    /// <summary>
    /// 操作节点ID
    /// </summary>
    public string? NodeId { get; set; }
    /// <summary>
    /// 操作节点名称
    /// </summary>
    public string? NodeName { get; set; }
    /// <summary>
    /// 操作内容
    /// </summary>
    public string? OperationContent { get; set; }
    /// <summary>
    /// 操作意见
    /// </summary>
    public string? OperationComment { get; set; }
    /// <summary>
    /// 操作结果
    /// </summary>
    public int? OperationResult { get; set; }
    /// <summary>
    /// 错误信息
    /// </summary>
    public string? ErrorMessage { get; set; }

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
/// Takt创建流程操作历史表DTO
/// </summary>
public partial class TaktFlowOperationCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowOperationCreateDto()
    {
        InstanceCode = string.Empty;
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
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
    /// 关联任务ID
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
    /// 操作类型
    /// </summary>
    public int OperationType { get; set; }

        /// <summary>
    /// 操作节点ID
    /// </summary>
    public string? NodeId { get; set; }

        /// <summary>
    /// 操作节点名称
    /// </summary>
    public string? NodeName { get; set; }

        /// <summary>
    /// 操作内容
    /// </summary>
    public string? OperationContent { get; set; }

        /// <summary>
    /// 操作意见
    /// </summary>
    public string? OperationComment { get; set; }

        /// <summary>
    /// 操作结果
    /// </summary>
    public int OperationResult { get; set; }

        /// <summary>
    /// 错误信息
    /// </summary>
    public string? ErrorMessage { get; set; }

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
/// Takt更新流程操作历史表DTO
/// </summary>
public partial class TaktFlowOperationUpdateDto : TaktFlowOperationCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowOperationUpdateDto()
    {
    }

        /// <summary>
    /// 流程操作历史表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowOperationId { get; set; }
}

/// <summary>
/// 流程操作历史表导入模板DTO
/// </summary>
public partial class TaktFlowOperationTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowOperationTemplateDto()
    {
        InstanceCode = string.Empty;
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
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
    /// 关联任务ID
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
    /// 操作类型
    /// </summary>
    public int OperationType { get; set; }

        /// <summary>
    /// 操作节点ID
    /// </summary>
    public string? NodeId { get; set; }

        /// <summary>
    /// 操作节点名称
    /// </summary>
    public string? NodeName { get; set; }

        /// <summary>
    /// 操作内容
    /// </summary>
    public string? OperationContent { get; set; }

        /// <summary>
    /// 操作意见
    /// </summary>
    public string? OperationComment { get; set; }

        /// <summary>
    /// 操作结果
    /// </summary>
    public int OperationResult { get; set; }

        /// <summary>
    /// 错误信息
    /// </summary>
    public string? ErrorMessage { get; set; }

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
/// 流程操作历史表导入DTO
/// </summary>
public partial class TaktFlowOperationImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowOperationImportDto()
    {
        InstanceCode = string.Empty;
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
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
    /// 关联任务ID
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
    /// 操作类型
    /// </summary>
    public int OperationType { get; set; }

        /// <summary>
    /// 操作节点ID
    /// </summary>
    public string? NodeId { get; set; }

        /// <summary>
    /// 操作节点名称
    /// </summary>
    public string? NodeName { get; set; }

        /// <summary>
    /// 操作内容
    /// </summary>
    public string? OperationContent { get; set; }

        /// <summary>
    /// 操作意见
    /// </summary>
    public string? OperationComment { get; set; }

        /// <summary>
    /// 操作结果
    /// </summary>
    public int OperationResult { get; set; }

        /// <summary>
    /// 错误信息
    /// </summary>
    public string? ErrorMessage { get; set; }

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
/// 流程操作历史表导出DTO
/// </summary>
public partial class TaktFlowOperationExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowOperationExportDto()
    {
        CreatedAt = DateTime.Now;
        InstanceCode = string.Empty;
        SchemeKey = string.Empty;
        SchemeName = string.Empty;
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
    /// 关联任务ID
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
    /// 操作类型
    /// </summary>
    public int OperationType { get; set; }

        /// <summary>
    /// 操作节点ID
    /// </summary>
    public string? NodeId { get; set; }

        /// <summary>
    /// 操作节点名称
    /// </summary>
    public string? NodeName { get; set; }

        /// <summary>
    /// 操作内容
    /// </summary>
    public string? OperationContent { get; set; }

        /// <summary>
    /// 操作意见
    /// </summary>
    public string? OperationComment { get; set; }

        /// <summary>
    /// 操作结果
    /// </summary>
    public int OperationResult { get; set; }

        /// <summary>
    /// 错误信息
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}