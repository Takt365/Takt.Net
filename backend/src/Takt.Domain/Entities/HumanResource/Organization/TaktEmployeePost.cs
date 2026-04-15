// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.HumanResource.Organization
// 文件名称：TaktEmployeePost.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工岗位关联实体，定义员工档案与岗位的多对多关系（人事侧）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Organization;

/// <summary>
/// Takt员工岗位关联实体（人事：员工档案与岗位的多对多，用于组织人事关系）
/// 与 TaktUserPost（RBAC：登录用户与岗位，用于权限）区分。
/// </summary>
[SugarTable("takt_humanresource_organization_employeepost", "员工岗位关联表(人事)")]
[SugarIndex("ix_takt_humanresource_organization_employeepost_postid_employeeid", nameof(PostId), OrderByType.Asc, nameof(EmployeeId), OrderByType.Asc, true)]
[SugarIndex("ix_takt_humanresource_organization_employeepost_postid", nameof(PostId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_employeepost_employeeid", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_employeepost_configid", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_employeepost_isdeleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEmployeePost : TaktEntityBase
{
    /// <summary>
    /// 员工ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 岗位ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "post_id", ColumnDescription = "岗位ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PostId { get; set; }
}
