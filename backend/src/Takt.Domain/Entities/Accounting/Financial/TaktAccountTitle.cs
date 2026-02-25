// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Accounting.Financial
// 文件名称：TaktAccountTitle.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt 科目（AccountTitle）实体，定义会计科目领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Accounting.Financial;

/// <summary>
/// Takt 科目（AccountTitle）实体
/// </summary>
[SugarTable("takt_accounting_financial_account_title", "会计科目表")]
[SugarIndex("ix_takt_accounting_financial_account_title_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_account_title_plant_id", nameof(PlantId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_account_title_title_code", nameof(TitleCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_accounting_financial_account_title_parent_id", nameof(ParentId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_account_title_title_type", nameof(TitleType), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_account_title_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_account_title_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_account_title_title_status", nameof(TitleStatus), OrderByType.Asc)]
public class TaktAccountTitle : TaktEntityBase
{
    /// <summary>
    /// 公司代码（关联公司主数据）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = false)]
    public string CompanyCode { get; set; } = string.Empty;

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
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = true)]
    public DateTime? EffectiveDate { get; set; }

    /// <summary>
    /// 失效日期（默认 9999-12-31 表示长期有效）
    /// </summary>
    [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "date", IsNullable = true, DefaultValue = "9999-12-31")]
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// 是否统驭科目（0=否，1=是；统驭科目由子账如应收/应付/资产等汇总过账）
    /// </summary>
    [SugarColumn(ColumnName = "is_reconciliation_account", ColumnDescription = "是否统驭科目", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsReconciliationAccount { get; set; } = 0;

    /// <summary>
    /// 科目状态（0=启用，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "title_status", ColumnDescription = "科目状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int TitleStatus { get; set; } = 0;

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
