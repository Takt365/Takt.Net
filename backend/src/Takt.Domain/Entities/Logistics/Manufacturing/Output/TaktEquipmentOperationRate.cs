#nullable enable
// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.Output
// 文件名称：TaktEquipmentOperationRate.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：机器稼动率实体（生产设备运行效率记录）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Output;

/// <summary>
/// 机器稼动率实体（生产设备运行效率记录）
/// 时间稼动率(%) = 稼动时间 ÷ 负荷时间 × 100%；为 OEE（设备综合效率）基础之一。
/// </summary>
[SugarTable("takt_logistics_manufacturing_output_equipment_operation_rate", "机器稼动率表")]
[SugarIndex("ix_takt_logistics_manufacturing_output_equipment_operation_rate_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_equipment_operation_rate_equipment_code", nameof(EquipmentCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_equipment_operation_rate_production_line", nameof(ProductionLine), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_equipment_operation_rate_time_category", nameof(TimeCategory), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_equipment_operation_rate_start_date", nameof(StartDate), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_equipment_operation_rate_end_date", nameof(EndDate), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_equipment_operation_rate_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_equipment_operation_rate_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktEquipmentOperationRate : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 8, IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 时间类别（1=天，2=周，3=月）
    /// </summary>
    [SugarColumn(ColumnName = "time_category", ColumnDescription = "时间类别", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int TimeCategory { get; set; } = 1;

    /// <summary>
    /// 开始日期
    /// </summary>
    [SugarColumn(ColumnName = "start_date", ColumnDescription = "开始日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// 结束日期
    /// </summary>
    [SugarColumn(ColumnName = "end_date", ColumnDescription = "结束日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime EndDate { get; set; }

    /// <summary>
    /// 周数（1-53）
    /// </summary>
    [SugarColumn(ColumnName = "week_number", ColumnDescription = "周数", ColumnDataType = "int", IsNullable = true)]
    public int? WeekNumber { get; set; }

    /// <summary>
    /// 月份（1-12）
    /// </summary>
    [SugarColumn(ColumnName = "month_number", ColumnDescription = "月份", ColumnDataType = "int", IsNullable = true)]
    public int? MonthNumber { get; set; }

    /// <summary>
    /// 设备编码
    /// </summary>
    [SugarColumn(ColumnName = "equipment_code", ColumnDescription = "设备编码", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string EquipmentCode { get; set; } = string.Empty;

    /// <summary>
    /// 设备名称
    /// </summary>
    [SugarColumn(ColumnName = "equipment_name", ColumnDescription = "设备名称", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string EquipmentName { get; set; } = string.Empty;

    /// <summary>
    /// 设备类型（1=生产设备，2=检测设备，3=包装设备，4=其他）
    /// </summary>
    [SugarColumn(ColumnName = "equipment_type", ColumnDescription = "设备类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int EquipmentType { get; set; } = 1;

    /// <summary>
    /// 生产线
    /// </summary>
    [SugarColumn(ColumnName = "production_line", ColumnDescription = "生产线", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? ProductionLine { get; set; }

    /// <summary>
    /// 班次（1=早班，2=中班，3=晚班）
    /// </summary>
    [SugarColumn(ColumnName = "shift_no", ColumnDescription = "班次", ColumnDataType = "int", IsNullable = false)]
    public int ShiftNo { get; set; }

    /// <summary>
    /// 负荷时间（分钟）。设备在计划内应运行的总时间，即 计划作业时间 - 计划停机时间。
    /// </summary>
    [SugarColumn(ColumnName = "planned_runtime", ColumnDescription = "负荷时间(分钟)", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal PlannedRuntime { get; set; } = 0;

    /// <summary>
    /// 稼动时间（分钟）。设备实际用于生产的时间，即 负荷时间 - 停线损失时间。
    /// </summary>
    [SugarColumn(ColumnName = "actual_runtime", ColumnDescription = "稼动时间(分钟)", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ActualRuntime { get; set; } = 0;

    /// <summary>
    /// 停线损失时间（分钟）。换模/换线、故障、品质异常、缺料等导致的停机。
    /// </summary>
    [SugarColumn(ColumnName = "downtime", ColumnDescription = "停线损失时间(分钟)", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal Downtime { get; set; } = 0;

    /// <summary>
    /// 时间稼动率（%）。计算公式：稼动时间 ÷ 负荷时间 × 100%。
    /// </summary>
    [SugarColumn(ColumnName = "equipment_operation_rate", ColumnDescription = "时间稼动率(%)", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal EquipmentOperationRate { get; set; } = 0;

    /// <summary>
    /// 计划产量
    /// </summary>
    [SugarColumn(ColumnName = "planned_output", ColumnDescription = "计划产量", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal PlannedOutput { get; set; } = 0;

    /// <summary>
    /// 实际产量
    /// </summary>
    [SugarColumn(ColumnName = "actual_output", ColumnDescription = "实际产量", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal ActualOutput { get; set; } = 0;

    /// <summary>
    /// 合格品数量
    /// </summary>
    [SugarColumn(ColumnName = "qualified_quantity", ColumnDescription = "合格品数量", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal QualifiedQuantity { get; set; } = 0;

    /// <summary>
    /// 不良品数量
    /// </summary>
    [SugarColumn(ColumnName = "defective_quantity", ColumnDescription = "不良品数量", ColumnDataType = "decimal", Length = 10, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal DefectiveQuantity { get; set; } = 0;

    /// <summary>
    /// 良品率（%）
    /// </summary>
    [SugarColumn(ColumnName = "yield_rate", ColumnDescription = "良品率(%)", ColumnDataType = "decimal", Length = 5, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal YieldRate { get; set; } = 0;

    /// <summary>
    /// 停机原因类型（1=设备故障，2=换型调试，3=缺料，4=人员不足，5=其他）
    /// </summary>
    [SugarColumn(ColumnName = "downtime_reason_type", ColumnDescription = "停机原因类型", ColumnDataType = "int", IsNullable = true)]
    public int? DowntimeReasonType { get; set; }

    /// <summary>
    /// 停机原因描述
    /// </summary>
    [SugarColumn(ColumnName = "downtime_reason", ColumnDescription = "停机原因描述", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? DowntimeReason { get; set; }

    /// <summary>
    /// 设备状态（1=正常运行，2=故障停机，3=维护保养，4=换型调试，5=其他）
    /// </summary>
    [SugarColumn(ColumnName = "equipment_status", ColumnDescription = "设备状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int EquipmentStatus { get; set; } = 1;

    /// <summary>
    /// 设备操作员
    /// </summary>
    [SugarColumn(ColumnName = "equipment_operator", ColumnDescription = "设备操作员", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? EquipmentOperator { get; set; }

    /// <summary>
    /// 设备维护员
    /// </summary>
    [SugarColumn(ColumnName = "equipment_maintainer", ColumnDescription = "设备维护员", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? EquipmentMaintainer { get; set; }

    /// <summary>
    /// 班组长
    /// </summary>
    [SugarColumn(ColumnName = "team_leader", ColumnDescription = "班组长", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? TeamLeader { get; set; }

    /// <summary>
    /// 状态（0=正常，1=停用）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
