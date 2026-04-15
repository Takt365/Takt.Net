// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Logistics.Materials
// 文件名称：TaktPurchaseOrderChangeLog.cs
// 功能描述：采购订单变更记录实体（变更记录表，非历史表）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Materials;

/// <summary>
/// 采购订单变更记录实体
/// </summary>
[SugarTable("takt_logistics_materials_purchase_order_change_log", "采购订单变更记录表")]
[SugarIndex("ix_takt_logistics_materials_purchase_order_change_log_order_id", nameof(OrderId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_order_change_log_change_time", nameof(ChangeTime), OrderByType.Desc)]
[SugarIndex("ix_takt_logistics_materials_purchase_order_change_log_change_user_id", nameof(ChangeUserId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_order_change_log_config_id", nameof(ConfigId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_materials_purchase_order_change_log_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktPurchaseOrderChangeLog : TaktEntityBase
{
    /// <summary>
    /// 订单ID（关联采购订单表）
    /// </summary>
    [SugarColumn(ColumnName = "order_id", ColumnDescription = "订单ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long OrderId { get; set; }

    /// <summary>
    /// 订单编码
    /// </summary>
    [SugarColumn(ColumnName = "order_code", ColumnDescription = "订单编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string OrderCode { get; set; } = string.Empty;

    /// <summary>
    /// 变更字段名
    /// </summary>
    [SugarColumn(ColumnName = "change_field", ColumnDescription = "变更字段名", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string ChangeField { get; set; } = string.Empty;

    /// <summary>
    /// 原值（JSON格式存储，支持复杂类型）
    /// </summary>
    [SugarColumn(ColumnName = "old_value", ColumnDescription = "原值", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? OldValue { get; set; }

    /// <summary>
    /// 新值（JSON格式存储，支持复杂类型）
    /// </summary>
    [SugarColumn(ColumnName = "new_value", ColumnDescription = "新值", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
    public string? NewValue { get; set; }

    /// <summary>
    /// 变更时间
    /// </summary>
    [SugarColumn(ColumnName = "change_time", ColumnDescription = "变更时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime ChangeTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 变更人ID
    /// </summary>
    [SugarColumn(ColumnName = "change_user_id", ColumnDescription = "变更人ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ChangeUserId { get; set; }

    /// <summary>
    /// 变更人姓名
    /// </summary>
    [SugarColumn(ColumnName = "change_user_name", ColumnDescription = "变更人姓名", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ChangeUserName { get; set; } = string.Empty;

    /// <summary>
    /// 变更原因
    /// </summary>
    [SugarColumn(ColumnName = "change_reason", ColumnDescription = "变更原因", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? ChangeReason { get; set; }
}
