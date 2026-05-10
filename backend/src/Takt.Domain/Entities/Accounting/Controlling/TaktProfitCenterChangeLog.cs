// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF)
// 命名空间：Takt.Domain.Entities.Accounting.Controlling
// 文件名称：TaktProfitCenterChangeLog.cs
// 功能描述：利润中心变更记录实体（变更记录表，非历史表）
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Accounting.Controlling;

/// <summary>
/// 利润中心变更记录实体
/// </summary>
[SugarTable("takt_accounting_controlling_profit_center_change_log", "利润中心变更记录表")]
[SugarIndex("ix_takt_accounting_controlling_profit_center_change_log_profit_center_id", nameof(ProfitCenterId), OrderByType.Asc)]
[SugarIndex("ix_takt_accounting_controlling_profit_center_change_log_change_time", nameof(ChangeTime), OrderByType.Desc)]
[SugarIndex("ix_takt_accounting_controlling_profit_center_change_log_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktProfitCenterChangeLog : TaktEntityBase
{
    /// <summary>
    /// 利润中心ID（主子表关系，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "profit_center_id", ColumnDescription = "利润中心ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long ProfitCenterId { get; set; }

    /// <summary>
    /// 利润中心编码（冗余字段，便于查询）
    /// </summary>
    [SugarColumn(ColumnName = "profit_center_code", ColumnDescription = "利润中心编码", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string ProfitCenterCode { get; set; } = string.Empty;

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
