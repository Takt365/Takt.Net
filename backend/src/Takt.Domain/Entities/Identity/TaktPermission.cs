// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Identity
// 文件名称：TaktPermission.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：权限实体，定义权限标识与菜单关联（从 TaktMenu.Permission 拆分）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Identity;

/// <summary>
/// 权限实体
/// </summary>
[SugarTable("takt_identity_permission", "权限表")]
[SugarIndex("ix_takt_identity_permission_permission_code", nameof(PermissionCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_identity_permission_menu_id", nameof(MenuId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_permission_module", nameof(Module), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_permission_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_permission_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktPermission : TaktEntityBase
{
    /// <summary>
    /// 权限标识（唯一，如 routine:activity:list、identity:user:create）
    /// </summary>
    [SugarColumn(ColumnName = "permission_code", ColumnDescription = "权限标识", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string PermissionCode { get; set; } = string.Empty;

    /// <summary>
    /// 权限名称（展示用）
    /// </summary>
    [SugarColumn(ColumnName = "permission_name", ColumnDescription = "权限名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? PermissionName { get; set; }

    /// <summary>
    /// 模块（如 identity、routine、workflow，便于分组）
    /// </summary>
    [SugarColumn(ColumnName = "module", ColumnDescription = "模块", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? Module { get; set; }

    /// <summary>
    /// 关联菜单ID（可选，序列化为 string 以避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "menu_id", ColumnDescription = "关联菜单ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? MenuId { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 权限状态（0=启用，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "permission_status", ColumnDescription = "权限状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PermissionStatus { get; set; } = 0;
}
