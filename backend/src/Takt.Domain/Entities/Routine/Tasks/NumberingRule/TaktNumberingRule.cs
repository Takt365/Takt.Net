// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.NumberingRule
// 文件名称：TaktNumberingRule.cs
// 创建时间：2025-02-26
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt编码规则实体，定义单据/业务编号规则领域模型
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Tasks.NumberingRule;

/// <summary>
/// Takt编码规则实体
/// </summary>
[SugarTable("takt_routine_numbering_rule", "编码规则表")]
[SugarIndex("ix_takt_routine_numbering_rule_rule_code", nameof(RuleCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_numbering_rule_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_numbering_rule_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_numbering_rule_rule_status", nameof(RuleStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_numbering_rule_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_numbering_rule_dept_code", nameof(DeptCode), OrderByType.Asc)]
public class TaktNumberingRule : TaktEntityBase
{
    /// <summary>
    /// 规则编码（唯一，如 PO、ORDER、INVOICE）
    /// </summary>
    [SugarColumn(ColumnName = "rule_code", ColumnDescription = "规则编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string RuleCode { get; set; } = string.Empty;

    /// <summary>
    /// 规则名称
    /// </summary>
    [SugarColumn(ColumnName = "rule_name", ColumnDescription = "规则名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string RuleName { get; set; } = string.Empty;

    /// <summary>
    /// 公司编码（可选，空表示不按公司区分）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 部门编码（可选，空表示不按部门区分）
    /// </summary>
    [SugarColumn(ColumnName = "dept_code", ColumnDescription = "部门编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? DeptCode { get; set; }

    /// <summary>
    /// 前缀（可选，如 PO、固定字符）
    /// </summary>
    [SugarColumn(ColumnName = "prefix", ColumnDescription = "前缀", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? Prefix { get; set; }

    /// <summary>
    /// 日期格式（可选，如 yyyyMMdd、yyyyMM；空表示不包含日期）
    /// </summary>
    [SugarColumn(ColumnName = "date_format", ColumnDescription = "日期格式", ColumnDataType = "nvarchar", Length = 32, IsNullable = true)]
    public string? DateFormat { get; set; }

    /// <summary>
    /// 序号长度（不足左侧补0，如 5 表示 00001）
    /// </summary>
    [SugarColumn(ColumnName = "number_length", ColumnDescription = "序号长度", ColumnDataType = "int", IsNullable = false, DefaultValue = "5")]
    public int NumberLength { get; set; } = 5;

    /// <summary>
    /// 后缀（可选）
    /// </summary>
    [SugarColumn(ColumnName = "suffix", ColumnDescription = "后缀", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? Suffix { get; set; }

    /// <summary>
    /// 当前序号（下次生成时使用，生成后自增 Step）
    /// </summary>
    [SugarColumn(ColumnName = "current_number", ColumnDescription = "当前序号", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    public long CurrentNumber { get; set; } = 0;

    /// <summary>
    /// 步长（每次递增值，默认 1）
    /// </summary>
    [SugarColumn(ColumnName = "step", ColumnDescription = "步长", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int Step { get; set; } = 1;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;

    /// <summary>
    /// 规则状态（0=启用，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "rule_status", ColumnDescription = "规则状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int RuleStatus { get; set; } = 0;
}
