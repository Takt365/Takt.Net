// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Accounting.Controlling
// 文件名称：TaktWageRate.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：工资率实体，按工厂、年月记录工作天数、销售额、直接/间接人数与工资、直接/间接加班小时与总额
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Accounting.Controlling;

/// <summary>
/// 工资率实体。按工厂、年月、工资率类别维护：工作天数（默认21.7）、销售额、直接人数、直接工资、间接人数、间接工资、
/// 直接加班小时、间接加班小时、直接加班总额、间接加班总额、直接工资率、间接工资率。
/// </summary>
[SugarTable("takt_accounting_controlling_wage_rate", "工资率表")]
[SugarIndex("ix_takt_accounting_controlling_wage_rate_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_wage_rate_plant_id", nameof(PlantId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_wage_rate_year_month", nameof(YearMonth), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_wage_rate_wage_rate_type", nameof(WageRateType), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_wage_rate_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_wage_rate_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktWageRate : TaktEntityBase
{
    /// <summary>
    /// 公司代码（关联公司主数据）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = false)]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 年月（格式 yyyyMM，如 202502）
    /// </summary>
    [SugarColumn(ColumnName = "year_month", ColumnDescription = "年月", ColumnDataType = "nvarchar", Length = 6, IsNullable = false)]
    public string YearMonth { get; set; } = string.Empty;

    /// <summary>
    /// 工资率类别（0=标准，1=预算，2=实际）
    /// </summary>
    [SugarColumn(ColumnName = "wage_rate_type", ColumnDescription = "工资率类别", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int WageRateType { get; set; } = 0;

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
    /// 工厂ID（关联 TaktPlant.Id；序列化为 string 避免前端精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "plant_id", ColumnDescription = "工厂ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long PlantId { get; set; }

    /// <summary>
    /// 工厂代码（冗余，便于列表展示；来源于 TaktPlant.PlantCode）
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? PlantCode { get; set; }
}
