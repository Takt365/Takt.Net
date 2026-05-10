// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.CompensationBenefits
// 文件名称：TaktSalaryComponent.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：薪资组成实体，定义薪资的各项组成部分（基本工资、津贴、奖金等）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.CompensationBenefits;

/// <summary>
/// 薪资组成实体
/// </summary>
[SugarTable("takt_human_resource_salary_component", "薪资组成表")]
[SugarIndex("ix_takt_human_resource_salary_component_component_code", nameof(ComponentCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_human_resource_salary_component_component_type", nameof(ComponentType), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_salary_component_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_salary_component_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktSalaryComponent : TaktEntityBase
{
    /// <summary>
    /// 组成编码
    /// </summary>
    [SugarColumn(ColumnName = "component_code", ColumnDescription = "组成编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ComponentCode { get; set; } = string.Empty;

    /// <summary>
    /// 组成名称
    /// </summary>
    [SugarColumn(ColumnName = "component_name", ColumnDescription = "组成名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ComponentName { get; set; } = string.Empty;

    /// <summary>
    /// 组成类型(基本工资/岗位津贴/绩效奖金/加班费/交通补贴/餐费补贴/住房补贴/其他)
    /// </summary>
    [SugarColumn(ColumnName = "component_type", ColumnDescription = "组成类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ComponentType { get; set; } = string.Empty;

    /// <summary>
    /// 计算方式(固定/比例/公式/计件)
    /// </summary>
    [SugarColumn(ColumnName = "calculation_method", ColumnDescription = "计算方式", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CalculationMethod { get; set; } = string.Empty;

    /// <summary>
    /// 计算公式
    /// </summary>
    [SugarColumn(ColumnName = "calculation_formula", ColumnDescription = "计算公式", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string CalculationFormula { get; set; } = string.Empty;

    /// <summary>
    /// 固定金额
    /// </summary>
    [SugarColumn(ColumnName = "fixed_amount", ColumnDescription = "固定金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal FixedAmount { get; set; } = 0;

    /// <summary>
    /// 比例(%)
    /// </summary>
    [SugarColumn(ColumnName = "percentage", ColumnDescription = "比例", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal Percentage { get; set; } = 0;

    /// <summary>
    /// 是否计税(0=否 1=是)
    /// </summary>
    [SugarColumn(ColumnName = "is_taxable", ColumnDescription = "是否计税", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsTaxable { get; set; } = 0;

    /// <summary>
    /// 是否纳入社保基数(0=否 1=是)
    /// </summary>
    [SugarColumn(ColumnName = "is_social_security_base", ColumnDescription = "是否纳入社保基数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsSocialSecurityBase { get; set; } = 0;

    /// <summary>
    /// 排序号
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 说明
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "说明", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 状态(0=启用 1=停用)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
