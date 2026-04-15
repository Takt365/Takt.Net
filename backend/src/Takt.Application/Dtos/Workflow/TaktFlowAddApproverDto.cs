// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Workflow
// 文件名称：TaktFlowAddApproverDto.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：流程加签审批人 DTO
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using Takt.Application.Dtos;
using SqlSugar;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Workflow;

/// <summary>
/// 流程加签审批人 DTO
/// </summary>
public class TaktFlowAddApproverDto : TaktDtoBase
{
    /// <summary>
    /// 加签记录ID（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long AddApproverId { get; set; }

    /// <summary>
    /// 流程实例ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 节点ID（流程定义中的节点 id）
    /// </summary>
    public string ActivityId { get; set; } = string.Empty;

    /// <summary>
    /// 审批人用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long ApproverUserId { get; set; }

    /// <summary>
    /// 审批人姓名
    /// </summary>
    public string ApproverUserName { get; set; } = string.Empty;

    /// <summary>
    /// 审批类型：sequential=顺序，all=并行且，one=并行或
    /// </summary>
    public string? ApproveType { get; set; }

    /// <summary>
    /// 顺序号
    /// </summary>
    public int? OrderNo { get; set; }

    /// <summary>
    /// 状态：0未处理，1通过，2未通过，3驳回
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
    /// 加签发起人用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? CreateUserId { get; set; }

    /// <summary>
    /// 加签发起人姓名
    /// </summary>
    public string? CreateUserName { get; set; }

    /// <summary>
    /// 加签完成后是否回到原节点再审批
    /// </summary>
    public bool? ReturnToSignNode { get; set; }
}

/// <summary>
/// 流程加签审批人查询 DTO
/// </summary>
public class TaktFlowAddApproverQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 流程实例ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? InstanceId { get; set; }

    /// <summary>
    /// 节点ID（流程定义中的节点 id）
    /// </summary>
    public string? ActivityId { get; set; }

    /// <summary>
    /// 状态：0未处理，1通过，2未通过，3驳回
    /// </summary>
    public int? Status { get; set; }
}

/// <summary>
/// 流程加签审批人导出 DTO
/// </summary>
public class TaktFlowAddApproverExportDto
{
    /// <summary>
    /// 流程实例ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long InstanceId { get; set; }

    /// <summary>
    /// 节点ID
    /// </summary>
    public string ActivityId { get; set; } = string.Empty;

    /// <summary>
    /// 审批人姓名
    /// </summary>
    public string ApproverUserName { get; set; } = string.Empty;

    /// <summary>
    /// 审批类型：sequential=顺序，all=并行且，one=并行或
    /// </summary>
    public string? ApproveType { get; set; }

    /// <summary>
    /// 顺序号
    /// </summary>
    public int? OrderNo { get; set; }

    /// <summary>
    /// 状态：0未处理，1通过，2未通过，3驳回
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
    /// 加签发起人姓名
    /// </summary>
    public string? CreateUserName { get; set; }
}
