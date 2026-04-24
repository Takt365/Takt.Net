// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Business.HelpDesk
// 文件名称：TaktTicketEvaluationDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：工单服务评价表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Business.HelpDesk;

/// <summary>
/// 工单服务评价表Dto
/// </summary>
public partial class TaktTicketEvaluationDto : TaktDtosEntityBase
{
    /// <summary>
    /// 工单服务评价表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketEvaluationId { get; set; }

    /// <summary>
    /// 工单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketId { get; set; }
    /// <summary>
    /// 综合评分
    /// </summary>
    public int Score { get; set; }
    /// <summary>
    /// 评价内容
    /// </summary>
    public string? Comment { get; set; }
    /// <summary>
    /// 评价人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EvaluatorId { get; set; }
    /// <summary>
    /// 评价人姓名
    /// </summary>
    public string? EvaluatorName { get; set; }
    /// <summary>
    /// 评价时间
    /// </summary>
    public DateTime EvaluatedAt { get; set; }
}

/// <summary>
/// 工单服务评价表查询DTO
/// </summary>
public partial class TaktTicketEvaluationQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketEvaluationQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工单服务评价表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketEvaluationId { get; set; }

    /// <summary>
    /// 工单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TicketId { get; set; }
    /// <summary>
    /// 综合评分
    /// </summary>
    public int? Score { get; set; }
    /// <summary>
    /// 评价内容
    /// </summary>
    public string? Comment { get; set; }
    /// <summary>
    /// 评价人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? EvaluatorId { get; set; }
    /// <summary>
    /// 评价人姓名
    /// </summary>
    public string? EvaluatorName { get; set; }
    /// <summary>
    /// 评价时间
    /// </summary>
    public DateTime? EvaluatedAt { get; set; }

    /// <summary>
    /// 评价时间开始时间
    /// </summary>
    public DateTime? EvaluatedAtStart { get; set; }
    /// <summary>
    /// 评价时间结束时间
    /// </summary>
    public DateTime? EvaluatedAtEnd { get; set; }

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
/// Takt创建工单服务评价表DTO
/// </summary>
public partial class TaktTicketEvaluationCreateDto
{
        /// <summary>
    /// 工单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketId { get; set; }

        /// <summary>
    /// 综合评分
    /// </summary>
    public int Score { get; set; }

        /// <summary>
    /// 评价内容
    /// </summary>
    public string? Comment { get; set; }

        /// <summary>
    /// 评价人ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long EvaluatorId { get; set; }

        /// <summary>
    /// 评价人姓名
    /// </summary>
    public string? EvaluatorName { get; set; }

        /// <summary>
    /// 评价时间
    /// </summary>
    public DateTime EvaluatedAt { get; set; }

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
/// Takt更新工单服务评价表DTO
/// </summary>
public partial class TaktTicketEvaluationUpdateDto : TaktTicketEvaluationCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketEvaluationUpdateDto()
    {
    }

        /// <summary>
    /// 工单服务评价表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketEvaluationId { get; set; }
}

/// <summary>
/// 工单服务评价表导入模板DTO
/// </summary>
public partial class TaktTicketEvaluationTemplateDto
{
        /// <summary>
    /// 工单ID
    /// </summary>
    public long TicketId { get; set; }

        /// <summary>
    /// 综合评分
    /// </summary>
    public int Score { get; set; }

        /// <summary>
    /// 评价内容
    /// </summary>
    public string? Comment { get; set; }

        /// <summary>
    /// 评价人ID
    /// </summary>
    public long EvaluatorId { get; set; }

        /// <summary>
    /// 评价人姓名
    /// </summary>
    public string? EvaluatorName { get; set; }

        /// <summary>
    /// 评价时间
    /// </summary>
    public DateTime EvaluatedAt { get; set; }

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
/// 工单服务评价表导入DTO
/// </summary>
public partial class TaktTicketEvaluationImportDto
{
        /// <summary>
    /// 工单ID
    /// </summary>
    public long TicketId { get; set; }

        /// <summary>
    /// 综合评分
    /// </summary>
    public int Score { get; set; }

        /// <summary>
    /// 评价内容
    /// </summary>
    public string? Comment { get; set; }

        /// <summary>
    /// 评价人ID
    /// </summary>
    public long EvaluatorId { get; set; }

        /// <summary>
    /// 评价人姓名
    /// </summary>
    public string? EvaluatorName { get; set; }

        /// <summary>
    /// 评价时间
    /// </summary>
    public DateTime EvaluatedAt { get; set; }

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
/// 工单服务评价表导出DTO
/// </summary>
public partial class TaktTicketEvaluationExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketEvaluationExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// 工单ID
    /// </summary>
    public long TicketId { get; set; }

        /// <summary>
    /// 综合评分
    /// </summary>
    public int Score { get; set; }

        /// <summary>
    /// 评价内容
    /// </summary>
    public string? Comment { get; set; }

        /// <summary>
    /// 评价人ID
    /// </summary>
    public long EvaluatorId { get; set; }

        /// <summary>
    /// 评价人姓名
    /// </summary>
    public string? EvaluatorName { get; set; }

        /// <summary>
    /// 评价时间
    /// </summary>
    public DateTime EvaluatedAt { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}