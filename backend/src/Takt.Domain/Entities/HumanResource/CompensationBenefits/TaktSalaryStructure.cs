// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.HumanResource.CompensationBenefits
// 文件名称：TaktSalaryStructure.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：薪资结构实体，定义薪资等级、薪级薪档体系
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.HumanResource.CompensationBenefits;

/// <summary>
/// 薪资结构实体
/// </summary>
[SugarTable("takt_human_resource_salary_structure", "薪资结构表")]
[SugarIndex("ix_takt_human_resource_salary_structure_structure_code", nameof(StructureCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_human_resource_salary_structure_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_human_resource_salary_structure_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktSalaryStructure : TaktEntityBase
{
    /// <summary>
    /// 薪资结构编码
    /// </summary>
    [SugarColumn(ColumnName = "structure_code", ColumnDescription = "薪资结构编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string StructureCode { get; set; } = string.Empty;

    /// <summary>
    /// 薪资结构名称
    /// </summary>
    [SugarColumn(ColumnName = "structure_name", ColumnDescription = "薪资结构名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string StructureName { get; set; } = string.Empty;

    /// <summary>
    /// 薪资等级(如：P1-P10, M1-M5)
    /// </summary>
    [SugarColumn(ColumnName = "salary_level", ColumnDescription = "薪资等级", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SalaryLevel { get; set; } = string.Empty;

    /// <summary>
    /// 薪资档次
    /// </summary>
    [SugarColumn(ColumnName = "salary_grade", ColumnDescription = "薪资档次", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string SalaryGrade { get; set; } = string.Empty;

    /// <summary>
    /// 最低薪资
    /// </summary>
    [SugarColumn(ColumnName = "min_salary", ColumnDescription = "最低薪资", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal MinSalary { get; set; } = 0;

    /// <summary>
    /// 中位薪资
    /// </summary>
    [SugarColumn(ColumnName = "mid_salary", ColumnDescription = "中位薪资", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal MidSalary { get; set; } = 0;

    /// <summary>
    /// 最高薪资
    /// </summary>
    [SugarColumn(ColumnName = "max_salary", ColumnDescription = "最高薪资", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal MaxSalary { get; set; } = 0;

    /// <summary>
    /// 适用部门
    /// </summary>
    [SugarColumn(ColumnName = "applicable_department", ColumnDescription = "适用部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ApplicableDepartment { get; set; } = string.Empty;

    /// <summary>
    /// 适用岗位
    /// </summary>
    [SugarColumn(ColumnName = "applicable_position", ColumnDescription = "适用岗位", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string ApplicablePosition { get; set; } = string.Empty;

    /// <summary>
    /// 说明
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "说明", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = false)]
    public DateTime EffectiveDate { get; set; }

    /// <summary>
    /// 状态(0=启用 1=停用)
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
