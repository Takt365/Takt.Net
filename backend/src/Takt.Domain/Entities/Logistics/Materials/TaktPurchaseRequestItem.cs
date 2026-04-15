// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Material
// 文件名称：TaktPurchaseRequestItem.cs
// 创建时间：2025-01-20
// 创建人：Takt365(Cursor AI)
// 功能描述：Takt采购申请明细实体，定义采购申请明细领域模型
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Materials;

/// <summary>
/// Takt采购申请明细实体
/// </summary>
[SugarTable("takt_logistics_materials_purchase_request_item", "采购申请明细表")]
[SugarIndex("ix_takt_logistics_materials_purchase_request_item_request_id", nameof(RequestId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_request_item_material_id", nameof(MaterialId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_request_item_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_request_item_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktPurchaseRequestItem : TaktEntityBase
{
    /// <summary>
    /// 申请ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "request_id", ColumnDescription = "申请ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long RequestId { get; set; }

    /// <summary>
    /// 申请编码
    /// </summary>
    [SugarColumn(ColumnName = "request_code", ColumnDescription = "申请编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string RequestCode { get; set; } = string.Empty;

    /// <summary>
    /// 物料ID（序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "material_id", ColumnDescription = "物料ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long MaterialId { get; set; }

    /// <summary>
    /// 物料编码
    /// </summary>
    [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string MaterialCode { get; set; } = string.Empty;

    /// <summary>
    /// 物料名称
    /// </summary>
    [SugarColumn(ColumnName = "material_name", ColumnDescription = "物料名称", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string MaterialName { get; set; } = string.Empty;

    /// <summary>
    /// 物料规格
    /// </summary>
    [SugarColumn(ColumnName = "material_specification", ColumnDescription = "物料规格", ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
    public string? MaterialSpecification { get; set; }

    /// <summary>
    /// 申请单位
    /// </summary>
    [SugarColumn(ColumnName = "request_unit", ColumnDescription = "申请单位", ColumnDataType = "nvarchar", Length = 20, IsNullable = false, DefaultValue = "个")]
    public string RequestUnit { get; set; } = "个";

    /// <summary>
    /// 申请数量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "request_quantity", ColumnDescription = "申请数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal RequestQuantity { get; set; } = 0;

    /// <summary>
    /// 已转订单数量（基本单位数量）
    /// </summary>
    [SugarColumn(ColumnName = "converted_quantity", ColumnDescription = "已转订单数量", ColumnDataType = "decimal", Length = 18, DecimalDigits = 4, IsNullable = false, DefaultValue = "0")]
    public decimal ConvertedQuantity { get; set; } = 0;

    /// <summary>
    /// 预计单价（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "estimated_unit_price", ColumnDescription = "预计单价", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal EstimatedUnitPrice { get; set; } = 0;

    /// <summary>
    /// 预计金额（精确到分，存储为整数，单位为分）
    /// </summary>
    [SugarColumn(ColumnName = "estimated_amount", ColumnDescription = "预计金额", ColumnDataType = "decimal", Length = 18, DecimalDigits = 2, IsNullable = false, DefaultValue = "0")]
    public decimal EstimatedAmount { get; set; } = 0;

    /// <summary>
    /// 行号（申请明细行号）
    /// </summary>
    [SugarColumn(ColumnName = "line_number", ColumnDescription = "行号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LineNumber { get; set; } = 0;
}
