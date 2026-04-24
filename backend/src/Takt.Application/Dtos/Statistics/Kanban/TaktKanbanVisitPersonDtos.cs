// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Application.Dtos.Statistics.Kanban
// 文件名称：TaktKanbanVisitPersonDtos.cs
// 创建时间：2026-04-24
// 创建人：Takt365
// 功能描述：看板来访人员表DTO，由 DtoCategory 配置驱动。UpdateDto 在同时存在 CreateDto 时继承 CreateDto；无 CreateDto 时退化为独立 UpdateDto 全字段形态。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Application.Dtos;
using Takt.Shared.Models;

namespace Takt.Application.Dtos.Statistics.Kanban;

/// <summary>
/// 看板来访人员表Dto
/// </summary>
public partial class TaktKanbanVisitPersonDto : TaktDtosEntityBase
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanVisitPersonDto()
    {
        PersonName = string.Empty;
    }

    /// <summary>
    /// 看板来访人员表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KanbanVisitPersonId { get; set; }

    /// <summary>
    /// 来访记录ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long VisitId { get; set; }
    /// <summary>
    /// 部门
    /// </summary>
    public string? Department { get; set; }
    /// <summary>
    /// 职称
    /// </summary>
    public string? JobTitle { get; set; }
    /// <summary>
    /// 来访人员
    /// </summary>
    public string PersonName { get; set; }
}

/// <summary>
/// 看板来访人员表查询DTO
/// </summary>
public partial class TaktKanbanVisitPersonQueryDto : TaktPagedQuery
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanVisitPersonQueryDto()
    {
    }

    // KeyWords 属性已从基类 TaktPagedQuery 继承，用于模糊查询

    /// <summary>
    /// 看板来访人员表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KanbanVisitPersonId { get; set; }

    /// <summary>
    /// 来访记录ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long? VisitId { get; set; }
    /// <summary>
    /// 部门
    /// </summary>
    public string? Department { get; set; }
    /// <summary>
    /// 职称
    /// </summary>
    public string? JobTitle { get; set; }
    /// <summary>
    /// 来访人员
    /// </summary>
    public string? PersonName { get; set; }

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
/// Takt创建看板来访人员表DTO
/// </summary>
public partial class TaktKanbanVisitPersonCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanVisitPersonCreateDto()
    {
        PersonName = string.Empty;
    }

        /// <summary>
    /// 来访记录ID
    /// </summary>
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long VisitId { get; set; }

        /// <summary>
    /// 部门
    /// </summary>
    public string? Department { get; set; }

        /// <summary>
    /// 职称
    /// </summary>
    public string? JobTitle { get; set; }

        /// <summary>
    /// 来访人员
    /// </summary>
    public string PersonName { get; set; }

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
/// Takt更新看板来访人员表DTO
/// </summary>
public partial class TaktKanbanVisitPersonUpdateDto : TaktKanbanVisitPersonCreateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanVisitPersonUpdateDto()
    {
    }

        /// <summary>
    /// 看板来访人员表（适配字段，序列化为string以避免Javascript精度问题）
    /// </summary>
    [AdaptMember("Id")]
    [JsonConverter(typeof(SqlSugar.ValueToStringConverter))]
    public long KanbanVisitPersonId { get; set; }
}

/// <summary>
/// 看板来访人员表导入模板DTO
/// </summary>
public partial class TaktKanbanVisitPersonTemplateDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanVisitPersonTemplateDto()
    {
        PersonName = string.Empty;
    }

        /// <summary>
    /// 来访记录ID
    /// </summary>
    public long VisitId { get; set; }

        /// <summary>
    /// 部门
    /// </summary>
    public string? Department { get; set; }

        /// <summary>
    /// 职称
    /// </summary>
    public string? JobTitle { get; set; }

        /// <summary>
    /// 来访人员
    /// </summary>
    public string PersonName { get; set; }

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
/// 看板来访人员表导入DTO
/// </summary>
public partial class TaktKanbanVisitPersonImportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanVisitPersonImportDto()
    {
        PersonName = string.Empty;
    }

        /// <summary>
    /// 来访记录ID
    /// </summary>
    public long VisitId { get; set; }

        /// <summary>
    /// 部门
    /// </summary>
    public string? Department { get; set; }

        /// <summary>
    /// 职称
    /// </summary>
    public string? JobTitle { get; set; }

        /// <summary>
    /// 来访人员
    /// </summary>
    public string PersonName { get; set; }

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
/// 看板来访人员表导出DTO
/// </summary>
public partial class TaktKanbanVisitPersonExportDto
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktKanbanVisitPersonExportDto()
    {
        CreatedAt = DateTime.Now;
        PersonName = string.Empty;
    }

        /// <summary>
    /// 来访记录ID
    /// </summary>
    public long VisitId { get; set; }

        /// <summary>
    /// 部门
    /// </summary>
    public string? Department { get; set; }

        /// <summary>
    /// 职称
    /// </summary>
    public string? JobTitle { get; set; }

        /// <summary>
    /// 来访人员
    /// </summary>
    public string PersonName { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedAt { get; set; }
}