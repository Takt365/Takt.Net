// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Accounting.Controlling
// 文件名称：TaktCostElement.cs
// 功能描述：成本要素实体，定义成本要素领域模型（初级/次级成本要素，用于成本归集与分摊）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Accounting.Controlling;

/// <summary>
/// 成本要素实体。用于对成本进行分类归集（如材料、人工、制造费用等），支持树形层级与初级/次级区分。
/// </summary>
[SugarTable("takt_accounting_controlling_cost_element", "成本要素表")]
[SugarIndex("ix_takt_accounting_controlling_cost_element_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_cost_element_plant_id", nameof(PlantId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_cost_element_company_code_cost_element_code", nameof(CompanyCode), OrderByType.Asc, nameof(CostElementCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_accounting_controlling_cost_element_parent_id", nameof(ParentId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_cost_element_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_cost_element_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_cost_element_cost_element_status", nameof(CostElementStatus), OrderByType.Asc)]
public class TaktCostElement : TaktEntityBase
{
    /// <summary>
    /// 公司代码（关联公司主数据）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = false)]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 成本要素编码（同一公司内唯一）
    /// </summary>
    [SugarColumn(ColumnName = "cost_element_code", ColumnDescription = "成本要素编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string CostElementCode { get; set; } = string.Empty;

    /// <summary>
    /// 成本要素名称
    /// </summary>
    [SugarColumn(ColumnName = "cost_element_name", ColumnDescription = "成本要素名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string CostElementName { get; set; } = string.Empty;

    /// <summary>
    /// 父级ID（树形结构，0表示根节点，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父级ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ParentId { get; set; } = 0;

    /// <summary>
    /// 成本要素类型（0=初级成本要素如材料/人工/制造费用，1=次级成本要素如内部作业分摊）
    /// </summary>
    [SugarColumn(ColumnName = "cost_element_type", ColumnDescription = "成本要素类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CostElementType { get; set; } = 0;

    /// <summary>
    /// 成本要素类别（0=材料 1=人工 2=制造费用 3=其他，便于报表与筛选）
    /// </summary>
    [SugarColumn(ColumnName = "cost_element_category", ColumnDescription = "成本要素类别", ColumnDataType = "int", IsNullable = false, DefaultValue = "3")]
    public int CostElementCategory { get; set; } = 3;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 成本要素状态（0=启用，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "cost_element_status", ColumnDescription = "成本要素状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int CostElementStatus { get; set; } = 0;

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
