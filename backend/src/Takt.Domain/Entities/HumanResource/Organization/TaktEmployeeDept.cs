// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.HumanResource.Organization
// 文件名称：TaktEmployeeDept.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt员工部门关联实体，定义员工档案与部门的多对多关系（人事侧）
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Organization;

/// <summary>
/// Takt员工部门关联实体（人事：员工档案与部门的多对多，用于组织人事关系）
/// 与 TaktUserDept（RBAC：登录用户与部门，用于权限）区分。
/// </summary>
[SugarTable("takt_humanresource_organization_employeedept", "员工部门关联表(人事)")]
[SugarIndex("ix_takt_humanresource_organization_employeedept_deptid_employeeid", nameof(DeptId), OrderByType.Asc, nameof(EmployeeId), OrderByType.Asc, true)]
[SugarIndex("ix_takt_humanresource_organization_employeedept_deptid", nameof(DeptId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_employeedept_employeeid", nameof(EmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_employeedept_configid", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_employeedept_isdeleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEmployeeDept : TaktEntityBase
{
    /// <summary>
    /// 员工ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long EmployeeId { get; set; }

    /// <summary>
    /// 部门ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }
}
