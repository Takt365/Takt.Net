// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Serial
// 文件名称：TaktProductSerialInbound.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：产品序列号入库主表实体，记录产品序列号入库的基本信息
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Serial;

/// <summary>
/// 产品序列号入库主表实体
/// </summary>
[SugarTable("takt_logistics_product_serial_inbound", "产品序列号入库表")]
[SugarIndex("ix_takt_logistics_product_serial_inbound_plant_code", nameof(PlantCode), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_product_serial_inbound_inbound_unique", nameof(PlantCode), OrderByType.Asc, nameof(InboundNo), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_product_serial_inbound_inbound_date", nameof(InboundDate), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_product_serial_inbound_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktProductSerialInbound : TaktEntityBase
{
    /// <summary>
    /// 工厂代码(4位字母数字组合)
    /// </summary>
    [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", ColumnDataType = "nvarchar", Length = 4, IsNullable = false, DefaultValue = "C100")]
    public string PlantCode { get; set; } = "C100";

    /// <summary>
    /// 入库单号（组合唯一索引：PlantCode + InboundNo）
    /// </summary>
    [SugarColumn(ColumnName = "inbound_no", ColumnDescription = "入库单号", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string InboundNo { get; set; } = string.Empty;

    /// <summary>
    /// 入库日期
    /// </summary>
    [SugarColumn(ColumnName = "inbound_date", ColumnDescription = "入库日期", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime InboundDate { get; set; } = DateTime.Today;

    /// <summary>
    /// 入库类型(0=采购入库,1=生产入库,2=退货入库,3=调拨入库,4=序列号入库,5=其他)
    /// </summary>
    [SugarColumn(ColumnName = "inbound_type", ColumnDescription = "入库类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "4")]
    public int InboundType { get; set; } = 4;

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
    /// 总数量
    /// </summary>
    [SugarColumn(ColumnName = "total_quantity", ColumnDescription = "总数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int TotalQuantity { get; set; } = 0;

    /// <summary>
    /// 关联公司
    /// </summary>
    [SugarColumn(ColumnName = "related_company", ColumnDescription = "关联公司", ColumnDataType = "nvarchar", Length = 50, IsNullable = false, DefaultValue = "2300")]
    public string RelatedCompany { get; set; } = "2300";
    
    /// <summary>
    /// 产品序列号入库明细列表(主子表关系)
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktProductSerialInboundItem.InboundId))]
    public List<TaktProductSerialInboundItem>? Items { get; set; }
}
