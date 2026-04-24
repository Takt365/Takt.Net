// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.Organization
// 文件名称：TaktDeptDelegate.cs
// 功能描述：部门维度的代理配置（部门 → 多条代理规则）；delegate_mode 取值见字典 hr_delegate_mode（与 Takt.Shared.Constants.TaktDelegateMode 一致）。
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Shared.Constants;

namespace Takt.Domain.Entities.HumanResource.Organization;

/// <summary>
/// 部门代理（一对多：部门 → 多条代理规则；模式见 <see cref="TaktDelegateMode"/>）。
/// </summary>
[SugarTable("takt_humanresource_organization_dept_delegate", "部门代理表")]
[SugarIndex("ix_takt_humanresource_organization_dept_delegate_dept_id", nameof(DeptId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_dept_delegate_mode", nameof(DelegateMode), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_dept_delegate_emp_id", nameof(DelegateEmployeeId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_dept_delegate_dept_ref", nameof(DelegateDeptId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_dept_delegate_post_ref", nameof(DelegatePostId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_dept_delegate_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_dept_delegate_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktDeptDelegate : TaktEntityBase
{
    /// <summary>
    /// 所属部门 Id（外键：<see cref="TaktDept"/>）
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptId { get; set; }

    /// <summary>
    /// 代理模式（字典类型 <see cref="TaktDelegateMode.DictTypeCode"/>，取值见 <see cref="TaktDelegateMode"/>）
    /// </summary>
    [SugarColumn(ColumnName = "delegate_mode", ColumnDescription = "代理模式(字典hr_delegate_mode)", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DelegateMode { get; set; } = TaktDelegateMode.DirectEmployee;

    /// <summary>
    /// 直接代理：被代理人员工 Id（<see cref="TaktDelegateMode.DirectEmployee"/> 时必填）
    /// </summary>
    [SugarColumn(ColumnName = "delegate_employee_id", ColumnDescription = "直接代理-员工ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DelegateEmployeeId { get; set; }

    /// <summary>
    /// 部门规则：被引用部门 Id（<see cref="TaktDelegateMode.DepartmentRule"/> 时必填）
    /// </summary>
    [SugarColumn(ColumnName = "delegate_dept_id", ColumnDescription = "部门规则-引用部门ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DelegateDeptId { get; set; }

    /// <summary>
    /// 岗位规则：被引用岗位 Id（<see cref="TaktDelegateMode.PostRule"/> 时必填）
    /// </summary>
    [SugarColumn(ColumnName = "delegate_post_id", ColumnDescription = "岗位规则-引用岗位ID", ColumnDataType = "bigint", IsNullable = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long? DelegatePostId { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;
}
