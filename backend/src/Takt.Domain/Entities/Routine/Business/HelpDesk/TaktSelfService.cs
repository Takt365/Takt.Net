// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.HelpDesk
// 文件名称：TaktSelfService.cs
// 创建时间：2025-02-26
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt自助服务实体，服务台自助服务项领域模型
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.Business.HelpDesk;

/// <summary>
/// Takt自助服务实体
/// </summary>
[SugarTable("takt_routine_help_desk_self_service", "自助服务表")]
[SugarIndex("ix_takt_routine_help_desk_self_service_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_self_service_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_self_service_service_type", nameof(ServiceType), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_help_desk_self_service_self_service_status", nameof(SelfServiceStatus), OrderByType.Asc)]
public class TaktSelfService : TaktEntityBase
{
    /// <summary>
    /// 自助服务名称
    /// </summary>
    [SugarColumn(ColumnName = "service_name", ColumnDescription = "服务名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string ServiceName { get; set; } = string.Empty;

    /// <summary>
    /// 服务类型（0=链接，1=表单，2=知识引导）
    /// </summary>
    [SugarColumn(ColumnName = "service_type", ColumnDescription = "服务类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ServiceType { get; set; } = 0;

    /// <summary>
    /// 描述
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "描述", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Description { get; set; }

    /// <summary>
    /// 链接地址或表单编码（类型为链接时存URL，类型为表单时存表单编码）
    /// </summary>
    [SugarColumn(ColumnName = "link_or_code", ColumnDescription = "链接或表单编码", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? LinkOrCode { get; set; }

    /// <summary>
    /// 图标或图片URL
    /// </summary>
    [SugarColumn(ColumnName = "icon_url", ColumnDescription = "图标URL", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? IconUrl { get; set; }

    /// <summary>
    /// 自助服务状态（0=启用，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "self_service_status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SelfServiceStatus { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;
}
