// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Workflow
// 文件名称：TaktFlowAddApproverDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：流程加签表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Workflow;

/// <summary>
/// 流程加签表Dto
/// </summary>
public partial class TaktFlowAddApproverDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowAddApproverDto()
    {
        ActivityId = string.Empty;
        ApproverUserName = string.Empty;
    }

    /// <summary>
    /// 流程加签表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowAddApproverId { get; set; }

    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }
    /// <summary>
    /// 节点ID
    /// </summary>
    public string ActivityId { get; set; }
    /// <summary>
    /// 审批人用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApproverUserId { get; set; }
    /// <summary>
    /// 审批人姓名
    /// </summary>
    public string ApproverUserName { get; set; }
    /// <summary>
    /// 审批类型
    /// </summary>
    public string? ApproveType { get; set; }
    /// <summary>
    /// 顺序号
    /// </summary>
    public int? OrderNo { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }
    /// <summary>
    /// 审批意见
    /// </summary>
    public string? VerifyComment { get; set; }
    /// <summary>
    /// 审批时间
    /// </summary>
    public DateTime? VerifyTime { get; set; }
    /// <summary>
    /// 加签原因
    /// </summary>
    public string? Reason { get; set; }
    /// <summary>
    /// 加签发起人用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreateUserId { get; set; }
    /// <summary>
    /// 加签发起人姓名
    /// </summary>
    public string? CreateUserName { get; set; }
    /// <summary>
    /// 加签完成后是否回原节点
    /// </summary>
    public bool? ReturnToSignNode { get; set; }
}

/// <summary>
/// 流程加签表查询DTO
/// </summary>
public partial class TaktFlowAddApproverQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowAddApproverQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 流程加签表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowAddApproverId { get; set; }

    /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? InstanceId { get; set; }
    /// <summary>
    /// 节点ID
    /// </summary>
    public string? ActivityId { get; set; }
    /// <summary>
    /// 审批人用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? ApproverUserId { get; set; }
    /// <summary>
    /// 审批人姓名
    /// </summary>
    public string? ApproverUserName { get; set; }
    /// <summary>
    /// 审批类型
    /// </summary>
    public string? ApproveType { get; set; }
    /// <summary>
    /// 顺序号
    /// </summary>
    public int? OrderNo { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }
    /// <summary>
    /// 审批意见
    /// </summary>
    public string? VerifyComment { get; set; }
    /// <summary>
    /// 审批时间
    /// </summary>
    public DateTime? VerifyTime { get; set; }

    /// <summary>
    /// 审批时间开始时间
    /// </summary>
    public DateTime? VerifyTimeStart { get; set; }
    /// <summary>
    /// 审批时间结束时间
    /// </summary>
    public DateTime? VerifyTimeEnd { get; set; }
    /// <summary>
    /// 加签原因
    /// </summary>
    public string? Reason { get; set; }
    /// <summary>
    /// 加签发起人用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreateUserId { get; set; }
    /// <summary>
    /// 加签发起人姓名
    /// </summary>
    public string? CreateUserName { get; set; }
    /// <summary>
    /// 加签完成后是否回原节点
    /// </summary>
    public bool? ReturnToSignNode { get; set; }

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
/// Takt创建流程加签表DTO
/// </summary>
public partial class TaktFlowAddApproverCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowAddApproverCreateDto()
    {
        ActivityId = string.Empty;
        ApproverUserName = string.Empty;
    }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }

        /// <summary>
    /// 节点ID
    /// </summary>
    public string ActivityId { get; set; }

        /// <summary>
    /// 审批人用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApproverUserId { get; set; }

        /// <summary>
    /// 审批人姓名
    /// </summary>
    public string ApproverUserName { get; set; }

        /// <summary>
    /// 审批类型
    /// </summary>
    public string? ApproveType { get; set; }

        /// <summary>
    /// 顺序号
    /// </summary>
    public int? OrderNo { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

        /// <summary>
    /// 审批意见
    /// </summary>
    public string? VerifyComment { get; set; }

        /// <summary>
    /// 审批时间
    /// </summary>
    public DateTime? VerifyTime { get; set; }

        /// <summary>
    /// 加签原因
    /// </summary>
    public string? Reason { get; set; }

        /// <summary>
    /// 加签发起人用户ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreateUserId { get; set; }

        /// <summary>
    /// 加签发起人姓名
    /// </summary>
    public string? CreateUserName { get; set; }

        /// <summary>
    /// 加签完成后是否回原节点
    /// </summary>
    public bool? ReturnToSignNode { get; set; }

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
/// Takt更新流程加签表DTO
/// </summary>
public partial class TaktFlowAddApproverUpdateDto : TaktFlowAddApproverCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowAddApproverUpdateDto()
    {
    }

        /// <summary>
    /// 流程加签表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowAddApproverId { get; set; }
}

/// <summary>
/// 流程加签表状态DTO
/// </summary>
public partial class TaktFlowAddApproverStatusDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowAddApproverStatusDto()
    {
    }

        /// <summary>
    /// 流程加签表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long FlowAddApproverId { get; set; }

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    public int Status { get; set; }
}

/// <summary>
/// 流程加签表导入模板DTO
/// </summary>
public partial class TaktFlowAddApproverTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowAddApproverTemplateDto()
    {
        ActivityId = string.Empty;
        ApproverUserName = string.Empty;
    }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long InstanceId { get; set; }

        /// <summary>
    /// 节点ID
    /// </summary>
    public string ActivityId { get; set; }

        /// <summary>
    /// 审批人用户ID
    /// </summary>
    public long ApproverUserId { get; set; }

        /// <summary>
    /// 审批人姓名
    /// </summary>
    public string ApproverUserName { get; set; }

        /// <summary>
    /// 审批类型
    /// </summary>
    public string? ApproveType { get; set; }

        /// <summary>
    /// 顺序号
    /// </summary>
    public int? OrderNo { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

        /// <summary>
    /// 审批意见
    /// </summary>
    public string? VerifyComment { get; set; }

        /// <summary>
    /// 审批时间
    /// </summary>
    public DateTime? VerifyTime { get; set; }

        /// <summary>
    /// 加签原因
    /// </summary>
    public string? Reason { get; set; }

        /// <summary>
    /// 加签发起人用户ID
    /// </summary>
    public long? CreateUserId { get; set; }

        /// <summary>
    /// 加签发起人姓名
    /// </summary>
    public string? CreateUserName { get; set; }

        /// <summary>
    /// 加签完成后是否回原节点
    /// </summary>
    public bool? ReturnToSignNode { get; set; }

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
/// 流程加签表导入DTO
/// </summary>
public partial class TaktFlowAddApproverImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowAddApproverImportDto()
    {
        ActivityId = string.Empty;
        ApproverUserName = string.Empty;
    }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long InstanceId { get; set; }

        /// <summary>
    /// 节点ID
    /// </summary>
    public string ActivityId { get; set; }

        /// <summary>
    /// 审批人用户ID
    /// </summary>
    public long ApproverUserId { get; set; }

        /// <summary>
    /// 审批人姓名
    /// </summary>
    public string ApproverUserName { get; set; }

        /// <summary>
    /// 审批类型
    /// </summary>
    public string? ApproveType { get; set; }

        /// <summary>
    /// 顺序号
    /// </summary>
    public int? OrderNo { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

        /// <summary>
    /// 审批意见
    /// </summary>
    public string? VerifyComment { get; set; }

        /// <summary>
    /// 审批时间
    /// </summary>
    public DateTime? VerifyTime { get; set; }

        /// <summary>
    /// 加签原因
    /// </summary>
    public string? Reason { get; set; }

        /// <summary>
    /// 加签发起人用户ID
    /// </summary>
    public long? CreateUserId { get; set; }

        /// <summary>
    /// 加签发起人姓名
    /// </summary>
    public string? CreateUserName { get; set; }

        /// <summary>
    /// 加签完成后是否回原节点
    /// </summary>
    public bool? ReturnToSignNode { get; set; }

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
/// 流程加签表导出DTO
/// </summary>
public partial class TaktFlowAddApproverExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktFlowAddApproverExportDto()
    {
        CreatedAt = DateTime.Now;
        ActivityId = string.Empty;
        ApproverUserName = string.Empty;
    }

        /// <summary>
    /// 流程实例ID
    /// </summary>
    public long InstanceId { get; set; }

        /// <summary>
    /// 节点ID
    /// </summary>
    public string ActivityId { get; set; }

        /// <summary>
    /// 审批人用户ID
    /// </summary>
    public long ApproverUserId { get; set; }

        /// <summary>
    /// 审批人姓名
    /// </summary>
    public string ApproverUserName { get; set; }

        /// <summary>
    /// 审批类型
    /// </summary>
    public string? ApproveType { get; set; }

        /// <summary>
    /// 顺序号
    /// </summary>
    public int? OrderNo { get; set; }

        /// <summary>
    /// 状态
    /// </summary>
    public int Status { get; set; }

        /// <summary>
    /// 审批意见
    /// </summary>
    public string? VerifyComment { get; set; }

        /// <summary>
    /// 审批时间
    /// </summary>
    public DateTime? VerifyTime { get; set; }

        /// <summary>
    /// 加签原因
    /// </summary>
    public string? Reason { get; set; }

        /// <summary>
    /// 加签发起人用户ID
    /// </summary>
    public long? CreateUserId { get; set; }

        /// <summary>
    /// 加签发起人姓名
    /// </summary>
    public string? CreateUserName { get; set; }

        /// <summary>
    /// 加签完成后是否回原节点
    /// </summary>
    public bool? ReturnToSignNode { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}