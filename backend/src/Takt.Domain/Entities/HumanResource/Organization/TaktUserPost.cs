// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Organization
// 文件名称：TaktUserPost.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户岗位关联实体，定义用户与岗位的多对多关系
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Organization;

/// <summary>
/// Takt用户岗位关联实体（RBAC：登录用户与岗位的多对多，用于权限/数据范围）
/// 与 TaktEmployeePost（人事：员工档案与岗位）区分。
/// </summary>
[SugarTable("takt_humanresource_organization_userpost", "岗位用户关联表(RBAC)")]
[SugarIndex("ix_takt_humanresource_organization_userpost_PostId_UserId", nameof(PostId), OrderByType.Asc, nameof(UserId), OrderByType.Asc, true)]
[SugarIndex("ix_takt_humanresource_organization_userpost_PostId", nameof(PostId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_userpost_UserId", nameof(UserId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_userpost_ConfigId", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_userpost_IsDeleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktUserPost : TaktEntityBase
{
    /// <summary>
    /// 用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 岗位ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "post_id", ColumnDescription = "岗位ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PostId { get; set; }
}
