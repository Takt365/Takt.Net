// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Identity
// 文件名称：TaktRole.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt角色实体，定义角色领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Identity;

/// <summary>
/// Takt角色实体
/// </summary>
[SugarTable("takt_identity_role", "角色信息表")]
[SugarIndex("ix_takt_identity_role_role_code", nameof(RoleCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_identity_role_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_role_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_role_role_status", nameof(RoleStatus), OrderByType.Asc)]
public class TaktRole : TaktEntityBase
{
    /// <summary>
    /// 角色名称
    /// </summary>
    [SugarColumn(ColumnName = "role_name", ColumnDescription = "角色名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// 角色编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "role_code", ColumnDescription = "角色编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string RoleCode { get; set; } = string.Empty;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 数据范围（0=全部数据，1=本部门数据，2=本部门及以下数据，3=仅本人数据，4=自定义数据范围）
    /// </summary>
    [SugarColumn(ColumnName = "data_scope", ColumnDescription = "数据范围", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DataScope { get; set; } = 0;

    /// <summary>
    /// 自定义范围（当DataScope为4时使用，存储部门ID列表，JSON格式或逗号分隔）
    /// </summary>
    [SugarColumn(ColumnName = "custom_scope", ColumnDescription = "自定义范围", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? CustomScope { get; set; }

    /// <summary>
    /// 角色状态（1=启用，0=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "role_status", ColumnDescription = "角色状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int RoleStatus { get; set; } = 1;
}
