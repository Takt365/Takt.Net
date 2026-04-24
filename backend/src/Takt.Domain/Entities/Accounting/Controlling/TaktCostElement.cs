// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Accounting.Controlling
// 文件名称：TaktCostElement.cs
// 创建时间：2025-03-16
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt成本要素实体，定义成本要素领域模型
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Accounting.Controlling;

/// <summary>
/// Takt成本要素实体
/// </summary>
[SugarTable("takt_accounting_controlling_cost_element", "成本要素表")]
[SugarIndex("ix_takt_accounting_controlling_cost_element_cost_element_code", nameof(CostElementCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_accounting_controlling_cost_element_parent_id", nameof(ParentId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_cost_element_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_cost_element_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_cost_element_cost_element_type", nameof(CostElementType), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_cost_element_cost_element_status", nameof(CostElementStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_cost_element_valid", nameof(ValidFrom), OrderByType.Asc, nameof(ValidTo), OrderByType.Asc)]
public class TaktCostElement : TaktEntityBase
{
    /// <summary>
    /// 公司代码
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 成本要素编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "cost_element_code", ColumnDescription = "成本要素编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string CostElementCode { get; set; } = string.Empty;

    /// <summary>
    /// 成本要素名称
    /// </summary>
    [SugarColumn(ColumnName = "cost_element_name", ColumnDescription = "成本要素名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string CostElementName { get; set; } = string.Empty;

    /// <summary>
    /// 成本要素类型（0=初级成本要素/科目相关，1=次级成本要素/内部分配）
    /// </summary>
    [SugarColumn(ColumnName = "cost_element_type", ColumnDescription = "成本要素类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CostElementType { get; set; } = 0;

    /// <summary>
    /// 成本要素类别（0=人工，1=材料，2=制造费用，3=其他）
    /// </summary>
    [SugarColumn(ColumnName = "cost_element_category", ColumnDescription = "成本要素类别", ColumnDataType = "int", IsNullable = false, DefaultValue = "3")]
    public int CostElementCategory { get; set; } = 3;

    /// <summary>
    /// 父级ID（树形结构，0表示根节点，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父级ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ParentId { get; set; } = 0;

    /// <summary>
    /// 成本要素层级（从1开始）
    /// </summary>
    [SugarColumn(ColumnName = "cost_element_level", ColumnDescription = "成本要素层级", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int CostElementLevel { get; set; } = 1;

    /// <summary>
    /// 成本要素状态（0=启用，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "cost_element_status", ColumnDescription = "成本要素状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CostElementStatus { get; set; } = 0;

    /// <summary>
    /// 生效日期（含当日，默认当前日期）
    /// </summary>
    [SugarColumn(ColumnName = "valid_from", ColumnDescription = "生效日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime ValidFrom { get; set; } = DateTime.Today;

    /// <summary>
    /// 失效日期（含当日，默认 9999-12-31 表示长期有效）
    /// </summary>
    [SugarColumn(ColumnName = "valid_to", ColumnDescription = "失效日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime ValidTo { get; set; } = new DateTime(9999, 12, 31);

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "sort_order", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int SortOrder { get; set; } = 0;
}
