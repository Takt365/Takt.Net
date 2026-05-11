// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Serial
// 文件名称：TaktProductSerialOutboundItem.cs
// 创建时间：2026-05-11
// 创建人：Takt365(Cursor AI)
// 功能描述：产品序列号出库明细实体，记录每个产品序列号的出库信息
// 
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Serial;

/// <summary>
/// 产品序列号出库明细实体
/// </summary>
[SugarTable("takt_logistics_product_serial_outbound_item", "产品序列号出库明细表")]
[SugarIndex("ix_takt_logistics_product_serial_outbound_item_outbound_id", nameof(OutboundId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_product_serial_outbound_item_outbound_serial_no", nameof(OutboundSerialNo), OrderByType.Asc, true)]
[SugarIndex("ix_takt_logistics_product_serial_outbound_item_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktProductSerialOutboundItem : TaktEntityBase
{
    /// <summary>
    /// 出库ID（主子表关系，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "outbound_id", ColumnDescription = "出库ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long OutboundId { get; set; }

    /// <summary>
    /// 出库单号（冗余字段，便于查询）
    /// </summary>
    [SugarColumn(ColumnName = "outbound_no", ColumnDescription = "出库单号", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string OutboundNo { get; set; } = string.Empty;

    /// <summary>
    /// 行号（出库明细行号）
    /// </summary>
    [SugarColumn(ColumnName = "line_number", ColumnDescription = "行号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LineNumber { get; set; } = 0;

    /// <summary>
    /// 出库序列号（唯一索引）
    /// </summary>
    [SugarColumn(ColumnName = "outbound_serial_no", ColumnDescription = "出库序列号", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string OutboundSerialNo { get; set; } = string.Empty;

    /// <summary>
    /// 关联入库ID(序列化为string以避免Javascript精度问题)
    /// </summary>
    [SugarColumn(ColumnName = "reference_inbound_id", ColumnDescription = "关联入库ID", ColumnDataType = "bigint", IsNullable = false, DefaultValue = "0")]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ReferenceInboundId { get; set; } = 0;

    /// <summary>
    /// 关联入库单号
    /// </summary>
    [SugarColumn(ColumnName = "reference_inbound_no", ColumnDescription = "关联入库单号", ColumnDataType = "nvarchar", Length = 50, IsNullable = false, DefaultValue = "")]
    public string ReferenceInboundNo { get; set; } = string.Empty;

    /// <summary>
    /// 关联入库行号
    /// </summary>
    [SugarColumn(ColumnName = "reference_inbound_line_number", ColumnDescription = "关联入库行号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ReferenceInboundLineNumber { get; set; } = 0;

    /// <summary>
    /// 出库时间
    /// </summary>
    [SugarColumn(ColumnName = "outbound_time", ColumnDescription = "出库时间", ColumnDataType = "datetime", IsNullable = false, DefaultValue = "GETDATE()")]
    public DateTime OutboundTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 出库主表
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(OutboundId))]
    public TaktProductSerialOutbound? Outbound { get; set; }
}
