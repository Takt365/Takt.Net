// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Identity
// 文件名称：TaktRolePermission.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：角色权限关联实体，定义角色与权限的多对多关系
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Identity;

/// <summary>
/// 角色权限关联实体
/// </summary>
[SugarTable("takt_identity_role_permission", "角色权限关联表")]
[SugarIndex("ix_takt_identity_role_permission_role_id_permission_id", nameof(RoleId), OrderByType.Asc, nameof(PermissionId), OrderByType.Asc, true)]
[SugarIndex("ix_takt_identity_role_permission_role_id", nameof(RoleId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_role_permission_permission_id", nameof(PermissionId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_role_permission_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_role_permission_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktRolePermission : TaktEntityBase
{
    /// <summary>
    /// 角色ID（序列化为 string 以避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "role_id", ColumnDescription = "角色ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long RoleId { get; set; }

    /// <summary>
    /// 权限ID（序列化为 string 以避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "permission_id", ColumnDescription = "权限ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PermissionId { get; set; }
}
