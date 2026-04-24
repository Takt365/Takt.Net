// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Statistics.Kanban
// 文件名称：TaktKanbanVisitDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：看板来访公司表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Statistics.Kanban;

/// <summary>
/// 看板来访公司表Dto
/// </summary>
public partial class TaktKanbanVisitDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanVisitDto()
    {
        CompanyName = string.Empty;
    }

    /// <summary>
    /// 看板来访公司表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KanbanVisitId { get; set; }

    /// <summary>
    /// 公司名称
    /// </summary>
    public string CompanyName { get; set; }
    /// <summary>
    /// 参访开始时间
    /// </summary>
    public DateTime VisitStartTime { get; set; }
    /// <summary>
    /// 参访结束时间
    /// </summary>
    public DateTime VisitEndTime { get; set; }

    /// <summary>
    /// 来访人员列表（主子表关系）
    /// </summary>
    public List<long>? PersonIds { get; set; }
}

/// <summary>
/// 看板来访公司表查询DTO
/// </summary>
public partial class TaktKanbanVisitQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanVisitQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 看板来访公司表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KanbanVisitId { get; set; }

    /// <summary>
    /// 公司名称
    /// </summary>
    public string? CompanyName { get; set; }
    /// <summary>
    /// 参访开始时间
    /// </summary>
    public DateTime? VisitStartTime { get; set; }

    /// <summary>
    /// 参访开始时间开始时间
    /// </summary>
    public DateTime? VisitStartTimeStart { get; set; }
    /// <summary>
    /// 参访开始时间结束时间
    /// </summary>
    public DateTime? VisitStartTimeEnd { get; set; }
    /// <summary>
    /// 参访结束时间
    /// </summary>
    public DateTime? VisitEndTime { get; set; }

    /// <summary>
    /// 参访结束时间开始时间
    /// </summary>
    public DateTime? VisitEndTimeStart { get; set; }
    /// <summary>
    /// 参访结束时间结束时间
    /// </summary>
    public DateTime? VisitEndTimeEnd { get; set; }

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
/// Takt创建看板来访公司表DTO
/// </summary>
public partial class TaktKanbanVisitCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanVisitCreateDto()
    {
        CompanyName = string.Empty;
    }

        /// <summary>
    /// 公司名称
    /// </summary>
    public string CompanyName { get; set; }

        /// <summary>
    /// 参访开始时间
    /// </summary>
    public DateTime VisitStartTime { get; set; }

        /// <summary>
    /// 参访结束时间
    /// </summary>
    public DateTime VisitEndTime { get; set; }

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
/// Takt更新看板来访公司表DTO
/// </summary>
public partial class TaktKanbanVisitUpdateDto : TaktKanbanVisitCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanVisitUpdateDto()
    {
    }

        /// <summary>
    /// 看板来访公司表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KanbanVisitId { get; set; }
}

/// <summary>
/// 看板来访公司表导入模板DTO
/// </summary>
public partial class TaktKanbanVisitTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanVisitTemplateDto()
    {
        CompanyName = string.Empty;
    }

        /// <summary>
    /// 公司名称
    /// </summary>
    public string CompanyName { get; set; }

        /// <summary>
    /// 参访开始时间
    /// </summary>
    public DateTime VisitStartTime { get; set; }

        /// <summary>
    /// 参访结束时间
    /// </summary>
    public DateTime VisitEndTime { get; set; }

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
/// 看板来访公司表导入DTO
/// </summary>
public partial class TaktKanbanVisitImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanVisitImportDto()
    {
        CompanyName = string.Empty;
    }

        /// <summary>
    /// 公司名称
    /// </summary>
    public string CompanyName { get; set; }

        /// <summary>
    /// 参访开始时间
    /// </summary>
    public DateTime VisitStartTime { get; set; }

        /// <summary>
    /// 参访结束时间
    /// </summary>
    public DateTime VisitEndTime { get; set; }

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
/// 看板来访公司表导出DTO
/// </summary>
public partial class TaktKanbanVisitExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanVisitExportDto()
    {
        CreatedAt = DateTime.Now;
        CompanyName = string.Empty;
    }

        /// <summary>
    /// 公司名称
    /// </summary>
    public string CompanyName { get; set; }

        /// <summary>
    /// 参访开始时间
    /// </summary>
    public DateTime VisitStartTime { get; set; }

        /// <summary>
    /// 参访结束时间
    /// </summary>
    public DateTime VisitEndTime { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}