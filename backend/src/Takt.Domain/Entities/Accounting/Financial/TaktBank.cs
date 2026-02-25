// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Accounting.Financial
// 文件名称：TaktBank.cs
// 功能描述：银行实体，定义银行主数据领域模型（用于银行科目、收付款等）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Accounting.Financial;

/// <summary>
/// 银行实体。维护银行主数据，供银行科目、收付款、对账等使用。
/// </summary>
[SugarTable("takt_accounting_financial_bank", "银行表")]
[SugarIndex("ix_takt_accounting_financial_bank_company_code", nameof(CompanyCode), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_bank_plant_id", nameof(PlantId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_bank_bank_code", nameof(BankCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_accounting_financial_bank_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_bank_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_financial_bank_bank_status", nameof(BankStatus), OrderByType.Asc)]
public class TaktBank : TaktEntityBase
{
    /// <summary>
    /// 公司代码（关联公司主数据）
    /// </summary>
    [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = false)]
    public string CompanyCode { get; set; } = string.Empty;

    /// <summary>
    /// 银行编码（唯一）
    /// </summary>
    [SugarColumn(ColumnName = "bank_code", ColumnDescription = "银行编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string BankCode { get; set; } = string.Empty;

    /// <summary>
    /// 银行名称
    /// </summary>
    [SugarColumn(ColumnName = "bank_name", ColumnDescription = "银行名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string BankName { get; set; } = string.Empty;

    /// <summary>
    /// 简称
    /// </summary>
    [SugarColumn(ColumnName = "short_name", ColumnDescription = "简称", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ShortName { get; set; }

    /// <summary>
    /// Swift 代码 / 联行号
    /// </summary>
    [SugarColumn(ColumnName = "swift_code", ColumnDescription = "Swift代码/联行号", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? SwiftCode { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    [SugarColumn(ColumnName = "address", ColumnDescription = "地址", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Address { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    [SugarColumn(ColumnName = "contact_phone", ColumnDescription = "联系电话", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ContactPhone { get; set; }

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 银行状态（0=启用，1=禁用）
    /// </summary>
    [SugarColumn(ColumnName = "bank_status", ColumnDescription = "银行状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int BankStatus { get; set; } = 0;

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
