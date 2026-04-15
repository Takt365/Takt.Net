// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Organization
// 文件名称：TaktDept.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt部门实体，定义部门领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Organization;

/// <summary>
/// Takt部门实体
/// </summary>
[SugarTable("takt_humanresource_organization_dept", "部门表")]
[SugarIndex("ix_takt_humanresource_organization_dept_DeptCode", nameof(DeptCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_humanresource_organization_dept_ParentId", nameof(ParentId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_dept_DeptHeadId", nameof(DeptHeadId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_dept_cost_center_code", nameof(CostCenterCode), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_dept_ConfigId", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_dept_IsDeleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_humanresource_organization_dept_DeptStatus", nameof(DeptStatus), OrderByType.Asc)]
public class TaktDept : TaktEntityBase
{
    /// <summary>
    /// 部门名称
    /// </summary>
    [SugarColumn(ColumnName = "dept_name", ColumnDescription = "部门名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string DeptName { get; set; } = string.Empty;

    /// <summary>
    /// 部门编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "dept_code", ColumnDescription = "部门编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string DeptCode { get; set; } = string.Empty;

    /// <summary>
    /// 父级ID（树形结构，0表示根节点，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父级ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ParentId { get; set; } = 0;

    /// <summary>
    /// 部门负责人对应员工 Id（外键语义：<see cref="Takt.Domain.Entities.HumanResource.Personnel.TaktEmployee"/>）
    /// </summary>
    [SugarColumn(ColumnName = "dept_head_id", ColumnDescription = "部门负责人员工ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long DeptHeadId { get; set; }

    /// <summary>
    /// 绑定成本中心编码（业务外键：<see cref="Takt.Domain.Entities.Accounting.Controlling.TaktCostCenter.CostCenterCode"/>，可空表示未绑定）
    /// </summary>
    [SugarColumn(ColumnName = "cost_center_code", ColumnDescription = "成本中心编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CostCenterCode { get; set; }

    /// <summary>
    /// 部门类型（字典值，参考 sys_dept_type 字典类型，0=直接，1=间接）
    /// </summary>
    [SugarColumn(ColumnName = "dept_type", ColumnDescription = "部门类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DeptType { get; set; } = 0;

    /// <summary>
    /// 部门电话
    /// </summary>
    [SugarColumn(ColumnName = "dept_phone", ColumnDescription = "部门电话", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? DeptPhone { get; set; }

    /// <summary>
    /// 部门邮箱
    /// </summary>
    [SugarColumn(ColumnName = "dept_mail", ColumnDescription = "部门邮箱", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? DeptMail { get; set; }

    /// <summary>
    /// 部门地址
    /// </summary>
    [SugarColumn(ColumnName = "dept_addr", ColumnDescription = "部门地址", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? DeptAddr { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

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
    /// 部门状态（0=启用，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "dept_status", ColumnDescription = "部门状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DeptStatus { get; set; } = 0;

    /// <summary>
    /// 部门代理人列表（外键在子表 <see cref="TaktDeptDelegate.DeptId"/>）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktDeptDelegate.DeptId))]
    public List<TaktDeptDelegate>? DeptDelegates { get; set; }
}
