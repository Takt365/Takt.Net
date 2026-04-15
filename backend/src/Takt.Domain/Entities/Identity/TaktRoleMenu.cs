// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Identity
// 文件名称：TaktRoleMenu.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt角色菜单关联实体，定义角色与菜单的多对多关系
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Identity;

/// <summary>
/// Takt角色菜单关联实体
/// </summary>
[SugarTable("takt_identity_role_menu", "角色菜单关联表")]
[SugarIndex("ix_takt_identity_role_menu_role_id_menu_id", nameof(RoleId), OrderByType.Asc, nameof(MenuId), OrderByType.Asc, true)]
[SugarIndex("ix_takt_identity_role_menu_role_id", nameof(RoleId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_role_menu_menu_id", nameof(MenuId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_role_menu_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_role_menu_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktRoleMenu : TaktEntityBase
{
    /// <summary>
    /// 角色ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "role_id", ColumnDescription = "角色ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long RoleId { get; set; }

    /// <summary>
    /// 菜单ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "menu_id", ColumnDescription = "菜单ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long MenuId { get; set; }
}
