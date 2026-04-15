// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Identity
// 文件名称：TaktUserRole.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户角色关联实体，定义用户与角色的多对多关系
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Identity;

/// <summary>
/// Takt用户角色关联实体
/// </summary>
[SugarTable("takt_identity_user_role", "用户角色关联表")]
[SugarIndex("ix_takt_identity_user_role_user_id_role_id", nameof(UserId), OrderByType.Asc, nameof(RoleId), OrderByType.Asc, true)]
[SugarIndex("ix_takt_identity_user_role_user_id", nameof(UserId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_user_role_role_id", nameof(RoleId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_user_role_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_identity_user_role_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktUserRole : TaktEntityBase
{
    /// <summary>
    /// 用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 角色ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "role_id", ColumnDescription = "角色ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long RoleId { get; set; }
}
