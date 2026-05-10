// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Statistics.Kanban
// 文件名称：TaktKanbanScheme.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：通用看板方案定义实体，定义看板的基本信息、数据源、展示配置、刷新策略等
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Statistics.Kanban;

/// <summary>
/// 通用看板方案定义实体（支持数据可视化、实时监控、指标展示等）
/// </summary>
[SugarTable("takt_statistics_kanban_scheme", "看板方案表")]
[SugarIndex("ix_takt_statistics_kanban_scheme_scheme_code", nameof(SchemeCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_statistics_kanban_scheme_scheme_name", nameof(SchemeName), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_kanban_scheme_kanban_type", nameof(KanbanType), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_kanban_scheme_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_statistics_kanban_scheme_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktKanbanScheme : TaktEntityBase
{
    /// <summary>
    /// 看板方案编码（如 KBN-PROD-001、KBN-QUAL-002）
    /// </summary>
    [SugarColumn(ColumnName = "scheme_code", ColumnDescription = "看板方案编码", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SchemeCode { get; set; } = string.Empty;

    /// <summary>
    /// 看板方案名称
    /// </summary>
    [SugarColumn(ColumnName = "scheme_name", ColumnDescription = "看板方案名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SchemeName { get; set; } = string.Empty;

    /// <summary>
    /// 看板类型(生产看板/质量看板/设备看板/库存看板/销售看板/人事看板/财务看板/其他)
    /// </summary>
    [SugarColumn(ColumnName = "kanban_type", ColumnDescription = "看板类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string KanbanType { get; set; } = string.Empty;

    /// <summary>
    /// 看板描述
    /// </summary>
    [SugarColumn(ColumnName = "scheme_description", ColumnDescription = "看板描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SchemeDescription { get; set; } = string.Empty;

    /// <summary>
    /// 数据源配置(JSON格式，定义数据查询接口或SQL)
    /// </summary>
    [SugarColumn(ColumnName = "data_source_config", ColumnDescription = "数据源配置", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
    public string DataSourceConfig { get; set; } = string.Empty;

    /// <summary>
    /// 布局配置(JSON格式，定义看板组件布局和位置)
    /// </summary>
    [SugarColumn(ColumnName = "layout_config", ColumnDescription = "布局配置", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
    public string LayoutConfig { get; set; } = string.Empty;

    /// <summary>
    /// 组件配置(JSON格式，定义各个图表/指标组件的配置)
    /// </summary>
    [SugarColumn(ColumnName = "component_config", ColumnDescription = "组件配置", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ComponentConfig { get; set; } = string.Empty;

    /// <summary>
    /// 刷新策略(实时/5秒/10秒/30秒/1分钟/5分钟/手动)
    /// </summary>
    [SugarColumn(ColumnName = "refresh_strategy", ColumnDescription = "刷新策略", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string RefreshStrategy { get; set; } = string.Empty;

    /// <summary>
    /// 刷新间隔（秒，配合刷新策略使用）
    /// </summary>
    [SugarColumn(ColumnName = "refresh_interval", ColumnDescription = "刷新间隔", ColumnDataType = "int", IsNullable = false, DefaultValue = "30")]
    public int RefreshInterval { get; set; } = 30;

    /// <summary>
    /// 主题风格(深色/浅色/蓝色/绿色/自定义)
    /// </summary>
    [SugarColumn(ColumnName = "theme_style", ColumnDescription = "主题风格", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ThemeStyle { get; set; } = string.Empty;

    /// <summary>
    /// 是否全屏显示(0=否 1=是)
    /// </summary>
    [SugarColumn(ColumnName = "is_fullscreen", ColumnDescription = "是否全屏显示", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsFullscreen { get; set; } = 0;

    /// <summary>
    /// 是否启用告警(0=否 1=是)
    /// </summary>
    [SugarColumn(ColumnName = "enable_alert", ColumnDescription = "是否启用告警", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int EnableAlert { get; set; } = 0;

    /// <summary>
    /// 告警配置(JSON格式，定义阈值和告警规则)
    /// </summary>
    [SugarColumn(ColumnName = "alert_config", ColumnDescription = "告警配置", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
    public string AlertConfig { get; set; } = string.Empty;

    /// <summary>
    /// 过滤条件配置(JSON格式，定义数据筛选条件)
    /// </summary>
    [SugarColumn(ColumnName = "filter_config", ColumnDescription = "过滤条件配置", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
    public string FilterConfig { get; set; } = string.Empty;

    /// <summary>
    /// 排序配置(JSON格式，定义数据排序规则)
    /// </summary>
    [SugarColumn(ColumnName = "sort_config", ColumnDescription = "排序配置", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SortConfig { get; set; } = string.Empty;

    /// <summary>
    /// 应用工厂（多工厂隔离，空表示全部工厂）
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "应用工厂", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 应用车间（空表示全部车间）
    /// </summary>
    [SugarColumn(ColumnName = "workshop_code", ColumnDescription = "应用车间", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string WorkshopCode { get; set; } = string.Empty;

    /// <summary>
    /// 应用生产线（空表示全部生产线）
    /// </summary>
    [SugarColumn(ColumnName = "line_code", ColumnDescription = "应用生产线", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string LineCode { get; set; } = string.Empty;

    /// <summary>
    /// 显示顺序（数字越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "display_order", ColumnDescription = "显示顺序", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DisplayOrder { get; set; } = 0;

    /// <summary>
    /// 状态(0=停用 1=启用)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Status { get; set; } = 1;

    /// <summary>
    /// 是否公开(0=私有 1=公开)
    /// </summary>
    [SugarColumn(ColumnName = "is_public", ColumnDescription = "是否公开", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsPublic { get; set; } = 0;

    /// <summary>
    /// 创建人ID列表（JSON格式，定义可编辑此看板的人员）
    /// </summary>
    [SugarColumn(ColumnName = "creator_ids", ColumnDescription = "创建人ID列表", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CreatorIds { get; set; } = string.Empty;

    /// <summary>
    /// 访问权限配置(JSON格式，定义查看/编辑权限)
    /// </summary>
    [SugarColumn(ColumnName = "access_config", ColumnDescription = "访问权限配置", Length = -1, ColumnDataType = "nvarchar", IsNullable = false)]
    public string AccessConfig { get; set; } = string.Empty;
}
