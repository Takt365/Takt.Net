// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Routine.Business.HelpDesk
// 文件名称：TaktTicketChangeLogDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：工单变更日志表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Business.HelpDesk;

/// <summary>
/// 工单变更日志表Dto
/// </summary>
public partial class TaktTicketChangeLogDto : TaktDtosEntityBase
{
    /// <summary>
    /// 工单变更日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketChangeLogId { get; set; }

    /// <summary>
    /// 工单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketId { get; set; }
    /// <summary>
    /// 工单编号
    /// </summary>
    public string? TicketNo { get; set; }
    /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }
    /// <summary>
    /// 修改工单内容摘要
    /// </summary>
    public string? ChangeSummary { get; set; }
    /// <summary>
    /// 变更字段名
    /// </summary>
    public string? ChangeField { get; set; }
    /// <summary>
    /// 原值
    /// </summary>
    public string? OldValue { get; set; }
    /// <summary>
    /// 新值
    /// </summary>
    public string? NewValue { get; set; }
    /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }
}

/// <summary>
/// 工单变更日志表查询DTO
/// </summary>
public partial class TaktTicketChangeLogQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketChangeLogQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 工单变更日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketChangeLogId { get; set; }

    /// <summary>
    /// 工单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? TicketId { get; set; }
    /// <summary>
    /// 工单编号
    /// </summary>
    public string? TicketNo { get; set; }
    /// <summary>
    /// 变更类型
    /// </summary>
    public int? ChangeType { get; set; }
    /// <summary>
    /// 修改工单内容摘要
    /// </summary>
    public string? ChangeSummary { get; set; }
    /// <summary>
    /// 变更字段名
    /// </summary>
    public string? ChangeField { get; set; }
    /// <summary>
    /// 原值
    /// </summary>
    public string? OldValue { get; set; }
    /// <summary>
    /// 新值
    /// </summary>
    public string? NewValue { get; set; }
    /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

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
/// Takt创建工单变更日志表DTO
/// </summary>
public partial class TaktTicketChangeLogCreateDto
{
        /// <summary>
    /// 工单ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketId { get; set; }

        /// <summary>
    /// 工单编号
    /// </summary>
    public string? TicketNo { get; set; }

        /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }

        /// <summary>
    /// 修改工单内容摘要
    /// </summary>
    public string? ChangeSummary { get; set; }

        /// <summary>
    /// 变更字段名
    /// </summary>
    public string? ChangeField { get; set; }

        /// <summary>
    /// 原值
    /// </summary>
    public string? OldValue { get; set; }

        /// <summary>
    /// 新值
    /// </summary>
    public string? NewValue { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

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
/// Takt更新工单变更日志表DTO
/// </summary>
public partial class TaktTicketChangeLogUpdateDto : TaktTicketChangeLogCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketChangeLogUpdateDto()
    {
    }

        /// <summary>
    /// 工单变更日志表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long TicketChangeLogId { get; set; }
}

/// <summary>
/// 工单变更日志表导入模板DTO
/// </summary>
public partial class TaktTicketChangeLogTemplateDto
{
        /// <summary>
    /// 工单ID
    /// </summary>
    public long TicketId { get; set; }

        /// <summary>
    /// 工单编号
    /// </summary>
    public string? TicketNo { get; set; }

        /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }

        /// <summary>
    /// 修改工单内容摘要
    /// </summary>
    public string? ChangeSummary { get; set; }

        /// <summary>
    /// 变更字段名
    /// </summary>
    public string? ChangeField { get; set; }

        /// <summary>
    /// 原值
    /// </summary>
    public string? OldValue { get; set; }

        /// <summary>
    /// 新值
    /// </summary>
    public string? NewValue { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

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
/// 工单变更日志表导入DTO
/// </summary>
public partial class TaktTicketChangeLogImportDto
{
        /// <summary>
    /// 工单ID
    /// </summary>
    public long TicketId { get; set; }

        /// <summary>
    /// 工单编号
    /// </summary>
    public string? TicketNo { get; set; }

        /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }

        /// <summary>
    /// 修改工单内容摘要
    /// </summary>
    public string? ChangeSummary { get; set; }

        /// <summary>
    /// 变更字段名
    /// </summary>
    public string? ChangeField { get; set; }

        /// <summary>
    /// 原值
    /// </summary>
    public string? OldValue { get; set; }

        /// <summary>
    /// 新值
    /// </summary>
    public string? NewValue { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

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
/// 工单变更日志表导出DTO
/// </summary>
public partial class TaktTicketChangeLogExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktTicketChangeLogExportDto()
    {
        CreatedAt = DateTime.Now;
    }

        /// <summary>
    /// 工单ID
    /// </summary>
    public long TicketId { get; set; }

        /// <summary>
    /// 工单编号
    /// </summary>
    public string? TicketNo { get; set; }

        /// <summary>
    /// 变更类型
    /// </summary>
    public int ChangeType { get; set; }

        /// <summary>
    /// 修改工单内容摘要
    /// </summary>
    public string? ChangeSummary { get; set; }

        /// <summary>
    /// 变更字段名
    /// </summary>
    public string? ChangeField { get; set; }

        /// <summary>
    /// 原值
    /// </summary>
    public string? OldValue { get; set; }

        /// <summary>
    /// 新值
    /// </summary>
    public string? NewValue { get; set; }

        /// <summary>
    /// 变更原因
    /// </summary>
    public string? ChangeReason { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}