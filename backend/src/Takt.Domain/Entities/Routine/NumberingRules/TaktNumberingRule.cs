// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Routine.NumberingRules
// 文件名称：TaktNumberingRule.cs
// 创建时间：2025-02-21
// 创建人：Takt365(Cursor AI)
// 功能描述：单据编码规则实体，定义各类业务单据的编号生成规则（前缀、后缀、日期格式、流水号及重置周期）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Routine.NumberingRules;

/// <summary>
/// 单据编码规则实体
/// </summary>
/// <remarks>用于生成业务单据唯一编号，规则编码唯一；生成逻辑由应用层按前缀+日期部分+流水号+后缀拼接，流水号按重置周期归零。</remarks>
[SugarTable("takt_routine_numbering_rule", "编码规则")]
[SugarIndex("ix_takt_routine_numbering_rule_rule_code", nameof(RuleCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_routine_numbering_rule_document_type", nameof(DocumentType), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_numbering_rule_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_numbering_rule_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_routine_numbering_rule_rule_status", nameof(RuleStatus), OrderByType.Asc)]
public class TaktNumberingRule : TaktEntityBase
{
    /// <summary>
    /// 规则编码（唯一索引，业务键，如 PurchaseOrder、SalesOrder、Announcement）
    /// </summary>
    [SugarColumn(ColumnName = "rule_code", ColumnDescription = "规则编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string RuleCode { get; set; } = string.Empty;

    /// <summary>
    /// 规则名称
    /// </summary>
    [SugarColumn(ColumnName = "rule_name", ColumnDescription = "规则名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string RuleName { get; set; } = string.Empty;

    /// <summary>
    /// 单据类型（与规则编码对应，如采购单、销售单、公告等；便于分类查询）
    /// </summary>
    [SugarColumn(ColumnName = "document_type", ColumnDescription = "单据类型", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string DocumentType { get; set; } = string.Empty;

    /// <summary>
    /// 公司代码（关联公司主数据；为空表示规则不限定公司）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = true)]
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 部门编码（关联部门；为空表示规则不限定部门）
    /// </summary>
    [SugarColumn(ColumnName = "dept_code", ColumnDescription = "部门编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? DeptCode { get; set; }

    /// <summary>
    /// 编号前缀（如 PO、SO、ANN；可为空表示无前缀）
    /// </summary>
    [SugarColumn(ColumnName = "prefix", ColumnDescription = "编号前缀", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? Prefix { get; set; }

    /// <summary>
    /// 日期部分格式（如 yyyyMMdd、yyyyMM、yyyy；为空表示不包含日期）
    /// </summary>
    [SugarColumn(ColumnName = "date_format", ColumnDescription = "日期格式", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? DateFormat { get; set; }

    /// <summary>
    /// 流水号位数（不足左侧补零，如 5 表示 00001～99999）
    /// </summary>
    [SugarColumn(ColumnName = "serial_length", ColumnDescription = "流水号位数", ColumnDataType = "int", IsNullable = false, DefaultValue = "5")]
    public int SerialLength { get; set; } = 5;

    /// <summary>
    /// 编号后缀（可为空表示无后缀）
    /// </summary>
    [SugarColumn(ColumnName = "suffix", ColumnDescription = "编号后缀", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? Suffix { get; set; }

    /// <summary>
    /// 当前流水号值（上次生成后的值，下次生成时+1 再格式化）
    /// </summary>
    [SugarColumn(ColumnName = "current_value", ColumnDescription = "当前流水号", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    public long CurrentValue { get; set; } = 0;

    /// <summary>
    /// 重置周期（0=不重置，1=按日，2=按月，3=按年；到周期后 CurrentValue 归零或按新周期重新计数）
    /// </summary>
    [SugarColumn(ColumnName = "reset_cycle", ColumnDescription = "重置周期", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ResetCycle { get; set; } = 0;

    /// <summary>
    /// 上次生成所属周期键（用于按周期重置流水号；按日=yyyyMMdd，按月=yyyyMM，按年=yyyy；周期切换时与当前周期键不同则 CurrentValue 归零）
    /// </summary>
    [SugarColumn(ColumnName = "last_cycle_key", ColumnDescription = "上次周期键", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? LastCycleKey { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 规则状态（0=启用，1=禁用；禁用后不再生成新编号）
    /// </summary>
    [SugarColumn(ColumnName = "rule_status", ColumnDescription = "规则状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int RuleStatus { get; set; } = 0;
}
