// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Organization
// 文件名称：TaktUserDept.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt用户部门关联实体，定义用户与部门的多对多关系
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Organization;

/// <summary>
/// Takt用户部门关联实体（RBAC：登录用户与部门的多对多，用于权限/数据范围）
/// 与 TaktEmployeeDept（人事：员工档案与部门）区分。
/// </summary>
[SugarTable("takt_humanresource_organization_userdept", "部门用户关联表(RBAC)")]
[SugarIndex("ix_takt_humanresource_organization_userdept_DeptId_UserId", nameof(DeptId), OrderByType.Asc, nameof(UserId), OrderByType.Asc, true)]
[SugarIndex("ix_takt_humanresource_organization_userdept_DeptId", nameof(DeptId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_userdept_UserId", nameof(UserId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_userdept_ConfigId", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_userdept_IsDeleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktUserDept : TaktEntityBase
{
    /// <summary>
    /// 用户ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long UserId { get; set; }

    /// <summary>
    /// 部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }
}
