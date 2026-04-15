#nullable enable
// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.Bom
// 文件名称：TaktStandardOperationTime.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：标准工序时间实体（基于 SAP PP 标准工时）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Bom;

/// <summary>
/// 标准工序时间实体（基于 SAP PP 标准工时）
/// </summary>
[SugarTable("takt_logistics_manufacturing_bom_standard_operation_time", "标准工序时间表")]
[SugarIndex("ix_takt_logistics_manufacturing_bom_standard_operation_time_material_code", nameof(MaterialCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_bom_standard_operation_time_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_bom_standard_operation_time_work_center", nameof(WorkCenter), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_bom_standard_operation_time_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_bom_standard_operation_time_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktStandardOperationTime : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 8, IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 物料编码
    /// </summary>
    [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string MaterialCode { get; set; } = string.Empty;

    /// <summary>
    /// 工作中心
    /// </summary>
    [SugarColumn(ColumnName = "work_center", ColumnDescription = "工作中心", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string WorkCenter { get; set; } = string.Empty;

    /// <summary>
    /// 工序描述
    /// </summary>
    [SugarColumn(ColumnName = "operation_desc", ColumnDescription = "工序描述", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
    public string? OperationDesc { get; set; }

    /// <summary>
    /// 标准工时（分钟）
    /// </summary>
    [SugarColumn(ColumnName = "standard_minutes", ColumnDescription = "标准工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal StandardMinutes { get; set; } = 0;

    /// <summary>
    /// 工时单位
    /// </summary>
    [SugarColumn(ColumnName = "time_unit", ColumnDescription = "工时单位", ColumnDataType = "nvarchar", Length = 3, IsNullable = false, DefaultValue = "MIN")]
    public string TimeUnit { get; set; } = "MIN";

    /// <summary>
    /// 标准点数
    /// </summary>
    [SugarColumn(ColumnName = "standard_shorts", ColumnDescription = "标准点数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int StandardShorts { get; set; } = 0;

    /// <summary>
    /// 点数单位
    /// </summary>
    [SugarColumn(ColumnName = "points_unit", ColumnDescription = "点数单位", ColumnDataType = "nvarchar", Length = 5, IsNullable = false, DefaultValue = "SHORT")]
    public string PointsUnit { get; set; } = "SHORT";

    /// <summary>
    /// 点数转分钟汇率（1 点数 = 多少分钟）
    /// </summary>
    [SugarColumn(ColumnName = "points_to_minutes_rate", ColumnDescription = "转换汇率", ColumnDataType = "decimal", Length = 8, DecimalDigits = 4, IsNullable = false, DefaultValue = "1")]
    public decimal PointsToMinutesRate { get; set; } = 1;

    /// <summary>
    /// 转换后标准工时（分钟）
    /// </summary>
    [SugarColumn(ColumnName = "converted_minutes", ColumnDescription = "转换工时", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ConvertedMinutes { get; set; } = 0;

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
    /// 审核状态（0=待审核，1=已通过，2=已驳回）
    /// </summary>
    [SugarColumn(ColumnName = "approval_status", ColumnDescription = "审核状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ApprovalStatus { get; set; } = 0;

    /// <summary>
    /// 审核人
    /// </summary>
    [SugarColumn(ColumnName = "approver", ColumnDescription = "审核人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? Approver { get; set; }

    /// <summary>
    /// 审核日期
    /// </summary>
    [SugarColumn(ColumnName = "approval_date", ColumnDescription = "审核日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ApprovalDate { get; set; }
}
