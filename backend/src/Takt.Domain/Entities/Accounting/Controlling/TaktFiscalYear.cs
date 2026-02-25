// ========================================
// 项目名称：节拍数字工厂 · Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Accounting.Controlling
// 文件名称：TaktFiscalYear.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：会计年度实体，定义会计年度领域模型（年度、起止日期、状态等），供费用、预算等模块引用
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Accounting.Controlling;

/// <summary>
/// 会计年度实体
/// </summary>
/// <remarks>定义会计年度及其起止日期、状态；费用 TaktExpense、预算 TaktBudget 等通过 FiscalYear（年度数值）与本表对应，便于校验与展示。</remarks>
[SugarTable("takt_accounting_controlling_fiscal_year", "会计年度表")]
[SugarIndex("ix_takt_accounting_controlling_fiscal_year_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_fiscal_year_plant_id", nameof(PlantId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_fiscal_year_company_code_fiscal_year", nameof(CompanyCode), OrderByType.Asc, nameof(FiscalYear), OrderByType.Asc, true)]
[SugarIndex("ix_takt_accounting_controlling_fiscal_year_fiscal_status", nameof(FiscalStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_fiscal_year_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_fiscal_year_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktFiscalYear : TaktEntityBase
{
    /// <summary>
    /// 公司代码（关联公司主数据）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = false)]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 会计年度（如 2025，同一公司内唯一；与 TaktExpense.FiscalYear、TaktBudget.FiscalYear 对应）
    /// </summary>
    [SugarColumn(ColumnName = "fiscal_year", ColumnDescription = "会计年度", ColumnDataType = "int", IsNullable = false)]
    public int FiscalYear { get; set; }

    /// <summary>
    /// 会计年度名称（如 2025年度）
    /// </summary>
    [SugarColumn(ColumnName = "fiscal_year_name", ColumnDescription = "会计年度名称", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string FiscalYearName { get; set; } = string.Empty;

    /// <summary>
    /// 年度开始日期
    /// </summary>
    [SugarColumn(ColumnName = "start_date", ColumnDescription = "开始日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 年度结束日期
    /// </summary>
    [SugarColumn(ColumnName = "end_date", ColumnDescription = "结束日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EndDate { get; set; }

    /// <summary>
    /// 会计年度状态（0=计划，1=开启，2=已关闭）
    /// </summary>
    [SugarColumn(ColumnName = "fiscal_status", ColumnDescription = "会计年度状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int FiscalStatus { get; set; } = 0;

    /// <summary>
    /// 是否当前年度（0=否，1=是；便于筛选当前可用的会计年度）
    /// </summary>
    [SugarColumn(ColumnName = "is_current", ColumnDescription = "是否当前年度", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsCurrent { get; set; } = 0;

    /// <summary>
    /// 排序号（越小越靠前，通常按年度倒序）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

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
