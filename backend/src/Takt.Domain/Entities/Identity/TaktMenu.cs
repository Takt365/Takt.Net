// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Identity
// 文件名称：TaktMenu.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt菜单实体，定义菜单领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Identity;

/// <summary>
/// Takt菜单实体
/// </summary>
[SugarTable("takt_identity_menu", "菜单表")]
[SugarIndex("ix_takt_identity_menu_menu_code", nameof(MenuCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_identity_menu_parent_id", nameof(ParentId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_menu_type", nameof(MenuType), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_menu_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_menu_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_menu_menu_status", nameof(MenuStatus), OrderByType.Asc)]
public class TaktMenu : TaktEntityBase
{
    /// <summary>
    /// 菜单名称
    /// </summary>
    [SugarColumn(ColumnName = "menu_name", ColumnDescription = "菜单名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string MenuName { get; set; } = string.Empty;

    /// <summary>
    /// 菜单编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "menu_code", ColumnDescription = "菜单编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string MenuCode { get; set; } = string.Empty;

    /// <summary>
    /// 菜单本地化键（用于多语言翻译）
    /// </summary>
    [SugarColumn(ColumnName = "menu_l10n_key", ColumnDescription = "菜单本地化键", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? MenuL10nKey { get; set; }

    /// <summary>
    /// 菜单父级ID（树形结构，0表示根节点，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "parent_id", ColumnDescription = "菜单父级ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ParentId { get; set; } = 0;

    /// <summary>
    /// 路由路径
    /// </summary>
    [SugarColumn(ColumnName = "path", ColumnDescription = "路由路径", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? Path { get; set; }

    /// <summary>
    /// 组件路径
    /// </summary>
    [SugarColumn(ColumnName = "component", ColumnDescription = "组件路径", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? Component { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    [SugarColumn(ColumnName = "menu_icon", ColumnDescription = "图标", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? MenuIcon { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 菜单类型（0=目录，1=菜单，2=按钮）
    /// </summary>
    [SugarColumn(ColumnName = "menu_type", ColumnDescription = "菜单类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MenuType { get; set; } = 0;

    /// <summary>
    /// 是否可见（0=是，1=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_visible", ColumnDescription = "是否可见", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsVisible { get; set; } = 0;

    /// <summary>
    /// 是否缓存（0=是，1=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_cache", ColumnDescription = "是否缓存", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsCache { get; set; } = 1;

    /// <summary>
    /// 是否外部链接（0=是，1=否）
    /// </summary>
    [SugarColumn(ColumnName = "is_external", ColumnDescription = "是否外部链接", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsExternal { get; set; } = 1;

    /// <summary>
    /// 链接URL
    /// </summary>
    [SugarColumn(ColumnName = "link_url", ColumnDescription = "链接URL", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? LinkUrl { get; set; }

    /// <summary>
    /// 菜单状态（0=启用，1=禁用，3=锁定）
    /// </summary>
    [SugarColumn(ColumnName = "menu_status", ColumnDescription = "菜单状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int MenuStatus { get; set; } = 0;
}
