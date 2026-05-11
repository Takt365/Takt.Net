// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Serial
// 文件名称：TaktProductSerialInboundItem.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：产品序列号入库明细实体，记录每个产品序列号的入库信息
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Serial;

/// <summary>
/// 产品序列号入库明细实体
/// </summary>
[SugarTable("takt_logistics_product_serial_inbound_item", "产品序列号入库明细表")]
[SugarIndex("ix_takt_logistics_product_serial_inbound_item_inbound_id", nameof(InboundId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_product_serial_inbound_item_inbound_serial_no", nameof(InboundSerialNo), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_product_serial_inbound_item_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktProductSerialInboundItem : TaktEntityBase
{
    /// <summary>
    /// 入库ID（主子表关系，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "inbound_id", ColumnDescription = "入库ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long InboundId { get; set; }

    /// <summary>
    /// 入库单号（冗余字段，便于查询）
    /// </summary>
    [SugarColumn(ColumnName = "inbound_no", ColumnDescription = "入库单号", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string InboundNo { get; set; } = string.Empty;

    /// <summary>
    /// 行号（入库明细行号）
    /// </summary>
    [SugarColumn(ColumnName = "line_number", ColumnDescription = "行号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LineNumber { get; set; } = 0;

    /// <summary>
    /// 入库序列号（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "inbound_serial_no", ColumnDescription = "入库序列号", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string InboundSerialNo { get; set; } = string.Empty;

    /// <summary>
    /// 入库时间
    /// </summary>
    [SugarColumn(ColumnName = "inbound_time", ColumnDescription = "入库时间", ColumnDataType = "datetime", IsNullable = false, DefaultValue = "GETDATE()")]
    public DateTime InboundTime { get; set; } = DateTime.Now;
    
    /// <summary>
    /// 入库主表
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(InboundId))]
    public TaktProductSerialInbound? Inbound { get; set; }
}
