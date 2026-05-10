// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Sales
// 文件名称：TaktSalesOrderChangeLog.cs
// 功能描述：销售订单变更记录实体（变更记录表，非历史表）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Sales;

/// <summary>
/// 销售订单变更记录实体
/// </summary>
[SugarTable("takt_logistics_sales_order_change_log", "销售订单变更记录表")]
[SugarIndex("ix_takt_logistics_sales_order_change_log_sales_order_id", nameof(SalesOrderId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_order_change_log_change_time", nameof(ChangeTime), OrderByType.Desc)]
[SugarIndex("ix_takt_logistics_sales_order_change_log_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_sales_order_change_log_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktSalesOrderChangeLog : TaktEntityBase
{
    /// <summary>
    /// 销售订单ID（主子表关系，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "sales_order_id", ColumnDescription = "销售订单ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long SalesOrderId { get; set; }

    /// <summary>
    /// 订单编码
    /// </summary>
    [SugarColumn(ColumnName = "order_code", ColumnDescription = "订单编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string OrderCode { get; set; } = string.Empty;

    /// <summary>
    /// 变更字段列表（JSON数组格式，记录同一时间点修改的所有字段及其旧值、新值）
    /// 格式：[{"field":"FieldName","description":"字段描述","oldValue":"旧值","newValue":"新值"}]
    /// </summary>
    [SugarColumn(ColumnName = "change_fields", ColumnDescription = "变更字段列表", ColumnDataType = "nvarchar", Length = 4000, IsNullable = true)]
    public string? ChangeFields { get; set; }

    /// <summary>
    /// 变更时间
    /// </summary>
    [SugarColumn(ColumnName = "change_time", ColumnDescription = "变更时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime ChangeTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 变更人（人员代码）
    /// </summary>
    [SugarColumn(ColumnName = "change_by", ColumnDescription = "变更人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ChangeBy { get; set; }

    /// <summary>
    /// 变更原因
    /// </summary>
    [SugarColumn(ColumnName = "change_reason", ColumnDescription = "变更原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ChangeReason { get; set; }
}
