// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Accounting.Controlling
// 文件名称：TaktStandardWageRate.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：标准工资率实体，按工厂、年月记录工作天数、销售额、直接/间接人数与工资、直接/间接加班小时与总额
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Accounting.Controlling;

/// <summary>
/// 标准工资率实体。按工厂、年月维护：工作天数（默认21.7）、销售额、直接人数、直接工资、间接人数、间接工资、
/// 直接加班小时、间接加班小时、直接加班总额、间接加班总额、直接工资率、间接工资率。
/// </summary>
[SugarTable("takt_accounting_controlling_standard_wage_rate", "标准工资率表")]
[SugarIndex("ix_takt_accounting_controlling_standard_wage_rate_plant_code", nameof(RelatedPlant), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_standard_wage_rate_year_month", nameof(YearMonth), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_standard_wage_rate_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_standard_wage_rate_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktStandardWageRate : TaktEntityBase
{
    /// <summary>
    /// 公司代码
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 年月（格式 yyyyMM，如 202502）
    /// </summary>
    [SugarColumn(ColumnName = "year_month", ColumnDescription = "年月", ColumnDataType = "nvarchar", Length = 6, IsNullable = false)]
    public string YearMonth { get; set; } = string.Empty;

    /// <summary>
    /// 工作天数（默认 21.7）
    /// </summary>
    [SugarColumn(ColumnName = "working_days", ColumnDescription = "工作天数", ColumnDataType = "decimal", Length = 8, DecimalDigits = 2, IsNullable = false, DefaultValue = "21.7")]
    public decimal WorkingDays { get; set; } = 21.7m;

    /// <summary>
    /// 销售额
    /// </summary>
    [SugarColumn(ColumnName = "sales_amount", ColumnDescription = "销售额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal SalesAmount { get; set; } = 0;

    // 直接
    /// <summary>
    /// 直接人数
    /// </summary>
    [SugarColumn(ColumnName = "direct_labor_count", ColumnDescription = "直接人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int DirectLaborCount { get; set; } = 0;

    /// <summary>
    /// 直接工资
    /// </summary>
    [SugarColumn(ColumnName = "direct_labor_wage", ColumnDescription = "直接工资", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal DirectLaborWage { get; set; } = 0;

    /// <summary>
    /// 直接加班小时
    /// </summary>
    [SugarColumn(ColumnName = "direct_overtime_hours", ColumnDescription = "直接加班小时", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal DirectOvertimeHours { get; set; } = 0;

    /// <summary>
    /// 直接加班总额
    /// </summary>
    [SugarColumn(ColumnName = "direct_overtime_total", ColumnDescription = "直接加班总额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal DirectOvertimeTotal { get; set; } = 0;

    /// <summary>
    /// 直接工资率（元/小时，可由直接工资、直接工时等计算得出）
    /// </summary>
    [SugarColumn(ColumnName = "direct_wage_rate", ColumnDescription = "直接工资率", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal DirectWageRate { get; set; } = 0;

    // 间接
    /// <summary>
    /// 间接人数
    /// </summary>
    [SugarColumn(ColumnName = "indirect_labor_count", ColumnDescription = "间接人数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IndirectLaborCount { get; set; } = 0;

    /// <summary>
    /// 间接工资
    /// </summary>
    [SugarColumn(ColumnName = "indirect_labor_wage", ColumnDescription = "间接工资", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal IndirectLaborWage { get; set; } = 0;

    /// <summary>
    /// 间接加班小时
    /// </summary>
    [SugarColumn(ColumnName = "indirect_overtime_hours", ColumnDescription = "间接加班小时", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal IndirectOvertimeHours { get; set; } = 0;

    /// <summary>
    /// 间接加班总额
    /// </summary>
    [SugarColumn(ColumnName = "indirect_overtime_total", ColumnDescription = "间接加班总额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal IndirectOvertimeTotal { get; set; } = 0;

    /// <summary>
    /// 间接工资率（元/小时，可由间接工资、间接工时等计算得出）
    /// </summary>
    [SugarColumn(ColumnName = "indirect_wage_rate", ColumnDescription = "间接工资率", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal IndirectWageRate { get; set; } = 0;

        /// <summary>
    /// 关联工厂
    /// </summary>
    [SugarColumn(ColumnName = "related_plant", ColumnDescription = "关联工厂", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? RelatedPlant { get; set; }
}
