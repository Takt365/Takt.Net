// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Serial
// 文件名称：TaktProductSerialOutbound.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：产品序列号出库主表实体，记录产品序列号出库的基本信息
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Serial;

/// <summary>
/// 产品序列号出库主表实体
/// </summary>
[SugarTable("takt_logistics_product_serial_outbound", "产品序列号出库表")]
[SugarIndex("ix_takt_logistics_product_serial_outbound_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_product_serial_outbound_outbound_unique", nameof(PlantCode), OrderByType.Asc, nameof(OutboundNo), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_product_serial_outbound_outbound_date", nameof(OutboundDate), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_product_serial_outbound_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktProductSerialOutbound : TaktEntityBase
{
    /// <summary>
    /// 工厂代码(4位字母数字组合)
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = false, DefaultValue = "C100")]
    public string PlantCode { get; set; } = "C100";

    /// <summary>
    /// 出库单号(组合唯一索引:PlantCode + OutboundNo)
    /// </summary>
    [SugarColumn(ColumnName = "outbound_no", ColumnDescription = "出库单号", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string OutboundNo { get; set; } = string.Empty;
    
    /// <summary>
    /// 出货发票号
    /// </summary>
    [SugarColumn(ColumnName = "shipping_invoice_no", ColumnDescription = "出货发票号", ColumnDataType = "nvarchar", Length = 50, IsNullable = false, DefaultValue = "")]
    public string ShippingInvoiceNo { get; set; } = string.Empty;
    
    /// <summary>
    /// 出库日期
    /// </summary>
    [SugarColumn(ColumnName = "outbound_date", ColumnDescription = "出库日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime OutboundDate { get; set; } = DateTime.Today;

    /// <summary>
    /// 仕向地(目的地)
    /// </summary>
    [SugarColumn(ColumnName = "destination", ColumnDescription = "仕向地", ColumnDataType = "nvarchar", Length = 200, IsNullable = false, DefaultValue = "")]
    public string Destination { get; set; } = string.Empty;

    /// <summary>
    /// 运输方式(0=海运,1=空运,2=陆运,3=铁路,4=快递,5=其他)
    /// </summary>
    [SugarColumn(ColumnName = "shipping_method", ColumnDescription = "运输方式", ColumnDataType = "nvarchar", Length = 50, IsNullable = false, DefaultValue = "")]
    public string ShippingMethod { get; set; } = string.Empty;

    /// <summary>
    /// 目的地港
    /// </summary>
    [SugarColumn(ColumnName = "destination_port", ColumnDescription = "目的地港", ColumnDataType = "nvarchar", Length = 200, IsNullable = false, DefaultValue = "")]
    public string DestinationPort { get; set; } = string.Empty;

    /// <summary>
    /// 出库类型(0=销售出库,1=生产领料,2=退货出库,3=调拨出库,4=报废出库,5=序列号出库,6=其他)
    /// </summary>
    [SugarColumn(ColumnName = "outbound_type", ColumnDescription = "出库类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "5")]
    public int OutboundType { get; set; } = 5;

    /// <summary>
    /// 仓库编码
    /// </summary>
    [SugarColumn(ColumnName = "warehouse_code", ColumnDescription = "仓库编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false, DefaultValue = "C008")]
    public string WarehouseCode { get; set; } = "C008";

    /// <summary>
    /// 库位编码
    /// </summary>
    [SugarColumn(ColumnName = "location_code", ColumnDescription = "库位编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false, DefaultValue = "1F-2")]
    public string LocationCode { get; set; } = "1F-2";

    /// <summary>
    /// 关联公司
    /// </summary>
    [SugarColumn(ColumnName = "related_company", ColumnDescription = "关联公司", ColumnDataType = "nvarchar", Length = 50, IsNullable = false, DefaultValue = "2300")]
    public string RelatedCompany { get; set; } = "2300";

    /// <summary>
    /// 总数量
    /// </summary>
    [SugarColumn(ColumnName = "total_quantity", ColumnDescription = "总数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int TotalQuantity { get; set; } = 0;

    /// <summary>
    /// 产品序列号出库明细列表（主子表关系）
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktProductSerialOutboundItem.OutboundId))]
    public List<TaktProductSerialOutboundItem>? Items { get; set; }
}
