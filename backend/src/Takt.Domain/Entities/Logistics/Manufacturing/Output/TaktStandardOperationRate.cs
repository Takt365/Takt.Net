// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.Output
// 文件名称：TaktStandardOperationRate.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：标准生产稼动率实体，定义工厂/年度/稼动率类型下的标准稼动率
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Output;

/// <summary>
/// 标准生产稼动率实体
/// </summary>
[SugarTable("takt_logistics_manufacturing_output_standard_operation_rate", "标准生产稼动率表")]
[SugarIndex("ix_takt_logistics_manufacturing_output_standard_operation_rate_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_standard_operation_rate_financial_year", nameof(FinancialYear), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_standard_operation_rate_operation_type", nameof(OperationType), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_standard_operation_rate_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_standard_operation_rate_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktStandardOperationRate : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 8, IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 财务年度
    /// </summary>
    [SugarColumn(ColumnName = "financial_year", ColumnDescription = "财务年度", ColumnDataType = "nvarchar", Length = 4, IsNullable = false)]
    public string FinancialYear { get; set; } = string.Empty;

    /// <summary>
    /// 稼动率类型（1=人员，2=SMT设备，3=测试设备，4=包装设备，5=其他）
    /// </summary>
    [SugarColumn(ColumnName = "operation_type", ColumnDescription = "稼动率类型", ColumnDataType = "int", IsNullable = false)]
    public int OperationType { get; set; }

    /// <summary>
    /// 稼动率（%）
    /// </summary>
    [SugarColumn(ColumnName = "operation_rate", ColumnDescription = "稼动率(%)", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal OperationRate { get; set; } = 0;

    /// <summary>
    /// 生效日期
    /// </summary>
    [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EffectiveDate { get; set; }

    /// <summary>
    /// 失效日期
    /// </summary>
    [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ExpiryDate { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
