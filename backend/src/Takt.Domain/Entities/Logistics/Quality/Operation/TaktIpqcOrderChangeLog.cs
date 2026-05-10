// ========================================
// 项目名称：节拍数字工厂 ·Takt Digital Factory (TDF) 
// 命名空间：Takt.Domain.Entities.Logistics.Quality
// 文件名称：TaktIpqcOrderChangeLog.cs
// 功能描述：IPQC制程检验单变更日志实体
//
// 版权信息：Copyright (c) 2025 Takt  All rights reserved.
// 免责声明：此软件使用 MIT License，作者不承担任何使用风险。
// ========================================

using SqlSugar;
using Takt.Domain.Entities;

namespace Takt.Domain.Entities.Logistics.Quality.Operation;

/// <summary>
/// IPQC制程检验单变更日志实体
/// </summary>
[SugarTable("takt_logistics_quality_ipqc_order_change_log", "制程检验单变更日志表")]
[SugarIndex("ix_takt_logistics_quality_ipqc_order_change_log_ipqc_order_id", nameof(IpqcOrderId), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_ipqc_order_change_log_change_time", nameof(ChangeTime), OrderByType.Asc)]
[SugarIndex("ix_takt_logistics_quality_ipqc_order_change_log_is_deleted", nameof(IsDeleted), OrderByType.Asc)]
public class TaktIpqcOrderChangeLog : TaktEntityBase
{
    /// <summary>
    /// IPQC检验单ID（主子表关系，序列化为string以避免Javascript精度问题）
    /// </summary>
    [SugarColumn(ColumnName = "ipqc_order_id", ColumnDescription = "IPQC检验单ID", ColumnDataType = "bigint", IsNullable = false)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public long IpqcOrderId { get; set; }

    /// <summary>
    /// 变更字段列表（JSON数组格式，记录同一时间点修改的所有字段及其旧值、新值）
    /// 格式：[{"field":"FieldName","description":"字段描述","oldValue":"旧值","newValue":"新值"}]
    /// </summary>
    [SugarColumn(ColumnName = "change_fields", ColumnDescription = "变更字段列表", ColumnDataType = "nvarchar", Length = 4000, IsNullable = true)]
    public string? ChangeFields { get; set; }

    /// <summary>
    /// 变更类型（0=新增，1=修改，2=删除，3=状态变更）
    /// </summary>
    [SugarColumn(ColumnName = "change_type", ColumnDescription = "变更类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int ChangeType { get; set; } = 0;

    /// <summary>
    /// 变更原因
    /// </summary>
    [SugarColumn(ColumnName = "change_reason", ColumnDescription = "变更原因", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
    public string? ChangeReason { get; set; }

    /// <summary>
    /// 变更人（人员代码）
    /// </summary>
    [SugarColumn(ColumnName = "change_by", ColumnDescription = "变更人", ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? ChangeBy { get; set; }

    /// <summary>
    /// 变更时间
    /// </summary>
    [SugarColumn(ColumnName = "change_time", ColumnDescription = "变更时间", ColumnDataType = "datetime", IsNullable = false)]
    public DateTime ChangeTime { get; set; } = DateTime.Now;

    /// <summary>
    /// IPQC检验单（主表）
    /// </summary>
    [Navigate(NavigateType.ManyToOne, nameof(IpqcOrderId))]
    public TaktIpqcOrder? Order { get; set; }
}
