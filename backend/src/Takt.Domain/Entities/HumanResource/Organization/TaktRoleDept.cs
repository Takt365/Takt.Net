// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Organization
// 文件名称：TaktRoleDept.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt角色部门关联实体，定义角色与部门的多对多关系
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Organization;

/// <summary>
/// Takt角色部门关联实体
/// </summary>
[SugarTable("takt_humanresource_organization_roledept", "角色部门关联表")]
[SugarIndex("ix_takt_humanresource_organization_roledept_RoleId_DeptId", nameof(RoleId), OrderByType.Asc, nameof(DeptId), OrderByType.Asc, true)]
[SugarIndex("ix_takt_humanresource_organization_roledept_RoleId", nameof(RoleId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_roledept_DeptId", nameof(DeptId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_roledept_ConfigId", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_roledept_IsDeleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktRoleDept : TaktEntityBase
{
    /// <summary>
    /// 角色ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "role_id", ColumnDescription = "角色ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long RoleId { get; set; }

    /// <summary>
    /// 部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }
}
