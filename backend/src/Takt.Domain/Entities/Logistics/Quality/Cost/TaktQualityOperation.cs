// ========================================
// 项目名称:节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间:Takt.Domain.Entities.Logistics.Quality.Cost
// 文件名称:TaktQualityOperation.cs
// 创建时间:2026-05-07
// 创建人:Takt365(Qoder AI)
// 功能描述:品质业务主表,用于记录品质业务的基础信息(年月)及汇总数据
//
// 版权信息:Copyright (c) 2025 Takt  All rights reserved.
// 免责声明:此软件使用 MIT License,作者不承担任何使用风险。
// ========================================

using Newtonsoft.Json;
using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Cost;

/// <summary>
/// 品质业务主表,用于记录品质业务的基础信息(年月、顾客)及汇总数据
/// </summary>
[SugarTable("takt_logistics_quality_operation", "品质业务主表")]
[SugarIndex("ix_takt_logistics_quality_operation_operation_no", nameof(OperationNo), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_quality_operation_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_operation_operation_month", nameof(OperationMonth), OrderByType.Desc)]
[SugarIndex("ix_takt_logistics_quality_operation_customer_name", nameof(CustomerName), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_operation_qo_unique", nameof(PlantCode), OrderByType.Asc, nameof(OperationMonth), OrderByType.Asc, nameof(CustomerName), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_quality_operation_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_operation_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktQualityOperation : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 品质业务编号(唯一,如:QO-2026-0001)
    /// </summary>
    [SugarColumn(ColumnName = "operation_no", ColumnDescription = "品质业务编号", Length = 30, ColumnDataType = "nvarchar", IsNullable = false)]
    public string OperationNo { get; set; } = string.Empty;

    // ==================== 基础日期与信息 ====================

    /// <summary>
    /// 业务年月(格式:2026-05)
    /// </summary>
    [SugarColumn(ColumnName = "operation_month", ColumnDescription = "业务年月", Length = 7, ColumnDataType = "nvarchar", IsNullable = false)]
    public string OperationMonth { get; set; } = string.Empty;

    /// <summary>
    /// 顾客名
    /// </summary>
    [SugarColumn(ColumnName = "customer_name", ColumnDescription = "顾客名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? CustomerName { get; set; }

    /// <summary>
    /// Debit Note No
    /// </summary>
    [SugarColumn(ColumnName = "debit_note_no", ColumnDescription = "Debit Note No", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? DebitNoteNo { get; set; }

    /// <summary>
    /// 记录者
    /// </summary>
    [SugarColumn(ColumnName = "recorder", ColumnDescription = "记录者", Length = 30, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Recorder { get; set; }

    // ==================== 汇总信息 ====================

    /// <summary>
    /// 质量总成本(元,自动计算 = 各子表费用合计)
    /// </summary>
    [SugarColumn(ColumnName = "total_quality_cost", ColumnDescription = "质量总成本", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal TotalQualityCost { get; set; } = 0;

    /// <summary>
    /// 成本币种(CNY/USD/JPY等)
    /// </summary>
    [SugarColumn(ColumnName = "cost_currency", ColumnDescription = "成本币种", Length = 10, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "CNY")]
    public string CostCurrency { get; set; } = "CNY";

    // ==================== 导航关系 ====================

    /// <summary>
    /// 来料检验费用明细列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktQualityOperationIncoming.QualityOperationId))]
    public List<TaktQualityOperationIncoming>? IncomingItems { get; set; }

    /// <summary>
    /// 初期/定期检定费用明细列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktQualityOperationFirstArticle.QualityOperationId))]
    public List<TaktQualityOperationFirstArticle>? FirstArticleItems { get; set; }

    /// <summary>
    /// 设备校正费用明细列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktQualityOperationCalibration.QualityOperationId))]
    public List<TaktQualityOperationCalibration>? CalibrationItems { get; set; }

    /// <summary>
    /// 其他通常业务费用明细列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktQualityOperationOther.QualityOperationId))]
    public List<TaktQualityOperationOther>? OtherItems { get; set; }

    /// <summary>
    /// 出货检验费用明细列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktQualityOperationOutgoing.QualityOperationId))]
    public List<TaktQualityOperationOutgoing>? OutgoingItems { get; set; }

    /// <summary>
    /// 信赖性评价/ORT费用明细列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktQualityOperationReliability.QualityOperationId))]
    public List<TaktQualityOperationReliability>? ReliabilityItems { get; set; }

    /// <summary>
    /// 顾客品质要求对应费用明细列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktQualityOperationCustomerResponse.QualityOperationId))]
    public List<TaktQualityOperationCustomerResponse>? CustomerResponseItems { get; set; }
}
