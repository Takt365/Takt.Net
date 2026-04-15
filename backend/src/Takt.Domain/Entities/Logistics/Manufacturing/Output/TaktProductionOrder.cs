// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing.Output
// 文件名称：TaktProductionOrder.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：生产工单实体，定义生产工单（制造订单）领域模型
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Output;

/// <summary>
/// 生产工单实体
/// </summary>
[SugarTable("takt_logistics_manufacturing_output_production_order", "生产工单表")]
[SugarIndex("ix_takt_logistics_manufacturing_output_production_order_prod_order_code", nameof(ProdOrderCode), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_manufacturing_output_production_order_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_production_order_material_code", nameof(MaterialCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_production_order_status", nameof(Status), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_production_order_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_output_production_order_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktProductionOrder : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 8, IsNullable = false)]
    public string PlantCode { get; set; } = string.Empty;

    /// <summary>
    /// 生产工单类型
    /// ZDTA=製造指図：DTA通常生産
    /// ZDTB=製造指図：DTA改造改修
    /// ZDTC=製造指図：DTA開発試作
    /// ZDTD=製造指図：DTA通常生産 PCBA
    /// ZDTE=製造指図：DTA改造改修 PCBA
    /// ZDTF=製造指図：DTA開発試作 PCBA
    /// </summary>
    [SugarColumn(ColumnName = "prod_order_type", ColumnDescription = "生产工单类型", ColumnDataType = "nvarchar", Length = 10, IsNullable = false, DefaultValue = "ZDTA")]
    public string ProdOrderType { get; set; } = "ZDTA";

    /// <summary>
    /// 生产工单号
    /// </summary>
    [SugarColumn(ColumnName = "prod_order_code", ColumnDescription = "生产工单号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string ProdOrderCode { get; set; } = string.Empty;

    /// <summary>
    /// 物料编码
    /// </summary>
    [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
    public string MaterialCode { get; set; } = string.Empty;

    /// <summary>
    /// 生产工单数量
    /// </summary>
    [SugarColumn(ColumnName = "prod_order_qty", ColumnDescription = "生产工单数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 3, IsNullable = false, DefaultValue = "0")]
    public decimal ProdOrderQty { get; set; } = 0;

    /// <summary>
    /// 已生产数量
    /// </summary>
    [SugarColumn(ColumnName = "produced_qty", ColumnDescription = "已生产数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 3, IsNullable = false, DefaultValue = "0")]
    public decimal ProducedQty { get; set; } = 0;

    /// <summary>
    /// 计量单位
    /// </summary>
    [SugarColumn(ColumnName = "unit_of_measure", ColumnDescription = "计量单位", ColumnDataType = "nvarchar", Length = 10, IsNullable = false)]
    public string UnitOfMeasure { get; set; } = string.Empty;

    /// <summary>
    /// 实际开始日期
    /// </summary>
    [SugarColumn(ColumnName = "actual_start_date", ColumnDescription = "实际开始日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ActualStartDate { get; set; }

    /// <summary>
    /// 实际完成日期
    /// </summary>
    [SugarColumn(ColumnName = "actual_end_date", ColumnDescription = "实际完成日期", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? ActualEndDate { get; set; }

    /// <summary>
    /// 优先级（1=低，2=中，3=高，4=紧急）
    /// </summary>
    [SugarColumn(ColumnName = "priority", ColumnDescription = "优先级", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
    public int Priority { get; set; } = 2;

    /// <summary>
    /// 工作中心
    /// </summary>
    [SugarColumn(ColumnName = "work_center", ColumnDescription = "工作中心", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? WorkCenter { get; set; }

    /// <summary>
    /// 生产线
    /// </summary>
    [SugarColumn(ColumnName = "prod_line", ColumnDescription = "生产线", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? ProdLine { get; set; }

    /// <summary>
    /// 生产批次
    /// </summary>
    [SugarColumn(ColumnName = "prod_batch", ColumnDescription = "生产批次", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? ProdBatch { get; set; }

    /// <summary>
    /// 序列号
    /// </summary>
    [SugarColumn(ColumnName = "serial_no", ColumnDescription = "序列号", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? SerialNo { get; set; }

    /// <summary>
    /// 工艺路线编码
    /// </summary>
    [SugarColumn(ColumnName = "routing_code", ColumnDescription = "工艺路线编码", ColumnDataType = "nvarchar", Length = 20, IsNullable = true)]
    public string? RoutingCode { get; set; }

    /// <summary>
    /// 状态（0=正常，1=生产中，2=已完成）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Status { get; set; } = 0;
}
