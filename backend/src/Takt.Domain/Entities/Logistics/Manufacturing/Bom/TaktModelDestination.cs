// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Manufacturing
// 文件名称：TaktModelDestination.cs
// 创建时间：2025-02-02
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt型号目的地实体，定义机型/型号与出货目的地的关联
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Bom;

/// <summary>
/// Takt型号目的地实体（物料名称、机种名称、仕向地名称）
/// </summary>
[SugarTable("takt_logistics_manufacturing_model_destination", "型号目的地表")]
[SugarIndex("ix_takt_logistics_manufacturing_model_destination_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_model_destination_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_model_destination_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_manufacturing_model_destination_order_num", nameof(OrderNum), OrderByType.Asc)]
public class TaktModelDestination : TaktEntityBase
{
    /// <summary>
    /// 工厂代码
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? PlantCode { get; set; }

    /// <summary>
    /// 物料名称
    /// </summary>
    [SugarColumn(ColumnName = "material_name", ColumnDescription = "物料名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string MaterialName { get; set; } = string.Empty;

    /// <summary>
    /// 机种名称
    /// </summary>
    [SugarColumn(ColumnName = "model_name", ColumnDescription = "机种名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string ModelName { get; set; } = string.Empty;

    /// <summary>
    /// 仕向地名称
    /// </summary>
    [SugarColumn(ColumnName = "destination_name", ColumnDescription = "仕向地名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string DestinationName { get; set; } = string.Empty;

    /// <summary>
    /// 排序号（越小越靠前）
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;
}
