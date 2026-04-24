// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Accounting.Financial
// 文件名称：TaktAccountingTitle.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt会计科目实体，定义会计科目领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Accounting.Financial;

/// <summary>
/// Takt会计科目实体
/// </summary>
[SugarTable("takt_accounting_financial_accounting_title", "会计科目表")]
[SugarIndex("ix_takt_accounting_financial_accounting_title_title_code", nameof(TitleCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_accounting_financial_accounting_title_parent_id", nameof(ParentId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_accounting_title_title_type", nameof(TitleType), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_accounting_title_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_accounting_title_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_accounting_title_title_status", nameof(TitleStatus), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_accounting_title_valid", nameof(ValidFrom), OrderByType.Asc, nameof(ValidTo), OrderByType.Asc)]
public class TaktAccountingTitle : TaktEntityBase
{
    /// <summary>
    /// 公司代码
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? CompanyCode { get; set; }

    /// <summary>
    /// 科目编码（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "title_code", ColumnDescription = "科目编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string TitleCode { get; set; } = string.Empty;

    /// <summary>
    /// 科目名称
    /// </summary>
    [SugarColumn(ColumnName = "title_name", ColumnDescription = "科目名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string TitleName { get; set; } = string.Empty;

    /// <summary>
    /// 父级ID（树形结构，0表示根节点，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父级ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ParentId { get; set; } = 0;

    /// <summary>
    /// 科目类型（0=资产，1=负债，2=所有者权益，3=收入，4=费用，5=成本）
    /// </summary>
    [SugarColumn(ColumnName = "title_type", ColumnDescription = "科目类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int TitleType { get; set; } = 0;

    /// <summary>
    /// 余额方向（0=借方，1=贷方）
    /// </summary>
    [SugarColumn(ColumnName = "balance_direction", ColumnDescription = "余额方向", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int BalanceDirection { get; set; } = 0;

    /// <summary>
    /// 科目层级（从1开始）
    /// </summary>
    [SugarColumn(ColumnName = "title_level", ColumnDescription = "科目层级", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int TitleLevel { get; set; } = 1;

    /// <summary>
    /// 是否末级科目（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_leaf", ColumnDescription = "是否末级科目", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int IsLeaf { get; set; } = 1;

    /// <summary>
    /// 是否辅助核算（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_auxiliary", ColumnDescription = "是否辅助核算", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsAuxiliary { get; set; } = 0;

    /// <summary>
    /// 辅助核算类型（0=无，1=部门，2=项目，3=客户，4=供应商，5=员工，6=自定义）
    /// </summary>
    [SugarColumn(ColumnName = "auxiliary_type", ColumnDescription = "辅助核算类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int AuxiliaryType { get; set; } = 0;

    /// <summary>
    /// 是否数量核算（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_quantity", ColumnDescription = "是否数量核算", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsQuantity { get; set; } = 0;

    /// <summary>
    /// 是否外币核算（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_currency", ColumnDescription = "是否外币核算", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsCurrency { get; set; } = 0;

    /// <summary>
    /// 是否现金科目（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_cash", ColumnDescription = "是否现金科目", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsCash { get; set; } = 0;

    /// <summary>
    /// 是否银行科目（0=否，1=是）
    /// </summary>
    [SugarColumn(ColumnName = "is_bank", ColumnDescription = "是否银行科目", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsBank { get; set; } = 0;

    /// <summary>
    /// 关联工厂
    /// </summary>
    [SugarColumn(ColumnName = "related_plant", ColumnDescription = "关联工厂", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? RelatedPlant { get; set; }

        /// <summary>
    /// 科目状态（0=启用，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "title_status", ColumnDescription = "科目状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int TitleStatus { get; set; } = 0;

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
